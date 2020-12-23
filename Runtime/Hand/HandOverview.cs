#if UNITY_EDITOR
using Sirenix.Utilities;
using System.Linq;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
    using UnityEditor;
#endif

[GlobalConfig("Scripts/Hand")]
public class HandOverview : GlobalConfig<HandOverview>
{
    [ReadOnly]
    [ListDrawerSettings(Expanded = true)]
    public HandAsset[] AllHandAssets;

#if UNITY_EDITOR
    [Button(ButtonSizes.Medium),PropertyOrder(-1)]
    public void UpdateHandAssetOverview()
    {
        //查找并分配所有HandAsset类型的HandAsset
        this.AllHandAssets = AssetDatabase.FindAssets("t:HandAsset")
            .Select(guid => AssetDatabase.LoadAssetAtPath<HandAsset>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToArray();
    }
#endif
}

#endif

