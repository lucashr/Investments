pipeline {
    agent any
    stages {
        stage('Test') {
            steps {
                //--no-build --collect:"XPlat Code Coverage"
                // bat 'dotnet test --filter "DisplayName~WebScrapingFundsAndYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~RankOfTheBestFundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~FundYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~FundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --no-build --collect:"XPlat Code Coverage" --filter "DisplayName~DetailedFundServiceTest" C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
                // bat 'dotnet build-server shutdown'
                // C:\Users\lucas\AppData\Local\Jenkins\.jenkins\workspace\DeployBack\WEB\Back\Investments.Test
                cd "%WORKSPACE%\\WEB\\Back\\Investments.Test"
                bat 'dotnet-coverage collect --session-id serverdemo -f xml -o .\\TestResults\\coverage.xml "dotnet test"'
            }
        }
        stage('Code Analysis') {
            steps {
                bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.vscoveragexml.reportsPaths=.\\TestResults\\coverage.xml'
                bat 'dotnet build C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
                bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
                // bat 'reportgenerator "-reports:.\\TestResults\\coverage.cobertura.xml" "-targetdir:sonarqubecoverage" "-reporttypes:SonarQube"'
                ///d:sonar.cs.coverageReportPaths.reportsPaths=".\\sonarqubecoverage\\SonarQube.xml
                // bat 'coverlet C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test\\bin\\Debug\\net5.0\\Investments.Tests.dll'
                // bat 'coverlet %WORKSPACE%\\WEB\\Back\\Investments.Test\\bin\\Debug\\net5.0\\Investments.Tests.dll --target "dotnet" --targetargs "test --no-build" -f=opencover -o="coverage.xml"'
            }

            
        }
    }
}