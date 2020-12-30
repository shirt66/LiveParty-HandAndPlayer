using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations;
using UnityEngine.Timeline;
using Sirenix.OdinInspector;
using System;

/**********************************************
* 模块名: HandAsset.cs
* 功能描述：手牌信息配置文件类
* 模块编写人：高陈翊
***********************************************/

//手牌类型的枚举
public enum HandType
{
    Active,
    Passive,
    Special
}


[CreateAssetMenu(menuName = "HandAsset",fileName = "handAsset",order = 2)]

public class HandAsset : ScriptableObject
{
    #region 手牌通用属性

    [Header("手牌图标")]
    [HorizontalGroup("Split", 55, LabelWidth = 70)]
    [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
    public Texture Icon;

    [Header("手牌名称")]
    public string handName;

    [Header("手牌描述")]
    public string handDescription;

    [EnumToggleButtons]
    [Header("手牌类型")]
    public HandType handType;

    [Header("手牌对应的模型预制体")]
    public GameObject handModel;

    [Header("手牌效果动画animation接口")]
    public Animation animation;

    [Header("手牌效果动画Timeline接口")]
    public TimelineClip timeline;

    #endregion 手牌通用属性

    [Header("主动手牌的属性")]
    public AttributesActive attributesActive;

    [Header("被动手牌对玩家各属性的影响")]
    public AttributesPassive attributesPassive;

}


[Serializable]
public class AttributesActive {

    [Header("主动手牌造成的攻击力伤害")]
    public int damage;

    [Header("主动手牌提供的防御减伤值")]
    public int defense;
}


[Serializable]
public class AttributesPassive {

    [Header("被动手牌对玩家力量的影响值")]
    public int strengthInfluence;

    [Header("被动手牌对玩家敏捷的影响值")]
    public int dexterityInfluence;

    [Header("被动手牌对玩家体格的影响值")]
    public int constitutionInfluence;

    [Header("被动手牌对玩家智力的影响值")]
    public int intelligenceInfluence;

    [Header("被动手牌对玩家感知的影响值")]
    public int widomInfluence;

    [Header("被动手牌对玩家魅力的影响值")]
    public int charismaInfluence;
}

