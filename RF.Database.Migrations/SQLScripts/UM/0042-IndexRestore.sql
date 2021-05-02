do $$
declare 
maxId int;
tables CURSOR FOR
       SELECT tablename
        FROM pg_tables
        Where schemaname = 'public' and 
		tablename NOT LIKE '_RF%' and 
		tablename NOT LIKE 'schema%' and 
		tablename != 'Label' and 
		tablename != 'Publisher' and
		tablename != 'Region'  and
		tablename != 'Society' and
		tablename != 'Song' and
		tablename != 'Territory' and
		tablename != 'Writer'
        ORDER BY tablename;
begin
    FOR table_record IN tables LOOP   	
        EXECUTE 'Select max("Id") from public.' || CONCAT('"',table_record.tablename,'"') INTO maxId;
		EXECUTE 'alter sequence public.' ||CONCAT('"',table_record.tablename,'_Id_seq','"') || ' RESTART with '|| COALESCE(maxId,1);
	END LOOP;  
end$$;