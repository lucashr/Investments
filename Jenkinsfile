pipeline {
    agent any
    stages {
        stage('Code Analysis') {
            steps {
                bat 'dotnet sonarscanner begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4" /d:sonar.cs.opencover.reportsPaths=coverage.xml'
                bat 'dotnet build --no-incremental %WORKSPACE%\\WEB\\Back\\src coverlet %WORKSPACE%\\WEB\\Back\\Investments.Test --target "dotnet" --targetargs "test --no-build" -f=opencover -o="coverage.xml"'
                bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
            }
        }
    }
}