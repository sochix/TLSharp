#!/usr/bin/env bash

export VERSION=`cat ./VERSION`

cd "TLSharp.Core"

dotnet pack -c Release