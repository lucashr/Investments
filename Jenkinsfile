pipeline {
    agent any
    stages {
        stage('Test') {
            steps { 

                bat """
                    cd %WORKSPACE%\\WEB\\Back\\Investments.Test && dotnet coverage collect dotnet test --output .\\TestResults\\coverage.xml --output-format xml
                    
                """

                // bat 'dotnet-coverage collect -f xml -o .\\TestResults\\coverage.xml dotnet test'
                //--no-build --collect:"XPlat Code Coverage"
                // bat 'dotnet test --filter "DisplayName~WebScrapingFundsAndYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~RankOfTheBestFundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~FundYeldsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --filter "DisplayName~FundsServiceTest" %WORKSPACE%\\WEB\\Back\\Investments.Test'
                // bat 'dotnet test --no-build --collect:"XPlat Code Coverage" --filter "DisplayName~DetailedFundServiceTest" C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
                // bat 'dotnet build-server shutdown'
                // C:\Users\lucas\AppData\Local\Jenkins\.jenkins\workspace\DeployBack\WEB\Back\Investments.Test
                
                //C:\\Users\\lucas\\Desktop\\Repositorios\\Meus_repositorios\\Investments\\WEB\\Back\\Investments.Tests
                
                
            }
        }
        stage('Code Analysis') {
            steps {
                /*
                    Funcionou

                    bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.vscoveragexml.reportsPaths=.\\TestResults\\coverage.xml'
                    bat 'dotnet build C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
                    bat 'dotnet-coverage collect dotnet test -f xml -o .\\TestResults\\coverage.xml'
                    bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'

                */
                
                bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.vscoveragexml.reportsPaths=.\\TestResults\\coverage.xml'
                bat 'dotnet build C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
                // bat 'dotnet build C:\\Users\\lucas\\Desktop\\Repositorios\\Meus_repositorios\\Investments\\WEB\\Back\\Investments.Tests'
                bat 'dotnet-coverage collect dotnet test -f xml -o .\\TestResults\\coverage.xml'
                bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
            }

            
        }
    }
}