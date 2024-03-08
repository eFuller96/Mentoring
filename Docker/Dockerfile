FROM mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Password1

WORKDIR /src

COPY my-data.sql ./my-data.sql
COPY configure-db.sh /src/configure-db.sh

# EXPOSE 1498

# RUN /src/configure-db.sh

ENTRYPOINT (/opt/mssql/bin/sqlservr)