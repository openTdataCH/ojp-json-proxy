# Use an official lightweight image
FROM alpine:latest

# Install curl and bash
RUN apk --no-cache add curl bash

# Copy the script into the container
COPY test_api.sh /usr/local/bin/test_api.sh

# Make the script executable
RUN chmod +x /usr/local/bin/test_api.sh

# Specify the script to run when the container starts
CMD ["/usr/local/bin/test_api.sh"]
