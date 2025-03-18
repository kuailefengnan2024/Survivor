# pull.ps1
# 用于从Git仓库拉取最新代码的PowerShell脚本

param (
    [Parameter(Mandatory=$false)]
    [string]$branch = "main",  # 默认分支为main
    
    [Parameter(Mandatory=$false)]
    [switch]$stash = $false    # 是否需要暂存当前更改
)

# 显示脚本开始信息
Write-Host "开始执行拉取操作..." -ForegroundColor Cyan

# 显示当前Git状态
Write-Host "`n当前Git状态:" -ForegroundColor Yellow
git status

# 检查是否有未提交的更改
$hasChanges = (git status --porcelain) -ne $null

# 如果有未提交的更改并且启用了stash选项
if ($hasChanges -and $stash) {
    # 提示用户确认
    $confirmation = Read-Host "`n检测到未提交的更改，是否暂存? (Y/N)"
    if ($confirmation -eq 'Y' -or $confirmation -eq 'y') {
        Write-Host "`n暂存当前更改..." -ForegroundColor Yellow
        git stash
        Write-Host "更改已暂存" -ForegroundColor Green
    } else {
        Write-Host "继续拉取操作但不暂存更改..." -ForegroundColor Yellow
    }
} elseif ($hasChanges) {
    Write-Host "`n警告: 存在未提交的更改，拉取可能会导致冲突。" -ForegroundColor Red
    $confirmation = Read-Host "是否继续? (Y/N)"
    if ($confirmation -ne 'Y' -and $confirmation -ne 'y') {
        Write-Host "操作已取消" -ForegroundColor Red
        exit
    }
}

# 拉取最新代码
Write-Host "`n从远程仓库拉取最新代码..." -ForegroundColor Yellow
git pull origin $branch

# 如果之前暂存了更改，现在恢复
if ($hasChanges -and $stash) {
    $confirmation = Read-Host "`n是否恢复暂存的更改? (Y/N)"
    if ($confirmation -eq 'Y' -or $confirmation -eq 'y') {
        Write-Host "`n恢复暂存的更改..." -ForegroundColor Yellow
        git stash pop
        Write-Host "更改已恢复" -ForegroundColor Green
    } else {
        Write-Host "暂存的更改未恢复，可以使用 'git stash pop' 手动恢复" -ForegroundColor Yellow
    }
}

# 完成
Write-Host "`n拉取操作完成!" -ForegroundColor Green
Write-Host "分支: $branch" -ForegroundColor Green
Write-Host "时间: $(Get-Date)" -ForegroundColor Green

# 显示最新提交记录
Write-Host "`n最新提交记录:" -ForegroundColor Yellow
git log -3 --pretty=format:"%h - %an, %ar : %s" 