-- ��������������� ������� � �������
DECLARE @t TABLE (number INT NOT NULL)
INSERT INTO @t 
    SELECT 0
    UNION All
    SELECT 1
    UNION All
    SELECT 2
    UNION All
    SELECT 3
    UNION All
    SELECT 4
    UNION All
    SELECT 5
    UNION All
    SELECT 6
    UNION All
    SELECT 7
    UNION All
    SELECT 8
    UNION All
    SELECT 9

-- ���������� ���������
INSERT INTO dbo.Category (Name)
SELECT CONCAT(N'��������� �', t1.number + t2.number*10 + 1)
FROM 
	@t as t1,
    @t as t2

-- ���������� �������
INSERT INTO dbo.[Order] (Name, Description, Value)
SELECT 
	CONCAT(N'����� �', t1.number + t2.number*10 + t3.number*100 + t4.number*1000 + t5.number*10000 + t6.number*100000 + 1),
    CONCAT(N'�������� ������ �', t1.number + t2.number*10 + t3.number*100 + t4.number*1000 + t5.number*10000 + t6.number*100000 + 1),
    CAST(t1.number + t2.number*10 + t3.number*100 + t4.number*1000 + t5.number*10000 + t6.number*100000 + 1 AS NVARCHAR(MAX))
FROM
    @t as t1,
    @t as t2,
    @t as t3,
    @t as t4,
    @t as t5,
    @t as t6

-- ����� ������� � ���������
INSERT INTO dbo.OrderCategory (OrderId, CategoryId)
SELECT o.Id, c.Id
FROM dbo.[Order] o
	JOIN dbo.Category c ON c.Id = CEILING(o.Id / 9000) + 1

