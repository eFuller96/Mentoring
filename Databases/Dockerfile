FROM mcr.microsoft.com/mssql/server:2019-CU5-ubuntu-18.04
ENV SA_PASSWORD=Password1
ENV ACCEPT_EULA=Y
USER mssql
COPY ./AdventureWorks2019.bak /var/opt/mssql/backup/
RUN ( /opt/mssql/bin/sqlservr & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P ${SA_PASSWORD} -Q 'RESTORE DATABASE AdventureWorks2019 FROM DISK = "/var/opt/mssql/backup/AdventureWorks2019.bak" WITH MOVE "AdventureWorks2017" to "/var/opt/mssql/data/AdventureWorks2019.mdf", MOVE "AdventureWorks2017_Log" to "/var/opt/mssql/data/AdventureWorks2019_log.ldf", NOUNLOAD, STATS = 5' \
    && pkill sqlservr
CMD ["/opt/mssql/bin/sqlservr"]