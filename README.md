
# AtEase.AspNetCore.Extensions.Middleware

## Installation:
execute the command in **Nuget** package manager console:
>`PM> Install-Package AtEase.AspNetCore.Extensions.Middleware`

### Argument Exceptions:
For handling argument exceptions add app.UseWebApiErrorHandling(config);
```C#
    public void Configure(IApplicationBuilder app ...)
    {	var config = new WebApiErrorHandlingConfig();
	    
		config.CatchArgumentNullException();
		config.CatchArgumentOutOfRangeException();
		config.CatchArgumentException();
		...
		CatchAllArgumentsExceptions(); // Catch all argument exceptions
	    
	    app.UseWebApiErrorHandling(config);
	    ...
    }
```

##  Custom Exceptions:
For handling custom exceptions there is two way
- Use attributes
- Use Mapper
    > Use this to not refrence infrastructure in domain model

### Use Attributes
For handling custom exceptions with attributes add app.UseWebApiErrorHandling();
```C#
    public void Configure(IApplicationBuilder app ...)
    {
	    app.UseWebApiErrorHandling();
	    ...
    }
```
And add following attributes
* ##### Api Validation Exception:
    Handling exceptions (e.g validation errors) that raised in the services and return `BadRequest (400)` HttpStatus with custom message.
    if the `message` argument left blank, the message value is taken from the Exception.
    Add `WebApiBadRequest` attribute to your exceptionclass:
    ```C#
        [WebApiBadRequest("Name", "Name must has our pattern.")]
        public class NameValidationException : Exception
        {
        }
    ```
    Web API result must be same as Web API BadRequest result.

* ##### Api Exception:
    Handling exceptions (e.g conflict errors) that raised in the services and return `Conflict (409)` HttpStatus with custom message.
    Add `WebApiConflict` attribute to your exception class:
    if the `message` argument left blank, the message value is taken from the Exception.
    ```C#
        [WebApiConflict(2001, "the Invoice accepted in the past!!")]
        public class InvoiceAcceptedInThePastException : Exception
        {
        }
    ```
Web API result must be same as Web API Conflict result.

### Use Mapper
Create new Mapper:
```C#
	public class WebApiErrorHandlingConflictExceptionMapper : WebApiErrorHandlingConflictMapper  // mapping conflicts
    {
        public override bool CanHandle(Exception exception)
        {
            return exception.GetType() == typeof(WebApiErrorHandlingConflictException); // Can handle new type of exceptions
        }

        public override object CreateContent(Exception exception)
        {
            var ex = (WebApiErrorHandlingConflictException) exception;

            if (ex.ErrorCode.IsNotNull())
            {
                return new ConflictObjectResult(CreateModelState(ex.ErrorCode.Value.ToString(),
                                                                 ex.Message));
            }

            if (ex.Message.IsNotNullOrEmpty())
            {
                return new ConflictObjectResult(ex.Message);
            }

            return new ConflictResult();
        }
    }
```
And config should be like this:
```C#
    public void Configure(IApplicationBuilder app ...)
    {	var config = new WebApiErrorHandlingConfig();
	    
		config.Map(new WebApiErrorHandlingConflictExceptionMapper());
	    
	    app.UseWebApiErrorHandling(config);
	    ...
    }
```
And web API result must be same as Web API Conflict result.


