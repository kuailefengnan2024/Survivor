#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
pull.py
用于从Git仓库拉取最新代码的Python脚本
"""

import os
import sys
import subprocess
import datetime
import argparse
from colorama import Fore, Style, init

# 初始化colorama
init(autoreset=True)

def run_command(command):
    """运行命令并返回结果"""
    try:
        result = subprocess.run(command, shell=True, check=True, text=True, 
                              stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        return result.stdout
    except subprocess.CalledProcessError as e:
        print(f"{Fore.RED}命令执行失败: {e}")
        print(f"{Fore.RED}错误输出: {e.stderr}")
        sys.exit(1)

def has_changes():
    """检查是否有未提交的更改"""
    result = subprocess.run("git status --porcelain", shell=True, text=True, 
                         stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    return bool(result.stdout.strip())

def main():
    # 设置命令行参数
    parser = argparse.ArgumentParser(description="从Git仓库拉取最新代码")
    parser.add_argument("-b", "--branch", default="main", help="要拉取的分支名 (默认: main)")
    parser.add_argument("-s", "--stash", action="store_true", help="是否需要暂存当前更改")
    args = parser.parse_args()
    
    # 显示脚本开始信息
    print(f"{Fore.CYAN}开始执行拉取操作...\n")
    
    # 显示当前Git状态
    print(f"{Fore.YELLOW}当前Git状态:")
    status_output = run_command("git status")
    print(status_output)
    
    # 检查是否有未提交的更改
    has_uncommitted_changes = has_changes()
    stashed = False
    
    # 如果有未提交的更改并且启用了stash选项
    if has_uncommitted_changes and args.stash:
        # 提示用户确认
        confirmation = input(f"\n{Fore.YELLOW}检测到未提交的更改，是否暂存? (Y/N): ")
        if confirmation.lower() == 'y':
            print(f"\n{Fore.YELLOW}暂存当前更改...")
            run_command("git stash")
            stashed = True
            print(f"{Fore.GREEN}更改已暂存")
        else:
            print(f"{Fore.YELLOW}继续拉取操作但不暂存更改...")
    elif has_uncommitted_changes:
        print(f"\n{Fore.RED}警告: 存在未提交的更改，拉取可能会导致冲突。")
        confirmation = input("是否继续? (Y/N): ")
        if confirmation.lower() != 'y':
            print(f"{Fore.RED}操作已取消")
            sys.exit(0)
    
    # 拉取最新代码
    print(f"\n{Fore.YELLOW}从远程仓库拉取最新代码...")
    run_command(f"git pull origin {args.branch}")
    
    # 如果之前暂存了更改，现在恢复
    if stashed:
        confirmation = input(f"\n{Fore.YELLOW}是否恢复暂存的更改? (Y/N): ")
        if confirmation.lower() == 'y':
            print(f"\n{Fore.YELLOW}恢复暂存的更改...")
            run_command("git stash pop")
            print(f"{Fore.GREEN}更改已恢复")
        else:
            print(f"{Fore.YELLOW}暂存的更改未恢复，可以使用 'git stash pop' 手动恢复")
    
    # 完成
    current_time = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    print(f"\n{Fore.GREEN}拉取操作完成!")
    print(f"{Fore.GREEN}分支: {args.branch}")
    print(f"{Fore.GREEN}时间: {current_time}")
    
    # 显示最新提交记录
    print(f"\n{Fore.YELLOW}最新提交记录:")
    log_output = run_command('git log -3 --pretty=format:"%h - %an, %ar : %s"')
    print(log_output)

if __name__ == "__main__":
    main() 