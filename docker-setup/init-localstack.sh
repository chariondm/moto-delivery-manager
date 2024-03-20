#!/bin/bash

# This script initializes resources in LocalStack, including an S3 bucket, an SQS queue, and a Dead Letter Queue (DLQ).
# It allows for easy customization of the resource names and the LocalStack URL through arguments or default values.

# Default values for the names of the S3 bucket, SQS queue, and DLQ.
# These can be overridden by passing arguments to the script in the order: S3 bucket name, SQS queue name, DLQ name.
S3_BUCKET_NAME=${1:-delivery-driver-licenses-photo}
SQS_QUEUE_NAME=${2:-delivery-driver-license-queue}
DLQ_QUEUE_NAME=${3:-delivery-driver-license-dlq}

# The URL for accessing LocalStack, defaulting to http://localstack:4566.
# This is useful for environments where LocalStack is accessed through a different port or host.
LOCALSTACK_URL="http://localstack:4566"

# Waits for LocalStack to be fully ready by repeatedly checking its health endpoint.
# It specifically waits for the S3 service to be reported as "available" before proceeding.
wait_for_localstack() {
    echo "Waiting for LocalStack to be ready..."
    while ! curl -s $LOCALSTACK_URL/_localstack/health | grep -q "\"s3\": \"available\""; do
        echo "LocalStack is not ready yet; trying again in 1 second..."
        sleep 1
    done
    echo "LocalStack is ready."
}

# Creates an S3 bucket with the name specified by the S3_BUCKET_NAME variable.
# The bucket is created using the AWS CLI, pointed at the LocalStack endpoint.
create_s3_bucket() {
    aws --endpoint-url=$LOCALSTACK_URL s3 mb s3://$S3_BUCKET_NAME
    echo "S3 bucket created: $S3_BUCKET_NAME"
}

# Sets a bucket policy on the newly created S3 bucket.
# The policy allows "PutObject" actions for files matching "*.bmp" and "*.png" extensions, from any principal.
set_bucket_policy() {
    POLICY=$(cat <<EOF
{
  "Version": "2012-10-17",
  "Statement": [{
    "Sid": "AllowOnlyBmpAndPngFiles",
    "Effect": "Allow",
    "Principal": "*",
    "Action": "s3:PutObject",
    "Resource": "arn:aws:s3:::$S3_BUCKET_NAME/*",
    "Condition": {
      "StringLike": {
        "s3:objectKey": [
          "*.bmp",
          "*.png"
        ]
      }
    }
  }]
}
EOF
    )

    aws --endpoint-url=$LOCALSTACK_URL s3api put-bucket-policy --bucket $S3_BUCKET_NAME --policy "$POLICY"
    echo "Bucket policy set for $S3_BUCKET_NAME."
}

# Creates an SQS queue with the name specified by the SQS_QUEUE_NAME variable.
# The queue URL is outputted to the console for reference.
create_sqs_queue() {
    QUEUE_URL=$(aws --endpoint-url=$LOCALSTACK_URL sqs create-queue --queue-name $SQS_QUEUE_NAME --query 'QueueUrl' --output text)
    echo "SQS Queue created: $SQS_QUEUE_NAME"
}

# Creates a Dead Letter Queue (DLQ) with the name specified by the DLQ_QUEUE_NAME variable.
# It then associates this DLQ with the primary SQS queue, setting the redrive policy to redirect messages
# that have been received more than 3 times without successful processing.
create_dlq() {
    DLQ_URL=$(aws --endpoint-url=$LOCALSTACK_URL sqs create-queue --queue-name $DLQ_QUEUE_NAME --query 'QueueUrl' --output text)
    echo "DLQ created: $DLQ_QUEUE_NAME"

    DLQ_ARN=$(aws --endpoint-url=$LOCALSTACK_URL sqs get-queue-attributes --queue-url $DLQ_URL --attribute-names QueueArn --query 'Attributes.QueueArn' --output text)

    # Use o caminho absoluto do arquivo JSON dentro do container.
    JSON_PATH="/docker-entrypoint-initaws.d/set-queue-attributes.json"

    # Substitui o placeholder no arquivo JSON pelos valores reais usando o caminho absoluto.
    sed -i "s|\[DLQ_ARN\]|$DLQ_ARN|g" $JSON_PATH

    # Usa o caminho absoluto no comando AWS CLI.
    aws --endpoint-url=$LOCALSTACK_URL sqs set-queue-attributes --queue-url $QUEUE_URL --attributes "file://$JSON_PATH"
    echo "DLQ set for SQS Queue: $SQS_QUEUE_NAME"
}

# The main execution flow of the script, calling the functions defined above in order to
# set up the LocalStack environment with the specified resources.
wait_for_localstack
create_s3_bucket
set_bucket_policy
create_sqs_queue
create_dlq
