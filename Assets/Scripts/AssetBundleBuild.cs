using UnityEngine;
using System.Collections;
using UnityEditor;

/*****************************************************************************
* @author : 张蕴星
* @date : 2016/11/16 15:16
* @instructions : AssetBundle打包
*****************************************************************************/
public class AssetBundleBuild : MonoBehaviour {
    [MenuItem("AssetBundle Editor/AssetBundle Build")]
    static void AssetBundlesBuild()
    {
        //注：第一个存放AssetBundle的路径取相对地址
        BuildPipeline.BuildAssetBundles(AssetBundleConfig.ASSETBUNDLE_PATH.Substring(AssetBundleConfig.PROJECT_PATH.Length),
            BuildAssetBundleOptions.DeterministicAssetBundle|BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64
            );
    }
}
