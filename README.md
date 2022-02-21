# Credentials for GMail

The username is your e-mail, not an alias. It's recommended to enable 2FA on your account and use app password (https://support.google.com/mail/answer/185833).

# For developers

## Build local image and run the container

```
docker build -t restsmtp RestSMTP/
docker run -it --rm -p 8080:80 restsmtp
```
