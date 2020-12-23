using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************
* 模块名: PlayerViewControl.cs
* 功能描述：控制玩家的视角移动以及视线交互
* 模块编写人：高陈翊
***********************************************/

public class PlayerViewControl : MonoBehaviour
{
    public Camera playerCamera;
    private Vector3 camAngle;
    private Transform cameraTrans;
    private HandController handController;
    private int rayLength = 100;
    private bool isShowHandInfo = true;
    private GameObject handInfo;

    private void Start()
    {
        cameraTrans = playerCamera.transform;
        camAngle = cameraTrans.eulerAngles;
        handController = FindObjectOfType<HandController>();
    }

    private void Update()
    {
        CameraRotate();
        PlayerRayCast();
    }

    /// <summary>
    /// 用于控制摄像机的旋转
    /// </summary>
    private void CameraRotate()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
        camAngle.x -= x;
        camAngle.y += y;
        cameraTrans.eulerAngles = camAngle;
    }

    /// <summary>
    /// 用于创建玩家视线的射线并进行射线检测
    /// </summary>
    private void PlayerRayCast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 如果射线与平面碰撞，打印碰撞物体信息  
            Debug.Log("碰撞对象: " + hit.collider.name);
            // 在场景视图中绘制射线  
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            if (hit.collider.gameObject.GetComponentInParent<ModelHandIndex>())
            {
                int handIndex = hit.collider.gameObject.GetComponentInParent<ModelHandIndex>().handIndex;
                if (isShowHandInfo)
                {
                    handInfo = handController.ShowHandInfo(handIndex);
                    isShowHandInfo = false;
                }
            }
            else
            {
                isShowHandInfo = true;
                Destroy(handInfo);
            }
        }
        else
        {
            isShowHandInfo = true;
            Destroy(handInfo);
        }
    }

}
