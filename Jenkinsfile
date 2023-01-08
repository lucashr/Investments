pipeline {
    agent any
    stages {
        stage('Restore packages') {
            steps {
                bat "dotnet restore %WORKSPACE%\\WEB\\Back\\src\\Investments.sln"
            }
        }
        stage('Clean') {
            steps {
                bat "dotnet clean %WORKSPACE%\\WEB\\Back\\src\\Investments.sln"
            }
        }
        stage('Build') {
            steps {
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src\\Investments.sln --configuration Release'
            }
        }
        stage('Test') {
            steps {
                bat 'dotnet test --filter "DisplayName~WebScrapingFundsAndYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~RankOfTheBestFundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~FundYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~FundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --filter "DisplayName~DetailedFundServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
            }
        }
        stage('Release') {
            steps {
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src\\Investments.sln /p:Platform="Any CPU" /p:DeployOnBuild=true /m'
            }
        }
    }
}