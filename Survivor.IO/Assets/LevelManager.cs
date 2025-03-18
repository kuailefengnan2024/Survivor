/*
这是一个游戏等级管理器类(LevelManager)
主要功能：
1. 管理玩家等级和经验值系统
2. 处理等级提升和解锁内容
3. 管理商店购买系统
4. 处理游戏进度保存
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]  // 可序列化标记，使得Unity可以在Inspector中显示和编辑这个类
public class LevelObjectUnlocked
{
    public int LevelUnlock;    // 定义解锁需要的等级
    public Sprite[] image;     // 存储该等级解锁后显示的图片数组
}

[System.Serializable]
public class itemUnlocked
{
    public string itemName;    // 商店物品名称
    public int price;         // 物品价格
    public string itemType;   // 物品类型(Health健康值或Speed速度)
}

public class LevelManager : MonoBehaviour
{
    ManagerMecanique managerMecanique;      // 游戏机制管理器引用
    public GameObject panelParent;          // 主面板对象
    public GameObject[] panelchilden;       // 子面板对象数组
    public itemUnlocked[] itemUnlockeds;    // 可解锁物品数组
    public GameObject nocoin;               // 金币不足提示面板
    int myButtonNumber;                     // 按钮编号
    public static LevelManager Instance;    // 单例模式实例
    
    // UI元素
    public Slider slider;                   // 进度条滑块
    public Slider slidelevel;              // 等级进度条
    public Text levelText;                 // 等级显示文本
    
    // 各等级解锁的游戏对象数组
    public GameObject[] level1;            // 等级1解锁内容
    public GameObject[] level2;            // 等级2解锁内容
    public GameObject[] level3;            // 等级3解锁内容
    public GameObject[] level4;            // 等级4解锁内容
    public GameObject[] level5;            // 等级5解锁内容
    public GameObject[] level6;            // 等级6解锁内容
    public GameObject[] level7;            // 等级7解锁内容
    public GameObject[] level8;            // 等级8解锁内容
    public GameObject[] level9;            // 等级9解锁内容
    
    public Button[] evolveButton;          // 进化按钮数组
    
    // 玩家数据
    public int level;                      // 当前等级
    public int xp;                         // 当前经验值
    
    // UI面板
    public GameObject LevelPopUp;          // 等级提升弹窗
    public GameObject coins;               // 金币显示
    public Image[] imageLevel;             // 等级图标数组

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance = this;      // 初始化单例实例
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }
        InitLevel();                      // 初始化等级系统
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculLevel(int x)
    {
        Debug.Log("CalculLevel");         // 调试日志
        xp += x;                          // 增加经验值
        level = PlayerPrefs.GetInt("level");  // 获取当前等级
        
        // 经验值达到升级条件 (当前等级 * 1000)
        if (xp>= (level * 1000))
        {
            xp -= (level * 1000);         // 减去升级所需经验值
            PlayerPrefs.SetInt("level", level + 1);  // 等级+1
            level = PlayerPrefs.GetInt("level");     // 更新当前等级
            PlayerPrefs.SetInt("xp", xp);           // 保存剩余经验值
            InitLevel();                            // 重新初始化等级
        }
        else
        {
            PlayerPrefs.SetInt("xp", xp);          // 保存经验值
            InitLevel();                           // 更新等级显示
        }
    }

    public void InitLevel()
    {
        // 初始化玩家数据
        xp = PlayerPrefs.GetInt("xp");            // 读取经验值
        level = PlayerPrefs.GetInt("level");      // 读取等级
        
        // 更新UI显示
        levelText.text = level.ToString();        // 更新等级文本
        slidelevel.maxValue = level * 1000;       // 设置经验条最大值
        slidelevel.value = xp;                    // 设置当前经验值
        slider.value = level*2;                   // 更新等级进度条
        
        // 根据当前等级激活相应内容
        if (level >= 1)
        {
            foreach(var ii in level1)
            {
                ii.SetActive(true);               // 激活等级1的内容
            }
        }
        if (level >= 2)
        {
            foreach (var ii in level2)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 3)
        {
            foreach (var ii in level3)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 4)
        {
            foreach (var ii in level4)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 5)
        {
            foreach (var ii in level5)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 6)
        {
            foreach (var ii in level6)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 7)
        {
            foreach (var ii in level7)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 8)
        {
            foreach (var ii in level8)
            {
                ii.SetActive(true);
            }
        }
        if (level >= 9)
        {
            foreach (var ii in level9)
            {
                ii.SetActive(true);
            }
        }
        
        CheckAvailabelButton();                   // 检查可用按钮
    }

    void CheckAvailabelButton()
    {
        // 检查并更新所有进化按钮状态
        for(int ii =0; ii< evolveButton.Length; ii++)
        {
            if(PlayerPrefs.GetInt("evolveButton"+ ii) == 1)
            {
                evolveButton[ii].interactable = false;  // 已购买的按钮设为不可交互
            }
        }
    }

    public void BuyEvolve(int ii)
    {
        myButtonNumber = ii;                          // 记录当前按钮编号
        int CoinsInt;
        CoinsInt = PlayerPrefs.GetInt("coins");      // 获取当前金币数
    }

    public void BuyItems(int ii)
    {
        int CoinsInt = PlayerPrefs.GetInt("coins");  // 获取当前金币数
        
        // 检查是否有足够金币
        if (itemUnlockeds[ii].price <= CoinsInt)
        {
            // 根据物品类型增加相应属性
            if (itemUnlockeds[ii].itemType == "Health") {
                AddHealth();                         // 增加生命值
            } else {
                AddSpead();                         // 增加速度
            }
            
            // 处理购买后的操作
            CoinsInt -= itemUnlockeds[ii].price;    // 扣除金币
            PlayerPrefs.SetInt("coins", CoinsInt);  // 保存金币数
            PlayerPrefs.SetInt("evolveButton" + ii, 1);  // 标记该物品已购买
            CheckAvailabelButton();                 // 更新按钮状态
            
            // 关闭相关面板
            panelParent.SetActive(false);
            foreach(var pp in panelchilden)
            {
                pp.SetActive(false);
            }
            
            // 更新游戏机制管理器
            managerMecanique = FindObjectOfType<ManagerMecanique>();
            managerMecanique.InitText();
        }
        else
        {
            nocoin.SetActive(true);                 // 显示金币不足提示
        }
    }

    public void AddHealth()
    {
        float hh = PlayerPrefs.GetFloat("Health");  // 获取当前生命值
        PlayerPrefs.SetFloat("Health", hh + 10);    // 增加10点生命值
        Debug.Log("Health"+PlayerPrefs.GetFloat("Health"));  // 输出调试信息
    }

    public void AddSpead()
    {
        float ss = PlayerPrefs.GetFloat("Spead");   // 获取当前速度值
        PlayerPrefs.SetFloat("Spead", ss + 2);      // 增加2点速度值
        Debug.Log("Spead"+PlayerPrefs.GetFloat("Spead"));   // 输出调试信息
    }
}
