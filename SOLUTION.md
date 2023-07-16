# AVIV technical test solution

You can use this file to write down your assumptions and list the missing features or technical revamp that should
be achieved with your implementation.

## Notes

Write here notes about your implementation choices and assumptions.

Worked 2h on this. 
- I never used Docker and had to figure out a lot of things such as how database migrations were handled.
- I never worked with PostgreSQL.
- Navigating through the documentation was complicated (Deadlinks, links to some other documentation which leads to some other documentation).
- docker compose through VS 2022 doesn't seem to work.

A few observations :
- Missing asynchronous queries on current endpoints implementations.
- Usage of `.ConfigureAwait(false)` although it's useless code in an .NET Core API.
- Logging isn't done through templates, but with interpollated strings
- Missing usage of newest APIs e.g. `IActionResult` instead of `Microsoft.AspNetCore.Http.Results`.
- Catching of exceptions in the endpoint itself and returning 500. This is normally done automatically, and can be handled through a filter.
- Some `List` are instanciated without using `Capacity`, this can introduce slow performance and high allocation on the heap (especially for this scenario of millions of queries).
- `DateTime.Now` is used, although it could be any locale loaded on the API, the time could be anything based on the server it is hosted on.
- Calling of `.Update(entity)` : causes the whole fields to be updated
- Calling of `.FirstOrDefault` when excepting only a single entity is not semantically correct, I'd rather call `SingleOrDefault`. 
- The whole functional code is located in the controller, for testability, I would've a done another class as to avoid types like `IActionResult`, which are not test friendly. However, recent versions of .net allows for better testing of types like this (`Microsoft.AspNetCore.Http.Results`).
- No usage of `.Select`. Using projection allows to retrieve only the necessary data.
- The json serialization / deserialization configuration isn't global. I saw attributes like `JsonProperty`, this can be configured in the API.
- `Enum`s are used with `.ToString`. .NET Generators could help increase performance for this (As well as for JSON serialization).
- Handling of sent user state. e.g. When the client sends bad data, we usually send `BadRequest`. Frameworks such as `FluentValidation` can help manage this. I'd use the `ModelState` as to indicate to the client what caused the error. Attributes in `System.Components.DataAnnotations` are supported, such as `RangeAttribute`.
- The test project is in the main project itself ??
- Usage of `DateTime` instead of `DateTimeOffset` (Important for an API that is used in different timezones).

Concerning the implementation :
- `PriceReadOnly.Price` is of type `int`, it should be `double` or even `decimal` as to not lose information. `decimal` is recommended for financial apps. 
- I purposely didn't handle exception, or errors : It should be done in a more global manner. Every dev won't handle it the same way or just do a noop without logging.
- I would add `DbContextPooling` to the API and load test the API to know how much the database can handle.
- I changed the default `Host` to `WebApplication`, for readibility and simplicity [Andrew Lock](https://andrewlock.net/exploring-dotnet-6-part-2-comparing-webapplicationbuilder-to-the-generic-host/) explains it well. 
- The currency type is missing in the database.
- I prefer separating the configuration from the entity itself, so I put everything in `OnModelCreation` of the `DbContext`. It futures proof as for complex link between tables for example. I'd let EFCore deduce less as possible.
- I added validation twice : as a `RangeAttribute` and one in the method itself. This could be refractored.
- We could add a package to handle SnakeCase naming instead of configuring it ourselves for each property / table.
- `DateTime.Now` should be replaced by a mockable interface for unit testing.

## Questions

This section contains additional questions your expected to answer before the debrief interview.

- **What is missing with your implementation to go to production?**

- **How would you deploy your implementation?**

- **If you had to implement the same application from scratch, what would you do differently?**

- **The application aims at storing hundreds of thousands listings and millions of prices, and be accessed by millions
  of users every month. What should be anticipated and done to handle it?**

  NB : You can update the [given architecture schema](./schemas/Aviv_Technical_Test_Architecture.drawio) by importing it
  on [diagrams.net](https://app.diagrams.net/) 
