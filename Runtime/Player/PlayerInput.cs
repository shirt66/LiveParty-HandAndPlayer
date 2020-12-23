using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
* 模块名: PlayerInput.cs
* 功能描述：进行一些玩家与卡牌相关功能的测试，仅为测试脚本
* 模块编写人：高陈翊
***********************************************/

public class PlayerInput : MonoBehaviour
{
    public PlayerController playerController;
    public HandController handController;
    private bool isModelShowed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        handController = FindObjectOfType<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < handController.handAssets.ToArray().Length; i++)
            {
                handController.AddHandToPlayer(0, i);
            }
            handController.AddHandToPlayer(0, 1);
            Debug.Log("添加卡牌完成");
            playerController.ChangeAttributes();
            handController.ShowAllHandsIcon(0);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isModelShowed)
            {
                handController.ShowAllHandsModel(0);
                isModelShowed = true;
            }
            else
            {
                handController.HideAllHandsMoedel(0);
                isModelShowed = false;
            }
        }

    }
}
