<a name="readme-top"></a>

<details>
  <summary>Table of Contents</summary>
  <ul>
    <li><a href="#project-architecture">Project Architecture</a>
      <ul>
        <li><a href="#hexagonal-architecture-fundamentals">Hexagonal Architecture Fundamentals</a></li>
        <li><a href="#modular-and-flexible-structure">Modular and Flexible Structure</a></li>
        <li><a href="#benefits">Benefits</a></li>
      </ul>
    </li>
    <li><a href="#project-structure">Project Structure</a>
      <ul>
        <li><a href="#core">Core</a></li>
        <li><a href="#adapters">Adapters</a></li>
        <li><a href="#infrastructure">Infrastructure</a></li>
        <li><a href="#docker-setup">Docker-setup</a></li>
        <li><a href="#docs">Docs</a></li>
      </ul>
    </li>
    <li><a href="#getting-started">Getting Started</a>
        <ul>
          <li><a href="#prerequisites">Prerequisites</a></li>
          <li><a href="#running-locally-with-net-8">Running Locally with .NET 8</a></li>
          <li><a href="#additional-notes">Additional Notes</a></li>
        </ul>
    </li>
    <li><a href="#use-cases">Use Cases</a></li>
  </ul>
</details>

# Moto Delivery Manager

<p align="right">(<a href="#readme-top">back to top</a>)</p>

`MotoDeliveryManager` is a comprehensive system developed using .NET 8 and C# designed to manage the rental of motorcycles and the registration and management of delivery drivers. It leverages the principles of Hexagonal and Clean Architecture to provide a robust and scalable solution for motorcycle rental services. This system includes functionalities such as registering new motorcycles, managing deliveries, and handling driver registrations and rentals. The project is organized into different modules, including Core, Adapters, and Infrastructure, to ensure a clear separation of concerns and facilitate the development and maintenance of the system.

## Project Architecture

<p align="right">(<a href="#readme-top">back to top</a>)</p>

The MotoDeliveryManager is built following the principles of **Hexagonal Architecture**, also known as **Ports and Adapters**. This architectural approach centers the system's business logic, keeping it decoupled from infrastructure details and user interfaces. This promotes greater flexibility, testability, and maintainability of the code.

### Hexagonal Architecture Fundamentals

The essence of Hexagonal Architecture lies in the clear separation between the core application logic and the interaction points with the external world. This separation is achieved through the use of:

- **Ports**: Interfaces that define entry and exit points for the application logic. They serve as contracts that external adapters must implement, allowing the application to interact with different technologies and services (such as databases, messaging systems, and web APIs) without direct dependency between them.

- **Adapters**: Concrete implementations of the ports, adapted to specific means of communication or technologies. For example, an adapter might be responsible for receiving HTTP requests and converting them into calls to the application logic, or for publishing messages in a Kafka topic based on events within the application.

### Modular and Flexible Structure

By adopting Hexagonal Architecture, `MotoDeliveryManager` organizes its code into clearly defined modules corresponding to different areas of responsibility:

- **Core**: Contains the essential business logic and domain models. This is the part of the application that encapsulates fundamental rules and operations, such as managing motorcycle rentals and delivery orders.

- **Adapters**: Divided into inbound and outbound adapters, these modules bridge the Core of the application to the external world. Inbound adapters process external requests into the application (e.g., through REST APIs), while outbound adapters handle the execution of external actions requested by the Core (like sending messages or persisting data).

- **Infrastructure**: Provides the necessary infrastructure for the application to run, including database configurations, such as migrations and entity mappings, and other infrastructure-related concerns.

### Benefits

Hexagonal Architecture offers multiple benefits for the development and maintenance of `MotoDeliveryManager`:

- **Testability**: The clear separation between business logic and external interfaces facilitates the creation of automated tests, allowing the business logic to be tested in isolation.

- **Flexibility**: Makes it easier to replace or add new adapters to connect to different services or technologies without changing the core application logic.

- **Maintainability**: The modularity and clarity of the project structure simplify understanding the code and implementing changes or improvements.

In summary, `MotoDeliveryManager's` architecture aims to create a robust and adaptable system, capable of evolving and integrating easily into a constantly changing technological ecosystem while keeping its business logic protected and clearly defined.

## Project Structure

<p align="right">(<a href="#readme-top">back to top</a>)</p>

The `MotoDeliveryManager` system adopts a modular architecture inspired by the principles of Hexagonal Architecture. This approach emphasizes the clear separation between the system's business logic and the interaction interfaces with the external world, through the use of ports and adapters. Below is a detailed view of each main project component:

### Core

#### Domain
- **Motorcycles**: Represents the central entity in the motorcycle management system, containing information such as year, model, and license plate. Unique constraints apply to license plates to prevent duplicates.
- **DeliveryDrivers**: Manages delivery driver entities, including details like name, CNPJ, birth date, driver's license number and type, and driver's license image. The system enforces uniqueness on CNPJ and driver's license number.

