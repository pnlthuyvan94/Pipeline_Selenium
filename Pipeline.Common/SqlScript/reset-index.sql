
DECLARE @tblSetSeedValue TABLE(Seq INT IDENTITY(1,1), TableName NVARCHAR(255), ColumnName NVARCHAR(255), DataType NVARCHAR(50), CurrentSeed INT)

INSERT INTO @tblSetSeedValue(TableName, ColumnName, DataType, CurrentSeed)
SELECT 
    concat('[',s.name,'].' , '[', t.name ,']') as TableName,
    c.name AS ColumnName,
    ty.name AS DataType,
    IDENT_CURRENT(t.name) AS CurrentSeedValue
FROM sys.tables t INNER JOIN sys.columns c ON t.object_id = c.object_id
	 INNER JOIN sys.types ty ON c.user_type_id = ty.user_type_id
	  INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
WHERE c.is_identity = 1 
      AND ty.name ='tinyint'
	  AND IDENT_CURRENT(t.name) IS NOT NULL

DECLARE @curr INT
	   , @threshold INT
	   , @currTableName NVARCHAR(255)
	   , @currColumnName NVARCHAR(255)
	   , @currDataType NVARCHAR(50)
	   , @currentSeed INT
	   , @maxNo INT;
SELECT @curr = ISNULL(MIN(Seq), 0), @threshold = ISNULL(MAX(Seq),-1)
FROM @tblSetSeedValue

DECLARE @strStatement NVARCHAR(MAX);
WHILE (@curr <= @threshold)
BEGIN
	SELECT @currTableName = TableName, @currColumnName = ColumnName, @currDataType = DataType, @currentSeed = CurrentSeed
	FROM @tblSetSeedValue
	WHERE Seq = @curr;
	SET @curr = @curr + 1;
	SET @strStatement = 'DECLARE @tblStoreSeq TABLE(seq INT IDENTITY(1,1), id INT);
						 IF NOT EXISTS (SELECT 1 FROM ' +  @currTableName +' WHERE '+ @currColumnName +' = 1)
						 BEGIN
							INSERT INTO @tblStoreSeq(Id)
							VALUES (1)
						 END;

						 INSERT INTO @tblStoreSeq(id)
						 SELECT ' + @currColumnName+'  
						 FROM ' +  @currTableName +' 
						 ORDER BY ' +@currColumnName + ';

						 IF NOT EXISTS (SELECT 1 FROM ' +@currTableName +' WHERE ' + @currColumnName + ' = 255)
						 BEGIN
							INSERT INTO @tblStoreSeq(Id)
							VALUES (255)
						 END;

						 DECLARE @tblGap TABLE(seq INT IDENTITY(1,1), id INT, StartIndex INT, EndIndex INT, numberOfspace INT);
						 INSERT INTO @tblGap(StartIndex, EndIndex, numberOfspace )
						 SELECT cur.id as StartIndex, n.Id as EndIndex, (n.id - cur.id -1) as numberOfspace
						 FROM @tblStoreSeq cur inner join @tblStoreSeq n ON (cur.seq = n.seq-1);

						 DECLARE @seed INT = (SELECT MAX(StartIndex)
						 FROM @tblGap 
						 WHERE numberOfspace = (SELECT max(numberOfspace) from @tblGap));
						
						 DBCC CHECKIDENT('''+  @currTableName+ ''' ,RESEED, @seed)
						 '
	

    EXECUTE sp_executesql @strStatement, N'@maxNo INT OUTPUT', @maxNo = @maxNo OUTPUT
	
END


