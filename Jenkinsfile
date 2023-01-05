pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src\\Investments.sln'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test --filter "DisplayName~WebScrapingFundsAndYeldsServiceTest" --collect="XPlat Code Coverage" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~RankOfTheBestFundsServiceTest" --collect="XPlat Code Coverage" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~FundYeldsServiceTest" --collect="XPlat Code Coverage" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~FundsServiceTest" --collect="XPlat Code Coverage" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~DetailedFundServiceTest" --collect="XPlat Code Coverage" %WORKSPACE%\\WEB\\Back\\Investments.Test'
            }
        }
    }
}