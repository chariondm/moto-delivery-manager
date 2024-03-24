#!/bin/bash

# This script initializes resources in LocalStack, including an S3 bucket, an SQS queue, and a Dead Letter Queue (DLQ).
# It allows for easy customization of the resource names and the LocalStack URL through arguments or default values.

# Default values for the names of the S3 bucket, SQS queue, and DLQ.
# These can be overridden by passing arguments to the script in the order: S3 bucket name, SQS queue name, DLQ name.
S3_BUCKET_NAME=${1:-delivery-driver-licenses-photo}
SQS_QUEUE_NAME=${2:-delivery-driver-license-queue}
DLQ_QUEUE_NAME=${3:-delivery-driver-license-dlq}
SQS_RENTAL_AGREEMENT_REQUEST_QUEUE_NAME=${4:-delivery-driver-rental-agreement-request-queue}
DLQ_RENTAL_AGREEMENT_REQUEST_QUEUE_NAME=${5:-delivery-driver-rental-agreement-request-dlq}
LOCALSTACK_URL="http://localstack:4566"

# Waits for LocalStack to be fully ready by repeatedly checking its health endpoint.
# It specifically waits for the S3 service to be reported as "available" before proceeding.
wait_for_localstack() {
    echo "Waiting for LocalStack to be ready..."
    while ! curl -s "$LOCALSTACK_URL"/_localstack/health | grep -q "\"s3\": \"available\""; do
        echo "LocalStack is not ready yet; trying again in 1 second..."
        sleep 1
    done
    echo "LocalStack is ready."
}

# Creates an S3 bucket with the name specified by the S3_BUCKET_NAME variable.
# The bucket is created using the AWS CLI, pointed at the LocalStack endpoint.
create_s3_bucket() {
    aws --endpoint-url="$LOCALSTACK_URL" s3 mb s3://"$1"
    echo "S3 bucket created: $1"
}

# Sets a bucket policy on the newly created S3 bucket.
# The policy allows "PutObject" actions for files matching "*.bmp" and "*.png" extensions, from any principal.
set_bucket_policy() {
    local policy=$(cat <<EOF
{
  "Version": "2012-10-17",
  "Statement": [{
    "Sid": "AllowOnlyBmpAndPngFiles",
    "Effect": "Allow",
    "Principal": "*",
    "Action": "s3:PutObject",
    "Resource": "arn:aws:s3:::$1/*",
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

    aws --endpoint-url="$LOCALSTACK_URL" s3api put-bucket-policy --bucket "$1" --policy "$policy"
    echo "Bucket policy set for $1."
}

# Creates an SQS queue with the name specified by the queue_name argument.
# Also creates a Dead Letter Queue (DLQ) with the name specified by the dlq_name argument.
# The DLQ is associated with the main queue, allowing messages that fail to be processed to be sent to the DLQ.
# The DLQ is created with a redrive policy that sends messages to the DLQ after a single receive attempt.
create_sqs_queue() {
    local queue_url=$(aws --endpoint-url="$LOCALSTACK_URL" sqs create-queue --queue-name "$1" --query 'QueueUrl' --output text)
    echo "SQS Queue created: $1"
    echo "$queue_url"

    local dlq_url=$(aws --endpoint-url="$LOCALSTACK_URL" sqs create-queue --queue-name "$2" --query 'QueueUrl' --output text)
    echo "SQS DLQ Queue created: $2"
    echo "$dlq_url"
  
    local dlq_arn=$(aws --endpoint-url="$LOCALSTACK_URL" sqs get-queue-attributes --queue-url "$dlq_url" --attribute-names QueueArn --query 'Attributes.QueueArn' --output text)
    local json_path="/docker-entrypoint-initaws.d/set-queue-attributes.json"

    aws --endpoint-url="$LOCALSTACK_URL" sqs set-queue-attributes --queue-url "$dlq_url" --attributes "file://$json_path"
    echo "DLQ $2 associated with SQS Queue $1"
}

# Main script logic
wait_for_localstack
create_s3_bucket "$S3_BUCKET_NAME"
set_bucket_policy "$S3_BUCKET_NAME"
create_sqs_queue "$SQS_QUEUE_NAME" "$DLQ_QUEUE_NAME"
create_sqs_queue "$SQS_RENTAL_AGREEMENT_REQUEST_QUEUE_NAME" "$DLQ_RENTAL_AGREEMENT_REQUEST_QUEUE_NAME"
