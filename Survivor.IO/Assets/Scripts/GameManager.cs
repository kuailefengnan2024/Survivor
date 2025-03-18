/*
 * GameManager.cs
 * 游戏管理器类
 * 这个类是游戏的核心控制器，负责管理游戏的主要状态、玩家生命值、分数、敌人生成等功能
 * 它协调其他管理器类之间的交互，并处理游戏进程中的主要逻辑
 */

using System.Collections; // 用于管理协程
using System.Collections.Generic; // 用于管理列表
using UnityEngine.UI; // 用于管理UI元素
using UnityEngine; // 用于管理游戏对象
using TMPro; // 用于管理文本
using System.Threading; // 用于管理线程
using Unity.VisualScripting; // 用于管理可视化脚本
using static UnityEngine.GraphicsBuffer; // 用于管理图形缓冲区
using Unity.VisualScripting.Antlr3.Runtime.Collections; // 用于管理集合
using System.Numerics; // 用于管理数值

public class GameManager : MonoBehaviour // 游戏管理器类，继承自MonoBehaviour
{
    LevelManager levelManager; // 关卡管理器引用
    [Header("Manager Controller")]
    public BooleanManager Boolean; // 布尔值管理器，用于管理游戏中的布尔状态
    public PlayerManager PlayerPerfb; // 玩家管理器引用
    public AudioManager Sounds; // 音效管理器
    public movementJoystick JoystManager; // 摇杆移动控制器
    public SpawenManager Spawner; // 敌人生成管理器
    public TimerManager Times; // 时间管理器
    public ManagerFloatingBtn ManagerDownBtn; // 浮动按钮管理器
    public ManagerWeapons WeaponSpawn; // 武器生成管理器
    public UIManager ManagerUI; // UI管理器

    [Header("GameObject ScreenMainMenu")]
    public GameObject ScreenHome; // 主屏幕UI
    public GameObject ScreenVolve; // 返回屏幕UI
    public GameObject ScreenShop; // 商店屏幕UI
    public GameObject ScreenDeath; // 死亡屏幕UI
    public GameObject ScreenEquipement; // 装备屏幕UI

    [Header("Perfabes Controller")]
    public GameObject UIWeapon; // 武器UI对象
    public GameObject FillingFlash; // 填充闪烁效果
    public GameObject ScreenAddonWap; // 武器附加屏幕
    public GameObject[] Enemys; // 敌人对象数组

    [Header("Levels World")]
    public GameObject ContainerWorld; // 世界容器，可能包含游戏场景

    [Header("UI Manager")]
    public Image HealthBar; // 生命值条UI
    public Image ReloadWeapon; // 武器重装填UI
    public Image ScoringLevel; // 分数等级UI
    public Image FillingReweapon; // 武器填充UI
    public Color[] myColors; // 颜色数组，可能用于UI效果

    [Header("UI Text Manager")]
    public TextMeshProUGUI ScoringValue; // 分数值文本
    public TextMeshProUGUI ScoringValueDeux; // 第二个分数值文本
    public TextMeshProUGUI ValueKilled; // 击杀数量文本
    public Text ValueKilledScreenFinish; // 游戏结束屏幕上的击杀数量
    public TextMeshProUGUI CurrentCoins; // 当前金币数量

    [Header("Float Manager")]
    public float SpeedEnemy; // 敌人速度
    internal float Valeur; // 通用值变量
    internal float ValureLevel; // 等级值变量
    public float LerpTime = 0.1f; // 插值时间，用于平滑过渡
    internal float t = 0; // 计时器变量
    public float Health; // 玩家生命值

    [Header("Integer Manager")]
    internal int len; // 长度变量，用于颜色数组
    internal int colorIndex = 0; // 当前颜色索引
    internal int CurrentKilled = 0; // 当前击杀数量
    internal int CurrentCurrency = 0; // 当前货币数量
    public int CurrentReload = 0; // 当前重装次数

    [Header("Boolean Manager")]
    internal bool EnemyAvailable = true; // 敌人是否可用
    internal bool SpawnObject = true; // 是否生成对象
    internal bool RightFill = true; // 右侧填充标志
    internal bool LeftFill = true; // 左侧填充标志

    internal bool NormalBolt = true; // 普通弹药标志
    internal bool DiamondBolt = false; // 钻石弹药标志
    public bool CheckFinish = true; // 检查完成标志
    internal bool StartFlashing = false; // 开始闪烁标志
    public bool startmove = false; // 开始移动标志
    internal bool AvailabelWeapon = true; // 武器可用标志
    internal bool GameStart = false; // 游戏开始标志
    internal bool SetActiveAll = true; // 激活所有对象标志
    internal bool PlayerDeath = false; // 玩家死亡标志
    internal bool Checkenemys = false; // 检查敌人标志
    public float hh; // 额外生命值变量

