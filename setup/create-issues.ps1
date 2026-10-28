<#
.SYNOPSIS
    Creates the starter labels and issues for the Advanced GitHub Copilot course in your copy of Globomantics Shipping.

.DESCRIPTION
    Uses the GitHub CLI (gh). Run it from anywhere inside your clone, or pass -Repo owner/name.
    It's safe to run more than once: issues that already exist (same title) are skipped.

.EXAMPLE
    ./setup/create-issues.ps1

.EXAMPLE
    ./setup/create-issues.ps1 -Repo your-name/globomantics-shipping
#>
param(
    [string] $Repo
)

$ErrorActionPreference = 'Stop'

if (-not (Get-Command gh -ErrorAction SilentlyContinue)) {
    throw 'The GitHub CLI (gh) is required. Install it from https://cli.github.com and run "gh auth login".'
}

gh auth status *> $null
if ($LASTEXITCODE -ne 0) {
    throw 'You are not signed in to the GitHub CLI. Run "gh auth login" and try again.'
}

if (-not $Repo) {
    $Repo = gh repo view --json nameWithOwner --jq .nameWithOwner
    if ($LASTEXITCODE -ne 0 -or -not $Repo) {
        throw 'Could not work out which repository to use. Run this from inside your clone, or pass -Repo owner/name.'
    }
}

Write-Host "Setting up issues in $Repo"

# Forks have issues turned off by default.
$hasIssues = gh repo view $Repo --json hasIssuesEnabled --jq .hasIssuesEnabled
if ($hasIssues -ne 'true') {
    Write-Host 'Turning on issues for the repository'
    gh repo edit $Repo --enable-issues | Out-Null
}

$setupDir = $PSScriptRoot

# Windows PowerShell 5.1 passes a JSON array through the pipeline as a single object,
# so read each file into a variable and enumerate it explicitly.
function Read-JsonList([string] $fileName) {
    $items = Get-Content (Join-Path $setupDir $fileName) -Raw | ConvertFrom-Json
    return @($items | ForEach-Object { $_ })
}

foreach ($label in (Read-JsonList 'labels.json')) {
    gh label create $label.name --repo $Repo --color $label.color --description $label.description --force | Out-Null
    Write-Host "  label   $($label.name)"
}

$existing = gh issue list --repo $Repo --state all --limit 500 --json title --jq '.[].title'

foreach ($issue in (Read-JsonList 'issues.json')) {
    if ($existing -contains $issue.title) {
        Write-Host "  skipped $($issue.title) (already exists)"
        continue
    }

    $arguments = @(
        'issue', 'create',
        '--repo', $Repo,
        '--title', $issue.title,
        '--body-file', (Join-Path $setupDir $issue.body)
    )
    foreach ($label in $issue.labels) {
        $arguments += @('--label', $label)
    }

    $url = gh @arguments
    Write-Host "  created $($issue.title) $url"
}

Write-Host 'Done.'
