/*
 * 广告管理器
 * 
 * 这个类负责处理游戏中的广告显示和奖励逻辑
 * 包括插页式广告(Interstitial)和激励视频广告(Rewarded Video)
 * 
 * 主要功能：
 * 1. 显示插页式广告
 * 2. 通过观看广告获取游戏内奖励(宝石、金币、能量)
 * 3. 在编辑器和发布版本中提供不同的处理逻辑
 * 
 * 注意：编辑器模式下直接发放奖励，不显示广告
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    // 单例模式，用于全局访问广告管理器
    public static AdsManager Ads;
    // 游戏机制管理器引用，用于更新UI显示
    ManagerMecanique managerMecanique;
    
    private void Start()
    {
        // 在场景中查找并获取游戏机制管理器
        managerMecanique = FindObjectOfType<ManagerMecanique>();
    }
    
    /// <summary>
    /// 显示插页式广告
    /// 如果玩家未购买去广告功能(ads不等于1)，则显示广告
    /// </summary>
    public void Showintertitial()
    {
        if (PlayerPrefs.GetInt("ads")!=1)
        {
            Advertisements.Instance.ShowInterstitial();
        }
    }
    
    /// <summary>
    /// 获取宝石奖励
    /// 编辑器模式下直接给予10个宝石
    /// 发布版本中需要观看激励视频
    /// </summary>
    public void GetGems()
    {
#if UNITY_EDITOR
        // 编辑器模式下直接给予10个宝石奖励
        int GemsInt;
        GemsInt = PlayerPrefs.GetInt("gems");
        GemsInt += 10;
        PlayerPrefs.SetInt("gems", GemsInt);
        managerMecanique.InitText(); // 更新UI显示

#else
        // 发布版本中显示激励视频广告，完成后调用CompleteMethodGems方法
        Advertisements.Instance.ShowRewardedVideo(CompleteMethodGems);

#endif
    }
    
    /// <summary>
    /// 宝石奖励的回调方法
    /// 当玩家完成观看激励视频后被调用
    /// </summary>
    /// <param name="completed">是否完成观看</param>
    /// <param name="advertiser">广告提供商</param>
    private void CompleteMethodGems(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            // 玩家完成观看，给予10个宝石奖励
            int GemsInt;
            GemsInt = PlayerPrefs.GetInt("gems");
            GemsInt += 10;
            PlayerPrefs.SetInt("gems", GemsInt);
            managerMecanique.InitText(); // 更新UI显示
        }
        else
        {
            //未完成观看，不给予奖励
        }
    }
    
    /// <summary>
    /// 获取金币奖励
    /// 编辑器模式下直接给予30个金币
    /// 发布版本中需要观看激励视频
    /// </summary>
    public void GetCoin()
    {
#if UNITY_EDITOR
        // 编辑器模式下直接给予30个金币奖励
        int CoinsInt;
        CoinsInt = PlayerPrefs.GetInt("coins");
        CoinsInt += 30;
        PlayerPrefs.SetInt("coins", CoinsInt);
        managerMecanique.InitText(); // 更新UI显示

#else
        // 发布版本中显示激励视频广告，完成后调用CompleteMethodCoins方法
        Advertisements.Instance.ShowRewardedVideo(CompleteMethodCoins);

#endif
    }
    
    /// <summary>
    /// 金币奖励的回调方法
    /// 当玩家完成观看激励视频后被调用
    /// </summary>
    /// <param name="completed">是否完成观看</param>
    /// <param name="advertiser">广告提供商</param>
    private void CompleteMethodCoins(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            // 玩家完成观看，给予30个金币奖励
            int CoinsInt;
            CoinsInt = PlayerPrefs.GetInt("coins");
            CoinsInt +=30;
            PlayerPrefs.SetInt("coins", CoinsInt);
            managerMecanique.InitText(); // 更新UI显示
        }
        else
        {
            //未完成观看，不给予奖励
        }
    }
    
    /// <summary>
    /// 获取能量奖励(flash)
    /// 编辑器模式下直接给予10点能量
    /// 发布版本中需要观看激励视频
    /// </summary>
    public void GetPower()
    {
#if UNITY_EDITOR
        // 编辑器模式下直接给予10点能量奖励
        int CoinsInt;
        CoinsInt = PlayerPrefs.GetInt("flash");
        CoinsInt += 10;
        PlayerPrefs.SetInt("flash", CoinsInt);
        managerMecanique.InitText(); // 更新UI显示

#else
        // 发布版本中显示激励视频广告，完成后调用CompleteMethodPower方法
        Advertisements.Instance.ShowRewardedVideo(CompleteMethodPower);

#endif

    }
    
    /// <summary>
    /// 能量奖励的回调方法
    /// 当玩家完成观看激励视频后被调用
    /// </summary>
    /// <param name="completed">是否完成观看</param>
    /// <param name="advertiser">广告提供商</param>
    private void CompleteMethodPower(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            // 玩家完成观看，给予10点能量奖励
            int CoinsInt;
            CoinsInt = PlayerPrefs.GetInt("flash");
            CoinsInt += 10;
            PlayerPrefs.SetInt("flash", CoinsInt);
            managerMecanique.InitText(); // 更新UI显示
        }
        else
        {
            //未完成观看，不给予奖励
        }
    }
    
    /// <summary>
    /// 显示通用激励视频广告
    /// 这是一个通用方法，可以用于将来扩展其他类型的奖励
    /// </summary>
    public void ShowrewardVideo()
    {
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);
        // 本地函数作为回调
        void CompleteMethod(bool completed, string advertiser)
        {
            Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
            if (completed == true)
            {
                //给予奖励(这里没有具体实现)
            }
            else
            {
                //未完成观看，不给予奖励
            }
        }
    }
}
