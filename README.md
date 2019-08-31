# atease.aspnetcore.extensions.middleware

## Installation:
execute the command in **Nuget** package manager console:
>`PM> Install-Package AtEase.AspNetCore.Extensions.Middleware`


### APi Error Handling Middleware:
this middleware contains from two part. 
* ### Api Validation Exception:
the first is for handling validation errors in WebApi and return `BadRequest (400)` HttpStatus with the custom information.
as the folow, you must create and throw custom Exception class an inheritance it from `Exception` class.
```C#
    public class EntityNotFoundException : Exception
    {
    }
```
```C#
throw new EntityNotFoundException();
```

if the error is related to validation of model that recieved from api, you must set the `ApiValidationException` attribute class in your custom exception class.
the `ApiValidationException` has three overload, the first is `fieldName` and the second is `fieldName, message` and the third is `title,fieldName, message`.
`fieldName` is name of the invalid field.
`message` is custom message to show to end user
`title` title of the error message
if the `message` argument left blank, the message value is taken from the Exception.

```C#
    [ApiValidationException("Id", "EntityNotFound!!")]
    public class EntityNotFoundException : Exception
    {
    }
```
or
```C#
    [ApiValidationException("Id")]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("EntityNotFound!!")
        {
        }
    }
```
or
```C#
    [ApiValidationException("Title of Error", "Id", "EntityNotFound!!")]
    public class EntityNotFoundExceptionWithMessage : Exception
    {
    }
```

the sample result of the http request is:
```
BadRequest (400)
{
  "errors": {
    "Id": [
      "EntityNotFound!!"
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
the sample result of the http request is:
```
Conflict (409)
{
  "errorCode": -1,
  "message": "the Factor accepted in the past!!"
}
```
