# Use the official Liquibase image from Docker Hub
FROM liquibase/liquibase:latest

ARG CACHE_DATE=1
#COPY ./tntchangelogs/auth ./tntchangelogs/auth 
COPY ["./TntChangeLogs", "./TntChangeLogs"] 


# Optionally, add your changelog files and other necessary files
COPY ./TntChangeLogs ./TntChangeLogs 

# Run a Liquibase command as an example (update the database)
#CMD ["liquibase", "update"]
#CMD ["liquibase", "update"]
#CMD ["liquibase", "update"]
#--defaults-file=auth/PGSQL/liquibase.properties update
#--defaults-file=auth/MSSQL/liquibase.properties update