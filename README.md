# aspnetcore-mongodb-sandbox [![Build Status](https://travis-ci.org/renaudcalmont/aspnetcore-mongodb-sandbox.svg?branch=master)](https://travis-ci.org/renaudcalmont/aspnetcore-mongodb-sandbox)


Built container is available on [Docker hub](https://hub.docker.com/r/renaudcalmont/aspnetcore-mongodb-sandbox/)

## The goal of this project is:

MVP for an enterprise-grade WebAPI... Still a lot going on!

## What it is so far:

An evolving pile of good practices collected over years from different real-world situations and technologies (mostly ASP.NET MVC anyway)

Here are some principles in mind. Any relevant design pattern might be added
* Well-designed interfaces are the key to architecture - both inheritance and layer definition
* Layers help enforcing SRP and are easily activated via dependency injection
* Data are kept in POCO classes "IModel" from which "IEntity" is a particular case denoting an atomic persistent data type.
* Data are manipulated by specialized classes. Ideally, every operation in these classes is stateless.
* The business logic should be reusable from many communication channel (SOAP, restful HTTP, server-side web pages, rich client...).
* As a key component, the business logic should be thouroughly covered by specific unit tests
* Full-stack integration tests are still relevant as automated check before any QA campaign

The layers are separated as follow:

| Layer          | Responsibility, class types                      |
|----------------|--------------------------------------------------|
| Web API        | Controller and associated Views + authentication |
| Business Logic | Handler (both for Entities and other Models)     |
| Data Access    | Repository (for Entities), other persistent data |
| Domain Objects | Models + layers definition (Interfaces)          |

A typical web request have parameters in its URL and/or the JSON representation of a View in its body.
Said request is sent by the framework to a relevant controller according to routing rules, after authentication of the origin.

The Controller should be as thin as possible, managing the transformation of the View in a corresponding Model/Entity and HTTP stuff.
It then delegates further processing of the Model/Entity to the corresponding Handler.

The Handler manages any operation on the data contained in the Model/Entity. In case of an Entity, it will delegate any persistence operation to the corresponding Repository.

The result of the operation, either useful artefact or error message, is followed all the way up to the Controller to construct the HTTP response.

## How to use it on your development machine:

Clone the project and open it in VS code. The prerequisite to run are:
* A running MongoDB server on localhost with default config
* .NET Core Command Line Tools

You should then be able to start the WebAPI from the debugger in VS code.

Alternatively, type __dotnet run__ from a command line in the _src/Sandbox.Server.Http_ sub-folder.

A sample __docker-composer.yml__ script is provided. After running an initial __docker-compose up__ from the command line on a Docker-enabled machine, the WebAPI will respond at http://localhost:5000 (same as running in the debugger) and mongodb will respond at its default location mongodb://localhost:27017.

The MongoDB container port mapping is for convenience only. So you can safely remove it to avoid conflict with any other running MongoDB daemon.

## TODO:
* Security
    * roles and ACL
    * integrate OWIN for OAuth2 authentication
    * deeply implement ownership for multi-tenancy?
    * manage session tokens
* Logging
    * send errors and security notifications to an external ELK stack
* Testing
    * make an example unit test
    * make an example integration test
* Functionality
    * use WebSockets (SignalR) to broadcast changes in real-time
    * archive older versions of an entity at update and delete operations
    * consider adopting a specification such as OData or JSON API - or keep it going its own way...
    * use swagger when in development mode
    * add minimal client-side libraries (such as bower and gulp)
* Persistence
    * get the connection strings from an external resource directory for production environment
* Yeoman
    * write the generator for the entire project
    * consider a generator for adding a model once the skeleton is stabilized
* Consistency
    * documentation
    * StyleCop? SonarCube?


## Credits:
Trying here to keep track of and thank all the building blocks composing this project
* Microsoft [ASP.NET Core](http://www.asp.net/core) of course and for [VS code](https://code.visualstudio.com) editor
* [MongoDB](https://www.mongodb.com/) and their [C# Driver](https://docs.mongodb.com/ecosystem/drivers/csharp/)
* [Yeoman](http://yeoman.io/) - every project.json here started with a "[yo aspnet](https://www.npmjs.com/package/generator-aspnet)"
* [Docker](https://www.docker.com/), including the hub
* [Travis CI](https://travis-ci.org/)

Feel free to PR on this document or the project's code.