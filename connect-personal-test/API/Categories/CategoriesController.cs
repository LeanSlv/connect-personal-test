using connect_personal_test.Extensions;
using connect_personal_test.Models;
using connect_personal_test.Models.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace connect_personal_test.API.Categories
{
    /// <summary>
    /// Работы с категориями.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CategoriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Список категорий.
        /// </summary>
        /// <param name="request">Данные для запроса.</param>
        /// <returns>Список категорий.</returns>
        [HttpGet]
        public async Task<CategoriesListResponse> ListAsync([FromQuery] CategoriesListRequest request)
        {
            var result = new CategoriesListResponse();

            var list = _dataContext.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                list = list.Where(i => EF.Functions.Like(i.Name, '%' + request.Name + '%'));

            result.TotalCount = await list.CountAsync();
            result.Items = await list
                .OrderBy(i => i.Name)
                .LimitOffset(request.Limit, request.Offset)
                .Select(i => new CategoryItem
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToArrayAsync();

            return result;
        }

        /// <summary>
        /// Подробности категории.
        /// </summary>
        /// <param name="id">ИД категории.</param>
        /// <returns>Подробности категории.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryItem>> DetailsAsync([FromRoute] int id)
        {
            var dbItem = await _dataContext.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (dbItem == null)
                return NotFound();

            return GetByDbItem(dbItem);
        }

        /// <summary>
        /// Создание категории.
        /// </summary>
        /// <param name="request">Данные для запроса.</param>
        /// <returns>Созданная категория.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoryItem>> CreateAsync([FromBody] CategoriesCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _dataContext.Categories.AnyAsync(i => i.Name == request.Name))
                return BadRequest("Категория с таким именем уже существует.");

            var dbItem = new Category
            {
                Name = request.Name
            };

            _dataContext.Categories.Add(dbItem);
            await _dataContext.SaveChangesAsync();

            return GetByDbItem(dbItem);
        }

        /// <summary>
        /// Редактирование категории.
        /// </summary>
        /// <param name="id">ИД категории.</param>
        /// <param name="request">Данные для запроса.</param>
        /// <returns>Отредактированная категория</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryItem>> EditAsync([FromRoute] int id, [FromBody] CategoriesEditRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbItem = await _dataContext.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (dbItem == null)
                return NotFound();

            if (await _dataContext.Categories.AnyAsync(i => i.Name == request.Name && i.Id != id))
                return BadRequest("Категория с таким именем уже существует.");

            dbItem.Name = request.Name;
            _dataContext.Categories.Update(dbItem);
            await _dataContext.SaveChangesAsync();

            return GetByDbItem(dbItem);
        }

        /// <summary>
        /// Удаление категории.
        /// </summary>
        /// <param name="id">ИД категории.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            var dbItem = await _dataContext.Categories.FirstOrDefaultAsync(i => i.Id == id);
            if (dbItem == null)
                return NotFound();

            _dataContext.Categories.Remove(dbItem);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Получение модели API из сущности БД.
        /// </summary>
        /// <param name="dbItem">Сущность БД.</param>
        /// <returns>Модель API.</returns>
        [NonAction]
        private CategoryItem GetByDbItem(Category dbItem)
        {
            return new CategoryItem
            {
                Id = dbItem.Id,
                Name = dbItem.Name
            };
        }
    }
}
