**QUESTION** **1**:

**Users most frequently search for movies by name using the following query:**

**SELECT \* FROM Foundation.Movies WHERE name = 'Bugonia';**

**Which type of index would be most efficient for this query? Write the SQL statement to create that index.**



**ANSWER**:



A Non-Clustered Index on the Name column is most efficient because the query filters by equality on Name.



SQL Statement:



CREATE NONCLUSTERED INDEX IX\_Foundation\_Movies\_Name

ON Foundation.Movies (Name);



This allows SQL Server to directly locate the movie using an Index Seek instead of scanning the entire table.



**QUESTION 2:**

**When a PRIMARY KEY constraint is created on a table, is a clustered index automatically created?**


**ANSWER:**



Yes, by default SQL Server creates a Clustered Index when a PRIMARY KEY constraint is defined.



Example:



CREATE TABLE Sample

(

&nbsp;   Id INT PRIMARY KEY,

&nbsp;   Name NVARCHAR(100)

);



This automatically creates a Clustered Index on Id, unless explicitly specified as NONCLUSTERED:



SQL Statement.



Id INT PRIMARY KEY NONCLUSTERED;



**QUESTION 3:**

**A non-clustered index exists on the DOB column in the Actors table. Consider the following query:**

**SELECT ID FROM Actors WHERE DATEDIFF(day, DOB, GETDATE()) > 30;?**

**Will this query use the index on DOB? If not, explore why and rewrite the query so that the index can be used efficiently.**



**ANSWER:**



No, it will not efficiently use the index.



Reason:

A function (DATEDIFF) is applied on the indexed column (DOB). When we use a function on an indexed column, SQL Server cannot perform an Index Seek properly.



Efficient Rewrite:

SELECT Id

FROM Actors

WHERE DOB < DATEADD(day, -30, GETDATE());



Now the column is directly compared without applying a function. This allows SQL Server to use an Index Seek on the DOB column.



**QUESTION 4:**

**You need to retrieve all movies produced by ‘Aditya Chopra’ by joining the Movies and Producers tables on Movies.ProducerId = Producers.Id.**

**Which columns should be indexed to optimize the join performance?** 

**Create the required index(es) and explore why they improve query efficiency.**



**ANSWER:**



SELECT \*

FROM Foundation.Movies M

JOIN Foundation.Producers P

&nbsp;   ON M.ProducerId = P.Id

WHERE P.Name = 'Aditya Chopra';



Columns that should be indexed:



Producers.Name



Movies.ProducerId



Creating Required Indexes:



CREATE NONCLUSTERED INDEX IX\_Producers\_Name

ON Foundation.Producers (Name);



CREATE NONCLUSTERED INDEX IX\_Movies\_ProducerId

ON Foundation.Movies (ProducerId);



**Index on Producers.Name helps us to quickly find the producer.**



**Index on Movies.ProducerId helps us to quickly match movies belonging to that producer.**



**This reduces table scans and improves join performance.**



