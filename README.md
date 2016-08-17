[![Build Status](https://travis-ci.org/renaudcalmont/aspnetcore-mongodb-sandbox.svg?branch=master)](https://travis-ci.org/renaudcalmont/aspnetcore-mongodb-sandbox)
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

## TODO:
* Persistence
    * read the connection strings from the configuration
    * get the connection strings from an external resource directory for production environment
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
* [Docker](https://www.docker.com/)

Feel free to PR on this document or the project's code.