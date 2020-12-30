using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
* 模块名: Player.cs
* 功能描述：存储玩家的属性值
* 模块编写人：高陈翊
***********************************************/

public class Player : MonoBehaviour
{
    [Header("玩家名称")]
    public string playerName;

    [Header("玩家ID")]
    public string playerID;

    //[Header("玩家当前力量")]
    //public int strength;

    //[Header("玩家当前敏捷")]
    //public int dexterity;

    //[Header("玩家当前体格")]
    //public int constitution;

    //[Header("玩家当前智力")]
    //public int intelligence;

    //[Header("玩家当前感知")]
    //public int widom;

    //[Header("玩家但前魅力")]
    //public int charisma;

    [Header("玩家当前属性")]
    public Attributes attributes;

    [Header("玩家对应相机")]
    public Camera playerCamera;

}

[Serializable]
public class Attributes
{
    [Header("玩家当前力量")]
    public int strength;

    [Header("玩家当前敏捷")]
    public int dexterity;

    [Header("玩家当前体格")]
    public int constitution;

    [Header("玩家当前智力")]
    public int intelligence;

    [Header("玩家当前感知")]
    public int widom;

    [Header("玩家但前魅力")]
    public int charisma;
}
