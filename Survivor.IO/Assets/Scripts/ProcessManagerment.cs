/*
 * ProcessManagerment.cs
 * 
 * 这个脚本负责管理游戏中的不同关卡进程和状态。
 * 它允许控制六个不同管理器(M1-M6)的关卡完成状态。
 * 每个管理器都有三个关卡，当完成后会将对应的Done标志设为true。
 * 脚本使用协程来延迟标记关卡完成状态，模拟加载或处理时间。


 游戏流程逻辑：
玩家通过UI选择要玩的关卡
ProcessManagerment设置相应的关卡并调用按钮点击事件
延迟一段时间后(ManagerTime)，将相应关卡标记为完成状态
FirsProcess检测到状态变化，更新UI显示并管理按钮交互性
完成一个关卡后，下一个关卡会被激活，前一个关卡按钮会被禁用
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessManagerment : MonoBehaviour
{
    [Header("Controller")]
    public Button Btn; // 控制器按钮，用于触发关卡加载或状态改变

    [Header("Procesing Manager")]
    public FirsProcess M1; // 第一个进程管理器
    public FirsProcess M2; // 第二个进程管理器
    public FirsProcess M3; // 第三个进程管理器
    public FirsProcess M4; // 第四个进程管理器
    public FirsProcess M5; // 第五个进程管理器
    public FirsProcess M6; // 第六个进程管理器

    [Header("Floating Manager")]
    internal float ManagerTime = 0.5f; // 管理器延迟时间，用于控制协程等待时间

    [Header("Managers Controller")]
    public LevelsManager Manager; // 关卡管理器，控制当前激活的关卡

    void Start()
    {
        // 启动时执行的初始化方法，目前为空
    }
    void Update()
    {
        // 每帧更新方法，目前为空
    }

    // 以下方法负责处理六个管理器(M1-M6)的三个不同关卡的激活和完成标记
    // 每个方法都遵循相似的模式：
    // 1. 设置当前关卡
    // 2. 触发按钮点击事件
    // 3. 启动协程来延迟标记关卡完成状态

    #region 第一个管理器(M1)的关卡控制方法
    public void OneLevel1()
    {
        Manager.Level1 = Manager.Levels[0]; // 设置当前关卡为第一个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckDoneOne()); // 启动协程检查完成状态
    }
    IEnumerator CheckDoneOne()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M1.LevOneDone = true; // 标记第一个管理器的第一关为完成状态
    }
    public void OneLevel2()
    {
        Manager.Level1 = Manager.Levels[0]; // 设置当前关卡为第一个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLevel2()); // 启动协程检查完成状态
    }
    IEnumerator CheckLevel2()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M1.LevTwoDone = true; // 标记第一个管理器的第二关为完成状态
    }
    public void OneLevel3()
    {
        Manager.Level1 = Manager.Levels[0]; // 设置当前关卡为第一个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLevelOneThree()); // 启动协程检查完成状态
    }
    IEnumerator CheckLevelOneThree()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M1.LevThreeDone = true; // 标记第一个管理器的第三关为完成状态
    }
    #endregion

    #region 第二个管理器(M2)的关卡控制方法
    public void TwoLevel1()
    {
        Manager.Level1 = Manager.Levels[1]; // 设置当前关卡为第二个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckleevlTwoOne()); // 启动协程检查完成状态
    }
    IEnumerator CheckleevlTwoOne()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M2.LevOneDone = true; // 标记第二个管理器的第一关为完成状态
    }
    public void TwoLevel2()
    {
        Manager.Level1 = Manager.Levels[1]; // 设置当前关卡为第二个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLevTwo()); // 启动协程检查完成状态
    }
    IEnumerator CheckLevTwo()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M2.LevTwoDone = true; // 标记第二个管理器的第二关为完成状态
    }
    public void TwoLevel3()
    {
        Manager.Level1 = Manager.Levels[1]; // 设置当前关卡为第二个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLevelTwoThree()); // 启动协程检查完成状态
    }
    IEnumerator CheckLevelTwoThree()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M2.LevThreeDone = true; // 标记第二个管理器的第三关为完成状态
    }
    #endregion

    #region 第三个管理器(M3)的关卡控制方法
    public void ThreeLevel1()
    {
        Manager.Level1 = Manager.Levels[2]; // 设置当前关卡为第三个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(Lev3One()); // 启动协程检查完成状态
    }
    IEnumerator Lev3One()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M3.LevOneDone = true; // 标记第三个管理器的第一关为完成状态
    }
    public void ThreeLevel2()
    {
        Manager.Level1 = Manager.Levels[2]; // 设置当前关卡为第三个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(Lev3Two()); // 启动协程检查完成状态
    }
    IEnumerator Lev3Two()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M3.LevThreeDone = true; // 标记第三个管理器的第二关为完成状态
    }
    public void ThreeLevel3()
    {
        Manager.Level1 = Manager.Levels[2]; // 设置当前关卡为第三个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        // 注意：这里缺少启动协程的代码
    }
    IEnumerator Lev3Three()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M3.LevThreeDone = true; // 标记第三个管理器的第三关为完成状态
    }
    #endregion

    #region 第四个管理器(M4)的关卡控制方法
    public void FourLevel1()
    {
        Manager.Level1 = Manager.Levels[3]; // 设置当前关卡为第四个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLev41()); // 启动协程检查完成状态
    }
    IEnumerator CheckLev41()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M4.LevOneDone = true; // 标记第四个管理器的第一关为完成状态
    }
    public void FourLevel2()
    {
        Manager.Level1 = Manager.Levels[3]; // 设置当前关卡为第四个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLev42()); // 启动协程检查完成状态
    }
    IEnumerator CheckLev42()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M4.LevTwoDone = true; // 标记第四个管理器的第二关为完成状态
    }
    public void FourLevel3()
    {
        Manager.Level1 = Manager.Levels[3]; // 设置当前关卡为第四个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(CheckLev43()); // 启动协程检查完成状态
    }
    IEnumerator CheckLev43()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M4.LevThreeDone = true; // 标记第四个管理器的第三关为完成状态
    }
    #endregion

    #region 第五个管理器(M5)的关卡控制方法
    public void FiveLevel1()
    {
        Manager.Level1 = Manager.Levels[4]; // 设置当前关卡为第五个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(checkLev51()); // 启动协程检查完成状态
    }
    IEnumerator checkLev51()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M5.LevOneDone = true; // 标记第五个管理器的第一关为完成状态
    }
    public void FiveLevel2()
    {
        Manager.Level1 = Manager.Levels[4]; // 设置当前关卡为第五个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(checkLev52()); // 启动协程检查完成状态
    }
    IEnumerator checkLev52()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M5.LevTwoDone = true; // 标记第五个管理器的第二关为完成状态
    }
    public void FiveLevel3()
    {
        Manager.Level1 = Manager.Levels[4]; // 设置当前关卡为第五个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(checkLev53()); // 启动协程检查完成状态
    }
    IEnumerator checkLev53()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M5.LevThreeDone = true; // 标记第五个管理器的第三关为完成状态
    }
    #endregion

    #region 第六个管理器(M6)的关卡控制方法
    public void SixLevel1()
    {
        Manager.Level1 = Manager.Levels[5]; // 设置当前关卡为第六个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(check61()); // 启动协程检查完成状态
    }
    IEnumerator check61()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M6.LevOneDone = true; // 标记第六个管理器的第一关为完成状态
    }
    public void SixLevel2()
    {
        Manager.Level1 = Manager.Levels[5]; // 设置当前关卡为第六个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(check62()); // 启动协程检查完成状态
    }
    IEnumerator check62()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M6.LevTwoDone = true; // 标记第六个管理器的第二关为完成状态
    }
    public void SixLevel3()
    {
        Manager.Level1 = Manager.Levels[5]; // 设置当前关卡为第六个关卡
        Btn.onClick.Invoke(); // 触发按钮点击事件
        StartCoroutine(check63()); // 启动协程检查完成状态
    }
    IEnumerator check63()
    {
        yield return new WaitForSeconds(ManagerTime); // 等待指定时间
        M6.LevThreeDone = true; // 标记第六个管理器的第三关为完成状态
    }
    #endregion
}
