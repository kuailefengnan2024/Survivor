/*
 * Diamond.cs
 * 钻石控制器类
 * 该类负责控制钻石的行为，包括跟随玩家、移动和收集效果
 * 钻石是游戏中的可收集物品，给玩家提供分数增益
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public GameObject Player; // 玩家对象引用
    public GameObject Manager; // 游戏管理器引用
    public GameObject Boolean; // 布尔值管理器引用
    public GameObject Flasher; // 闪光效果预制体
    private AudioSource Audio; // 音频源组件
    internal bool FollowPlayer = false; // 是否跟随玩家的标志
    internal bool StartMove = true; // 是否开始移动的标志
    internal bool AddOnce = true; // 是否只添加一次组件的标志

    void Start()
    {
        // 初始化引用
        Player = GameObject.FindGameObjectWithTag("Player"); // 查找玩家对象
        Boolean = GameObject.Find("Controller"); // 查找控制器对象
        Manager = GameObject.Find("GameManager"); // 查找游戏管理器
        Audio = GetComponent<AudioSource>(); // 获取音频源组件
        Audio.volume = 0.5f; // 设置音量为50%
    }
    
    void FixedUpdate()
    {
        if(Boolean.GetComponent<BooleanManager>().GameStart == true) // 如果游戏已开始
        {
            if (FollowPlayer == true) // 如果应该跟随玩家
            {
                if (StartMove == true) // 如果应该开始移动
                {
                    if (AddOnce == true) // 如果是第一次
                    {
                        // 添加Rigidbody2D组件并设置无重力
                        this.gameObject.AddComponent<Rigidbody2D>();
                        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                        AddOnce = false; // 标记已添加过
                    }
                    // 给钻石添加向上的力，使其上升
                    this.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 15);
                    StartCoroutine(BackPoint()); // 启动回到起点的协程
                }
            }
        }
    }
    
    void Update()
    {
        if (Boolean.GetComponent<BooleanManager>().GameStart == true) // 如果游戏已开始
        {
            if (StartMove == false) // 如果不应该继续初始移动
            {
                // 钻石朝向玩家移动
                transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, 15f * Time.deltaTime);
            }
        }
    }
    
    // 控制钻石上升一段时间后开始向玩家移动的协程
    IEnumerator BackPoint()
    {
        if (Boolean.GetComponent<BooleanManager>().GameStart == true)
        {
            yield return new WaitForSeconds(0.4f); // 等待0.4秒
            StartMove = false; // 停止初始移动，开始朝玩家移动
        }
    }
    
    // 处理钻石销毁的协程
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.8f); // 等待0.8秒
        Destroy(this.gameObject); // 销毁钻石对象
    }
    
    // 处理碰撞事件
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 如果碰到玩家
        {
            // 增加游戏管理器中的分数值
            Manager.GetComponent<GameManager>().ValureLevel += 0.75f;
            // 生成闪光效果
            Instantiate(Flasher, transform.position, transform.rotation);
            Audio.Play(); // 播放收集音效
            StartCoroutine(Destroy()); // 启动销毁协程
        }
        if (other.CompareTag("RoadDetections")) // 如果碰到道路检测器
        {
            FollowPlayer = true; // 开始跟随玩家
        }
    }
}
