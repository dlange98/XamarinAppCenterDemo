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
find $APPCENTER_SOURCE_DIRECTORY -name 'TestResult.xml' -exec cat {} \;
exit 1
