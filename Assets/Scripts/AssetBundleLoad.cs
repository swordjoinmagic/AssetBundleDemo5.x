using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*****************************************************************************
* @author : 张蕴星
* @date : 2016/11/16 15:20
* @instructions : AssetBundle的加载，采用字典存贮依赖项，避免频繁地加载和卸载AssetBundle
*****************************************************************************/
public class AssetBundleLoad : MonoBehaviour
{

    private static AssetBundleManifest manifest = null;
    private static Dictionary<string, AssetBundle> assetBundleDic = new Dictionary<string, AssetBundle>();
    void OnGUI()
    {
        if (GUILayout.Button("Load JushiGuai"))
        {
            StartCoroutine(InstanceAsset("jushiguai"));
        }
    }

    public AssetBundle LoadAssetBundle(string Url)
    {
        if (assetBundleDic.ContainsKey(Url))
            return assetBundleDic[Url];
        if (manifest == null)
        {
            //Debug.Log(AssetBundleConfig.ASSETBUNDLE_PATH);
            AssetBundle manifestAssetBundle = AssetBundle.LoadFromFile(AssetBundleConfig.ASSETBUNDLE_PATH + "StreamingAssets");
            manifest = (AssetBundleManifest)manifestAssetBundle.LoadAsset("AssetBundleManifest");
        }
        if (manifest != null)
        {
            //获取当前加载AssetBundle的所有依赖项的路径
            string[] objectDependUrl = manifest.GetAllDependencies(Url);
            foreach (string tmpUrl in objectDependUrl)
            {
                //通过递归调用加载所有依赖项
                LoadAssetBundle(tmpUrl);
            }
            Debug.Log(AssetBundleConfig.ASSETBUNDLE_PATH + Url);
            assetBundleDic[Url] = AssetBundle.LoadFromFile(AssetBundleConfig.ASSETBUNDLE_PATH + Url);
            return assetBundleDic[Url];
        }
        return null;
    }


    private IEnumerator InstanceAsset(string assetBundleName)
    {
        string assetBundlePath = assetBundleName + AssetBundleConfig.SUFFIX;
        int index = assetBundleName.LastIndexOf('/');
        string realName = assetBundleName.Substring(index + 1, assetBundleName.Length - index - 1);
        yield return LoadAssetBundle(assetBundlePath);
        if (assetBundleDic.ContainsKey(assetBundlePath) && assetBundleDic[assetBundlePath] != null)
        {
            Object tmpObj = assetBundleDic[assetBundlePath].LoadAsset(realName);
            yield return Instantiate(tmpObj);
            assetBundleDic[assetBundlePath].Unload(false);
        }
        yield break;
    }
}
