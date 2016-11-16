using UnityEngine;
using System.Collections;

/*****************************************************************************
* @author : 张蕴星
* @date : 2016/11/16 15:15
* @instructions : 设置打包AssetBundle用的的相关路径
*****************************************************************************/
public class AssetBundleConfig : MonoBehaviour {

    //AssetBundle打包后存储的路径
    public static string ASSETBUNDLE_PATH = Application.dataPath + "/StreamingAssets/";

    //资源地址
    public static string APPLICATION_PATH = Application.dataPath + "/";

    //工程根目录地址
    public static string PROJECT_PATH = APPLICATION_PATH.Substring(0, APPLICATION_PATH.Length - 7);

    //AssetBundle打包的后缀名
    public static string SUFFIX = ".assetbundle";
}
