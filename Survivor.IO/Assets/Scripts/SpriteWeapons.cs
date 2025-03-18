/*
 * SpriteWeapons.cs
 * 武器精灵管理器类
 * 这个类负责管理游戏中所有武器的视觉和功能表现
 * 包括武器GameObject、图标、名称和描述文本的管理
 * 以及武器激活/停用控制
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpriteWeapons : MonoBehaviour
{
   public ManagerWeapons managerWeapons; // 武器管理器引用
    [Header("Weapons Container")]
    public GameObject ProtecteurGreen; // 绿色保护装置武器对象
    public GameObject DroneA; // A型无人机武器对象
    public GameObject DroneB; // B型无人机武器对象
    public GameObject DroneC; // C型无人机武器对象
    public GameObject BrikWall; // 砖墙武器对象
    public GameObject Gun; // 枪械武器对象
    public GameObject SprineA; // A型旋转武器对象
    public GameObject SprineB; // B型旋转武器对象
    public GameObject Ball; // 球类武器对象
    public GameObject SalsaRanshom; // 火焰杯武器对象
    public GameObject FireGase; // 火焰气体武器对象
    public GameObject Aguel; // 针类武器对象

    [Header("Controller GameObjects")]
    public GameObject DroneContainer; // 无人机容器对象
    public GameObject DroneObjectsContainer; // 无人机对象容器
    public GameObject SprineContainer; // 旋转武器容器对象

    [Header("Drones Objects")]
    public GameObject DroneObjectA; // A型无人机具体对象
    public GameObject DroneObjectB; // B型无人机具体对象
    public GameObject DroneObjectC; // C型无人机具体对象

    [Header("WeaponsIcons")]
    public Sprite SProtecteurGreen; // 绿色保护装置图标
    public Sprite SDroneA; // A型无人机图标
    public Sprite SDroneB; // B型无人机图标
    public Sprite SDroneC; // C型无人机图标
    public Sprite SBrikWall; // 砖墙武器图标
    public Sprite SGun; // 枪械武器图标
    public Sprite SSprineA; // A型旋转武器图标
    public Sprite SSprineB; // B型旋转武器图标
    public Sprite SBall; // 球类武器图标
    public Sprite SSalsaRanshone; // 火焰杯武器图标
    public Sprite SFireGase; // 火焰气体武器图标
    public Sprite SAguel; // 针类武器图标

    [Header("NamesWeapons")]
    internal string NProtecteurGreen; // 绿色保护装置名称
    internal string NDroneA; // A型无人机名称
    internal string NDroneB; // B型无人机名称
    internal string NDroneC; // C型无人机名称
    internal string NBrikeWall; // 砖墙武器名称
    internal string NGun; // 枪械武器名称
    internal string NSprineA; // A型旋转武器名称
    internal string NSprineB; // B型旋转武器名称
    internal string NBall; // 球类武器名称
    internal string NSalsaRanshon; // 火焰杯武器名称
    internal string NFireGase; // 火焰气体武器名称
    internal string Naguel; // 针类武器名称

    [Header("DescritionWeapons")]
    internal string DProtecteurGreen; // 绿色保护装置描述
    internal string DDroneA; // A型无人机描述
    internal string DDroneB; // B型无人机描述
    internal string DDroneC; // C型无人机描述
    internal string DBrikWall; // 砖墙武器描述
    internal string DGun; // 枪械武器描述
    internal string DSprineA; // A型旋转武器描述
    internal string DSprineB; // B型旋转武器描述
    internal string DBall; // 球类武器描述
    internal string DSalsaRanshon; // 火焰杯武器描述
    internal string DFireGase; // 火焰气体武器描述
    internal string DAguel; // 针类武器描述

    void Start()
    {
        ManagerTexting(); // 初始化所有武器的文本描述
    }
    void Update()
    {
        
    }
    public void DesactivateAll()
    {
        ProtecteurGreen.SetActive(false);
        DroneA.SetActive(false);
        DroneB.SetActive(false);
        DroneC.SetActive(false);
        BrikWall.SetActive(false);
        Gun.SetActive(false);
        SprineA.SetActive(false);
        SprineB.SetActive(false);
        Ball.SetActive(false);
        SalsaRanshom.SetActive(false);
        FireGase.SetActive(false);
        Aguel.SetActive(false);
        DroneObjectA.SetActive(false);
        DroneObjectB.SetActive(false);
        DroneObjectC.SetActive(false);
        DroneContainer.SetActive(false);
        DroneObjectsContainer.SetActive(false);
        SprineContainer.SetActive(false);

    }
    public void Onaguel()
    {
        managerWeapons.AddImages(SAguel);

        Aguel.SetActive(true);
    }
    public void OnFireGase()
    {
        managerWeapons.AddImages(SFireGase);
        FireGase.SetActive(true);
    }
    public void OnSalsaRanshon()
    {
        managerWeapons.AddImages(SSalsaRanshone);
        SalsaRanshom.SetActive(true);
    }
    public void OnBall()
    {
        managerWeapons.AddImages(SBall);
        Ball.SetActive(true);
    }
    public void OnSprineB()
    {
        managerWeapons.AddImages(SSprineB);
        SprineContainer.SetActive(true);
        SprineB.SetActive(true);
    }
    public void OnSprineA()
    {
        managerWeapons.AddImages(SSprineA);
        SprineContainer.SetActive(true);
        SprineA.SetActive(true);
    }
    public void OnGun()
    {
        managerWeapons.AddImages(SGun);
        Gun.SetActive(true);
    }
    public void OnProtecteurGreen()
    {
        managerWeapons.AddImages(SProtecteurGreen);
        ProtecteurGreen.SetActive(true);
    }
    public void OnBrikWall()
    {
        managerWeapons.AddImages(SBrikWall);
        BrikWall.SetActive(true);
    }
    public void OnDroneA()
    {
        managerWeapons.AddImages(SDroneA);
        DroneContainer.SetActive(true);
        DroneObjectsContainer.SetActive(true);
        DroneObjectA.SetActive(true);
        DroneA.SetActive(true);
    }
    public void OnDroneB()
    {
        managerWeapons.AddImages(SDroneB);
        DroneContainer.SetActive(true);
        DroneObjectsContainer.SetActive(true);
        DroneObjectB.SetActive(true);
        DroneB.SetActive(true);
    }
    public void OnDroneC()
    {
        managerWeapons.AddImages(SDroneC);
        DroneContainer.SetActive(true);
        DroneObjectsContainer.SetActive(true);
        DroneObjectC.SetActive(true);
        DroneC.SetActive(true);
    }
    void ManagerTexting()
    {
        NProtecteurGreen = "Protecteur Zz";
        DProtecteurGreen = "This weapon Help you protect Your Self From Zombies thats Attac You";
        /////
        NDroneA = "Drone Monster";
        DDroneA = "This Drone Help You Attace Enemys with bolt";
        /////
        NDroneB = "Drone Electricaly";
        DDroneB = "This Drone Make Other Drones If Available With Multie Shoootings";
        /////
        NDroneC = "Drone Magicaly";
        DDroneC = "This Drone Make Player Move Eays Beside Enemys By Killing Them";
        /////
        NBrikeWall = " Briking Wall";
        DBrikWall = "This Kill Enemys By Breking Them Fulledly";
        /////
        NGun = " M45-7A";
        DGun = " You Shoot Enemys By this Gun On HeadShoot Directly To KillThem With Six Bults";
        /////
        NSprineA = "Springing H1A";
        DSprineA = "Help You To Kill Enemys By Shooting Rotatly On Head";
        /////
        NSprineB = "Protecteur H1B";
        DSprineB = " Make Enemys Move Down By Touching Them With H1B";
        /////
        NBall = "Ball Fooot";
        DBall = " Footbal Ball Heating Heads Of Enemys To Destroy Them Infinitly";
        /////
        NSalsaRanshon = "Fire Cup";
        DSalsaRanshon = " Firing Enemys And Kill Them Exactly Wheen The Fire Exploded";
        /////
        NFireGase = "Fire Gase";
        DFireGase = "Fire Gase Firing Enets With 100% Hydroilcation Type Of Fires";
        ////
        Naguel = "Aguel";
        DAguel = " Aguel Like Firebale But wuth Arrow Shoting On Head with HeadShoot";
    }
}
