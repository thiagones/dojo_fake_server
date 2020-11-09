# Dojo Stub com Wiremock

## 1. Wiremock
### Iremos instalar e configurar o Wiremock para substituir a API do GitHub quando necessário

- Instalar wiremock: npm install wiremock

- Criar estrutura de pastas: `/wiremock/mappings` (Aqui firarão nossos stubs)

- Configurar um script em package.json para executar o seguindo comando para startar o wiremock
```
wiremock --root-dir ./wiremock --port 8081 --https-port 8443

Onde:
    --root-dir aponta para os stubs
    --port define a borta http
    --https-port define a porta https
```
- Ajustar `./stub/Dockerfile` para executar o script criado ao startar o container

- Criar um mapemaneto para a rota Users
    - [Rota original Users](https://api.github.com/users/thiagones)

- Criar um mapemaneto para a rota Repos
    - [Rota original Repos](https://api.github.com/users/thiagones/repos)

Exemplo:

``` json
{
  "request": {
    "method": "GET",
    "url": "/api/exemplo"
  },
  "response": {
    "status": 200,
    "jsonBody": {
      "mensage": "exemplo dojo"
    },
    "headers": {
      "Content-Type": "application/json"
    }
  }
}
```

## 2. API
### Neste passo iremos preparar nossa api para apontar para o Wiremock ao invés de apontar para a api original

- Criar `appsettings.#NomeDoEnvironmentDesejado#.json`

- Apontar rotas para o Wiremock (Lembrando que o mesmo irá executar em um container na mesma rede que a api)

## 3. Docker Compose
### Agora vamos configurar nosso arquivo `docker-compose.yml` para subir o Wiremock junto à API

- Criar service no `docker-compose.yml` para buildar o Dockerfile `./stub/Dockerfile`

- Mapear a porta onde subimos o wiremock para fora da rede docker (passo opcional)

- Adicionar o variável de ambiente `ASPNETCORE_ENVIRONMENT` no serviço `dojo-api` para apontar para o environment que criamos no passo "2. API"


## Agora vamos testar. :grimacing: