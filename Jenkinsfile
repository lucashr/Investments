pipeline {

    agent any

    stages {
        stage('Run Tests') {
            steps {
                bat 'dotnet test --collect="XPlat Code Coverage"'
            }
        }
    }

}