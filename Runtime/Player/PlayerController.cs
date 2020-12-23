using System.Collections;
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
    /// 根据所有玩家的当前手牌改变所有玩家的属性数值
    /// </summary>
    public void ChangeAttributes()
    {
        HandController handController = FindObjectOfType<HandController>().GetComponent<HandController>();
        var playerHandDictionary = handController.handAssetDictionary;
        var handAssets = handController.handAssets;
        foreach (var playerHand in playerHandDictionary)
        {
            foreach(var handIndex in playerHand.Value)
            {
                players[playerHand.Key].strength += handAssets[handIndex].strengthInfluence;
                players[playerHand.Key].dexterity += handAssets[handIndex].dexterityInfluence;
                players[playerHand.Key].constitution += handAssets[handIndex].constitutionInfluence;
                players[playerHand.Key].intelligence += handAssets[handIndex].intelligenceInfluence;
                players[playerHand.Key].widom += handAssets[handIndex].widomInfluence;
                players[playerHand.Key].charisma += handAssets[handIndex].charismaInfluence;
            }
        }
    }



    


}