    void Start() // 游戏开始时执行
    {
        // 初始化时查找关卡管理器
        levelManager = FindObjectOfType<LevelManager>();
        // 获取颜色数组长度
        len = myColors.Length;
        // 从PlayerPrefs获取额外生命值
        hh = PlayerPrefs.GetFloat("Health");
        // 设置初始生命值（基础100加上额外值）
        Health = 100f+ hh;
        // 设置各个UI界面状态
        ManagerDownBtn.Shop = true;
        ManagerDownBtn.Equipement = true;
        ManagerDownBtn.Death = true;
    }

    void Update() // 游戏更新时执行
    {
        // 检查敌人标志处理（当前为空）
        if(Checkenemys == true)
        {

        }
        
        // 游戏开始且需要激活所有对象时的处理
        if(GameStart == true && SetActiveAll == true)
        {
            Boolean.GameStart = true; // 设置布尔管理器中的游戏开始标志
            ContainerWorld.SetActive(true); // 激活世界容器
            UIWeapon.SetActive(true); // 激活武器UI
            SetActiveAll = false; // 防止重复激活
        }
        
        // 游戏未开始时的初始化处理
        if(Boolean.GameStart == false)
        {
            // 重置各种值
            Valeur = 0;
            ValureLevel = 0;
            ReloadWeapon.fillAmount = 0;
            ScoringLevel.fillAmount = 0;
        }
        
        // 游戏进行中的主要逻辑
        if(Boolean.GameStart == true)
        {
            // 调用敌人检查方法
            CheckEnemy();
            // 调用武器重装方法
            ReloadingWapeons();
           
            // 更新生命值条显示
            HealthBar.fillAmount = Health / (100f+ hh);
            // 根据生命值百分比改变颜色
            if (HealthBar.fillAmount == 0.5f)
            {
                HealthBar.color = Color.yellow; // 生命值降至50%时变黄
            }
            if (HealthBar.fillAmount == 0.3f)
            {
                HealthBar.color = Color.red; // 生命值降至30%时变红
            }
            // 检查玩家死亡
            if(HealthBar.fillAmount == 0 && PlayerDeath == false)
            {
                Debug.Log("You are Death");
                BtnPause(); // 调用暂停方法
                PlayerDeath = true; // 设置玩家死亡标志
            }
            
            // 更新分数显示
            ScoringValue.text = "" + CurrentReload;
            ScoringValueDeux.text = "" + CurrentReload;
            
            // 检查关卡完成逻辑
            if (CheckFinish == true)
            {
                CheckValeurFill(); // 检查填充值
                // 分数等级填满时的处理
                if (ScoringLevel.fillAmount == 1)
                {
                    Debug.Log("ScoringLevel.fillAmount == 1");
                    // 启用武器
                    WeaponSpawn.ActivateWeapon = true;
                    // 停止计时
                    Times.StopTime();
                    // 增加重装次数
                    CurrentReload += 1;
                    // 开始闪烁效果
                    StartFlashing = true;
                    // 显示武器附加屏幕
                    ScreenAddonWap.SetActive(true);
                    // 禁用玩家控制
                    PlayerPerfb.GetComponent<PlayerManager>().enabled = false;
                    PlayerPerfb.GetComponent<Rigidbody2D>().simulated = false;
                    
                    // 敌人可用时暂停生成
                    if (EnemyAvailable == true)
                    {
                        Debug.Log("EnemyAvailable");
                        Spawner.enabled = false;
                        startmove = true;
                    }
                    // 重置等级值
                    ValureLevel = 0f;
                    CheckFinish = false;
                }
            }
            
            // 开始移动敌人的处理
            if (startmove == true)
            {
                foreach (GameObject joint in Enemys)
                {
                    Debug.Log("startmove");
                    // 禁用敌人控制和物理模拟
                    joint.GetComponent<EnemyManager>().enabled = false;
                    joint.GetComponent<Rigidbody2D>().simulated = false;
                }
            }
            
            // 闪烁效果处理
            if (StartFlashing == true)
            {
                // 使用颜色插值制作闪烁效果
                FillingReweapon.color = Color.Lerp(FillingReweapon.color, myColors[colorIndex], LerpTime * Time.deltaTime);
                t = Mathf.Lerp(t, 1f, LerpTime * Time.deltaTime);
                if (t > 0.9f)
                {
                    t = 0;
                    colorIndex++;
                    colorIndex = (colorIndex >= len) ? 0 : colorIndex; // 循环使用颜色
                }
            }
            
            // 更新击杀和金币显示
            CheckKilledAndCoins();
        }
    }

    // 暂停按钮方法
    public void BtnPause() // 暂停按钮方法
    {
        AvailabelWeapon = false; // 禁用武器
        Times.StopTime(); // 停止计时
        // 禁用玩家控制和物理模拟
        PlayerPerfb.GetComponent<PlayerManager>().enabled = false;
        PlayerPerfb.GetComponent<Rigidbody2D>().simulated = false;
        
        // 敌人可用时暂停生成
        if (EnemyAvailable == true)
        {
            Spawner.enabled = false;
            startmove = true;
        }
    }

