#!/bin/bash

# Default values for environment variables
REQUEST_COUNT=${REQUEST_COUNT:-1440}
INTERVAL=${INTERVAL:-5}
URL=${URL:-'http://localhost:5047/api/locationInformation?locationName=Bern&limit=2'}
AUTH_HEADER=${AUTH_HEADER:-'Authorization: Bearer eyJvcmciOiI2NDA2NTFhNTIyZmEwNTAwMDEyOWJiZTEiLCJpZCI6ImVkYTBkMDY1NDk5NjRjMWU4OTY0ZjljNWJjOGEyMzExIiwiaCI6Im11cm11cjEyOCJ9'}

# Loop to make the requests
for ((i=1; i<=REQUEST_COUNT; i++))
do
  echo "Making request $i to $URL..."
  curl --location "$URL" --header "$AUTH_HEADER"
  
  # Check if the request count is less than the total to avoid an unnecessary sleep at the end
  if [ $i -lt $REQUEST_COUNT ]; then
    sleep $INTERVAL
  fi
done

echo "All $REQUEST_COUNT requests completed."


#  api-tester:
 #    image: api-tester
 #    environment:
 #      - REQUEST_COUNT=1440
 #      - INTERVAL=5
 #      - URL=http://host.docker.internal:5047/api/locationInformation?locationName=Bern&limit=2
 #    networks:
 #      - monitoring
 #    entrypoint: /usr/local/bin/test_api.sh