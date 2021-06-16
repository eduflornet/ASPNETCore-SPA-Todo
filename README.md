# Questions

## How long did you spend on your solution?
I was working at times every day, and I think that in total between backend and frontend I invested between 7 and 8 hours including the unit tests.

## How do you build and run your solution?
For backend with the following commands:

For the API, switch to directory: PerfectChannel.WebApi
Dotnet build
Dotnet run

For the test, switch to directory: PerfectChannel.WebApi.Test
Dotnet test

For the frontend part with the following commands:
npm run build
npm start
npm test
## What technical and functional assumptions did you make when implementing your solution?
I do not understand the question very well, but this solution is based at a functional level on all user stories, and for the implementation I have based on the technical requirements, and above all on what is expected as a result. As a result I assumed a UX design with the minimal and tight functionality required.

## Explain briefly your technical design and why do you think is the best approach to this problem.
Basically, it is an API with a Controller that obtains HTTP requests to consult pending and completed tasks. As well as to create and update the status of it.
This API implements Entity Framework Core InMemory to persist the data. I chose this option because I could persist data in memory without using a database.
It uses the Repository pattern to separate the logic that retrieves the data and assigns it to an entity model from the business logic that acts on the model, allowing the business logic to be independent of the type of data that the data layer comprises. data origin. In addition, I chose to use Automapper to generate projections or convert between domain entities and service contracts since it allows us to reuse the mapping code.

In the frontend part with Angular lives an app which has a main component "main-page" where all the logic is in addition to coexisting with a core layer where two interfaces and a service reside.

The latter is the one in charge of making the HTTP requests to the ASPNET Core API.

## If you were unable to complete any user stories, outline why and how would you have liked to implement them.

