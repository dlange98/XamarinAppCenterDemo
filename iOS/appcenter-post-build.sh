echo "-= Build nUnit test projects: =-"
find . -regex '.*\RefApp\.NUnitTest\.csproj' -exec msbuild {} \;
echo
echo "-= Found projects to run nUnit tests: =-"
find . -regex '.*bin.*\RefApp\.NUnitTest\.dll' -exec echo {} \;
echo
echo "-= Running nUnit tests: =-"
find . -regex '.*bin.*\RefApp\.NUnitTest\.dll' -exec nunit-console {} \;
echo
echo "-= nUnit test result: =-"
find . -name 'TestResult.xml' -exec cat {} \;
