DELETE FROM [dbo].[LAppServer]
      WHERE [Application] is not Null


INSERT INTO [dbo].[LAppServer]
SELECT Applications.Name As 'Application', Hosts.Name As 'Server', Relationships.Score As 'Bundle'
FROM   Relationships INNER JOIN
             Hosts ON Relationships.[Entity1_Name] = Hosts.Name INNER JOIN
             Applications ON Relationships.[Entity2_Name] = Applications.Name
UNION
SELECT Applications.Name As 'Application', Hosts.Name As 'Server', Relationships.Score As 'Bundle'
FROM   Applications INNER JOIN
Relationships ON Applications.Name = Relationships.[Entity1_Name] INNER JOIN
Hosts ON Relationships.[Entity2_Name] = Hosts.Name

DECLARE @MyCursor CURSOR;
DECLARE @innerCursor CURSOR;
DECLARE @MyField varchar(50);
DECLARE @Myapplication varchar(50);
DECLARE @serverCounter INT;
DECLARE @MyNum INT;
DECLARE @myCounter INT;
DECLARE @bundleName varchar(10);
DECLARE @cbundleName varchar(10);
DECLARE @apps TABLE ([Application] VARCHAR(100));
SET @myCounter = 0
---------------------------outter Cursor--------------------
BEGIN
    SET @MyCursor = CURSOR FOR
              SELECT [Server],COUNT(*) as Num
    FROM [dbo].[LAppServer]
    GROUP BY [Server]
    HAVING COUNT(*) > 0
              ORDER BY Num DESC
    OPEN @MyCursor 
    FETCH NEXT FROM @MyCursor INTO 
                             @MyField,
                             @MyNum;

    WHILE @@FETCH_STATUS = 0
    BEGIN
                             set @myCounter = @myCounter + 1
                             set @bundleName = concat('bundle ', + @myCounter)
                             print (@MyField)
----------------------------Inner Cursor Start----------------
                             SET @cbundleName = (SELECT [bundle]
                                           FROM [LAppServer]
                                           WHERE [Server] = @MyField and [bundle] is Not Null
                                           GROUP BY [bundle])

                             
                             IF (@cbundleName IS NULL)
                                           BEGIN
                                           set @myCounter = @myCounter + 1
                                           set @bundleName = concat('bundle ', + @myCounter)
                                           END
        ELSE
                                           BEGIN
                                           set @bundleName = @cbundleName
                                           END
                                           SET @innerCursor = CURSOR FOR
                                           (SELECT [Application]
                                                          FROM [LAppServer]
                                                          WHERE [Server] = @MyField and [bundle] is Null)
                                           OPEN @innerCursor 
                                           FETCH NEXT FROM @innerCursor INTO 
                                                           @Myapplication;
                                           WHILE @@FETCH_STATUS = 0
                                           BEGIN
                                                          set @serverCounter =  (Select count( [Server])
                                                                        FROM [LAppServer]
                                                                        WHERE [Application] = @Myapplication and [bundle] is Null)
                             
                                                          if (@serverCounter >0)
                                                                        print (@Myapplication)

                                                                        UPDATE [LAppServer]  
                                                                        SET    [bundle] = @bundleName
                                                                        WHERE  [Application] = @Myapplication 

                                                          FETCH NEXT FROM @innerCursor 
                                             INTO @Myapplication;
                                           END;

                                           CLOSE @innerCursor ;
                                           DEALLOCATE @innerCursor;

----------------------------Inner Cursor End -------------------        
      FETCH NEXT FROM @MyCursor 
      INTO @MyField,@MyNum;
    END;

    CLOSE @MyCursor ;
    DEALLOCATE @MyCursor;
END;

--------------------END Outter Cursor---------------------------
---------------------second time

DECLARE @MyCursor2 CURSOR;
DECLARE @innerCursor2 CURSOR;
DECLARE @MyField2 varchar(50);
DECLARE @Myapplication2 varchar(50);
DECLARE @serverCounter2 INT;
DECLARE @MyNum2 INT;
DECLARE @myCounter2 INT;
DECLARE @bundleName2 varchar(10);
DECLARE @cbundleName2 varchar(10);
SET @myCounter2 = 1000
---------------------------outter Cursor--------------------
BEGIN
    SET @MyCursor2 = CURSOR FOR
              SELECT [Server],COUNT(*) as Num
    FROM (SELECT Distinct [Server],[bundle]
    FROM [dbo].[LAppServer])tb1
    GROUP BY [Server]
              Having COUNT(*)>1
              ORDER BY Num DESC
    OPEN @MyCursor2 
    FETCH NEXT FROM @MyCursor2 INTO 
                             @MyField2,
                             @MyNum2;

    WHILE @@FETCH_STATUS = 0
    BEGIN
                             set @myCounter2 = @myCounter2 + 1
                             set @bundleName2 = concat('bun', + @myCounter2)
----------------------------Inner Cursor Start----------------

                                           SET @innerCursor2 = CURSOR FOR
                                           (SELECT [bundle]
                                                          FROM [LAppServer]
                                                          WHERE [Server] = @MyField2 and [bundle] is Not Null)
                                           OPEN @innerCursor2 
                                           FETCH NEXT FROM @innerCursor2 INTO 
                                                           @Myapplication2;
                                           WHILE @@FETCH_STATUS = 0
                                           BEGIN
                                                          set @serverCounter2 =  (Select count( [Server])
                                                                        FROM [LAppServer]
                                                                        WHERE [bundle] = @Myapplication2)
                             
                                                          if (@serverCounter2 >0)
                                                                        print (@Myapplication2)

                                                                        UPDATE [LAppServer]  
                                                                        SET    [bundle] = @bundleName2
                                                                        WHERE  [bundle] = @Myapplication2 

                                                          FETCH NEXT FROM @innerCursor2 
                                             INTO @Myapplication2;
                                           END;

                                           CLOSE @innerCursor2 ;
                                           DEALLOCATE @innerCursor2;

----------------------------Inner Cursor End -------------------        
      FETCH NEXT FROM @MyCursor2 
      INTO @MyField2,@MyNum2;
    END;

    CLOSE @MyCursor2 ;
    DEALLOCATE @MyCursor2;
END;

--------------------END Outter Cursor---------------------------



 

