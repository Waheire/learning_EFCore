CREATE PROCEDURE dbo.AuthorsPublisherinYearRange
@yearstart int,
@yearend int
AS
SELECT Distinct Authors.* FROM AUTHORS
LEFT JOIN Books ON Authors.Authorid=books.authorId
WHERE year(books.PublishDate)>@yearstart AND Year(books.PublishDate)<=@yearend