#### Application
- **UseCases**: Encapsulate business logic for operations such as registering a new motorcycle, managing deliveries, and handling driver registrations and rentals. Each use case is implemented to be independent of input and output means, allowing reuse in different contexts. For example, the `RegisterMotorcycle` use case can be triggered by an HTTP request or a message from a queue, without changing the core logic.

### Adapters

#### Inbound
- **DeliveryDriverHttpApiAdapter**: Contains controllers that expose HTTP endpoints for external interaction, like driver registration and motorcycle rental requests.
- **MotorcycleHttpApiAdapter**: Manages HTTP requests for motorcycle management operations, providing endpoints for registration, updates, and queries.
- **SQSDriverLicensePhotoProcessorAdapter**: Processes driver's license photo uploads using the Amazon SQS messaging service, handling asynchronous receipt and processing of photo upload requests.

#### Outbound
- **AwsS3StorageAdapter**: Implements functionality for storing files in Amazon S3, used for storing driver's license images outside the database.
- **PostgresDbAdapter**: Manages database operations using PostgreSQL, facilitating data persistence for motorcycles, drivers, and rentals.

### Infrastructure

#### Database
- **PostgresDb**: Infrastructure related to PostgreSQL database operations, including entity configurations and migrations. This component is crucial for data persistence and retrieval, supporting the application's data storage needs.

### Docker-setup

The `docker-setup` directory includes Docker Compose files necessary to configure and run the development environment in an isolated and consistent manner. Within this directory, you'll find configurations for services like localstack for simulating AWS services and PostgreSQL database for data storage. This organization facilitates the management of different system components, allowing them to be run separately or together, depending on the developer's needs.

### Docs

- **[001-DriverLicensePhotoUpload](./docs/design-docs/001-DriverLicensePhotoUpload/DesignDoc001_DriverLicensePhotoUpload.md)**: Documentation detailing the driver's license photo upload feature, including API specifications, data flow diagrams, and integration details with AWS S3 for photo storage.

This structure provides a solid foundation for the development, maintenance, and expansion of the `MotoDeliveryManager` system, allowing developers to contribute efficiently and the system to evolve in an organized and controlled manner.

## Getting Started

<p align="right">(<a href="#readme-top">back to top</a>)</p>

This section provides guidance on how to run the `MotoDeliveryManager` system locally, including requirements and step-by-step instructions for both .NET 8 and Docker Compose environments.

### Prerequisites

