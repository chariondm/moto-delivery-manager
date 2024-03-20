#!/bin/bash

# Wait for LocalStack to be ready
echo "Waiting for LocalStack to be ready..."
while ! curl -s http://localstack:4566/_localstack/health | grep -q "\"s3\": \"available\""; do
  echo "LocalStack is not ready yet; trying again in 1 second..."
  curl -v http://localstack:4566/_localstack/health
  sleep 1
done
echo "LocalStack is ready."

# Create S3 bucket
aws --endpoint-url=http://localstack:4566 s3 mb s3://delivery-driver-licenses-photo

# Create S3 bucket policy
POLICY=$(cat <<EOF
{
  "Version": "2012-10-17",
  "Statement": [{
    "Sid": "AllowOnlyBmpAndPngFiles",
    "Effect": "Allow",
    "Principal": "*",
    "Action": "s3:PutObject",
    "Resource": "arn:aws:s3:::delivery-driver-licenses-photo/*",
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

# Set the policy
aws --endpoint-url=http://localstack:4566 s3api put-bucket-policy --bucket delivery-driver-licenses-photo --policy "$POLICY"

# Show the bucket configuration and policy
aws --endpoint-url=http://localstack:4566 s3api get-bucket-policy --bucket delivery-driver-licenses-photo
