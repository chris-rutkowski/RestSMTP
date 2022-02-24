# About

Developed by [Chris Rutkowski](https://rkw.ski)

Dockerised .NET6 application to send e-mails via post request (REST).

# Know-how

## Run the container

### with docker run CLI

```
docker run -d -p 80:80 --name restsmtp --restart unless-stopped \
  -e "SMTP:Host=TYPE_HOST" \
  -e "SMTP:Port=TYPE_PORT" \
  -e "SMTP:SSL=true/false" \
  -e "SMTP:Username=TYPE_USERNAME" \
  -e "SMTP:Password=TYPE_PASSWORD" \
  chrisrkw/restsmtp:latest
```

### with docker-compose:

```
version: '2'

services:
  restsmtp:
    container_name: restsmtp
    image: chrisrkw/restsmtp
    restart: unless-stopped
    environment:
      - "SMTP:Host=TYPE_HOST"
      - "SMTP:Port=TYPE_PORT"
      - "SMTP:SSL=true/false"
      - "SMTP:Username=TYPE_USERNAME"
      - "SMTP:Password=TYPE_PASSWORD"
    ports:
      - 80:80
```

### port/network hints

- if port 80 on your host is busy, use any other port e.g. `12345:80`
- if you use reverse proxy or other facade it may be useful to listen on localhost only `127.0.0.1:12345:80`, or skip exposing ports and use docker networks

### Configuration for GMail

- Host is `smtp.gmail.com`
- Username is your full e-mail address (not an alias)
- Password is your account password. If you use 2FA (you should) use app password instead (https://support.google.com/mail/answer/185833).

Port (587) and SSL (true) are default, omit them.

## REST

`SERVER` in the instruction below can be either `localhost` (with default port 80), or the different port you've exposed e.g. `localhost:12345`, or your network e.g. `restsmtp-network`

### Send e-mail

Send a `POST` request to `http://SERVER`, with the following JSON request body example:

```
{
    "FromName": "J. Smith",
    "FromEmail": "jamessmith@example.com",
    "To": robertbrown@example.com,
    "ReplyTo": null,
    "Subject": "Example email",
    "Content": "<h1>Hi Robert!</h1><p>Lorem ipsum...</p>",
    "IsContentHTML": true
}
```

#### JSON parameters:

- `FromName` - optional, no validation
- `FromEmail` - required, must be a valid e-mail
- `To` - required, must be a valid e-mail
- `ReplyTo` - optional, must be a valid e-mail if provided
- `Subject` - required, must be at least one non-whitespace character
- `Content` - optional, but email should have some content, shouldn't it?
- `IsContentHTML` - `true` or `false` (default `false`)

#### Responses:

- 204: success
- 400: validation error
- 500: couldn't send email (check logs)

### Ping

Send a `GET` request to `http://SERVER`, you will receive the JSON response similar to the one below:

```
{
    "name": "RestSMTP",
    "version": "1.0.4"
}
```

# TODOs (Accepting pull requests)

- de-duplication
- priority, quotas (although it should be a separate mail balancer service)
- usage (e.g. influx)

# Some commands useful for local development

```
docker build --pull --no-cache -t restsmtp RestSMTP/
docker run -it --rm -p 8080:80 restsmtp
docker run --rm -it --entrypoint=/bin/bash restsmtp
```
