#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
commit.py
用于提交和推送代码到Git仓库的Python脚本
"""

import os
import sys
import subprocess
import datetime
import argparse
from colorama import Fore, Style, init

# 初始化colorama
init(autoreset=True)

# 设置环境变量以使用UTF-8编码
os.environ["PYTHONIOENCODING"] = "utf-8"

def run_command(command):
    """运行命令并返回结果"""
    try:
        # 设置环境变量使用UTF-8编码
        my_env = os.environ.copy()
        my_env["LANG"] = "en_US.UTF-8"
        
        result = subprocess.run(command, shell=True, check=True, text=True, 
                              stdout=subprocess.PIPE, stderr=subprocess.PIPE,
                              encoding='utf-8', errors='replace',
                              env=my_env)
        return result.stdout
    except subprocess.CalledProcessError as e:
        print(f"{Fore.RED}命令执行失败: {e}")
        print(f"{Fore.RED}错误输出: {e.stderr}")
        sys.exit(1)

def main():
    # 获取当前时间作为默认提交信息
    current_time = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
    
    # 设置命令行参数
    parser = argparse.ArgumentParser(description="提交并推送更改到Git仓库")
    parser.add_argument("commit_message", nargs='?', default=current_time, 
                      help="提交信息 (默认: 当前时间)")
    parser.add_argument("-b", "--branch", default="main", help="要推送到的分支名 (默认: main)")
    args = parser.parse_args()
    
    # 显示脚本开始信息
    print(f"{Fore.CYAN}开始执行提交和推送操作...\n")
    
    # 显示当前Git状态
    print(f"{Fore.YELLOW}当前Git状态:")
    status_output = run_command("git status")
    print(status_output)
    
    # 添加所有更改
    print(f"\n{Fore.YELLOW}添加所有更改...")
    run_command("git add .")
    
    # 提交更改
    print(f"\n{Fore.YELLOW}提交更改...")
    run_command(f'git commit -m "{args.commit_message}"')
    
    # 推送到远程仓库
    print(f"\n{Fore.YELLOW}推送到远程仓库...")
    run_command(f"git push origin {args.branch}")
    
    # 完成
    print(f"\n{Fore.GREEN}操作完成!")
    print(f"{Fore.GREEN}提交信息: {args.commit_message}")
    print(f"{Fore.GREEN}分支: {args.branch}")
    print(f"{Fore.GREEN}时间: {current_time}")
    
    # 显示最近的提交记录
    print(f"\n{Fore.YELLOW}最近的提交记录:")
    try:
        log_output = run_command('git log -3 --pretty=format:"%h - %an, %ar : %s"')
        print(log_output)
    except Exception as e:
        print(f"{Fore.RED}无法显示提交记录: {e}")
        print(f"{Fore.YELLOW}但提交和推送操作已完成。")

if __name__ == "__main__":
    main() 