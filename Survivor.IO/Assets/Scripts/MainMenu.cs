/*
 * MainMenu.cs
 * 
 * 这个脚本用于控制游戏主菜单的功能
 * 负责管理设置面板和消息面板的显示与隐藏
 * 
 * 包含的主要功能:
 * - 打开和关闭设置面板
 * - 打开和关闭消息面板
 * 
 * 这是一个基础的UI控制脚本，挂载在主菜单对象上
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // MSettings: 设置面板的GameObject引用，用于控制其显示和隐藏
    public GameObject MSettings;
    // MMessages: 消息面板的GameObject引用，用于控制其显示和隐藏
    public GameObject MMessages;

    // Start方法在脚本启用时被调用一次
    void Start()
    {
        // 初始化函数，目前为空，可以在这里添加初始化逻辑
    }
    
    // Update方法在每一帧被调用
    void Update()
    {
        // 每帧更新函数，目前为空，可以在这里添加需要持续检测的逻辑
    }
    
    // OpenSetting方法：打开设置面板，通常绑定到UI按钮上
    public void OpenSetting()
    {
        // 通过设置GameObject的active属性为true来显示设置面板
        MSettings.SetActive(true);
    }
    
    // OpenMessages方法：打开消息面板，通常绑定到UI按钮上
    public void OpenMessages()
    {
        // 通过设置GameObject的active属性为true来显示消息面板
        MMessages.SetActive(true);
    }
    
    // ExitSetting方法：关闭设置面板，通常绑定到"返回"或"关闭"按钮上
    public void ExitSetting()
    {
        // 通过设置GameObject的active属性为false来隐藏设置面板
        MSettings.SetActive(false);
    }
    
    // ExitMessages方法：关闭消息面板，通常绑定到"返回"或"关闭"按钮上
    public void ExitMessages()
    {
        // 通过设置GameObject的active属性为false来隐藏消息面板
        MMessages.SetActive(false);
    }
}
