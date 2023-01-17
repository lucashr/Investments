pipeline {
    agent any
    stages {
        stage('Code Analysis') {
            steps {
                bat 'dotnet sonarscanner begin /d:sonar.verbose="true" /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src'
                bat 'dotnet sonarscanner end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
            }
        }
    }
}