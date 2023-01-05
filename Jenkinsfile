pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src\\Investments.sln'
            }
        }
    }
}