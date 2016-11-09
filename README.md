# iis-log-analyzer

The project uses paket and fake to build.
In order to run the project do the following:
 1. clone the repository local
 
 ```
  git clone https://github.com/mrpinkzh/iis-log-analyzer.git
 ```
 2. change to the newly created folder 'iis-log-analyzer' and execute the build script:
 
 ```
 .\build.bat
 ```
 3. host the content of the .\build-src folder in your IIS.
  * the easiest way is to open the solution IisLogAnalyzer.sln and press `Shift + F5`


More information about Fake: https://fsharp.github.io/FAKE/
More information about Paket: http://fsprojects.github.io/Paket/
