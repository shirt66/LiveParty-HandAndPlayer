using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**********************************************
* 模块名: uiSet.cs
* 功能描述：进行ui的生成和管理，一般挂载在Canvas上
* 模块编写人：高陈翊
***********************************************/

public class UISet : MonoBehaviour
{
    private Canvas uiCanvas;
    
    void Start()
    {
        uiCanvas = GetComponent<Canvas>();
    }


    #region 创建Image的方法

    /// <summary>
    /// 在uiCanvas下创建图片,默认为sprite大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    public void CreateImage(string name,Sprite sprite,Vector3 position)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position);
    }

    /// <summary>
    /// 在uiCanvas下创建图片,可自主设置大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <param name="width">图片宽度</param>
    /// <param name="height">图片高度</param>
    public void CreateImage(string name, Sprite sprite, Vector3 position, float width, float height)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position, width, height);
    }

    /// <summary>
    /// 在已有Canvas中的物体下创建图片,默认为Sprite本身大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <param name="parent">图片父物体</param>
    public void CreateImage(string name, Sprite sprite, Vector3 position, Transform parent)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position, parent);
    }

    /// <summary>
    /// 在已有Canvas中的物体下创建图片，可自主设置大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <param name="parent">图片父物体</param>
    /// <param name="width">图片宽度</param>
    /// <param name="height">图片高度</param>
    public void CreateImage(string name, Sprite sprite, Vector3 position, Transform parent, float width, float height)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position, parent, width, height);
    }

    /// <summary>
    /// 在uiCanvas下创建图片并返回游戏物体，默认为sprite大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <returns>图片游戏物体</returns>
    public GameObject CreateImgAndReturn(string name, Sprite sprite, Vector3 position)
    {
        GameObject imageObj = new GameObject(name, typeof(Image));
        imageObj.GetComponent<Image>().raycastTarget = false;
        imageObj.transform.SetParent(uiCanvas.gameObject.transform);
        imageObj.GetComponent<Image>().sprite = sprite;
        imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
        imageObj.GetComponent<RectTransform>().localPosition = position;
        return imageObj;
    }

    /// <summary>
    /// 在uiCanvas下创建图片并返回物体,可自主设置大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <param name="width">图片宽度</param>
    /// <param name="height">图片高度</param>
    /// <returns>图片游戏物体</returns>
    public GameObject CreateImgAndReturn(string name, Sprite sprite, Vector3 position, float width, float height)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position);
        imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        return imageObj;
    }

    /// <summary>
    /// 在已有Canvas中的物体下创建图片并返回游戏物体，默认为Sprite本身大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <param name="parent">图片父物体</param>
    /// <returns>图片游戏物体</returns>
    public GameObject CreateImgAndReturn(string name, Sprite sprite, Vector3 position, Transform parent)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position);
        imageObj.transform.SetParent(parent);
        imageObj.GetComponent<RectTransform>().localPosition = position;
        return imageObj;
    }

    /// <summary>
    /// 在已有Canvas中的物体下创建图片并返回游戏物体，可自主设置大小
    /// </summary>
    /// <param name="name">图片名称</param>
    /// <param name="sprite">图片内容</param>
    /// <param name="position">图片位置</param>
    /// <param name="parent">图片父物体</param>
    /// <param name="width">图片宽度</param>
    /// <param name="height">图片高度</param>
    /// <returns>图片游戏物体</returns>
    public GameObject CreateImgAndReturn(string name, Sprite sprite, Vector3 position, Transform parent, float width, float height)
    {
        GameObject imageObj = CreateImgAndReturn(name, sprite, position, parent);
        imageObj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        return imageObj;
    }

    #endregion 创建Image的方法


    #region 创建Text的方法

    /// <summary>
    /// 在uiCanvas下创建文本，默认文本类型为Arial
    /// </summary>
    /// <param name="name">文本名称</param>
    /// <param name="text">文本内容</param>
    /// <param name="position">文本位置</param>
    /// <param name="fontSize">字体大小</param>
    /// <param name="fontStyle">字体形式</param>
    /// <param name="lineSpace">行间距</param>
    /// <param name="width">每行的宽度</param>
    public void CreateText(string name,string text,Vector3 position,int fontSize,FontStyle fontStyle,float lineSpace,float width)
    {
        GameObject textObj = CreateTextAndReturn(name, text, position, fontSize, fontStyle, lineSpace, width);
    }

    /// <summary>
    /// 在已有Canvas中的物体下创建文本
    /// </summary>
    /// <param name="name">文本名称</param>
    /// <param name="text">文本内容</param>
    /// <param name="position">文本位置</param>
    /// <param name="fontSize">字体大小</param>
    /// <param name="fontStyle">字体形式</param>
    /// <param name="lineSpace">行间距</param>
    /// <param name="width">每行的宽度</param>
    /// <param name="parent">文本的父物体</param>
    public void CreateText(string name, string text, Vector3 position, int fontSize, FontStyle fontStyle, float lineSpace, float width,Transform parent)
    {
        GameObject textObj = CreateTextAndReturn(name, text, position, fontSize, fontStyle, lineSpace, width, parent);       
    }

    /// <summary>
    /// 在uiCanvas下创建文本并返回游戏物体，默认文本类型为Arial
    /// </summary>
    /// <param name="name">文本名称</param>
    /// <param name="text">文本内容</param>
    /// <param name="position">文本位置</param>
    /// <param name="fontSize">字体大小</param>
    /// <param name="fontStyle">字体形式</param>
    /// <param name="lineSpace">行间距</param>
    /// <param name="width">每行的宽度</param>
    /// <returns>文本游戏物体</returns>
    public GameObject CreateTextAndReturn(string name, string text, Vector3 position, int fontSize, FontStyle fontStyle, float lineSpace, float width)
    {
        GameObject textObj = new GameObject(name, typeof(Text));
        textObj.transform.SetParent(uiCanvas.gameObject.transform);
        textObj.GetComponent<Text>().text = text;
        RectTransform recTrans = textObj.GetComponent<RectTransform>();
        recTrans.localPosition = position;
        recTrans.sizeDelta = new Vector2(width, 40);
        textObj.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textObj.GetComponent<Text>().fontStyle = fontStyle;
        textObj.GetComponent<Text>().fontSize = fontSize;

        //添加组件可以随字体大小自动调节高度
        textObj.AddComponent<ContentSizeFitter>();
        textObj.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        recTrans.pivot = new Vector2(0, 1);

        return textObj;
    }

    /// <summary>
    /// 在已有Canvas中的物体下创建文本并返回游戏物体，默认文本类型为Arial
    /// </summary>
    /// <param name="name">文本名称</param>
    /// <param name="text">文本内容</param>
    /// <param name="position">文本位置</param>
    /// <param name="fontSize">字体大小</param>
    /// <param name="fontStyle">字体形式</param>
    /// <param name="lineSpace">行间距</param>
    /// <param name="width">每行的宽度</param>
    /// <param name="parent">文本的父物体</param>
    /// <returns>文本游戏物体</returns>
    public GameObject CreateTextAndReturn(string name, string text, Vector3 position, int fontSize, FontStyle fontStyle, float lineSpace, float width, Transform parent)
    {
        GameObject textObj = CreateTextAndReturn(name, text, position, fontSize, fontStyle, lineSpace, width);
        textObj.transform.SetParent(parent);
        textObj.GetComponent<RectTransform>().localPosition = position;
        return textObj;
    }

    #endregion 创建Text的方法

}
