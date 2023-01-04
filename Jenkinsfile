pipeline {

    agent any

    stages {
        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }
        stage('Run Tests') {
            steps {
                bat returnStdout: true, script: '''cd /d "C:\\Users\\lucas\\Desktop\\Repositorios\\Meus repositorios\\Investments\\WEB\\Back\\Investimentos.Test" dotnet test --collect="XPlat Code Coverage"'''
            }
        }
    }

}