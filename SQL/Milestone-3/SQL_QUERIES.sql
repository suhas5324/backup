-- 1.Write a query to get the age of the Actors in Days(Number of days).
SELECT Name
	,DATEDIFF(DAY, DateOfBirth, GETDATE()) AS AgeInTotalDays
FROM Foundation.Actors

-- 2.Write a query to get the list of Actors who have worked with a given producer X.
SELECT DISTINCT A.Name
FROM Foundation.Actors A
JOIN Foundation.Actors_Movies AM ON A.Id = AM.ActorId
JOIN Foundation.Movies M ON AM.MovieId = M.Id
JOIN Foundation.Producers P ON M.ProducerId = P.Id
WHERE P.Name = 'X'

-- 3.Write a query to get the list of actors who have acted together in two or more movies.
SELECT A1.Name AS Actor1
	,A2.Name AS Actor2
	,COUNT(*) AS MoviesTogether
FROM Foundation.Actors_Movies AM1
JOIN Foundation.Actors_Movies AM2 ON AM1.MovieId = AM2.MovieId
	AND AM1.ActorId < AM2.ActorId
JOIN Foundation.Actors A1 ON AM1.ActorId = A1.Id
JOIN Foundation.Actors A2 ON AM2.ActorId = A2.Id
GROUP BY A1.Name
	,A2.Name
HAVING COUNT(*) >= 2

-- 4.Write a query to get the youngest actor.
SELECT TOP 1 *
FROM Foundation.Actors
ORDER BY DateOfBirth DESC;

-- 5.Write a query to get the actors who have never worked together.
SELECT A1.Name AS Actor1
	,A2.Name AS Actor2
FROM Foundation.Actors A1
JOIN Foundation.Actors A2 ON A1.Id < A2.Id
LEFT JOIN Foundation.Actors_Movies AM1 ON A1.Id = AM1.ActorId
LEFT JOIN Foundation.Actors_Movies AM2 ON A2.Id = AM2.ActorId
	AND AM1.MovieId = AM2.MovieId
GROUP BY A1.Name
	,A2.Name
HAVING COUNT(AM2.ActorId) = 0;

-- 6.Write a query to get the number of movies in each language.
SELECT Language
	,COUNT(*) AS MovieCountByLanguage
FROM Foundation.Movies
GROUP BY Language

-- 7.Write a query to get me the total profit of all the movies in each language separately.
SELECT Language
	,SUM(Profit) AS TotalProfitByLanguage
FROM Foundation.Movies
GROUP BY Language

-- 8.Write a query to get the total profit of movies which have actor X in each language.
SELECT M.Language
	,SUM(M.Profit) AS TotalProfit
FROM Foundation.Movies M
JOIN Foundation.Actors_Movies AM ON M.Id = AM.MovieId
JOIN Foundation.Actors A ON AM.ActorId = A.Id
WHERE A.Name = 'X'
GROUP BY M.Language

-- 9.Write a query to get the Total profit by year of release and language
SELECT YearOfRelease
	,Language
	,SUM(Profit) AS TotalProfitByYearAndLanguage
FROM Foundation.Movies
GROUP BY YearOfRelease
	,Language

-- 10.Write a query to get number of movies in each language produced by each producer
SELECT P.Name
	,M.Language
	,COUNT(*) AS MovieCount
FROM Foundation.Movies M
JOIN Foundation.Producers P ON M.ProducerId = P.Id
GROUP BY P.Name
	,M.Language