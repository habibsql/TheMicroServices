# TheMicroServices

Demonastrate Event Driven Microservice based architecture.


## Domain

Borrowed 3 Boundned context from ERP:

1. **Purchase**: User can purchase Item. Purchase department store that purchased item information.
2. **Sales**: After saling items, user can store sales information
3. **Inventory**: When any purchase or Sales occured, Stock will be automatically updated.


## Architecture

System is consist of 3 Microservices:

* **Purchase Microservice**: This microservice is responsible to manage Purchase related informtion and raised an event after purchase done. Based on the event,
consumer will be updated accordingly.

* **Sales Microservice**: This microservice is responsible to manage Sales related information. When any sales happend it will raised an event so that its interested
party can update and act accordingly.

* **Inventory Microservice**: This microservice is responsible to manage Products and also when any purchase or sales happended it will update stock based on their raised events.

* **CQRS**: CQRS is implemented inside Microservices.

* **Nothing shared architecture**. Every Microservice has its own database.


Here, I have shouwn 3 ways to communicate Microservice to Microservice communication:

* **Event Driven**: Any microservice Publish events to the Message Queue. Other Microservices subscribe those events and act accordingly.
* **REST**: Not recommended approach. But showing here as an example. One Microservice Directly call other Micrservice to the REST way.
* **GRPC**: Recommended approach for now a days. It is very similar like old Remote Procedure Call(RPC).  

#### High Level

![High Level](https://github.com/habibsql/TheMicroservices/blob/main/Docs/highlevel.JPG?raw=true)

#### Command/Query

![CommandQuery](https://github.com/habibsql/TheMicroservices/blob/main/Docs/cq.JPG?raw=true)


## Technology

* C#
* ASP.NET Core
* RabbitMQ: Message Broker for event pub/sub feature.
* MongoDB: NoSQL database (Prefefer NoSQL instead of SQL for better schemaless/scalability/speed etc.
* Rest/GRPC for communication
* Polly: A Nuget package for setting Auto Retry Remote call settings.

