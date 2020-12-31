﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/**********************************************
* 模块名: PlayerController.cs
* 功能描述：管理游戏内所有玩家，提供玩家属性值更新的接口
* 模块编写人：高陈翊
***********************************************/

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("当前游戏内所有玩家")]
    public List<Player> players;

    [Header("玩家预制体")]
    public GameObject playerPrefab;

    [Button("创建玩家")]   
    /// <summary>
    /// 在游戏中创建玩家的GameObject，并加入玩家容器中
    /// </summary>
    public void CreatePlayer()
    {
        GameObject playerObj = GameObject.Instantiate(playerPrefab);
        Player[] playerObjs = FindObjectsOfType<Player>();
        playerObj.name = "Player" + playerObjs.Length.ToString();
        Player player = playerObj.GetComponent<Player>();
        players.Add(player);
    }

    /// <summary>
    /// 判断某玩家编号是否存在
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns></returns>
    bool isPlayerIn(int playerIndex)
    {
        if (players[playerIndex] == null)
        {
            Debug.LogError("未在容器中找到编号为" + playerIndex + "的玩家");
            return false;
        }
        return true;
    }

    #region 玩家属性值相关的接口

    /// <summary>
    /// 通过玩家编号获取其力量值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>力量值,若玩家不存在返回0</returns>
    public int GetStrength(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return 0;
        return players[playerIndex].attributes.strength;
    }

    /// <summary>
    /// 通过玩家编号获取其敏捷值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>敏捷值,若玩家不存在返回0</returns>
    public int GetDexterity(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return 0;
        return players[playerIndex].attributes.dexterity;
    }

    /// <summary>
    /// 通过玩家编号获取其体格值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>敏捷值,若玩家不存在返回0</returns>
    public int GetConstitution(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return 0;
        return players[playerIndex].attributes.constitution;
    }

    /// <summary>
    /// 通过玩家编号获取其智力值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>智力值,若玩家不存在返回0</returns>
    public int GetIntelligence(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return 0;
        return players[playerIndex].attributes.intelligence;
    }

    /// <summary>
    /// 通过玩家编号获取其感知值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>感知值,若玩家不存在返回0</returns>
    public int GetWidom(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return 0;
        return players[playerIndex].attributes.wisdom;
    }

    /// <summary>
    /// 通过玩家编号获取其魅力值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>魅力值,若玩家不存在返回0</returns>
    public int GetCharisma(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return 0;
        return players[playerIndex].attributes.charisma;
    }

    /// <summary>
    /// 根据所有玩家的当前手牌改变所有玩家的属性数值
    /// </summary>
    public void ChangeAttributes()
    {
        //HandController handController = FindObjectOfType<HandController>().GetComponent<HandController>();
        //var playerHandDictionary = handController.handAssetDictionary;
        //var handAssets = handController.handAssets;
        //foreach (var playerHand in playerHandDictionary)
        //{
        //    foreach(var handIndex in playerHand.Value)
        //    {
        //        players[playerHand.Key].strength += handAssets[handIndex].strengthInfluence;
        //        players[playerHand.Key].dexterity += handAssets[handIndex].dexterityInfluence;
        //        players[playerHand.Key].constitution += handAssets[handIndex].constitutionInfluence;
        //        players[playerHand.Key].intelligence += handAssets[handIndex].intelligenceInfluence;
        //        players[playerHand.Key].widom += handAssets[handIndex].widomInfluence;
        //        players[playerHand.Key].charisma += handAssets[handIndex].charismaInfluence;
        //    }
        //}
        HandController handController = FindObjectOfType<HandController>().GetComponent<HandController>();
        var playerHandDictionary = handController.handAssetDictionary;
        foreach (var playerHand in playerHandDictionary)
        {
            ChangeAttribute(playerHand.Key);
        }
    }

    /// <summary>
    /// 根据特定玩家编号的所有手牌改变其所有属性值
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    public void ChangeAttribute(int playerIndex)
    {
        if (!isPlayerIn(playerIndex))
            return;

        HandController handController = FindObjectOfType<HandController>().GetComponent<HandController>();
        if (!handController.IsPlayerIn(playerIndex))
        {
            Debug.Log("编号为" + playerIndex + "的玩家目前没有手牌");
            return;
        }

        List<int> handIndexs = handController.GetHandsOfPlayer(playerIndex);
        foreach (int handIndex in handIndexs)
        {
            players[playerIndex].attributes.strength += handController.handAssets[handIndex].attributesPassive.strengthInfluence;
            players[playerIndex].attributes.dexterity += handController.handAssets[handIndex].attributesPassive.dexterityInfluence;
            players[playerIndex].attributes.constitution += handController.handAssets[handIndex].attributesPassive.constitutionInfluence;
            players[playerIndex].attributes.intelligence += handController.handAssets[handIndex].attributesPassive.intelligenceInfluence;
            players[playerIndex].attributes.wisdom += handController.handAssets[handIndex].attributesPassive.widomInfluence;
            players[playerIndex].attributes.charisma += handController.handAssets[handIndex].attributesPassive.charismaInfluence;
        }
    }

    /// <summary>
    /// 根据判定类型获取改判定属性值最大的玩家编号
    /// </summary>
    /// <param name="judgeType">判定类型</param>
    /// <returns>玩家编号</returns>
    public int GetMaxAttribute(string judgeType)
    {
        int index;
        switch (judgeType)
        {
            case "Strength":
                index = GetMaxStrength();
                break;
            case "Dexterity":
                index =GetMaxDexterity();
                break;
            case "Constitution":
                index = GetMaxConstitution();
                break;
            case "Intelligence":
                index = GetMaxIntelligence();
                break;
            case "Wisdom":
                index = GetMaxWisdom();
                break;
            case "Charisma":
                index = GetMaxCharisma();
                break;
            default:
                index = -1;
                Debug.LogError("不存在名为" + judgeType + "的判定");
                break;
        }
        return index;
    }

    /// <summary>
    /// 获取玩家中力量值最大的玩家编号
    /// </summary>
    /// <returns>玩家编号</returns>
    private int GetMaxStrength()
    {
        int maxIndex = 0;
        for (int i = 1; i < players.ToArray().Length; i++)
        {
            if (players[i].attributes.strength > players[maxIndex].attributes.strength)
                maxIndex = i;
        }
        return maxIndex;
    }

    /// <summary>
    /// 获取玩家中敏捷值最大的玩家编号
    /// </summary>
    /// <returns>玩家编号</returns>
    private int GetMaxDexterity()
    {
        int maxIndex = 0;
        for(int i = 1; i < players.ToArray().Length; i++)
        {
            if (players[i].attributes.dexterity > players[maxIndex].attributes.dexterity)
                maxIndex = i;
        }
        return maxIndex;
    }

    /// <summary>
    /// 获取玩家中体格值最大的玩家编号
    /// </summary>
    /// <returns>玩家编号</returns>
    private int GetMaxConstitution()
    {
        int maxIndex = 0;
        for (int i = 1; i < players.ToArray().Length; i++)
        {
            if (players[i].attributes.constitution > players[maxIndex].attributes.constitution)
                maxIndex = i;
        }
        return maxIndex;
    }

    /// <summary>
    /// 获取玩家中智力值最大的玩家编号
    /// </summary>
    /// <returns>玩家编号</returns>
    private int GetMaxIntelligence()
    {
        int maxIndex = 0;
        for (int i = 1; i < players.ToArray().Length; i++)
        {
            if (players[i].attributes.intelligence > players[maxIndex].attributes.intelligence)
                maxIndex = i;
        }
        return maxIndex;
    }

    /// <summary>
    /// 获取玩家中感知值最大的玩家编号
    /// </summary>
    /// <returns>玩家编号</returns>
    private int GetMaxWisdom()
    {
        int maxIndex = 0;
        for (int i = 1; i < players.ToArray().Length; i++)
        {
            if (players[i].attributes.wisdom > players[maxIndex].attributes.wisdom)
                maxIndex = i;
        }
        return maxIndex;
    }

    /// <summary>
    /// 获取玩家中魅力值最大的玩家编号
    /// </summary>
    /// <returns>玩家编号</returns>
    private int GetMaxCharisma()
    {
        int maxIndex = 0;
        for (int i = 1; i < players.ToArray().Length; i++)
        {
            if (players[i].attributes.charisma > players[maxIndex].attributes.charisma)
                maxIndex = i;
        }
        return maxIndex;
    }


    #endregion 玩家属性值相关的接口





}
