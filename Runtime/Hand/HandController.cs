using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Animations;
using UnityEditor;
using System.Linq;

/**********************************************
* 模块名: HandController.cs
* 功能描述：进行所有手牌的管理，手牌与玩家之间的管理以及与卡牌相关的一些方法接口
* 模块编写人：高陈翊
***********************************************/

public class HandController : MonoBehaviour
{

    public static HandController Instance;

    private void Awake()
    {
        Instance = this;
    }


    [Header("所有配置手牌")]
    public List<HandAsset> handAssets;

    [Header("手牌信息的背景图片")]
    public Sprite backImag;

    [HideInInspector]
    //卡牌与玩家的一一对应的字典,前者为玩家的编号，后者为玩家拥有的所有手牌在手牌容器中的编号
    public Dictionary<int, List<int>> handAssetDictionary = new Dictionary<int, List<int>>();

    #region 手牌容器的一些方法接口

    /// <summary>
    /// 通过手牌名称获取手牌编号
    /// </summary>
    /// <param name="handName">手牌名称</param>
    /// <returns>手牌编号，不在容器中返回-1</returns>
    private int GetIndex(string handName)
    {
        for (int i = 0; i < handAssets.ToArray().Length; i++)
        {
            if (handAssets[i].name.Equals(handName))
            {
                return i;
            }
        }
        //Debug.LogError("未在容器中找到名称为" + handName + "的卡牌");
        return -1;
    }

    /// <summary>
    /// 判断某一编号的手牌是否在手牌容器中
    /// </summary>
    /// <param name="handIndex">手牌编号</param>
    /// <returns></returns>
    private bool IsHandIn(int handIndex)
    {
        if (handAssets[handIndex] == null)
        {
            Debug.LogError("未在容器中找到编号为" + handIndex + "的卡牌");
            return false;
        }
        else
            return true;
    }

    /// <summary>
    /// 判断某一名称的手牌是否在手牌容器中
    /// </summary>
    /// <param name="handName">手牌名称</param>
    /// <returns></returns>
    private bool IsHandIn(string handName)
    {
        foreach(HandAsset handAsset in handAssets)
        {
            if (handAsset.name.Equals(handName))
                return true;
        }
        Debug.LogError("未在容器中找到名称为" + handName + "的卡牌");
        return false;
    }

    /// <summary>
    /// 播放手牌的动画
    /// </summary>
    /// <param name="handIndex">卡牌在容器中的编号</param>
    public void PlayHandAnimation(int handIndex)
    {
        if (!IsHandIn(handIndex))
        {
            Debug.LogError("未找到对应卡牌");
            return;
        }
        else
        {
            var animation = handAssets[handIndex].handModel.GetComponent<Animation>();
            if (animation)
            {
                animation.Play();
            }
            else
            {
                Debug.Log("该卡牌没有对应动画");
            }
        }
    }

