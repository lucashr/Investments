pipeline {
    agent any
    stages {
        stage('Test') {
            steps {
                // bat 'dotnet test --filter "DisplayName~WebScrapingFundsAndYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~RankOfTheBestFundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~FundYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~FundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --filter "DisplayName~DetailedFundServiceTest" C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
                bat 'dotnet build-server shutdown'
            }
        }
        stage('Code Analysis') {
            steps {
                bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.opencover.reportsPaths=coverage.xml /d:sonar.coverage.exclusions="**Test*.cs"'
                bat 'dotnet build --no-incremental %WORKSPACE%\\WEB\\Back\\src'
                // bat 'coverlet %WORKSPACE%\\WEB\\Back\\Investments.Test\\bin\\Debug\\net5.0\\Investments.Tests.dll --target "dotnet" --targetargs "test --no-build" -f=opencover -o="coverage.xml"'
                bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
            }
        }
    }
}