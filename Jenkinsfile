pipeline {
    agent any

    environment {
        XXXXX = 'VARIAVEL LOCAL JENKINS!'
    }

    stages {
        stage('Test') {
            steps { 
                
                echo 'VARIAVEL TESTE'
                echo "Valor variavel ${XXXXX}"
                echo "Do Windows ${VS_EXTENSIONS}"

                // bat 'coverlet %WORKSPACE%\\WEB\\Back\\Investments.Tests\\bin\\Debug\\net5.0\\Investments.Tests.dll --target %env.VS_EXTENSIONS%\\TestWindow\\vstest.console.exe --targetargs %WORKSPACE%\\WEB\\Back\\Investments.Tests\\bin\\Debug\\net5.0\\Investments.Tests.dll --format opencover -o %WORKSPACE%\\WEB\\Back\\Investments.Tests\\TestResults\\coverage.cobertura.xml'
                
            }
        }
        // stage('Code Analysis') {
        //     steps {
        //         /*
        //             Funcionou

        //             bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.vscoveragexml.reportsPaths=.\\TestResults\\coverage.xml'
        //             bat 'dotnet build C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\DeployBack\\WEB\\Back\\Investments.Test'
        //             bat 'dotnet-coverage collect dotnet test -f xml -o .\\TestResults\\coverage.xml'
        //             bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'

        //         */
                
        //         bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.opencover.reportsPaths=%WORKSPACE%\\WEB\\Back\\Investments.Tests\\TestResults\\coverage.cobertura.xml'
        //         bat 'dotnet build %WORKSPACE%\\WEB\\Back\\Investments.Test'
        //         // bat 'dotnet build C:\\Users\\lucas\\Desktop\\Repositorios\\Meus_repositorios\\Investments\\WEB\\Back\\Investments.Tests'
        //         // bat 'dotnet-coverage collect dotnet test -f xml -o .\\TestResults\\coverage.xml'
        //         bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
        //     }

            
        // }
    }
}

