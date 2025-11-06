sql_server:
	@echo "sql_server  start ....."  # 注意：这里必须是 Tab 缩进
	@echo "port is 1433"
	@echo "password is Looksaw123"
	@docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Looksaw123" -p 1433:1433 -v sqlvolumme:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
	@echo "finishing ......"
.PHONY: sql_server