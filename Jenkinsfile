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
                bat 'dotnet test --collect="XPlat Code Coverage"'
            }
        }
    }

}