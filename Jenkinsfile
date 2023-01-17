pipeline {
    agent any
    stages {
        stage('Code Analysis') {
            steps {
                bat 'dotnet C:\\Sonar-scanner\\SonarScanner.MSBuild.dll begin /k:"DeployBack" /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src"'
                bat 'dotnet C:\\Sonar-scanner\\SonarScanner.MSBuild.dll end /d:sonar.login="f4f2f069bc50fc86e24ecdc8343f0fd7b0239da4"'
            }
        }
    }
}