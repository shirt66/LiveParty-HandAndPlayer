using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;

/**********************************************
* 模块名: HandAssetEditorWindow.cs
* 功能描述：手牌文件管理的编辑器窗口编写
* 模块编写人：高陈翊
***********************************************/

public class HandAssetEditorWindow : OdinMenuEditorWindow
{
    private string mAssetPath = "Assets/Resources/Hands";

    [MenuItem("Tool/手牌文件管理")]
    private static void OpenWindow()
    {
        var window = GetWindow(typeof(HandAssetEditorWindow));
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(supportsMultiSelect: true);
        tree.DefaultMenuStyle.IconSize = 28.00f;
        tree.Config.DrawSearchToolbar = true;

        //添加卡牌预览的界面
        HandOverview.Instance.UpdateHandAssetOverview();
        tree.Add("HandAssets", new HandTable(HandOverview.Instance.AllHandAssets));

        //添加所有的HandAsset的配置界面
        tree.AddAllAssetsAtPath("HandAssets", "Assets/Resources/Hands", typeof(HandAsset),true,true);

        //给HandAsset配置界面添加Icon
        tree.EnumerateTree().AddIcons<HandAsset>(x => x.Icon);

        return tree;
    }

    protected override void OnBeginDrawEditors()
    {
        var selected = this.MenuTree.Selection.FirstOrDefault();
        var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

        //使用当前选定菜单项的名称绘制工具栏。
        SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
        {
            if(selected != null)
            {
                GUILayout.Label(selected.Name);
            }

            if(SirenixEditorGUI.ToolbarButton(new GUIContent("创建HandAsset")))
            {
                ScriptableObjectCreator.ShowDialog<HandAsset>("Assets/Resources/Hands", obj =>
                {
                    obj.handName = obj.name;
                    base.TrySelectMenuItemWithObject(obj);
                });
            }

            if(SirenixEditorGUI.ToolbarButton(new GUIContent("添加所有手牌至容器中")))
            {
                HandController handController = GameObject.FindObjectOfType<HandController>().GetComponent<HandController>();
                handController.SendAllToHandAssets();
            }

            if (SirenixEditorGUI.ToolbarButton(new GUIContent("清除容器内所有手牌")))
            {
                HandController handController = GameObject.FindObjectOfType<HandController>().GetComponent<HandController>();
                handController.ClearAllInHandAssets();
            }
        }

        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
