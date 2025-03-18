# commit.ps1
# 用于提交和推送代码到Git仓库的PowerShell脚本

param (
    [Parameter(Mandatory=$true, Position=0)]
    [string]$commitMessage,
    
    [Parameter(Mandatory=$false)]
    [string]$branch = "main"  # 默认分支为main
)

# 显示脚本开始信息
Write-Host "开始执行提交和推送操作..." -ForegroundColor Cyan

# 显示当前Git状态
Write-Host "`n当前Git状态:" -ForegroundColor Yellow
git status

# 提示用户确认
$confirmation = Read-Host "`n是否继续提交更改? (Y/N)"
if ($confirmation -ne 'Y' -and $confirmation -ne 'y') {
    Write-Host "操作已取消" -ForegroundColor Red
    exit
}

# 添加所有更改
Write-Host "`n添加所有更改..." -ForegroundColor Yellow
git add .

# 提交更改
Write-Host "`n提交更改..." -ForegroundColor Yellow
git commit -m "$commitMessage"

# 推送到远程仓库
Write-Host "`n推送到远程仓库..." -ForegroundColor Yellow
git push origin $branch

# 完成
Write-Host "`n操作完成!" -ForegroundColor Green
Write-Host "提交信息: $commitMessage" -ForegroundColor Green
Write-Host "分支: $branch" -ForegroundColor Green
Write-Host "时间: $(Get-Date)" -ForegroundColor Green

# 显示最近的提交记录
Write-Host "`n最近的提交记录:" -ForegroundColor Yellow
git log -3 --pretty=format:"%h - %an, %ar : %s" 