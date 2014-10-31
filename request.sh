#!/bin/sh

cat "$0" | tail -n 8 | netcat localhost 8999 || echo 'Please start your Revenj server.' >&2 && echo
exit

POST /RestApplication.svc/RevenjCommandExample HTTP/1.1
Host: localhost:8999
Content-Type: application/json
Accept: application/json
Content-Length: 38

{ "OrderURI1": "1", "OrderURI2": "2" }

