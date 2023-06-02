# .Net 7 JWT Authorization

Here I show how to use Bearer tokens to authorize the access to APIs.

Used Tools and Technologies:
 - .NET 7
 - Entity Framework Core
 - Postgre SQL
 - JWT 

On the folder of the project, execute:

```
docker-compose up -d
```

This will execute the Postgres container.

Execute the project and send a request to the Home/Get/{id} route.

You will receive a status code 401: Unauthorized.

Now send a request to the Auth/Login route, get the token and try the Home/Get/{id} route again. This time passing the token on Authorization header.

You will receive a status code 200: Success.