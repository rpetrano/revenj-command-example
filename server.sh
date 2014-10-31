#!/bin/sh

cd "$(dirname "$0")"
exec mono server/bin/Revenj.Http.exe
