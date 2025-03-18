/*
 * EnemyManager.cs
 * 敌人管理器类
 * 该类负责控制敌人的行为，包括跟踪玩家、受伤反应、死亡处理和钻石掉落等
 * 是游戏中敌人AI和交互的核心组件
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

public class EnemyManager : MonoBehaviour
{
    [Header("Manager Enemy")]
    public GameObject Manager; // 游戏管理器引用
    public GameObject HitEffect; // 受击特效
    public GameObject BloodLocalisation; // 血液效果的位置容器
    public GameObject UImanager; // UI管理器引用
    private AudioSource Audio; // 音频源组件，用于播放敌人音效

    public GameObject Bolt; // 伤害数值显示对象
    private Transform Player; // 玩家位置的引用
    internal bool FollowPlayer = true; // 是否跟随玩家的标志
    internal int ValueAdd; // 击杀该敌人获得的分数值
    internal int Diamond; // 决定掉落哪种钻石的随机值

    [Header("Diamond")]
    public GameObject BlueDiamond; // 蓝色钻石预制体
    public GameObject RedDiamond; // 红色钻石预制体
    public GameObject GreenDiamond; // 绿色钻石预制体

    void Start()
    {
        // 初始化各种引用
        Manager = GameObject.Find("GameManager"); // 查找游戏管理器
        UImanager = GameObject.Find("UI"); // 查找UI管理器
        BloodLocalisation = GameObject.Find("BloodManager"); // 查找血液效果管理器
        Player = GameObject.FindGameObjectWithTag("Player").transform; // 查找玩家位置
        ValueAdd = Random.Range(1, 100); // 随机生成击杀分数值(1-99)
        Diamond = Random.Range(1, 3); // 随机决定掉落的钻石类型(1-2)
        Audio = GetComponent<AudioSource>(); // 获取音频源组件
    }

    void Update()
    {
        if(FollowPlayer == true)
        {
            // 使敌人朝向玩家移动
            transform.position = Vector2.MoveTowards(this.transform.position, Player.position, Manager.GetComponent<GameManager>().SpeedEnemy * Time.deltaTime);
            // 根据玩家位置翻转敌人的图像，保证敌人始终面向玩家
            this.gameObject.GetComponent<SpriteRenderer>().flipX = Player.transform.position.x < this.transform.position.x;
        }
        // 如果UI管理器设置了销毁敌人标志，则销毁该敌人
        if(UImanager.GetComponent<UIManager>().DestroyEnemys == true)
        {
            Destroy(this.gameObject);
        }
    }

    // 处理死亡动画和钻石生成的协程
    IEnumerator AnimationController()
    {
        yield return new WaitForSeconds(0.5f); // 等待0.5秒后生成钻石
        // 根据之前随机的Diamond值生成对应类型的钻石
        if(Diamond == 1)
        {
            // 生成蓝色钻石并设置为BloodLocalisation的子对象
            (Instantiate(BlueDiamond, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
        }
        if (Diamond == 2)
        {
            // 生成红色钻石并设置为BloodLocalisation的子对象
            (Instantiate(RedDiamond, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
        }
        if (Diamond == 3)
        {
            // 生成绿色钻石并设置为BloodLocalisation的子对象
            (Instantiate(GreenDiamond, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
        }
        Destroy(this.gameObject); // 销毁敌人对象
    }
    
    // 与AnimationController类似，但可能是专门用于球类武器击杀的处理
    IEnumerator BallController()
    {
        yield return new WaitForSeconds(0.5f); // 等待0.5秒后生成钻石
        // 根据之前随机的Diamond值生成对应类型的钻石
        if (Diamond == 1)
        {
            (Instantiate(BlueDiamond, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
        }
        if (Diamond == 2)
        {
            (Instantiate(RedDiamond, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
        }
        if (Diamond == 3)
        {
            (Instantiate(GreenDiamond, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
        }
        Destroy(this.gameObject); // 销毁敌人对象
    }
    
    // 处理与其他对象的碰撞
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 被普通弹药(Bolt)击中
        if (other.CompareTag("Bolt"))
        {
            Audio.Play(); // 播放音效
            Bolt.SetActive(true); // 显示伤害数值
            Bolt.GetComponent<TextMeshProUGUI>().color = Color.red; // 设置伤害数值为红色
            Bolt.GetComponent<TextMeshProUGUI>().text = "" + ValueAdd; // 显示随机生成的伤害值
            this.gameObject.GetComponent<Animator>().Play("ZombieDeath"); // 播放死亡动画
            // 生成击中特效并设置为BloodLocalisation的子对象
            (Instantiate(HitEffect, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
            Manager.GetComponent<GameManager>().CurrentKilled += 1; // 增加游戏管理器中的击杀计数
            StartCoroutine(AnimationController()); // 启动死亡动画协程
        }
        
        // 被球(ball)击中
        if (other.CompareTag("ball"))
        {
            Audio.Play(); // 播放音效
            Bolt.SetActive(true); // 显示伤害数值
            Bolt.GetComponent<TextMeshProUGUI>().text = "" + ValueAdd; // 显示随机生成的伤害值
            this.gameObject.GetComponent<Animator>().Play("ZombieDeath"); // 播放死亡动画
            // 生成击中特效
            (Instantiate(HitEffect, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
            Manager.GetComponent<GameManager>().CurrentKilled += 1; // 增加击杀计数
            StartCoroutine(BallController()); // 启动球类击杀协程
        }
        
        // 被火(Fire)击中
        if (other.CompareTag("Fire"))
        {
            Audio.Play(); // 播放音效
            Bolt.SetActive(true); // 显示伤害数值
            Bolt.GetComponent<TextMeshProUGUI>().text = "" + ValueAdd; // 显示随机生成的伤害值
            this.gameObject.GetComponent<Animator>().Play("ZombieDeath"); // 播放死亡动画
            // 生成击中特效
            (Instantiate(HitEffect, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
            Manager.GetComponent<GameManager>().CurrentKilled += 1; // 增加击杀计数
            StartCoroutine(BallController()); // 启动球类击杀协程
        }
        
        // 被旋转武器(Spiner)击中
        if (other.CompareTag("Spiner"))
        {
            Audio.Play(); // 播放音效
            Bolt.SetActive(true); // 显示伤害数值
            Bolt.GetComponent<TextMeshProUGUI>().text = "" + ValueAdd; // 显示随机生成的伤害值
            this.gameObject.GetComponent<Animator>().Play("ZombieDeath"); // 播放死亡动画
            // 生成击中特效
            (Instantiate(HitEffect, transform.position, transform.rotation) as GameObject).transform.SetParent(BloodLocalisation.transform);
            Manager.GetComponent<GameManager>().CurrentKilled += 1; // 增加击杀计数
            StartCoroutine(AnimationController()); // 启动死亡动画协程
        }
        
        // 与玩家碰撞
        if (other.CompareTag("Player"))
        {
            Audio.Play(); // 播放音效
            FollowPlayer = false; // 停止跟踪玩家，可能是因为已经攻击到玩家
        }
    }
    
    // 处理离开触发器的事件
    private void OnTriggerExit2D(Collider2D other)
    {
        // 当离开玩家触发器时，重新开始跟踪玩家
        if (other.CompareTag("Player"))
        {
            FollowPlayer = true;
        }
    }
}
