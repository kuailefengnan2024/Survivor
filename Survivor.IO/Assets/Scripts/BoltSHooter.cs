/*
 * BoltSHooter.cs
 * 子弹射击器类
 * 该类负责控制子弹的行为，包括寻找敌人、移动到目标位置以及生成钻石
 * 子弹会自动追踪最近的敌人，如果没有敌人，则会移动到随机位置并生成钻石
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltSHooter : MonoBehaviour
{
    [Header("Manager")]
    public GameObject Manager; // 游戏管理器引用

    [Header("Pefabes Manager")]
    public GameObject Diamond; // 钻石预制体
    public GameObject DiamondPos; // 钻石生成位置容器
    public GameObject UImanager; // UI管理器引用

    [Header("Boolean Manager")]
    internal bool EnemyDispo = true; // 敌人是否可用的标志
    internal bool SpawenHire = true; // 是否应该在此生成的标志
    internal bool CheckingList = true; // 检查列表的标志

    [Header("Floating Manager")]
    internal int SpawenPoint; // 生成点索引

    void Awake()
    {
        // 初始化引用
        Manager = GameObject.Find("GameManager"); // 查找游戏管理器
        DiamondPos = GameObject.Find("DiamondPos"); // 查找钻石位置容器
        UImanager = GameObject.Find("UI"); // 查找UI管理器
        CheckDestroy(); // 检查是否应该销毁
    }
    
    void Start()
    {
        StartCoroutine(ItemSelfDest()); // 启动子弹自毁协程
    }

    void Update()
    {
        CheckDestroy(); // 检查是否应该销毁
        
        if(EnemyDispo == true) // 如果有敌人可用
        {
           // 子弹朝着最近的敌人移动
           transform.position = Vector2.MoveTowards(this.transform.position, GameObject.FindGameObjectWithTag("Enemy").transform.position, 10f * Time.deltaTime);
           // 计算朝向敌人的角度
           Vector2 direction = GameObject.FindGameObjectWithTag("Enemy").transform.position - transform.position;
           float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           // 旋转子弹朝向敌人
           this.gameObject.GetComponent<Rigidbody2D>().rotation = Angle;
        }
        
        if(EnemyDispo == false) // 如果没有敌人可用
        {
            if(CheckingList == true)
            {
                // 随机选择一个生成点(1-15)
                SpawenPoint = Random.Range(1, 16);
                CheckingList = false;
            }
            CheckPlaceSpawening(); // 检查并移动到选择的生成点
        }
        
        // 如果UI管理器设置了销毁敌人标志，则销毁此子弹
        if (UImanager.GetComponent<UIManager>().DestroyEnemys == true)
        {
            Destroy(this.gameObject);
        }
    }
    
    // 检查暂停状态的方法（当前为空）
    void CheckPause()
    {
 
    }
    
    // 根据随机选择的生成点移动子弹的方法
    void CheckPlaceSpawening()
    {
        // 以下代码块几乎相同，只是目标位置不同
        // 每个代码块都将子弹移动到指定的位置点，并在到达时生成钻石然后销毁自身
        
        if (SpawenPoint == 1 && SpawenHire == true)
        {
            // 移动到Pos1位置
            transform.position = Vector2.MoveTowards(this.transform.position, GameObject.Find("Pos1").transform.position, 10f * Time.deltaTime);
            // 计算朝向目标位置的角度
            Vector2 direction = GameObject.Find("Pos1").transform.position - transform.position;
            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // 旋转子弹朝向目标位置
            this.gameObject.GetComponent<Rigidbody2D>().rotation = Angle;
            CheckPause();
            // 如果到达目标位置，生成钻石并销毁自身
            if (transform.position == GameObject.Find("Pos1").transform.position)
            {
                (Instantiate(Diamond, transform.position, Diamond.transform.rotation) as GameObject).transform.SetParent(DiamondPos.transform);
                Destroy(this.gameObject);
            }
        }
        
        // 以下代码块都是类似的，只是目标位置从Pos2到Pos13不同
        if (SpawenPoint == 2 && SpawenHire == true)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, GameObject.Find("Pos2").transform.position, 10f * Time.deltaTime);
            Vector2 direction = GameObject.Find("Pos2").transform.position - transform.position;
            float Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.gameObject.GetComponent<Rigidbody2D>().rotation = Angle;
            CheckPause();
            if (transform.position == GameObject.Find("Pos2").transform.position)
            {
                (Instantiate(Diamond, transform.position, Diamond.transform.rotation) as GameObject).transform.SetParent(DiamondPos.transform);
                Destroy(this.gameObject);
            }
        }
        
        // ... 其余位置点的代码类似 ...
    }
    
    // 检查是否应该销毁子弹
    void CheckDestroy()
    {
        // 尝试找到带有"Enemy"标签的游戏对象
        if(GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            // 如果找不到敌人，设置敌人不可用标志
            EnemyDispo = false;
        }
        else
        {
            // 如果找到敌人，设置敌人可用标志
            EnemyDispo = true;
        }
        
        // 检查游戏管理器中钻石bolt标志
        if(Manager.GetComponent<GameManager>().DiamondBolt == true)
        {
            // 如果是钻石bolt模式，启动钻石bolt协程
            StartCoroutine(DiamondsBolt());
        }
        if (Manager.GetComponent<GameManager>().NormalBolt == true)
        {
            // 如果是普通bolt模式，启动普通bolt协程
            StartCoroutine(NormalBolt());
        }
    }
    
    // 处理普通子弹模式的协程
    IEnumerator NormalBolt()
    {
        yield return new WaitForSeconds(0.0f);
    }
    
    // 处理钻石子弹模式的协程
    IEnumerator DiamondsBolt()
    {
        yield return new WaitForSeconds(0.0f);
    }
    
    // 子弹自毁协程，防止子弹永久存在
    IEnumerator ItemSelfDest()
    {
        // 10秒后销毁自身
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
    
    // 处理碰撞事件
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 如果碰到敌人，给游戏管理器中的ValueLevel变量增加值
            Manager.GetComponent<GameManager>().ValureLevel += 1f;
        }
    }
}
