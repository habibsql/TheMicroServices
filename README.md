# TheMicroServices

Demonastrate Event Driven Microservice based architecture.


# Domain

Borrowed 3 Boundned context from ERP.

1) Purchase: User can purchase Item. Purchase department store that purchased item information.
2) Sales: After saling items, user can store sales information
3) Inventory: When any purchase or Sales occured, Stock will be automatically updated.


# Architecture

System is consist of 3 Microservices:

0. CQRS (DDD) is implemented inside Microservices.

1. Purchase Microservice: This microservice is responsible to manage Purchase related informtion and raised an event after purchase done. Based on the event,
consumer will be updated accordingly.

2. Sales Microservice: This microservice is responsible to manage Sales related information. When any sales happend it will raised an event so that its interested
party can update and act accordingly.

3. Inventory Microservice: This microservice is responsible to manage Products and also when any purchase or sales happended it will update stock based on their raised events.

4. Follow no share architecture. Means Every Microservice has its own database.


Here, I have shouwn 3 ways to communicate Microservice to Microservice communication:

1. Event Driven: Any microservice Publish events to the Message Queue. Other Microservices subscribe those events and act accordingly.
2. REST: Not recommended approach. But showing here as an example. One Microservice Directly call other Micrservice to the REST way.
3. GRPC: Recommended approach for now a days. It is very similar like old Remote Procedure Call(RPC).  

![Overall Architecture](https://github.com/habibsql/TheMicroservices/blob/main/Docs/OverallArchitecture.JPG?raw=true)

# Technology

1. C#
2. ASP.NET Core
3. RabbitMQ: Message Broker for event pub/sub feature.
4. MongoDB: NoSQL database (Prefefer NoSQL instead of SQL for better schemaless/scalability/speed etc.
5. Rest/GRPC for communication
6. Polly: A Nuget package for setting Auto Retry Remote call settings.

