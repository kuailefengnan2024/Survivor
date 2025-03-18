/*
 * PlayerManager.cs
 * 玩家管理器类
 * 这个类负责控制玩家角色的行为，包括位置更新、武器射击、生命值管理和碰撞检测等功能
 * 与GameManager和其他管理器协同工作，处理玩家相关的游戏逻辑
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerManager : MonoBehaviour
{
    [Header("Manager Controller")]
    public GameManager Manager; // 游戏管理器引用
    public BooleanManager Bool; // 布尔值管理器引用


    [Header("Spawen Transformer")]
    public AudioSource EffectArrow; // 箭矢音效的音频源
    public AudioSource AudioHeat; // 受击音效的音频源
    public GameObject Death; // 死亡对象，可能是死亡特效或动画
    public GameObject SpawenShoot; // 射击生成点，子弹/武器从这里发射
    public GameObject ContainerWap; // 武器容器对象
    public Vector3 Offest; // UI位置偏移量
    private Vector3 rb; // 用于平滑阻尼移动的参考向量

    [Header("Containers")]
    public GameObject Bolts; // 子弹/弹药预制体
    public GameObject UI; // 玩家UI对象

    [Header("Boolean manager")]
    internal bool Deaths = true; // 死亡状态标志


    void Start()
    {
        // 目前为空，可能在未来添加初始化代码
    }
    
    void Update()
    {
        if(Bool.GameStart == true)
        {
            // 使UI跟随玩家位置，添加偏移量并使用平滑阻尼
            UI.transform.position = Vector3.SmoothDamp(transform.position, transform.position + Offest, ref rb, 0);
            // 更新射击生成点位置，跟随玩家
            SpawenShoot.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            // 调用射击方法
            HitBolts();
        }
        
        // 根据生命值控制死亡对象的显示
        if(Manager.HealthBar.fillAmount == 0)
        {
            Death.SetActive(true); // 生命值为0时显示死亡效果
        }
        else
        {
            Death.SetActive(false); // 否则隐藏死亡效果
        }
    }
    
    // 处理射击/发射子弹的方法
    void HitBolts()
    {
        if (Bool.GameStart == true)
        {
            if (Manager.AvailabelWeapon == true) // 检查武器是否可用
            {
                if (Manager.SpawnObject == true) // 检查是否可以生成对象
                {
                    // 在射击点生成子弹，并将其设置为武器容器的子对象
                    (Instantiate(Bolts, SpawenShoot.transform.position, Quaternion.identity) as GameObject).transform.SetParent(ContainerWap.transform);
                    EffectArrow.Play(); // 播放射击音效
                }
            }
        }
    }
    
    // 触发器碰撞检测，处理玩家与敌人的碰撞
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Bool.GameStart == true)
        {
            if (other.CompareTag("Enemy")) // 检查碰撞对象是否为敌人
            {
                AudioHeat.Play(); // 播放受击音效
                Manager.Health -= 0.5f; // 减少玩家生命值
            }
        }
    }

}
