pipeline {
    agent any
    environment {
        dotnet = 'C:\\Program Files (x86)\\dotnet\\dotnet.exe'
    }
    stages {
        stage('Build') {
            steps {
                bat 'dotnet build C:\\Users\\lucas\\AppData\\Local\\Jenkins\\.jenkins\\workspace\\Execução testes App Investimentos\\WEB\\Back\\src\\Investments.sln --configuration Release'
            }
        }
    }
}