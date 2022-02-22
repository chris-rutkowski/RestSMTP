# How to run the image

# TODOs (Accepting pull requests)

- quotas
- priority
- usage (influx)

# Information for GMail

- Username is your e-mail (not an alias)
- If you account is secured by 2FA use app password (https://support.google.com/mail/answer/185833), otherwise your account password.
- Host=smtp.gmail.com
- Port=587 (default)
- SSL=true (default)

# For developers

## Build local image and run the container

```
docker build -t restsmtp RestSMTP/
docker run -it --rm -p 8080:80 restsmtp
```
