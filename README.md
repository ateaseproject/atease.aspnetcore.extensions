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
this is for return `Conflict (409)` status to end user.
if the error is related to Conflict, you must set the `ApiException` attribute class in your custom exception class.
the `ApiException` has two overload, the first is `errorCode` and the second is `errorCode, message`.
`errorCode` is custom error code you want to assign to specific error.
`message` is custom message to show to end user
if the `message` argument left blank, the message value is taken from the Exception.

```C#
    [ApiException(-1)]
    public class FactorAcceptedInThePastException : Exception
    {
        public FactorAcceptedInThePastException() : base("the Factor accepted in the past!!")
        {
        }
    }
```
or
```C#
    [ApiException(-1, "the Factor accepted in the past!!")]
    public class FactorAcceptedInThePastException : Exception
    {
    }
```
Web API result:
```
Conflict (409)
{
  "errorCode": -1,
  "message": "the Factor accepted in the past!!"
}
```
