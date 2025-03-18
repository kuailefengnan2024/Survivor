/*
 * BooleanManager.cs
 * 布尔值管理器类
 * 这个类负责管理游戏中的全局布尔值状态，包括游戏是否开始、音乐和音效开关等设置
 * 作为一个中心化的布尔状态控制器，供其他组件引用
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanManager : MonoBehaviour
{
    public bool GameStart = false; // 游戏是否已开始的标志
    public bool Music = true; // 背景音乐开关状态
    internal bool Sound = true; // 音效开关状态
    internal bool Vibration = true; // 振动开关状态
    
    private void Update()
    {
        if(GameStart == true)
        {
          // 游戏开始后的逻辑，目前为空
        }
    }
    
    // 控制声音设置的方法
    public void SoundM()
    {
        if (Music == false)
        {
            // 如果音乐当前是关闭的，则打开音乐和音效
            Music = true;
            Sound = true;
        }
        else
        {
            // 如果音乐当前是打开的，则关闭音乐和音效
            Music = false;
            Sound = false;
        }
    }
}
