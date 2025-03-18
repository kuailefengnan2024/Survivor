# Git Python 脚本使用说明

这两个Python脚本用于简化Git的提交推送和拉取操作，特别适合Unity项目的日常开发。

## 脚本文件

1. `commit.py` - 用于提交和推送代码到Git仓库
2. `pull.py` - 用于从Git仓库拉取最新代码

## 安装依赖

这两个脚本依赖于colorama库来实现彩色输出，使用前请先安装：

```bash
pip install colorama
```

## commit.py 使用方法

此脚本用于将当前更改提交并推送到远程仓库。

### 基本用法

```bash
python commit.py "提交信息"
```

这将执行以下操作：
1. 显示当前Git状态
2. 请求确认是否继续
3. 添加所有更改 (`git add .`)
4. 提交更改 (`git commit -m "提交信息"`)
5. 推送到远程仓库默认分支 (`git push origin main`)

### 指定分支

```bash
python commit.py "提交信息" -b develop
# 或
python commit.py "提交信息" --branch develop
```

这将推送到指定的分支，而不是默认的main分支。

## pull.py 使用方法

此脚本用于从远程仓库拉取最新代码。

### 基本用法

```bash
python pull.py
```

这将执行以下操作：
1. 显示当前Git状态
2. 如果有未提交的更改，会警告并请求确认
3. 从远程仓库拉取最新代码到当前分支 (`git pull origin main`)

### 指定分支

```bash
python pull.py -b develop
# 或
python pull.py --branch develop
```

这将从指定的分支拉取，而不是默认的main分支。

### 自动暂存更改

```bash
python pull.py -s
# 或
python pull.py --stash
```

使用 `-s` 或 `--stash` 参数时，脚本会在检测到未提交的更改时：
1. 询问是否暂存当前更改
2. 如果确认，暂存当前更改 (`git stash`)
3. 拉取最新代码
4. 询问是否恢复暂存的更改
5. 如果确认，恢复暂存的更改 (`git stash pop`)

## 使脚本可执行 (Linux/macOS)

在Linux或macOS系统上，您可以使脚本设置为可执行文件，这样就不需要每次都输入python：

```bash
chmod +x commit.py pull.py
```

然后可以直接运行：

```bash
./commit.py "提交信息"
./pull.py -s
```

## 注意事项

1. 这些脚本需要Python 3.6+版本
2. 确保已经安装了Git，并且能在命令行中使用
3. 使用脚本前请确保已配置好Git仓库的远程地址

## 示例场景

### 提交今天的工作

```bash
python commit.py "完成了角色动画和敌人生成功能"
```

### 开始工作前获取最新代码

```bash
python pull.py -s
```

### 切换到特定分支并拉取

```bash
git checkout feature-branch
python pull.py
``` 