    // 延迟恢复方法
    public void ResumeAfter()
    {
        startmove = false; // 停止敌人移动
    }

    // 恢复按钮方法
   public void ResumeBtn()
    {
        AvailabelWeapon = true; // 启用武器
        Checkenemys = true; // 启用敌人检查
        Times.StartTime(); // 开始计时
        // 恢复玩家生命值
        hh = PlayerPrefs.GetFloat("Health");
        Health = 100f + hh;
        // 恢复玩家控制和物理模拟
        PlayerPerfb.GetComponent<PlayerManager>().enabled = true;
        PlayerPerfb.GetComponent<Rigidbody2D>().simulated = true;
        
        // 敌人可用时恢复敌人状态
        if (EnemyAvailable == true)
        {
            startmove = false;
            // 查找所有敌人
            Enemys = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject joint in Enemys)
            {
                // 恢复敌人控制和物理模拟
                joint.GetComponent<EnemyManager>().enabled = true;
                joint.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
    }

    // 更新击杀和金币显示
    void CheckKilledAndCoins() // 检查击杀和金币显示
    {
        // 更新UI显示
        ValueKilled.text =  "" + CurrentKilled;
        ValueKilledScreenFinish.text =  "" + CurrentKilled;
        CurrentCoins.text = "" + CurrentCurrency;
        // 保存当前击杀数据到PlayerPrefs
        PlayerPrefs.SetInt("CurrentKilled", CurrentKilled);
    }
    
    ManagerMecanique managerMecanique;
    public void SaveDatta()
    {

        levelManager.CalculLevel(CurrentKilled);


        int CoinsInt;
        CoinsInt = PlayerPrefs.GetInt("coins");
        CoinsInt += CurrentCurrency;
        PlayerPrefs.SetInt("coins", CoinsInt);
        managerMecanique = FindObjectOfType<ManagerMecanique>();
        managerMecanique.InitText();

    }
    void CheckValeurFill() // 检查填充值
    {
        if (CheckFinish == true)
        {
            ScoringLevel.fillAmount = ValureLevel / (100 +CurrentReload*50);
        }
    }
    void ReloadingWapeons() // 武器重装
    {    
        Valeur += 1f * Time.deltaTime;
        if(ReloadWeapon.fillAmount < 1 && RightFill == true)
        {
            SpawnObject = false;
            ReloadWeapon.fillAmount = Valeur;
            LeftFill = true;
        }
        if(ReloadWeapon.fillAmount == 1 && LeftFill == true)
        {
            SpawnObject = true;
            ReloadWeapon.fillAmount = 0;
            Valeur = 0;
            LeftFill = false;
        }
    }
    void CheckEnemy()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemys.Length == 0)
          {
            EnemyAvailable = false;
            NormalBolt = false;
            DiamondBolt = true;
          }
        else
          {
            EnemyAvailable = true;
            NormalBolt = true;
            DiamondBolt = false;
        }
    }
    
    public void FirstCont() // 第一关
    {
        ScreenAddonWap.SetActive(false);
        Times.StartTime();
        PlayerPerfb.GetComponent<PlayerManager>().enabled = true;
        PlayerPerfb.GetComponent<Rigidbody2D>().simulated = true;
        if (EnemyAvailable == true)
        {
            startmove = false;
            foreach (GameObject joint in Enemys)
            {
                joint.GetComponent<EnemyManager>().enabled = true;
                joint.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        ScoringLevel.fillAmount = 0;
        StartCoroutine(CheckingFinishBtn());
    }
    public void SecondCont()
    {
        ScreenAddonWap.SetActive(false);
        Times.StartTime();
        PlayerPerfb.GetComponent<PlayerManager>().enabled = true;
        PlayerPerfb.GetComponent<Rigidbody2D>().simulated = true;
        if (EnemyAvailable == true)
        {
            startmove = false;
            foreach (GameObject joint in Enemys)
            {
                joint.GetComponent<EnemyManager>().enabled = true;
                joint.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        ScoringLevel.fillAmount = 0;
        StartCoroutine(CheckingFinishBtn());
    }
    public void ThirdCont() // 第三关
    {
        ScreenAddonWap.SetActive(false);
        Times.StartTime();
        PlayerPerfb.GetComponent<PlayerManager>().enabled = true;
        PlayerPerfb.GetComponent<Rigidbody2D>().simulated = true;
        //Spawner.enabled = true;
        if (EnemyAvailable == true)
        {
            startmove = false;
            foreach (GameObject joint in Enemys)
            {
                joint.GetComponent<EnemyManager>().enabled = true;
                joint.GetComponent<Rigidbody2D>().simulated = true;
            }
        }
        ScoringLevel.fillAmount = 0;
        StartCoroutine(CheckingFinishBtn());
    }

    IEnumerator CheckingFinishBtn()
    {
        yield return new WaitForSeconds(0.5f);
        CheckFinish = true;
    }
}
