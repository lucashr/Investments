pipeline {

    agent any

    stages {
        stage('Build') {
            steps {
                bat 'C:\\Users\\lucas\\Desktop\\Repositorios\\Meus repositorios\\Investments\\WEB\\Back\\src'
                bat 'dotnet build'
            }
        }
        stage('Run Tests') {
            steps {
                bat 'C:\\Users\\lucas\\Desktop\\Repositorios\\Meus repositorios\\Investments\\WEB\\Back\\Investments.Test'
                bat 'dotnet test --collect="XPlat Code Coverage"'
            }
        }
    }

}