pipeline
{
    agent any

    stages
    {
        stage('Checkout')
        {
            steps
            {
                git branch: 'main',
                url: 'https://github.com/anushaxoriant/DesktopAutomationFramework'
            }
        }

        stage('Restore')
        {
            steps
            {
                bat 'dotnet restore'
            }
        }

        stage('Build')
        {
            steps
            {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Execute Tests')
{
    steps
    {
        {
            bat 'dotnet test --logger trx'
        }
    }
}

        stage('Generate Allure Report')
{
    steps
    {
        bat 'allure generate ./bin/Debug/net8.0-windows/allure-results --clean -o allure-report'
    }
}
    }

    post
    {
        always
        {
            allure(
                includeProperties: false,
                jdk: '',
                results: [[path:'bin/Debug/net8.0-windows/allure-results']],
				commandline: 'Allure'
            )
        }
    }
}