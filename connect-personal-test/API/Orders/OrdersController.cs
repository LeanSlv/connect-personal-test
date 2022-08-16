using connect_personal_test.Extensions;
using connect_personal_test.Models;
using connect_personal_test.Models.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace connect_personal_test.API.Orders
{
    /// <summary>
    /// Работа с заказами.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public OrdersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Список заказов.
        /// </summary>
        /// <param name="request">Данные для запроса.</param>
        /// <returns>Список заказов.</returns>
        [HttpGet]
        public async Task<OrdersListResponse> ListAsync([FromQuery] OrdersListRequest request)
        {
            var result = new OrdersListResponse();

            var list = _dataContext.Orders.AsQueryable();

            if (request.CategoryId.HasValue)
                list = list.Where(i => i.OrderCategories.Any(oc => oc.CategoryId == request.CategoryId.Value));

            if (!string.IsNullOrEmpty(request.Name))
                list = list.Where(i => EF.Functions.Like(i.Name, '%' + request.Name + '%'));

            result.TotalCount = await list.CountAsync();
            result.Items = await list
                .OrderBy(i => i.Name)
                .LimitOffset(request.Limit, request.Offset)
                .Select(i => new OrderItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Value = i.Value,
                    CategoryNames = i.OrderCategories.Select(i => i.Category.Name).ToArray()
                })
                .ToArrayAsync();

            return result;
        }

        /// <summary>
        /// Подробности заказа.
        /// </summary>
        /// <param name="id">ИД заказа.</param>
        /// <returns>Подробности заказа.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> DetailsAsync([FromRoute] int id)
        {
            var dbItem = await _dataContext.Orders.FirstOrDefaultAsync(i => i.Id == id);
            if (dbItem == null)
                return NotFound();

            return await GetByDbItem(dbItem);
        }

        /// <summary>
        /// Создание заказа.
        /// </summary>
        /// <param name="request">Данные запроса.</param>
        /// <returns>Созданный заказ.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateAsync([FromBody] OrdersCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var tr = await _dataContext.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
            };

            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();

            foreach (var categoryId in request.CategoryIds)
            {
                if (await _dataContext.Categories.AnyAsync(i => i.Id == categoryId) == false)
                    return NotFound($"Категории с ИД {categoryId} не существует.");

                var orderCategory = new OrderCategory
                {
                    OrderId = order.Id,
                    CategoryId = categoryId,
                };

                _dataContext.OrderCategories.Add(orderCategory);
            }

            await _dataContext.SaveChangesAsync();

            await tr.CommitAsync();

            return await GetByDbItem(order);
        }

        /// <summary>
        /// Редактирование заказа.
        /// </summary>
        /// <param name="id">ИД заказа.</param>
        /// <param name="request">Данные запроса.</param>
        /// <returns>Отредактированный заказ.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> EditAsync([FromRoute] int id, [FromBody] OrdersEditRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderDbItem = await _dataContext.Orders.FirstOrDefaultAsync(i => i.Id == id);
            if (orderDbItem == null)
                return NotFound();

            using var tr = await _dataContext.Database.BeginTransactionAsync();

            orderDbItem.Name = request.Name;
            orderDbItem.Description = request.Description;
            orderDbItem.Value = request.Value;
            _dataContext.Orders.Update(orderDbItem);

            var orderCategoryIds = await _dataContext.OrderCategories
                .Where(i => i.OrderId == orderDbItem.Id)
                .Select(i => i.CategoryId)
                .ToArrayAsync();

            var orderCategoryIdsToRemove = orderCategoryIds.Except(request.CategoryIds).ToArray();
            var orderCategoryIdsToAdd = request.CategoryIds.Except(orderCategoryIds).ToArray();

            // remove order categories
            var orderCategoriesToRemove = await _dataContext.OrderCategories
                .Where(i => i.OrderId == orderDbItem.Id && orderCategoryIdsToRemove.Contains(i.CategoryId))
                .ToArrayAsync();
            _dataContext.OrderCategories.RemoveRange(orderCategoriesToRemove);

            // add new order categories
            foreach(var categoryId in orderCategoryIdsToAdd)
            {
                var orderCategory = new OrderCategory
                {
                    OrderId = orderDbItem.Id,
                    CategoryId = categoryId,
                };
                _dataContext.Add(orderCategory);
            }

            await _dataContext.SaveChangesAsync();

            await tr.CommitAsync();

            return await GetByDbItem(orderDbItem);
        }

        /// <summary>
        /// Удаление заказа.
        /// </summary>
        /// <param name="id">ИД заказа.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            var orderDbItem = await _dataContext.Orders.FirstOrDefaultAsync(i => i.Id == id);
            if (orderDbItem == null)
                return NotFound();

            using var tr = await _dataContext.Database.BeginTransactionAsync();

            var orderCategories = await _dataContext.OrderCategories.Where(i => i.OrderId == orderDbItem.Id).ToArrayAsync();

            _dataContext.OrderCategories.RemoveRange(orderCategories);
            _dataContext.Orders.Remove(orderDbItem);
            await _dataContext.SaveChangesAsync();
            
            await tr.CommitAsync();

            return Ok();
        }

        /// <summary>
        /// Получение модели API из сущности БД.
        /// </summary>
        /// <param name="dbItem">Сущность БД.</param>
        /// <returns>Модель API.</returns>
        [NonAction]
        private async Task<OrderItem> GetByDbItem(Order dbItem)
        {
            var categories = await (from c in _dataContext.Categories
                                    join oc in _dataContext.OrderCategories on c.Id equals oc.CategoryId
                                    where oc.OrderId == dbItem.Id
                                    select c.Name).ToArrayAsync();

            return new OrderItem
            {
                Id = dbItem.Id,
                Name = dbItem.Name,
                Description = dbItem.Description,
                Value = dbItem.Value,
                CategoryNames = categories,
            };
        }
    }
}
