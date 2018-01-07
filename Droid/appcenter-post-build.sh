#!/bin/bash
echo "-= Build nUnit test projects: =-"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*\RefApp\.NUnitTest\.csproj' -exec msbuild {} \;
echo

echo "-= Found projects to run nUnit tests: =-"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*bin.*\RefApp\.NUnitTest\.dll' -exec echo {} \;
echo

echo "-= Running nUnit tests: =-"
find $APPCENTER_SOURCE_DIRECTORY -regex '.*bin.*\RefApp\.NUnitTest\.dll' -exec nunit-console {} \;
echo

echo "-= nUnit test result: =-"
pathOfTestResults=$(find $APPCENTER_SOURCE_DIRECTORY -name 'TestResult.xml')
cat $pathOfTestResults

#look for a failing test
grep -q 'success="False"' $pathOfTestResults

if [[ $? -eq 0 ]]
    then echo "a test Failed" exit 1
    else echo "all tests passed" exit 0
fi
