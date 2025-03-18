/*
 * FirsProcess.cs
 * 
 * 这个脚本负责管理游戏中单个关卡组的UI状态和交互。
 * 它跟踪每个关卡的完成状态，并控制相应的UI元素显示（完成/未完成图标和按钮交互性）。
 * 每个FirsProcess实例管理三个关卡，当关卡完成时会更新UI状态。
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirsProcess : MonoBehaviour
{
    [Header("Managers")]
    public ProcessManagerment Management; // 指向关卡进程管理器的引用

    [Header("GameObjects Done")]
    public GameObject Done1; // 第一关完成状态的UI图标
    public GameObject Done2; // 第二关完成状态的UI图标
    public GameObject Done3; // 第三关完成状态的UI图标

    [Header("GameObjestNotFinish")]
    public GameObject NotDone1; // 第一关未完成状态的UI图标
    public GameObject NotDone2; // 第二关未完成状态的UI图标
    public GameObject NotDone3; // 第三关未完成状态的UI图标

    [Header("Btns")]
    public Button Btn1; // 第一关的交互按钮
    public Button Btn2; // 第二关的交互按钮
    public Button Btn3; // 第三关的交互按钮

    [Header("Boolean Manager")]
    internal bool LevOneDone = false;  // 第一关完成状态标志
    internal bool LevTwoDone = false;  // 第二关完成状态标志
    internal bool LevThreeDone = false; // 第三关完成状态标志
    internal bool LevOneFinish = false; // 第一关最终完成状态标志（可能用于额外逻辑）
    internal bool LevTwoFinish = false; // 第二关最终完成状态标志
    internal bool LevThreeFinish = false; // 第三关最终完成状态标志
    
    void Start()
    {
        NotDone1.SetActive(false); // 初始化时隐藏第一关的未完成图标
    }
    
    void Update()
    {
        // 检查第一关完成状态并更新UI
        if(LevOneDone == true)
        {
            Done1.SetActive(true); // 显示第一关完成图标
            NotDone1.SetActive(false); // 隐藏第一关未完成图标
            Btn1.interactable = true; // 使第一关按钮可交互
        }
        
        // 检查第二关状态：如果第一关完成但第二关未完成，启用第二关按钮
        if(LevTwoDone == false && LevOneDone == true)
        {
            NotDone2.SetActive(false); // 隐藏第二关未完成图标
            Btn2.interactable = true; // 使第二关按钮可交互
            Btn1.interactable = false; // 禁用第一关按钮（玩家应该继续前进）
        }
        
        // 检查第二关完成状态并更新UI
        if (LevTwoDone == true)
        {
            Done2.SetActive(true); // 显示第二关完成图标
            NotDone2.SetActive(false); // 隐藏第二关未完成图标
            Btn2.interactable = true; // 使第二关按钮可交互
        }
        
        // 检查第三关状态：如果第二关完成但第三关未完成，启用第三关按钮
        if(LevThreeDone == false && LevTwoDone == true)
        {
            NotDone3.SetActive(false); // 隐藏第三关未完成图标
            Btn3.interactable = true; // 使第三关按钮可交互
            Btn2.interactable = false; // 禁用第二关按钮
        }
        
        // 检查第三关完成状态并更新UI
        if (LevThreeDone == true)
        {
            Done3.SetActive(true); // 显示第三关完成图标
            NotDone3.SetActive(false); // 隐藏第三关未完成图标
            Btn3.interactable = true; // 使第三关按钮可交互
        }
    }
}
