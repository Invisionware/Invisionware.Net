# Cakebuild-Starter
Provide a starting framework for using [CakeBuild](http://cakebuild.net) as your automated build scripting environment.

| File | Purpose |
| ---- | ------- |
| .gitignore | Standard Visual Studio git ignore file |
| .tfignore | Standard Visual Studio TFS ignore file |
| build.cake | Actual cake build script (Details below) |
| build.ps1 | Poweshell script to start automated build on Windows |
| build.osx.sh | Shell Script for OSX to start auotmated build |
| build.sh | Shell Script for Linux to start automated build |
| LICENSE | Standard License Agreement |
| settings.json | JSON file for handling settings for automated build (Details below) |
| version.json | JSON file for handling version for automated build (Details below) |
| src/AssembyInfo.Shared.cs | Sample Shared AssemblyInfo file (Details below |)

# build.cake

# build.ps1
Usage:
* .\build.ps1 -Target <Task> -Configuration=<Debug|Release> -skipBuild=<true|false> -skipUnitTest=<true|false> -skipPackage=<true|false>
  * Task: Which cakebuild task to execute (Build|Clean|CleanAll|UnitTest|Package|Publish|DisplayHelp) [Default: DisplayHelp]
  * Configuration: Build Configuration (Debug|Release) [Default: Release]
  * skipBuild: Allows for skipping the build process (useful if you want to package or test without building) [Default: false]
  * skipUnitTest: Allows for skipping the uit testing  process (useful if you want to build without runnign unit tests) [Default: false]
  * skipPackage: Allows for skipping the packaging process (useful if you want to publish without repackaging ) [Default: false]

# settings.json
Contains all of the various settings for the build/test/packaging.  By default this is setup for 99% of standard projects

# version.json
Contains information for the current version information

# AssemblyInfo.Shared.cs
A sample AssemblyInfo.cs that can be included in all of your projects to handle consolidating assembly details into a single location
Note: The leverage this, you'll need to edit your existing AssemblyInfo.cs files and comment out the following attributes
* [assembly: AssemblyCompany("")]
* [assembly: AssemblyProduct("")]
* [assembly: AssemblyCopyright("")]
* [assembly: AssemblyTrademark("")]
* [assembly: AssemblyConfiguration("")]
* [assembly: AssemblyVersion("1.0.0")]
