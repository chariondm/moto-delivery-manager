<a name="readme-top"></a>

# Design Document: Driver's License Photo Upload System for Delivery Drivers

## Summary

This design outlines a system that enables delivery drivers to securely and efficiently upload photos of their driver's licenses directly to an AWS S3 environment using pre-signed URLs. The system is designed to be secure, scalable, and cost-efficient, adhering to best practices in software architecture and technical documentation. It incorporates file format validation and automated file management to meet specific user requirements.

<details>
   <summary>Table of Contents</summary>
   <ul>
      <li><a href="#diagram">Diagram</a></li>
      <li><a href="#system-components">System Components</a></li>
      <li><a href="#operation-flow">Operation Flow</a></li>
      <li><a href="#architectural-considerations">Architectural Considerations</a></li>
      <li><a href="#poc-with-localstack">POC with Localstack</a></li>
      <li><a href="#conclusion">Conclusion</a></li>
   </ul>
</details>

## Diagram
<p align="right">(<a href="#readme-top">back to top</a>)</p>

![C4 Model - Dynamic Diagram](./dynamic-diagram.png)
Source: [Dynamic Diagram](dynamic-diagram.puml)

## System Components
<p align="right">(<a href="#readme-top">back to top</a>)</p>

- **Amazon S3**: Serves as the storage solution for the driver's license photos of delivery drivers, with a process to handle initial uploads into a temporary directory before moving to a final directory upon validation.
- **AWS Lambda**: Validates the format of uploaded files, ensuring they are in `png` or `bmp` format, and moves validated files to the `/driver-licenses` directory.
  - :warning: **Important Note**: Due to Localstack's limitations, S3 event-based triggers for Lambda functions are not directly supported. For more details on event types supported by Localstack, visit [Localstack Event Support](https://docs.localstack.cloud/user-guide/aws/events/#supported-target-types).
- **Amazon SQS**: Acts as a message queue for notifications about successfully processed driver's license photos, facilitating communication within the workflow.
- **Consumer (Worker Services in .NET)**: Updates the delivery driver's record with the new photo path information in the database.
- **Localstack (Community)**: Simulates AWS services locally for development and testing purposes.

## Operation Flow
<p align="right">(<a href="#readme-top">back to top</a>)</p>

1. **Driver's License Photo Upload**:
   - Delivery drivers upload their photos using a pre-signed URL to S3, into a `/temp` directory.

2. **Validation and Processing by AWS Lambda**:
   - AWS Lambda is triggered to validate the file format. The following pseudocode illustrates the validation process using the `Pillow` library:
   
     ```python
     from PIL import Image
     
     def validate_image(file_path):
         try:
             with Image.open(file_path) as img:
                 if img.format not in ['PNG', 'BMP']:
                     return False
                 return True
         except IOError:
             return False
     
     # Usage example
     file_path = 'path/to/image.file'
     is_valid = validate_image(file_path)
     ```
   
   - If the file is in a valid format (`png` or `bmp`), AWS Lambda moves the file to the `/driver-licenses` directory.
   - A notification is sent to the Amazon SQS queue about the successful upload and file location.

3. **Updating the Driver’s Record**:
   - The Consumer, a Worker Service in .NET, listens to the SQS queue and updates the driver’s database record with the new photo path upon receiving a message.

## Architectural Considerations
<p align="right">(<a href="#readme-top">back to top</a>)</p>

- The separation of directories in S3 ensures a secure and validated storage workflow.
- Implementing the Consumer as a Worker Service in .NET provides reliability and scalability for processing photo uploads and updates.
- Utilizing SQS for message queuing enables a decoupled and scalable architecture suitable for handling asynchronous processes.

## POC with Localstack
<p align="right">(<a href="#readme-top">back to top</a>)</p>

- The POC phase focuses on developing and testing the upload process and workflow within a simulated AWS environment, validating the proposed architecture and component interactions.

## Conclusion
<p align="right">(<a href="#readme-top">back to top</a>)</p>

This design document introduces a secure, efficient system for delivery drivers to upload their driver's license photos, fulfilling the user story requirements. By leveraging AWS services and implementing a dedicated validation and file management process, including the use of `Pillow` for file format validation, this system is prepared for deployment. It ensures that it meets the needs for security, scalability, and user satisfaction.
