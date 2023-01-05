pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                bat 'dotnet build %WORKSPACE%\\Execucao testes App Investments\\WEB\\Back\\src\\Investments.sln'
            }
        }
    }
}