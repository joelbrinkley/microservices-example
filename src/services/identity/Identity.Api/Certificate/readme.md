openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -days 365

openssl pkcs12 -export -out microexample-identity.pfx -inkey key.pem -in cert.pem