- **.NET 8 SDK**: Required to build and run the .NET projects locally. You can download the .NET 8 SDK from the official .NET website: [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

- **AWS CLI**: If you plan to run the system using Docker Compose and want to use the `SQSDriverLicensePhotoProcessorAdapter` to process driver's license photo uploads, you'll need to have the AWS CLI installed. You can download the AWS CLI from the official AWS website: [Download AWS CLI](https://aws.amazon.com/cli/).

- **Localstack**: If you're using Docker Compose and want to simulate AWS services locally, you'll need to have Localstack installed. Localstack provides a local environment that simulates a variety of AWS services, including S3 and SQS. For more information, see the official Localstack documentation: [Install Localstack](https://docs.localstack.cloud/getting-started/installation/#localstack-cli).

- **Docker Desktop**: If you prefer to run the system using Docker Compose, you'll need to install Docker Desktop to manage and run the containers. You can download Docker Desktop from the official Docker website: [Download Docker Desktop](https://www.docker.com/products/docker-desktop).

- **Docker Compose**: If you're using Docker Desktop, you'll need to have Docker Compose installed. Docker Compose is included with Docker Desktop for Windows and macOS, but you can also install it separately on Linux. For more information, see the official Docker Compose documentation: [Install Docker Compose](https://docs.docker.com/compose/install/).

### Running Locally with .NET 8

1. **Clone the Repository**: Start by cloning the `MotoDeliveryManager` repository to your local machine using the following command:

   ```bash
   git clone https://github.com/chariondm/moto-delivery-manager.git
    ```
2. **Navigate to the Project Directory**: Move into the `MotoDeliveryManager` directory using the following command:

   ```bash
   cd moto-delivery-manager
   ```
3. **Run the specific project**: Use the `dotnet run --project` command to run the application, specifying the project to run. For example, to run the `MotorcycleHttpApiAdapter` project, use the following command:
    
   ```bash
   dotnet run --project src/Adapters/Inbound/MotorcycleHttpApiAdapter/MotorcycleHttpApiAdapter.csproj
   ```
   :warning: You can also run the `DeliveryDriverHttpApiAdapter` or the `SQSDriverLicensePhotoProcessorAdapter` projects using the same command, replacing the project path accordingly.

4. **Access the Application**: Once the application is running, you can access the exposed HTTP endpoints using a tool like `curl` or OpenAPI clients like Swagger or Postman. For example, you can use the following `curl` command to send a POST request to the `RegisterMotorcycle` endpoint:

   ```bash
    curl -X 'POST' \
      'http://localhost:5232/api/v1/motorcycles' \
      -H 'accept: */*' \
      -H 'Content-Type: application/json' \
      -d '{
        "year": 2022,
        "model": "Ninja 300",
        "licensePlate": "ABC1234"
      }'
    ```
    :warning: Replace the URL and payload with the appropriate values for the other endpoints and projects.

#### Application URLs and Descriptions

For your reference, here are the URLs and descriptions for the different applications and their endpoints:

| Application                   | URL                   | Description |
|-----------------------------|-----------------------|-----------|
| **DeliveryDriverHttpApiAdapter** | [http://localhost:5236](http://localhost:5236/swagger/index.html) | Exposes HTTP endpoints for driver registration and motorcycle rental requests. |
| **MotorcycleHttpApiAdapter** | [http://localhost:5232](http://localhost:5232/swagger/index.html) | Manages HTTP requests for motorcycle management operations, providing endpoints for registration, updates, and queries. |
| **SQSDriverLicensePhotoProcessorAdapter** | N/A | Processes driver's license photo uploads using the Amazon SQS messaging service, handling asynchronous receipt and processing of photo upload requests. |

### Additional Notes

#### Sending driver's license photo to the S3 bucket

1. **Start the Docker Compose environment**: If you want to use the `SQSDriverLicensePhotoProcessorAdapter` to process driver's license photo uploads, you'll need to start the Docker Compose environment using the following command:

   ```bash
   docker compose -f docker-setup/docker-compose.services.yml up
   ```
   :warning: This command starts the Docker Compose environment, including the Localstack services for simulating AWS services like S3 and SQS.

2. **Run the `DeliveryDriverHttpApiAdapter` project**: Use the `dotnet run --project` command to run the `DeliveryDriverHttpApiAdapter` project, as described in the previous section.

3. **Create a delivery driver**: Use the `DeliveryDriverHttpApiAdapter` to create a delivery driver. You can use the following `curl` command to send a POST request to the `delivery-drivers` endpoint:

   Request:
   ```bash
   curl --request POST \
      --url http://localhost:5236/api/v1/delivery-drivers \
      --header 'content-type: application/json' \
      --data '{"name": "full name","cnpj": "valid-cnpj","dateOfBirth": "yyyy-mm-dd","driverLicenseNumber": "11111111","driverLicenseCategory": "B"}'
   ```
   :warning: Replace the payload with the appropriate values for the driver you want to create.

   Response:
   ```json
   {
       "success": true,
       "message": "The delivery driver was successfully registered.",
       "data": {
           "id": "0d91b3b0-a19a-48ad-910c-1221cf0b0e36",
           "presignedUrl": "https://localhost:4566/delivery-driver-licenses-photo/tmp/0d91b3b0-a19a-48ad-910c-1221cf0b0e36.png?AWSAccessKeyId=localstack&Expires=1711053327&Signature=%2FNzsWFtd04J6%2FbdIP2Su8w6c9%2BI%3D"
       }
   }
   ```
   :warning: The `presignedUrl` returned in the response is used to send the driver's license photo to the S3 bucket.

3. **Send the driver's license photo to the S3 bucket**: Use the `presignedUrl` returned in the response to send a PUT request to the S3 bucket. For example, you can use the following `curl` command to send a PUT request to the `presignedUrl`:

   ```bash
   curl --request PUT \
      --url 'http://localhost:4566/delivery-driver-licenses-photo/tmp/0d91b3b0-a19a-48ad-910c-1221cf0b0e36.png?AWSAccessKeyId=localstack&Expires=1711053327&Signature=%2FNzsWFtd04J6%2FbdIP2Su8w6c9%2BI%3D' \
      -F 'text=title' \
      -F 'image=@.tmp/dog_license.png;type=image/png' \
      --fail \
      -v   
   ```
   :warning: Replace the URL and payload with the appropriate values for the driver's license photo you want to send.

   Response:
   ```plaintext
   *   Trying 127.0.0.1:4566...
   * Connected to localhost (127.0.0.1) port 4566 (#0)
   > PUT /delivery-driver-licenses-photo/tmp/0d91b3b0-a19a-48ad-910c-1221cf0b0e36.png?AWSAccessKeyId=localstack&Expires=1711053327&Signature=%2FNzsWFtd04J6%2FbdIP2Su8w6c9%2BI%3D HTTP/1.1
   > Host: localhost:4566
   > User-Agent: curl/7.81.0
   > Accept: */*
   > Content-Length: 215797
   > Content-Type: multipart/form-data; boundary=------------------------7c4fba1f55ed196d
   > 
   * We are completely uploaded and fine
   * Mark bundle as not supporting multiuse
   < HTTP/1.1 200 ############################## -----> The response code should be 200 <----- ##############################
   < Content-Type: application/xml
   < ETag: "b3d33cb07641fa143eec5bae6d6ebfc5"
   < x-amz-server-side-encryption: AES256
   < x-amz-request-id: cfe983c6-e43a-405e-b33d-ee63c21ce32b
   < x-amz-id-2: s9lzHYrFp76ZVxRcpX9+5cjAnEH2ROuNkd2BHfIa6UkFVdtjf5mKR3/eTPFvsiP/XV/VLi31234=
   < Connection: close
   < Content-Length: 0
   < date: Thu, 21 Mar 2024 20:59:14 GMT
   < server: hypercorn-h11
   < 
   * Closing connection 0
   ```
   :warning: The response code should be 200, indicating that the driver's license photo was successfully sent to the S3 bucket.

4. **Check the S3 bucket**: You can use the AWS CLI to check the contents of the S3 bucket. For example, you can use the following command to list the contents of the `delivery-driver-licenses-photo` bucket:

   ```bash
   aws --endpoint-url=http://localhost:4566 s3 ls --recursive --human-readable --summarize delivery-driver-licenses-photo
   ```
   :warning: Replace the URL with the appropriate value for the S3 bucket you want to list.

   Response:
   ```plaintext
   2024-03-21 17:59:14  210.7 KiB tmp/0d91b3b0-a19a-48ad-910c-1221cf0b0e36.png
   2024-03-21 18:05:34  342.2 KiB tmp/1a81b3b0-a19a-48ad-910c-1221cf0b0e36.png
   2024-03-21 18:06:27  342.2 KiB tmp/1a81b3b0-a19a-48ad-910c-1221cf0b0e42.png

   Total Objects: 3
      Total Size: 895.2 KiB
   ```
   :warning: The response should list the contents of the `delivery-driver-licenses-photo` bucket, including the driver's license photo you uploaded.

#### Sending messages to the SQS queue

To send a message to the SQS queue, you can use the AWS CLI to send a message to the `delivery-driver-license-queue` queue. For example, you can use the following command to send a message to the queue:

   ```bash
   aws --endpoint-url=http://localhost:4566 sqs send-message --queue-url http://localhost:4566/000000000000/delivery-driver-license-queue --message-body '{
   "deliveryDriverId": "0d91b3b0-a19a-48ad-910c-1221cf0b0e36",
   "photoPath": "tmp/0d91b3b0-a19a-48ad-910c-1221cf0b0e36.png",
   "timestamp": "2024-03-21T15:03:00Z"
   }' | cat
   ```
   :warning: Replace the URL and payload with the appropriate values for the message you want to send.

   Response:
   ```json
   {
      "MD5OfMessageBody": "d41d8cd98f00b204e9800998ecf8427e",
      "MessageId": "d41d8cd98f00b204e9800998ecf8427e"
   }
   ```

To check the messages in the queue, you can use the following command:
   
   ```bash
   aws --endpoint-url=http://localhost:4566 sqs receive-message --queue-url http://localhost:4566/000000000000/delivery-driver-license-queue | cat
   ```
   :warning: Replace the URL with the appropriate value for the queue you want to check.

   Response:
   ```json
   {
      "Messages": [
         {
            "MessageId": "a3750c5f-c817-4fd7-ad62-788f1fc91d49",
            "ReceiptHandle": "NTdlMDBmMzctYjJhNC00MjY2LTg2OTctZTJlN2Q4ODEyZWM2IGFybjphd3M6c3FzOnVzLWVhc3QtMTowMDAwMDAwMDAwMDA6ZGVsaXZlcnktZHJpdmVyLWxpY2Vuc2UtcXVldWUgYTM3NTBjNWYtYzgxNy00ZmQ3LWFkNjItNzg4ZjFmYzkxZDQ5IDE3MTEwNTU4MDguMTY1MDc3Mg==",
            "MD5OfBody": "ca3711477c601a8a9b831fbc74fed566",
            "Body": "{\n  \"deliveryDriverId\": \"0d91b3b0-a19a-48ad-910c-1221cf0b0e36\",\n  \"photoPath\": \"tmp/0d91b3b0-a19a-48ad-910c-1221cf0b0e36.png\",\n  \"timestamp\": \"2024-03-21T15:03:00Z\"\n}"
         }
      ]
   }
   ```
   :warning: The response should list the messages in the queue, including the message you sent.

## Use Cases

For a detailed explanation of the system's use cases, see the [Use Cases documentation](./docs/use-cases/use-cases.md)
