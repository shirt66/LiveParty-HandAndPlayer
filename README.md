此包主要为手牌与玩家管理的一些编辑器脚本与方法接口，内容主要包括：
1.手牌资源文件HandAsset和手牌文件管理编辑器
2.手牌控制器HandController及其接口
3.玩家控制器PlayerController及其接口
4.玩家视线交互操作的控制脚本PlayerViewControl
5.自定义UI创建接口脚本UIset

#以下是各个内容的简要概述

内容1：手牌资源文件HandAsset和手牌文件管理编辑器

开发过程中的各手牌的信息都保存在HandAsset中，以ScriptableObject的形式保存。
手牌文件管理编辑器为方便创建编辑HandAsset内容的编辑器窗口工具，导入该包后可点击Unity编辑界面的顶部菜单栏的Tool选项下的“手牌文件管理”选项以打开编辑窗口
在编辑窗口中会显示目前工程文件中已有的HandAsset文件，并提供文件各属性的可调节界面，编辑窗口左上方的三个按钮的功能分别为：
创建HandAsset：默认在Asset/Resources/Hands路径下创建HandAsset，点击后也可自己选择路径
添加所有手牌至容器：将所有的HandAsset加入场景中挂有的HandController脚本的HandAssets容器中
清除容器内所有卡牌：将场景中挂有的HandController脚本的HandAssets容器中的所有卡牌从容器中移除


内容2：手牌控制器HandController及其接口

HandController主要为对手牌HandAsset集中管理的一个脚本，其中主要括：
容器handAssets：为List<HandAsset>类型的容器，主要用于在场景中储存所有手牌资源。
容器handAssetDictionary：为Dictionary<int, List<int>>类型的字典容器，字典的Key代表玩家的编号，字典的Value代表该编号玩家目前拥有的所有手牌在容器中的编号(从0开始计)
主要的接口方法：
手牌容器的接口：
GetHandType：获取手牌类型，有两个重载方法，可根据手牌编号或手牌名称获取
GetHandDamage：获取手牌攻击力，有两个重载方法，可根据手牌编号或手牌名称获取
GetHandDefense：获取手牌防御力，有两个重载方法，可根据手牌编号或手牌名称获取
ShowHandInfo：根据传入手牌编号在游戏的UI界面显示对应编号的手牌信息，包括手牌名称，各属性值以及手牌描述，并返回对应信息UI的父物体
SendAllToHandAssets：将工程文件中的所有HandAsset文件存入handAssets容器中，在挂载上的脚本Inspector界面有对应的调用按钮
ClearAllInHandAssets：将handAssets容器中的所有HandAsset文件移除出容器，在挂载上的脚本Inspector界面有对应的调用按钮
手牌玩家字典的接口：
AddHandToPlayer：添加手牌给指定编号的玩家，有两个重载方法，分别根据手牌的编号和手牌的名称进行添加
AddHandsToPlayer：根据一个手牌名称的string数组将一连串对应名称的手牌添加给指定编号的玩家
RemoveHandFromPlayer：将某一手牌从指定编号的玩家手里移除，有两个重载方法，分别根据手牌的编号和手牌的名称进行移除
RemoveHandsFromPlayer：将传入的string数组中一连串对应名称的手牌从指定编号玩家手里移除
GetHandsOfPlayer：获取指定编号玩家手里的所有手牌，放回手牌编号的List
ShowAllHandsIcon：在UI界面显示指定编号玩家的所有手牌图标
HideAllHandsIcon：消除指定编号玩家的所有手牌图标
ShowAllHandsModel：显示指定编号玩家的所有手牌模型
HideAllHandsMoedel：消除指定编号玩家的所有手牌模型


内容3：玩家控制器PlayerController及其接口

游戏中的玩家有一个Player脚本用于储存玩家的六维属性值，PlayerController主要是对玩家进行集中管理的脚本，其中包括：
容器players：为List<Player>类型的容器，主要储存当前游戏的所有玩家属性数据
playerPrefab：玩家预制体，主要由名为Player的父物体及其下的Camera子物体组成
主要接口方法：
CreatePlayer：根据玩家预制体创建玩家并存入容器中，不知道有没有用
GetStrength：根据玩家编号获取其力量值
GetDexterity：根据玩家编号获取其敏捷值
GetConstitution：通过玩家编号获取其体格值
GetIntelligence：通过玩家编号获取其智力值
GetWidom：通过玩家编号获取其感知值
GetCharisma：通过玩家编号获取其魅力值
ChangeAttribute：根据特定玩家编号的所有手牌改变其所有属性值
ChangeAttributes：根据玩家手上的手牌更新玩家的六维属性
GetMaxAttribute：根据判定类型获取改判定属性值最大的玩家编号


内容4.玩家视线交互操作的控制脚本PlayerViewControl

此脚本需挂载在玩家游戏物体上，主要用于玩家的视角控制，以及视线的射线交互检测内容


内容5.自定义UI创建接口脚本UIset

UIset脚本中主要提供了一些自定义创建UI的接口方法，主要接口如下：
CreateImage：含四个重载方法，允许在UICanvas或现有父物体下自定义建立新的Image
CreateImgAndReturn：含四个重载方法，允许在UICanvas或现有父物体下自定义建立新的图片并返回游戏物体
CreateText：含两个重载方法，允许在UICanvas或现有父物体下自定义建立新的Text，并可根据字数自动增加行数
CreateTextAndReturn：含两个重载方法，允许在UICanvas或现有父物体下自定义建立新的Text并返回游戏物体，并可根据字数自动增加行数