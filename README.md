# Instruction

## Run the container

### Using docker run

```

```

### Using docker-compose:

```
version: '2'

services:
  restsmtp:
    container_name: restsmtp
    image: chrisrkw/restsmtp
    restart: unless-stopped
    environment:
      - "SMTP:Host=..."
      - "SMTP:Port=..."
      - "SMTP:SSL=..."
      - "SMTP:Username=..."
      - "SMTP:Password=..."
    ports:
      - 80:80
```

# How to send an e-mail

Send POST request with the payload:

```
{
    "FromName": "",
    "FromEmail": "",
    "To": "",
    "ReplyTo": "",
    "Subject": "",
    "Content": "",
    "IsContentHTML": true
}
```

# TODOs (Accepting pull requests)

- quotas
- priority
- usage (influx)

# Configuration for GMail

- SMTP:Host=smtp.gmail.com
- SMTP:Username=Username is your e-mail (not an alias)
- SMTP:Host=If you account is secured by 2FA use app password (https://support.google.com/mail/answer/185833), otherwise your account password.

Port (587) and SSL (true) are default, omit them.

# Developer tips

```
docker build --pull --no-cache -t restsmtp RestSMTP/
docker run -it --rm -p 8080:80 restsmtp
docker run --rm -it --entrypoint=/bin/bash restsmtp
```
