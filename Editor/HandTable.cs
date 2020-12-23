#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;

/**********************************************
* 模块名: HandTable.cs
* 功能描述：用于在编辑窗口显示HandAsset的属性
* 模块编写人：高陈翊
***********************************************/

public class HandTable
{
    [FormerlySerializedAs("allHands")]
    [TableList(IsReadOnly = true, AlwaysExpanded = true), ShowInInspector]
    private readonly List<HandAssetWrapper> allHandAssts;

    public HandAsset this[int index]
    {
        get { return this.allHandAssts[index].HandAsset; }
    }

    public HandTable(IEnumerable<HandAsset> handAssets)
    {
        this.allHandAssts = handAssets.Select(x => new HandAssetWrapper(x)).ToList();
    }


    private class HandAssetWrapper
    {
        private HandAsset handAsset;

        public HandAsset HandAsset
        {
            get { return this.handAsset; }
        }

        public HandAssetWrapper(HandAsset handAsset)
        {
            this.handAsset = handAsset;
        }

        [TableColumnWidth(50,false)]
        [ShowInInspector, PreviewField(45,ObjectFieldAlignment.Center)]
        public Texture 图标 { get { return this.handAsset.Icon; } set { this.handAsset.Icon = value; EditorUtility.SetDirty(this.handAsset); } }

        [TableColumnWidth(120)]
        [ShowInInspector]
        public string 手牌名称 { get { return this.handAsset.handName; } set { this.handAsset.handName = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector,ProgressBar(-20,20)]
        public int 伤害 { get { return this.handAsset.damage; } set { this.handAsset.damage = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 防御 { get { return this.handAsset.defense; } set { this.handAsset.defense = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 力量 { get { return this.handAsset.strengthInfluence; } set { this.handAsset.strengthInfluence = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 敏捷 { get { return this.handAsset.dexterityInfluence; } set { this.handAsset.dexterityInfluence = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 体格 { get { return this.handAsset.constitutionInfluence; } set { this.handAsset.constitutionInfluence = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 智力 { get { return this.handAsset.intelligenceInfluence; } set { this.handAsset.intelligenceInfluence = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 感知 { get { return this.handAsset.widomInfluence; } set { this.handAsset.widomInfluence = value; EditorUtility.SetDirty(this.handAsset); } }

        [ShowInInspector, ProgressBar(-20, 20)]
        public int 魅力 { get { return this.handAsset.charismaInfluence; } set { this.handAsset.charismaInfluence = value; EditorUtility.SetDirty(this.handAsset); } }

    }



}

#endif
