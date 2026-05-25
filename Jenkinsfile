pipeline
{
    agent any

    stages
    {
        // =========================
        // CHECKOUT
        // =========================

        stage('Checkout')
        {
            steps
            {
                git branch: 'main',
                url: 'https://github.com/anushaxoriant/DesktopAutomationFramework'
            }
        }

        // =========================
        // CLEAN
        // =========================

        stage('Clean')
        {
            steps
            {
                bat 'dotnet clean'
            }
        }

        // =========================
        // RESTORE
        // =========================

        stage('Restore')
        {
            steps
            {
                bat 'dotnet restore'
            }
        }

        // =========================
        // BUILD
        // =========================

        stage('Build')
        {
            steps
            {
                bat 'dotnet build --no-restore'
            }
        }

        // =========================
        // POSITIVE TESTS
        // =========================

        stage('Execute Positive Tests')
        {
            steps
            {
                bat 'dotnet test --filter "Category!=P3" --logger trx'
            }
        }

        // =========================
        // NEGATIVE TESTS
        // =========================

        stage('Execute Negative Tests')
        {
            steps
            {
                bat 'dotnet test --filter "Category=P3" --logger trx'
            }
        }

        // =========================
        // GENERATE ALLURE REPORT
        // =========================

        stage('Generate Allure Report')
        {
            steps
            {
                bat 'allure generate ./bin/Debug/net8.0-windows/allure-results --clean -o allure-report'
            }
        }
    }

    // =========================
    // POST ACTIONS
    // =========================

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