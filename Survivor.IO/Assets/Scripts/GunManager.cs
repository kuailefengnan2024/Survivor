/*
 * GunManager.cs
 * 枪械管理器类
 * 这个类负责控制枪械武器的行为，包括子弹生成、发射位置和音效播放
 * 它定期在六个不同位置生成子弹，实现枪械的自动射击功能
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [Header("Manager Player")]
    public GameObject Player; // 玩家对象引用
    public AudioSource Audio; // 射击音效的音频源

    [Header("Localisation PosDeb")]
    public GameObject Pos0; // 第一个子弹发射位置
    public GameObject Pos1; // 第二个子弹发射位置
    public GameObject Pos2; // 第三个子弹发射位置
    public GameObject Pos3; // 第四个子弹发射位置
    public GameObject Pos4; // 第五个子弹发射位置
    public GameObject Pos5; // 第六个子弹发射位置

    [Header("Bullet Effect")]
    public GameObject Bullet0; // 第一个子弹预制体
    public GameObject Bullet1; // 第二个子弹预制体
    public GameObject Bullet2; // 第三个子弹预制体
    public GameObject Bullet3; // 第四个子弹预制体
    public GameObject Bullet4; // 第五个子弹预制体
    public GameObject Bullet5; // 第六个子弹预制体

    [Header("Container")]
    public GameObject ContainerBolt; // 子弹的父容器对象

    void Start()
    {
        StartCoroutine(SpaweningBolt()); // 启动子弹生成协程
    }
    
    void Update()
    {
        // 注释掉的代码可能是使枪械跟随玩家移动的逻辑
        //transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, 100f * Time.deltaTime);
    }
    
    // 子弹生成协程，每隔2秒在六个位置生成子弹
    IEnumerator SpaweningBolt()
    {
        yield return new WaitForEndOfFrame(); // 等待当前帧结束
        Audio.Play(); // 播放射击音效
        
        // 在六个不同位置生成子弹，并将它们设置为ContainerBolt的子对象
        (Instantiate(Bullet0, Pos0.transform.position, transform.rotation) as GameObject).transform.SetParent(ContainerBolt.transform);
        (Instantiate(Bullet1, Pos1.transform.position, transform.rotation) as GameObject).transform.SetParent(ContainerBolt.transform);
        (Instantiate(Bullet2, Pos2.transform.position, transform.rotation) as GameObject).transform.SetParent(ContainerBolt.transform);
        (Instantiate(Bullet3, Pos3.transform.position, transform.rotation) as GameObject).transform.SetParent(ContainerBolt.transform);
        (Instantiate(Bullet4, Pos4.transform.position, transform.rotation) as GameObject).transform.SetParent(ContainerBolt.transform);
        (Instantiate(Bullet5, Pos5.transform.position, transform.rotation) as GameObject).transform.SetParent(ContainerBolt.transform);
        
        yield return new WaitForSeconds(2f); // 等待2秒
        StartCoroutine(SpaweningBolt()); // 循环执行本协程
    }
}
