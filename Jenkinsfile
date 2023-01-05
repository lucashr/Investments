pipeline {
    agent any
    environment {
        dotnet = 'C:\\Program Files (x86)\\dotnet\\dotnet.exe'
    }
    stages {
        stage('Build') {
            steps {
                bat 'dotnet build %WORKSPACE%\\WEB\\Back\\src\\Investments.sln'
            }
        }
    }
}