pipeline {
    agent any

    environment {
        COVERAGE_PATH = "%WORKSPACE%\\WEB\\Back\\Investments.Tests\\TestResults\\"
        DLL_PATH_PROJECT = "%WORKSPACE%\\WEB\\Back\\Investments.Tests\\bin\\Debug\\net5.0\\Investments.Tests.dll"
        VS_EXTENSIONS = "C:\\development\\Visual_Studio_Test\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe"
    }

    stages {
        stage('Build') {
            steps { 
                
                bat "dotnet build %WORKSPACE%\\WEB\\Back\\src"
                bat "dotnet build %WORKSPACE%\\WEB\\Back\\Investments.Tests"
                
            }
        }
        stage('Test') {
            steps { 

                bat "coverlet ${DLL_PATH_PROJECT} --target ${VS_EXTENSIONS} --targetargs ${DLL_PATH_PROJECT} --format opencover -o coverage.cobertura.xml"
                
            }
        }
        stage('Code Analysis') {
            steps {
                bat "dotnet sonarscanner begin /k:DeployBack /d:sonar.login=42c7a0267ad08995d92948e337b1da34476b3e2d /d:sonar.exclusions=**src/test/**/*.* /d:sonar.cs.opencover.reportsPaths=${COVERAGE_PATH}coverage.cobertura.xml"
                bat "dotnet build %WORKSPACE%\\WEB\\Back\\Investments.Tests"
                bat "dotnet sonarscanner end /d:sonar.login=42c7a0267ad08995d92948e337b1da34476b3e2d"
            }

            
        }
    }
}

