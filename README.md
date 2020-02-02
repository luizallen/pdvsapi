# Pdv's Api.

Api responsavel pelo cadastro/consulta de Pdvs.

Stack:
- Asp.net Core 3.0;
- Elk;
- Postgres with Postgis;
- Adminer;
- Docker;
- Swager;

Urls:
- http://localhost:7777/ - Pdv's api;
- http://localhost:7777/swagger - Swagger da aplicação;
- http://localhost:8080/ - AdMiner(Para consultar os registros no postgres);
- http://localhost:5601/ - Kibana;

Inicializando a Api:

- Executar o comando  ```docker-compose up``` na pasta da aplicação;

Kibana:
- Ao inicializar o Kibana é so criar um indice e brincar :).

Postgres:
- Dados para utilizar o Postgres:
   - Host - "postgres"
   - User = "postgres"
   - Password = "admin"
   - Database = "postgres"

Postman:
- https://www.getpostman.com/collections/975daed61779061b97a3
**Para utilizar a collection, terá q alterar a Url para url local;

**Obs: Caso o seu SO for windows ou mac, alterar na connectionString para utilizar "Server=host.docker.internal" ao invez de "Server=localhost"
