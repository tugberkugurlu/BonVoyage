#!/bin/bash

for i in "$@"
do
case $i in
    -k=*|--apikey=*)
    APIKEY="${i#*=}"
    shift # past argument=value
    ;;
    -p=*|--packagePath=*)
    PACKAGEPATH="${i#*=}"
    shift # past argument=value
    ;;
    -v=*|--version=*)
    VERSION="${i#*=}"
    shift # past argument=value
    ;;
    *)
            # unknown option
    ;;
esac
done

if [ -z ${APIKEY+x} ]
then
    echo "Error: ApiKey needs to be specified through --apikey|-k parameter"
    exit 1
fi

if [ -z ${PACKAGEPATH+x} ]
then
    echo "Error: PackagePath needs to be specified through --packagePath|-p parameter"
    exit 1
fi

if [ -z ${VERSION+x} ]
then
    echo "Error: Version needs to be specified through --version|-v parameter"
    exit 1
fi

echo "pushing $packagePath to NuGet.org..."
dotnet nuget push $PACKAGEPATH --api-key $APIKEY --source https://www.nuget.org/api/v2/package --force-english-output || exit 1