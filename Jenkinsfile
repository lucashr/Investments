pipeline {

    agent any

    stages {
        stage('Build') {
            steps {
                bat 'WEB\\Back\\src'
                bat 'dotnet build'
            }
        }
        stage('Run Tests') {
            steps {
                bat 'WEB\\Back\\Investments.Test'
                bat 'dotnet test --collect="XPlat Code Coverage"'
            }
        }
    }

}