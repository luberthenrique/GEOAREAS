# GEOAREAS

Projeto criado para utilização na defesa de TCC do curso de pós graduação em arquitetura de software - PUC Minas

## O que é o GEOAREAS
O GEOAREAS projeto visa fornece uma plataforma aberta e centralizada para consultas de dados de delimitações geográficas de qualquer região do Brasil, utilizando-se de arquivos de setores censitários do IBGE para construção das áreas do sistema.

Links de acesso a aplicação:

> WEB - https://geoareas.azurewebsites.net/

> API - https://geoareas-api.azurewebsites.net/swagger/index.html

## Como utilizar a plataforma

Para acessar o sistema Web, utilizar o e-mail 'teste@teste.com' e a senha '123456'.

Para utilizar a API, é necessário possuir uma 'ApiKey' e 'SecretKey', que podem ser obtidos no cadastro de clientes da aplicação web, clicando <a href="https://geoareas.azurewebsites.net/cliente">aqui</a>. Estes parâmetros devem ser utilizados para efetuar a autenticação na API e obter o token para ser utilizado nas outras chamadas.

### Exemplos de requisição na API utilizando Postman

> Requisição de autenticação


    "request": {
          "method": "POST",
          "header": [],
          "body": {
            "mode": "raw",
            "raw": "{\r\n  \"apiKey\": \"klAtHhUw06tXe8eYKq3Qm3MIJzIYbdqeDMMsISnB1e5\",\r\n  \"secretKey\": \"erW3Xh11Tx5nJUxg1VtRAncvLX5KkvDCCZt6HlDDtw7\"\r\n}",
            "options": {
              "raw": {
                "language": "json"
              }
            }
          },
          "url": {
            "raw": "https://geoareas-api.azurewebsites.net/api/auth",
            "protocol": "https",
            "host": [
              "geoareas-api",
              "azurewebsites",
              "net"
            ],
            "path": [
              "api",
              "auth"
            ]
          }
        },

> Requisição de obtenção de setores

    "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "https://geoareas-api.azurewebsites.net/api/area?latitude=-21.75053636556511&longitude=-43.330857775140956&raio=10",
              "protocol": "https",
              "host": [
                "geoareas-api",
                "azurewebsites",
                "net"
              ],
              "path": [
                "api",
                "area"
              ],
              "query": [
                {
                  "key": "latitude",
                  "value": "-21.75053636556511"
                },
                {
                  "key": "longitude",
                  "value": "-43.330857775140956"
                },
                {
                  "key": "raio",
                  "value": "10"
                }
              ]
            }
          }
