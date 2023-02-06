pipeline {
    agent any

    environment {
        COVERAGE_PATH = "%WORKSPACE%\\WEB\\Back\\Investments.Tests\\TestResults\\coverage.cobertura.xml"
        DLL_PATH_PROJECT = "%WORKSPACE%\\WEB\\Back\\Investments.Tests\\bin\\Debug\\net5.0\\Investments.Tests.dll"
        VS_EXTENSIONS = "C:\\development\\Visual_Studio_Test\\Common7\\IDE\\CommonExtensions\\Microsoft\\TestWindow\\vstest.console.exe"
    }

    stages {
        stage('Test') {
            steps { 

                bat "coverlet ${DLL_PATH_PROJECT} --target ${VS_EXTENSIONS} --targetargs ${DLL_PATH_PROJECT} --format opencover -o ${COVERAGE_PATH}"
                
            }
        }
        stage('Code Analysis') {
            steps {
                bat "dotnet sonarscanner begin /k:'DeployBack' /d:sonar.login='f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4' /d:sonar.cs.opencover.reportsPaths=${COVERAGE_PATH}"
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\Investments.Test'
                bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
            }

            
        }
    }
}

