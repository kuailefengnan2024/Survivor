/*











*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 球体力量添加脚本
/// 该脚本用于在球体碰撞到特定区域时，向球体施加力量
/// </summary>
public class AddBallForce : MonoBehaviour
{
    // 目标球体对象引用
    public GameObject Ball;
    
    // 力量方向控制变量
    public bool Left;    // 控制是否向左施加力量
    public bool Right;   // 控制是否向右施加力量
    public bool Up;      // 控制是否向上施加力量
    public bool Down;    // 控制是否向下施加力量

    /// <summary>
    /// 当有物体进入触发器区域时调用此方法
    /// </summary>
    /// <param name="collision">进入触发器的碰撞体</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查进入触发器的物体是否带有"ball"标签
        if (collision.CompareTag("ball"))
        {
            // 如果Left为true，则向球体施加向左的力量
            if(Left == true)
            {
                // 向球体添加一个向左的力，大小为500
                Ball.GetComponent<Rigidbody2D>().AddForce(-transform.right * 500);
            }
            
            // 如果Right为true，则向球体施加向右的力量
            if (Right == true)
            {
                // 向球体添加一个向右的力，大小为500
                Ball.GetComponent<Rigidbody2D>().AddForce(transform.right * 500);
            }
            
            // 如果Up为true，则向球体施加向上的力量
            if (Up == true)
            {
                // 向球体添加一个向上的力，大小为500
                Ball.GetComponent<Rigidbody2D>().AddForce(transform.up * 500);
            }
            
            // 如果Down为true，则向球体施加向下的力量
            if (Down == true)
            {
                // 向球体添加一个向下的力，大小为500
                Ball.GetComponent<Rigidbody2D>().AddForce(-transform.up * 500);
            }
        }
    }
}
