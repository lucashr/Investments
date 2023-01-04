pipeline {

    agent any

    environment {
        dotnet = 'C:\\Program Files (x86)\\dotnet'
    }

    stages {
        stage('Build') {
            steps {
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src\\Investments.sln --configuration Release'
            }
        }
    }

}