# atease.aspnetcore.extensions.middleware

## Installation:
execute the command in **Nuget** package manager console:
>`PM> Install-Package AtEase.AspNetCore.Extensions.Middleware`


### APi Error Handling Middleware:
* ### Api Validation Exception:
Handling validation errors that raised in the services and return `BadRequest (400)` HttpStatus with the custom message.
Add `ApiValidationException` attribute to your exceptionclass:
```C#
    [ApiValidationException("Name", "Name must has our pattern.")]
    public class NameValidationException : Exception
    {
    }
```
Web API result:
```
BadRequest (400)
{
  "errors": {
    "Id": [
      "Name must has our pattern."
    ]
  },
  "title": "Title of Error",
  "status": 400,
  "traceId": "80000005-0000-fd00-b63f-84710c7967bb"
}
```



* ### Api Exception:
Handling Conflict errors that raised in the services and return `Conflict (409)` HttpStatus with the custom message.
Add `ApiException` attribute to your exceptionclass:
if the `message` argument left blank, the message value is taken from the Exception.
```C#
    [ApiException(-1, "the Invoice accepted in the past!!")]
    public class InvoiceAcceptedInThePastException : Exception
    {
    }
```
Web API result:
```
Conflict (409)
{
  "errorCode": -1,
  "message": "the Invoice accepted in the past!!"
}
```
