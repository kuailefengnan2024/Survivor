/*
UIManager 类是整个游戏的用户界面管理器
负责处理游戏各种UI界面的显示和切换
包括主菜单、游戏界面、暂停界面、结束界面等
还管理武器显示和游戏状态
*/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    ManagerWeapons managerWeapons;    // 武器管理器
    [Header("Managers")]              // 游戏中的各种管理器引用
    public GameManager Manager;       // 游戏总管理器
    public TimerManager timer;        // 计时器管理器
    public BooleanManager Bool;       // 布尔值管理器
    public LevelsManager Level;       // 关卡管理器
    public SpriteWeapons Weapons;     // 武器精灵图管理器
    public ManagerMecanique Mecanique;// 机械管理器

    [Header("Componenet Player")]     // 玩家相关组件
    public GameObject Player;               // 玩家游戏对象
    public GameObject HelthUI;              // 血量UI界面
    public GameObject LevelLocalisation;    // 关卡位置对象，作为关卡实例的父对象
    public GameObject CurrentLevel;         // 当前关卡对象引用

    [Header("UI Manager")]            // UI界面管理
    public GameObject ScreenPause;          // 暂停界面
    public GameObject ScreenMainMenu;       // 主菜单界面
    public GameObject ScreenGamePlay;       // 游戏界面
    public GameObject EffectFade;           // 淡入淡出效果对象
    public GameObject EffectFadeGamePlay;   // 游戏中淡入淡出效果对象
    public GameObject FinishScreen;         // 结束界面

    [Header("Weapons Manager")]       // 武器管理
    public GameObject SpinerContainer;      // 旋转武器容器
    public GameObject GunW;                 // 枪械武器对象
    public GameObject SpinnerAW;            // 旋转武器A对象
    public GameObject SpinnerBW;            // 旋转武器B对象
    

    [Header("Sprites Guns")]          // 武器精灵图片
    public Sprite Sahem;                    // 武器精灵图片 - Sahem
    public Sprite Gun;                      // 武器精灵图片 - 枪
    public Sprite SpinerA;                  // 武器精灵图片 - 旋转武器A
    public Sprite SpinerB;                  // 武器精灵图片 - 旋转武器B
    public Sprite Ball;                     // 武器精灵图片 - 球
    public Sprite Rocket;                   // 武器精灵图片 - 火箭
    public Sprite DroneA;                   // 武器精灵图片 - 无人机A
    public Sprite DroneB;                   // 武器精灵图片 - 无人机B
    public Sprite DroneC;                   // 武器精灵图片 - 无人机C
    public Sprite FireGlass;                // 武器精灵图片 - 火焰玻璃
    public Sprite Brick;                    // 武器精灵图片 - 砖块
    public Sprite FireGase;                 // 武器精灵图片 - 火焰气体

    [Header("Strings Manager")]       // 字符串管理
    internal string CheckEvolve;            // 检查进化状态的字符串
    internal string Checking;               // 检查状态的字符串
    public string CurrentName;              // 当前对象名称

    [Header("Boolaen Manager")]       // 布尔值管理
    internal bool FinishScreenB = false;    // 结束界面显示状态
    internal bool DestroyEnemys = false;    // 是否销毁敌人
    private bool StopAllAudios = false;     // 是否停止所有音频
   
    internal bool MapReady = false;         // 地图是否准备好

    AudioCheckerPlayer audioCheckerPlayer;  // 音频检查器
    
    void Start()
    {
        // 游戏启动时从PlayerPrefs获取"CheckEvolve"的值
        Checking = PlayerPrefs.GetString("CheckEvolve");
    }
    
    void Update()
    {
        // 每帧从PlayerPrefs获取"CheckEvolve"的值
        CheckEvolve = PlayerPrefs.GetString("CheckEvolve");
        if (CheckEvolve == "work")
        {
            // 如果CheckEvolve值为"work"，激活进化按钮
            Manager.ManagerDownBtn.Evolve = true;
        }
        
        // 处理玩家死亡逻辑
        if(Manager.PlayerDeath == true && FinishScreenB == false)
        {
            DestroyEnemys = true;           // 标记销毁所有敌人
            StopAllAudios = true;           // 停止所有音频
            FinishScreen.SetActive(true);   // 激活结束界面
            timer.StopTime();               // 停止计时器
            
            // 如果没有购买移除广告，显示插页广告
            if (PlayerPrefs.GetInt("ads") != 1)
            {
                Advertisements.Instance.ShowInterstitial();
            }
            
            // 重置玩家状态
            Manager.PlayerDeath = false;
            Manager.Health = 100;
            Manager.HealthBar.color = Color.green;
            FinishScreenB = true;           // 标记结束界面已显示
        }
        else
        {
            StopAllAudios = false;
        }
        
        // 通过名称查找当前关卡对象
        CurrentLevel = GameObject.Find(CurrentName);
    }
    
    // 返回按钮点击处理
    public void BackBtn()
    {
        managerWeapons = FindObjectOfType<ManagerWeapons>();  // 查找武器管理器
        MapReady = false;                               // 标记地图未准备好
        EffectFadeGamePlay.SetActive(true);            // 激活游戏淡出效果
        DestroyEnemys = true;                          // 标记销毁所有敌人
        Weapons.DesactivateAll();                      // 停用所有武器
        managerWeapons.CleanImageW();                  // 清理武器图像
        Destroy(CurrentLevel);                         // 销毁当前关卡
        StartCoroutine(StartBacking());                // 启动返回协程
    }
    
    // 游戏结束返回按钮点击处理
    public void BackFinish()
    {
        managerWeapons = FindObjectOfType<ManagerWeapons>();  // 查找武器管理器
        MapReady = false;                               // 标记地图未准备好
        DestroyEnemys = true;                          // 标记销毁所有敌人
        EffectFadeGamePlay.SetActive(true);            // 激活游戏淡出效果
        FinishScreen.SetActive(false);                 // 隐藏结束界面
        Weapons.DesactivateAll();                      // 停用所有武器
        managerWeapons.CleanImageW();                  // 清理武器图像
        Destroy(CurrentLevel);                         // 销毁当前关卡
        StartCoroutine(StartBacking());                // 启动返回协程
        Advertisements.Instance.ShowInterstitial();     // 显示插页广告
    }
    
    // 返回主菜单的协程
    IEnumerator StartBacking()
    {
        yield return new WaitForSeconds(0.8f);         // 等待0.8秒
        Player.gameObject.GetComponent<Rigidbody2D>().simulated = true;  // 激活玩家物理模拟
        DestroyEnemys = false;                         // 取消销毁敌人标记
        
        // 重置游戏状态
        Manager.CurrentReload = 0;
        Manager.CurrentCurrency = 0;
        Manager.CurrentKilled = 0;
        timer.timeRemaining = 0;
        FinishScreenB = false;
        EffectFadeGamePlay.SetActive(false);           // 关闭淡出效果
        
        // 如果游戏已开始，则返回主菜单
        if (Manager.Boolean.GameStart == true)
        {
            Player.transform.position = new Vector3(0, 0, 0);  // 重置玩家位置
            HelthUI.SetActive(false);                  // 隐藏血量UI
            ScreenPause.SetActive(false);              // 隐藏暂停界面
            ScreenGamePlay.SetActive(false);           // 隐藏游戏界面
            ScreenMainMenu.SetActive(true);            // 显示主菜单
            Manager.Boolean.GameStart = false;         // 标记游戏未开始
        }
    }
    
    // 开始游戏按钮点击处理
    public void PlayBtn()
    {
        EffectFade.SetActive(true);                    // 激活淡入效果
        // 实例化第一关卡并设置其父对象为关卡位置对象
        (Instantiate(Level.Level1, Level.Level1.transform.position, Level.Level1.transform.rotation) as GameObject).transform.SetParent(LevelLocalisation.transform);
        CurrentName = Level.Level1.gameObject.name + "(Clone)";  // 记录当前关卡名称
        StartCoroutine(GameStart());                   // 启动游戏开始协程
    }
    
    // 游戏开始协程
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.7f);         // 等待0.7秒
        
        // 处理进化状态
        if(Checking == "")
        {
            Checking = "work";
        }
        if (CheckEvolve == "")
        {
            PlayerPrefs.SetString("CheckEvolve", Checking);
        }
        Debug.Log("GameStart");

        MapReady = true;                               // 标记地图已准备好
        //Manager.startmove = true;                    // 注释掉的代码，可能是控制开始移动
        EffectFade.SetActive(false);                   // 关闭淡入效果
        Bool.GameStart = true;                         // 标记游戏已开始
        Player.GetComponent<PlayerManager>().enabled = true;  // 启用玩家管理器组件
        Manager.AvailabelWeapon = true;                // 标记武器可用
        Manager.GameStart = true;                      // 标记游戏已开始
        ScreenMainMenu.SetActive(false);               // 隐藏主菜单
        ScreenGamePlay.SetActive(true);                // 显示游戏界面
        HelthUI.SetActive(true);                       // 显示血量UI
        timer.StartTime();                             // 启动计时器
        
        // 如果敌人可用，激活所有敌人
        if (Manager.EnemyAvailable == true)
        {
            Manager.startmove = false;
            foreach (GameObject joint in Manager.Enemys)
            {
                joint.GetComponent<EnemyManager>().enabled = true;  // 启用敌人管理器组件
                joint.GetComponent<Rigidbody2D>().simulated = true; // 启用敌人物理模拟
            }
        }
    }
    
    // 暂停按钮点击处理
    public void Pause()
    {
        ScreenPause.SetActive(true);                   // 显示暂停界面
        Manager.BtnPause();                            // 调用游戏管理器的暂停方法
    }
    
    // 继续按钮点击处理
    public void Resume()
    {
        ScreenPause.SetActive(false);                  // 隐藏暂停界面
        Manager.ResumeBtn();                           // 调用游戏管理器的继续方法
    }
    
    bool myaudio = true;                               // 音频状态变量
    
    // 切换音频状态
    public void ChangeAudio()
    {
        audioCheckerPlayer = FindObjectOfType<AudioCheckerPlayer>();  // 查找音频检查器
        if (myaudio == true)
        {
            myaudio = false;
            audioCheckerPlayer.AudioManager(false);    // 关闭音频
        }
        else
        {
            myaudio = true;
            audioCheckerPlayer.AudioManager(true);     // 打开音频
        }
    }
}
