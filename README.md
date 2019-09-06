# AtEase.AspNetCore.Extensions.Middleware

## Installation:
execute the command in **Nuget** package manager console:
>`PM> Install-Package AtEase.AspNetCore.Extensions.Middleware`


### APi Error Handling Middleware:
* ### Api Validation Exception:
Handling exceptions (e.g validation errors) that raised in the services and return `BadRequest (400)` HttpStatus with the custom message.
if the `message` argument left blank, the message value is taken from the Exception.
Add `WebApiBadRequest` attribute to your exceptionclass:
```C#
    [WebApiBadRequest("Name", "Name must has our pattern.")]
    public class NameValidationException : Exception
    {
    }
```
Web API result must be same as web api BadRequest result.




* ### Api Exception:
Handling exceptions (e.g conflict errors) that raised in the services and return `Conflict (409)` HttpStatus with the custom message.
Add `WebApiConflict` attribute to your exception class:
if the `message` argument left blank, the message value is taken from the Exception.
```C#
    [WebApiConflict(2001, "the Invoice accepted in the past!!")]
    public class InvoiceAcceptedInThePastException : Exception
    {
    }
```
Web API result must be same as web api Conflict result.
`
