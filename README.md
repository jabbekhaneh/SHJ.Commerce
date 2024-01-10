# How To run solution
1.Install Docker
</br>
2.build
```
 docker-compose build
```
3.up
```
docker-compose up
```

# Domain Driven Design
DDD example focus on Core concept of DDD like

This is simple packing app built on top of clean architecture and CQRS 

While .NET 6 offers significant benefits for new applications and application patterns, .NET Framework 
will continue to be a good choice for many existing scenarios


# Design a DDD-oriented microservice
Domain-driven design (DDD) advocates modeling based on the reality of business as relevant to your use cases. In the context of building applications, DDD talks about problems as domains. It describes independent problem areas as Bounded Contexts (each Bounded Context correlates to a microservice), and emphasizes a common language to talk about these problems. It also suggests many technical concepts and patterns, like domain entities with rich models (no anemic-domain model), value objects, aggregates, and aggregate root (or root entity) rules to support the internal implementation. This section introduces the design and implementation of those internal patterns.
[NET-Microservices PDF](https://github.com/dotnet-architecture/eBooks/raw/main/current/microservices/NET-Microservices-Architecture-for-Containerized-NET-Applications.pdf?WT.mc_id=dotnet-35129-website)
- Entities
- Value Objects
- Aggregates
- Repository
- UnitOfWork
- CQRS(Command Query Responsibility Segregation)
- Docker

# What is CQRS
 CQRS stands for Command Query Responsibility Segregation. At its heart is the notion that you can use a different model to update information than the model you use to read information. For some situations, this separation can be valuable, but beware that for most systems CQRS adds risky complexity.

# What are microservices?
Microservices - also known as the microservice architecture - is an architectural style that structures an application as a collection of services that are

- Highly maintainable and testable
- Loosely coupled
- Independently deployable
- Organized around business capabilities
- Owned by a small team


The microservice architecture enables the rapid, frequent and reliable delivery of large, complex applications. It also enables an organization to evolve its technology stack.


[Microservices more read](hhttps://microservices.io)