#!/bin/bash

for i in "$@"
do
case $i in
    -v=*|--version=*)
    BUILDVERSION="${i#*=}"
    shift # past argument=value
    ;;
    -c=*|--configuration=*)
    CONFIGURATION="${i#*=}"
    shift # past argument=value
    ;;
    --pack)
    PACK="YES"
    shift # past argument with no value
    ;;
    *)
            # unknown option
    ;;
esac
done

echo "BUILD VERSION = ${BUILDVERSION}"
echo "CONFIGURATION = ${CONFIGURATION}"
echo "PACK          = ${PACK}"

if [ -z ${CONFIGURATION+x} ]
then
    echo "No configuration is specified, defaulting to DEBUG"
    CONFIGURATION=DEBUG
fi

if [ -z ${BUILDVERSION+x} ]
then
    echo "No build version is specified, defaulting to 0.0.0"
    BUILDVERSION=0.0.0
fi

scriptsDir=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
testProjectRootDirectories=(${scriptsDir%%/}/tests/*/)

dotnet restore $scriptsDir || exit 1
dotnet clean $scriptsDir --configuration $CONFIGURATION --verbosity normal || exit 1
dotnet build $scriptsDir --configuration $CONFIGURATION --verbosity normal --version-suffix '' /property:VersionPrefix=$BUILDVERSION || exit 1

if [ -z ${PACK+x} ]
then
    echo "Pack is disabled. Skipping pack for the solution"
else
    echo "starting to pack for $scriptsDir"
    dotnet pack --configuration $CONFIGURATION --version-suffix '' /property:VersionPrefix=$BUILDVERSION --no-build || exit 1
fi