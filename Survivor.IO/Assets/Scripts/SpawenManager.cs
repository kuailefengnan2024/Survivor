/*
 * SpawenManager.cs
 * 敌人生成管理器类
 * 这个类负责游戏中敌人的生成和控制
 * 包括生成位置、生成时机、敌人数量控制以及根据游戏进度调整敌人类型
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawenManager : MonoBehaviour
{
    GameObject[] Enemys; // 场景中所有敌人的数组
    public int EnemysLength; // 当前场景中敌人的数量
    [Header("Spawener Managers")]
    public UIManager UI; // UI管理器引用
    public GameManager Managers; // 游戏管理器引用

    [Header("Enemys Manager")]
    public GameObject Zombie; // 基础僵尸敌人预制体
    public GameObject[] ZombieLevel; // 不同等级的僵尸敌人预制体数组

    [Header("Spawents Poins")]
    public GameObject SpawenLocalisation; // 敌人生成的父对象容器
    public GameObject Spawen1; // 第一个生成点
    public GameObject Spawen2; // 第二个生成点
    public GameObject Spawen3; // 第三个生成点
    public GameObject Spawen4; // 第四个生成点

    [Header("Boolean Manager")]
    internal bool SpawenPos1 = false; // 第一个生成点的状态标志
    internal bool SpawenPos2 = false; // 第二个生成点的状态标志
    internal bool SpawenPos3 = false; // 第三个生成点的状态标志
    internal bool SpawenPos4 = false; // 第四个生成点的状态标志
    internal bool ManagerPose = false; // 管理生成暂停的标志
    internal bool MakeIt = false; // 控制是否开始生成的标志
    [Header("Vectors Controller")]
    internal Vector3 Pos; // 用于存储生成位置的向量
 
    void OnEnable()
    {
        // 组件启用时开始生成敌人并控制生成节奏
        StartCoroutine(SpaweningManager()); // 启动敌人生成协程
        StartCoroutine(stopSpawning()); // 启动生成暂停控制协程
    }
    
    void Update()
    {
        // 每帧将MakeIt设为true，允许生成敌人
        MakeIt = true;
    }
    
    // 控制敌人生成的停止和开始的协程
    public IEnumerator stopSpawning()
    {
        yield return new WaitForEndOfFrame(); // 等待当前帧结束
        if(MakeIt == true)
        {
            yield return new WaitForSeconds(1f); // 等待1秒
            ManagerPose = true; // 暂停生成
            yield return new WaitForSeconds(10f); // 暂停10秒
            ManagerPose = false; // 恢复生成
            StartCoroutine(SpaweningManager()); // 重新启动生成协程
        }
        StartCoroutine(stopSpawning()); // 循环执行本协程
    }

    // 敌人生成的主要协程
    public IEnumerator SpaweningManager()
    {
        Debug.Log("SpaweningManager");
        yield return new WaitForEndOfFrame(); // 等待当前帧结束
        if (ManagerPose == false) // 如果没有暂停生成
        {
            if(MakeIt == true) // 如果允许生成
            {
                // 获取场景中所有敌人并记录数量
                Enemys = GameObject.FindGameObjectsWithTag("Enemy");
                EnemysLength = Enemys.Length;
                if (EnemysLength < 100) // 限制最大敌人数量为100
                {
                    // 在第一个生成点生成基础僵尸，位置有随机偏移
                    (Instantiate(Zombie, new Vector3(Spawen1.transform.position.x, Spawen1.transform.position.y + Random.Range(-5, 5), Spawen1.transform.position.z), Spawen1.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    
                    // 根据当前游戏进度(CurrentReload)生成不同等级的僵尸
                    if (Managers.CurrentReload == 0) // 开始阶段
                    {
                        // 生成0级僵尸
                        (Instantiate(ZombieLevel[0], new Vector3(Spawen2.transform.position.x + Random.Range(-5, 5), Spawen2.transform.position.y, Spawen2.transform.position.z), Spawen2.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                    else if (Managers.CurrentReload == 1) // 第一次重装后
                    {
                        // 生成1级僵尸
                        (Instantiate(ZombieLevel[1], new Vector3(Spawen2.transform.position.x + Random.Range(-5, 5), Spawen2.transform.position.y, Spawen2.transform.position.z), Spawen2.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                    else if (Managers.CurrentReload == 2) // 第二次重装后
                    {
                        // 生成2级僵尸
                        (Instantiate(ZombieLevel[2], new Vector3(Spawen2.transform.position.x + Random.Range(-5, 5), Spawen2.transform.position.y, Spawen2.transform.position.z), Spawen2.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                    else if (Managers.CurrentReload == 3) // 第三次重装后
                    {
                        // 生成3级僵尸
                        (Instantiate(ZombieLevel[3], new Vector3(Spawen2.transform.position.x + Random.Range(-5, 5), Spawen2.transform.position.y, Spawen2.transform.position.z), Spawen2.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                    else if (Managers.CurrentReload == 4) // 第四次重装后
                    {
                        // 生成4级僵尸
                        (Instantiate(ZombieLevel[4], new Vector3(Spawen2.transform.position.x + Random.Range(-5, 5), Spawen2.transform.position.y, Spawen2.transform.position.z), Spawen2.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                    else if (Managers.CurrentReload >= 4) // 第四次重装之后
                    {
                        // 继续生成最高级(4级)僵尸
                        (Instantiate(ZombieLevel[4], new Vector3(Spawen2.transform.position.x + Random.Range(-5, 5), Spawen2.transform.position.y, Spawen2.transform.position.z), Spawen2.transform.rotation) as GameObject).transform.SetParent(SpawenLocalisation.transform);
                    }
                }
                yield return new WaitForSeconds(0.9f); // 等待0.9秒后再次生成
            }
            StartCoroutine(SpaweningManager()); // 循环执行生成协程
        }
    }
}
