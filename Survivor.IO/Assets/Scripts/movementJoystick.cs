/*
 * movementJoystick.cs
 * 移动摇杆控制器类
 * 该类负责处理游戏中的虚拟摇杆逻辑，包括触摸输入、移动控制和方向指示
 * 它使玩家能够通过触摸/鼠标拖动来控制角色移动方向
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.EventSystems.EventTrigger;

public class movementJoystick : MonoBehaviour
{
    public GameObject ArrowDirecteur; // 方向箭头，指示移动方向
    public GameObject Gun; // 枪械对象，会根据摇杆方向旋转
    public GameObject joystick; // 摇杆内部的可移动手柄
    public GameObject joystickBG; // 摇杆的背景/边界
    public Vector2 joystickVec; // 摇杆的方向向量，表示移动方向
    private Vector2 joystickTouchPos; // 触摸/点击摇杆的初始位置
    private Vector2 joystickOriginalPos; // 摇杆的原始/默认位置
    private float joystickRadius; // 摇杆的活动半径


    void Update()
    {
        // 根据摇杆向量控制方向箭头的显示和旋转
        if(joystickVec.x == 0 && joystickVec.y == 0)
        {
            ArrowDirecteur.SetActive(false); // 如果摇杆没有移动，隐藏方向箭头
        }
        else
        {
            ArrowDirecteur.SetActive(true); // 摇杆移动时显示方向箭头
            // 根据摇杆方向计算并设置箭头的旋转角度
            ArrowDirecteur.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-joystickVec.x, joystickVec.y) * 180 / Mathf.PI);
        }
        
        // 如果枪械对象激活，也根据摇杆方向旋转枪械
        if(Gun.activeSelf == true)
        {
            Gun.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-joystickVec.x, joystickVec.y) * 180 / Mathf.PI);
        }
    }
    
    void Start()
    {
        // 初始化摇杆的原始位置和活动半径
        joystickOriginalPos = joystickBG.transform.position; // 记录摇杆背景的初始位置
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4; // 计算摇杆活动半径为背景高度的1/4
    }
    
    // 处理按下摇杆的事件
    public void PointerDown()
    {
        // 当玩家点击屏幕时，将摇杆移动到点击位置
        joystick.transform.position = Input.mousePosition; // 移动摇杆手柄到鼠标位置
        joystickBG.transform.position = Input.mousePosition; // 移动摇杆背景到鼠标位置
        joystickTouchPos = Input.mousePosition; // 记录触摸位置
    }
    
    // 处理拖动摇杆的事件
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData; // 转换事件数据为指针事件数据
        Vector2 dragePos = pointerEventData.position; // 获取当前拖动位置
        joystickVec = (dragePos - joystickTouchPos).normalized; // 计算并归一化方向向量

        float joystickDist = Vector2.Distance(dragePos, joystickTouchPos); // 计算拖动距离
        
        // 限制摇杆手柄在活动半径内
        if(joystickDist < joystickRadius)
        {
            // 如果拖动距离小于半径，直接移动到拖动位置
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            // 如果拖动距离超过半径，限制在半径范围内
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }
    
    // 处理释放摇杆的事件
    public void PointerUp()
    {
        joystickVec = Vector2.zero; // 重置方向向量为零
        joystick.transform.position = joystickOriginalPos; // 恢复摇杆手柄到原始位置
        joystickBG.transform.position = joystickOriginalPos; // 恢复摇杆背景到原始位置
    }
}
