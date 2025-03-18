/*
 * TimerManager.cs
 * 计时器管理器类
 * 该类负责游戏中的计时系统，记录和显示游戏持续时间
 * 包括计时启动、停止和时间格式化显示等功能
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 10; // 剩余时间，实际用作累计时间计数器
    bool timerIsRunning = false; // 计时器是否正在运行的标志
    public Text timeText; // 主界面时间显示文本
    public Text timeTextScreenFinish; // 结束界面时间显示文本
    public Text BestTime; // 最佳时间显示文本
    
    private void Start()
    {
        // 游戏开始时自动启动计时器
        timerIsRunning = true;
    }
    
    void Update()
    {
        // 将主界面的时间同步到结束界面和最佳时间显示
        timeTextScreenFinish.text = timeText.text;
        BestTime.text = timeText.text;
        
        // 如果计时器正在运行
        if (timerIsRunning)
        {
            if (timeRemaining > -1) // 时间大于-1时继续计时
            {
                timeRemaining += Time.deltaTime; // 累加时间
                DisplayTime(timeRemaining); // 更新显示
            }
            else
            {
                // 注释掉的部分可能是计时结束时的处理逻辑
                //Debug.Log("Time has run out!");
                //timeRemaining = 0;
                //timerIsRunning = false;
            }
        }
    }
    
    // 格式化并显示时间（分:秒）
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1; // 加1秒，可能是为了显示效果
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); // 计算分钟数
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // 计算秒数
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 格式化为 "00:00" 形式
    }

    // 启动计时器
    public void StartTime()
    {
        timerIsRunning = true; // 设置计时器运行标志为true
        Debug.Log("StartTime");
    }
    
    // 停止计时器
    public void StopTime()
    {
        timerIsRunning = false; // 设置计时器运行标志为false
        Debug.Log("StopTime");
    }
}

