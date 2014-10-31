#!/bin/sh

cd "$(dirname $0)"
exec java -jar dsl-clc.jar -properties=dsl-clc.properties