    /// <summary>
    /// 显示手牌的信息，包括描述和属性加成
    /// </summary>
    /// <param name="handIndex"></param>
    public GameObject ShowHandInfo(int handIndex)
    {
        if (!IsHandIn(handIndex))
        {
            Debug.LogError("未找到对应卡牌");
            return null;
        }
        else
        {
            UISet uiSet = FindObjectOfType<UISet>();
            Vector3 imagPos = new Vector3(0, 0, 0);
            float posXBegin = -420;
            float posYBegin = 200;
            float intervalX = 150;
            float intervalY = 50;
            HandAsset handAsset = handAssets[handIndex];
            //创建背景图
            GameObject imagObj = uiSet.CreateImgAndReturn("BackImage", backImag, imagPos);

            //创建手牌标题文本
            string nameText = handAsset.handName.ToString();
            float namePosX = posXBegin;
            float namePosY = posYBegin;
            uiSet.CreateText("手牌名称", nameText, new Vector3(namePosX, posYBegin, 0), 60, FontStyle.Bold, 1, 800, imagObj.transform);


            //卡牌主动属性的文本内容
            string damageText = "攻击力:" + handAsset.damage.ToString();
            string defenseText = "防御力:" + handAsset.defense.ToString();
            float activePosY = namePosY - intervalY * 2;     //主动文本的y轴位置
            float damgePosX = posXBegin;      //伤害数值的x轴位置
            float defensePosX = posXBegin + intervalX + 10;  //防御数值的x轴位置
            uiSet.CreateText("攻击力数值", damageText, new Vector3(damgePosX, activePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);
            uiSet.CreateText("防御力数值", defenseText, new Vector3(defensePosX, activePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);

            //卡牌被动属性的文本内容
            string strText = "力量:" + handAsset.strengthInfluence.ToString();
            string dexText = "敏捷:" + handAsset.dexterityInfluence.ToString();
            string conText = "体格:" + handAsset.constitutionInfluence.ToString();
            string intText = "智力:" + handAsset.intelligenceInfluence.ToString();
            string widText = "感知:" + handAsset.widomInfluence.ToString();
            string chaText = "魅力:" + handAsset.charismaInfluence.ToString();
            float passivePosY = activePosY - intervalY;
            float strPosX = posXBegin;
            float dexPosX = posXBegin + intervalX;
            float conPosX = posXBegin + intervalX * 2;
            float intPosX = posXBegin + intervalX * 3;
            float widPosX = posXBegin + intervalX * 4;
            float chaPosX = posXBegin + intervalX * 5;
            uiSet.CreateText("力量加成数值", strText, new Vector3(strPosX, passivePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);
            uiSet.CreateText("敏捷加成数值", dexText, new Vector3(dexPosX, passivePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);
            uiSet.CreateText("体格加成数值", conText, new Vector3(conPosX, passivePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);
            uiSet.CreateText("智力加成数值", intText, new Vector3(intPosX, passivePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);
            uiSet.CreateText("感知加成数值", widText, new Vector3(widPosX, passivePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);
            uiSet.CreateText("魅力加成数值", chaText, new Vector3(chaPosX, passivePosY, 0), 30, FontStyle.Bold, 1, 200, imagObj.transform);

            //创建手牌文本描述
            Vector3 descripPos = new Vector3(posXBegin, passivePosY - intervalY * 2, 0);
            string description = handAsset.handDescription;
            uiSet.CreateText("Description", description, descripPos, 30, FontStyle.Normal, 1, 800, imagObj.transform);

            return imagObj;
        }
    }

    [Button("将所有手牌存入容器")]
    /// <summary>
    /// 获取文件夹中的所有手牌文件并存入容器
    /// </summary>
    public void SendAllToHandAssets()
    {
        Debug.Log("SendAllToHandAssets");
        string fullPath = "Assets/Resources/Hands";
        string[] guids = AssetDatabase.FindAssets("t:HandAsset", new string[] { fullPath});
        Debug.Log(guids[0]);
        List<string> paths = new List<string>();
        guids.ToList().ForEach(m => paths.Add(AssetDatabase.GUIDToAssetPath(m)));
        handAssets.Clear();
        paths.ForEach(p => handAssets.Add(AssetDatabase.LoadAssetAtPath(p, typeof(HandAsset)) as HandAsset));
    }

    [Button("清除容器中所有手牌")]
    /// <summary>
    /// 清除容器中的所有手牌文件
    /// </summary>
    public void ClearAllInHandAssets()
    {
        handAssets.Clear();
    }

    #endregion 手牌容器的一些方法接口


    #region 手牌玩家字典的一些方法接口

    /// <summary>
    /// 将指定编号的卡牌添加给玩家，一同存入字典中
    /// </summary>
    /// <param name="handIndex">卡牌在容器中的序号</param>
    /// <param name="playerIndex">玩家编号</param>
    public void AddHandToPlayer(int playerIndex, int handIndex)
    {
        if (!IsHandIn(handIndex))
            return;
        if(IsPlayerIn(playerIndex))
        {
            handAssetDictionary[playerIndex].Add(handIndex);
        }
        else
        {
            List<int> handList = new List<int> { handIndex };
            handAssetDictionary.Add(playerIndex, handList);
        }
    }

    /// <summary>
    /// 将指定名称的卡牌添加给玩家，一同存入字典中
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <param name="handName">卡牌名称</param>
    public void AddHandToPlayer(int playerIndex,string handName)
    {
        if (!IsHandIn(handName))
            return;
        AddHandToPlayer(playerIndex, GetIndex(handName));
    }

    /// <summary>
    /// 将一连串名称的卡牌添加给玩家，一同存入字典中
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <param name="handNames">卡牌的名称数组</param>
    public void AddHandsToPlayer(int playerIndex,string[] handNames)
    {
        foreach(string handName in handNames)
        {
            AddHandToPlayer(playerIndex, handName);
        }
    }

    /// <summary>
    /// 将指定编号的卡牌从玩家身上删除
    /// </summary>
    /// <param name="handIndex">卡牌在容器中的序号</param>
    /// <param name="palyerIndex">玩家编号</param>
    public void RemoveHandFromPlayer(int handIndex,int playerIndex)
    {
        if(!IsPlayerIn(playerIndex))
        {
            Debug.LogError("字典里不存在此玩家的卡牌信息");
            return;
        }

        if (!IsHandIn(handIndex))
            return;

        handAssetDictionary[playerIndex].Remove(handIndex);
    }

    /// <summary>
    /// 将指定名称的卡牌从玩家身上删除
    /// </summary>
    /// <param name="handName">玩家名称</param>
    /// <param name="playerIndex">卡牌编号</param>
    public void RemoveHandFromPlayer(string handName,int playerIndex)
    {
        if (!IsHandIn(handName))
            return;

        RemoveHandFromPlayer(GetIndex(handName), playerIndex);
    }

    /// <summary>
    /// 将一连串名称的卡牌从玩家身上删除
    /// </summary>
    /// <param name="handNames">卡牌名称数组</param>
    /// <param name="playerIndex">玩家编号</param>
    public void RemoveHandsFromPlayer(string[] handNames,int playerIndex)
    {
        foreach (string handName in handNames)
        {
            RemoveHandFromPlayer(handName, playerIndex);
        }
    }

    /// <summary>
    /// 获得某一玩家身上的所有手牌编号
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns>手牌编号列表</returns>
    public List<int> GetHandsOfPlayer(int playerIndex)
    {
        if (IsPlayerIn(playerIndex))
        {
            return handAssetDictionary[playerIndex];
        }
        else
        {
            Debug.LogError("该玩家不在字典中");
            return null;
        }
    }

    /// <summary>
    /// 判断某编号的玩家是否在字典中
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    /// <returns></returns>
    public bool IsPlayerIn(int playerIndex)
    {
        return handAssetDictionary.ContainsKey(playerIndex);
    }

    /// <summary>
    /// 显示某一玩家的所有卡牌图标
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    public void ShowAllHandsIcon(int playerIndex)
    {
        Debug.Log("显示所有卡牌图标");
        if (!IsPlayerIn(playerIndex))
        {
            Debug.LogError("该玩家不在字典中");
            return;
        }
        else
        {
            //每次生成时都将现有ui清除后再生成
            HideAllHandsIcon();
            Canvas uiCanvas = FindObjectOfType<Canvas>();
            UISet uiSet = FindObjectOfType<UISet>();
            List<int> handIndexs = GetHandsOfPlayer(playerIndex);
            //用于存不同值的handIndex及其数量
            Dictionary<int, int> handIndexsType = new Dictionary<int, int>();
            //将卡牌编号以及对应数量存入字典中
            foreach(int handIndex in handIndexs)
            {
                if (handIndexsType.ContainsKey(handIndex))
                    handIndexsType[handIndex] += 1;
                else
                    handIndexsType.Add(handIndex, 1);
            }

            //当前此玩家拥有的所有卡牌的Icon
            List<Texture> handIcons = new List<Texture>();
            //对应卡牌Icon的数量
            List<int> handCounts = new List<int>();
            foreach (var handIndexType in handIndexsType)
            {
                handIcons.Add(handAssets[handIndexType.Key].Icon);
                handCounts.Add(handIndexType.Value);
            }
            var handsCount = handIcons.ToArray().Length;
            float height = -430f;
            float originWeight = -840f;
            for (int i = 0; i < handsCount; i++)
            {
                var iconPos = new Vector3(originWeight + i * 110, height, 0);
                var textPos = new Vector3(originWeight + i * 110, height - 50, 0);

                //生成游戏中的图标
                Texture2D texture = handIcons[i] as Texture2D;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                uiSet.CreateImage("Icon" + i, sprite, iconPos, 100, 100);

                //生成图标下的技术文本
                string handText = handCounts[i].ToString();
                uiSet.CreateText("TextCount" + i, handText, textPos, 24, FontStyle.Bold, 1, 30);
            }
        }
    }

    /// <summary>
    /// 消除某一玩家的所有卡牌图标
    /// </summary>
    public void HideAllHandsIcon()
    {
        Image[] images = FindObjectsOfType<Image>();
        Text[] texts = FindObjectsOfType<Text>();
        foreach(Image image in images)
        {
            if (image.name.Contains("Icon"))
                Destroy(image.gameObject);
        }
        foreach (Text text in texts)
        {
            if (text.name.Contains("Icon"))
                Destroy(text.gameObject);
        }
    }

    /// <summary>
    /// 显示某一玩家的所有卡牌模型
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    public void ShowAllHandsModel(int playerIndex)
    {
        Debug.Log("显示所有卡牌模型");
        if (!IsPlayerIn(playerIndex))
        {
            Debug.LogError("该玩家不在字典中");
            return;
        }
        else
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            Camera playerCamera = playerController.players[playerIndex].playerCamera;
            Debug.Log("获取玩家相机完成");
            List<int> handIndexs = GetHandsOfPlayer(playerIndex);
            Debug.Log("获取卡牌完成");
            //用于存不同值的handIndex及其数量，Key为handIndex，Value为数量
            Dictionary<int, int> handIndexsType = new Dictionary<int, int>();
            //将卡牌编号以及对应数量存入字典中
            foreach (int handIndex in handIndexs)
            {
                if (handIndexsType.ContainsKey(handIndex))
                    handIndexsType[handIndex] += 1;
                else
                    handIndexsType.Add(handIndex, 1);
            }
            //当前玩家拥有的所有卡牌的模型
            List<GameObject> handModels = new List<GameObject>();
            //对应卡牌Icon的数量
            List<int> handCounts = new List<int>();
            //对应卡牌的编号
            List<int> modelHandIndexs = new List<int>();
            foreach (var handIndexType in handIndexsType)
            {
                handModels.Add(handAssets[handIndexType.Key].handModel);
                handCounts.Add(handIndexType.Value);
                modelHandIndexs.Add(handIndexType.Key);
            }
            var handsCount = handModels.ToArray().Length;
            for(int i = 0; i < handsCount; i++)
            {
                var centerPos = playerCamera.transform.position;
                var interval = 0.5f;
                var modelPos = new Vector3(0, 0, 0);
                //卡牌为偶数
                if (handsCount%2 == 0)
                {
                    Debug.Log("卡牌为偶数");
                    if (i + 1 < handsCount / 2)
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z - interval * ((handsCount / 2) - (i + 1) + 0.5f));
                    else if (i + 1 == handsCount / 2)
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z - interval / 2);
                    else if (i + 1 == handsCount / 2 + 1)
                    {
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z + interval / 2);
                    }
                    else
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z + interval * (i - (handsCount / 2) + 1 - 0.5f));
                }
                //卡牌为奇数
                else
                {
                    if (i + 1 < handsCount / 2 + 1)
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z - interval * ((handsCount / 2) + 1 - (i + 1)));
                    else if (i + 1 > handsCount / 2 + 1)
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z + interval * ((i + 1) - (handsCount / 2 + 1)));
                    else
                        modelPos = new Vector3(centerPos.x - 1.8f, centerPos.y, centerPos.z);
                }
                //生成模型
                GameObject cardModelObj = new GameObject("HandModels" + i, typeof(ModelHandIndex));
                cardModelObj.transform.SetParent(playerController.players[playerIndex].transform);
                cardModelObj.transform.position = modelPos;
                cardModelObj.GetComponent<ModelHandIndex>().handIndex = modelHandIndexs[i];
                GameObject.Instantiate(handModels[i], modelPos, Quaternion.Euler(0, 0, 0), cardModelObj.transform);
            }

        }
    }

    /// <summary>
    /// 隐藏某一玩家的所有卡牌模型
    /// </summary>
    /// <param name="playerIndex">玩家编号</param>
    public void HideAllHandsMoedel(int playerIndex)
    {
        Debug.Log("隐藏所有卡牌模型");
        if (!IsPlayerIn(playerIndex))
        {
            Debug.LogError("该玩家不在字典中");
            return;
        }
        else
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            Player player = playerController.players[playerIndex];
            Transform[] childs = player.gameObject.transform.GetComponentsInChildren<Transform>();
            foreach(Transform child in childs)
            {
                if (child.name.Contains("HandModels"))
                    Destroy(child.gameObject);
            }
        }
    }

    #endregion 手牌玩家字典的一些方法接口
}
