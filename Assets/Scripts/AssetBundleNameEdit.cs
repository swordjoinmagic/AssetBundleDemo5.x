using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

/*****************************************************************************
* @author : Zhang Yunxing
* @date : 2016/11/16 16:47
* @instructions : 添加工具栏,一键式添加，移除AssetBundleName
*****************************************************************************/
public class AssetBundleNameEdit : MonoBehaviour
{

    [MenuItem("AssetBundle Editor/SetAssetBundleName")]
    static void SetResourcesAssetBundleName()
    {
        //只读取当前选中的目录，排除子目录
        Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Assets | SelectionMode.ExcludePrefab);
        //此处添加需要命名的资源后缀名,注意大小写。
        string[] Filtersuffix = new string[] { ".prefab", ".mat", ".dds" };
        if (SelectedAsset.Length == 0) return;
        foreach (Object tmpFolder in SelectedAsset)
        {
            string fullPath = AssetBundleConfig.PROJECT_PATH + AssetDatabase.GetAssetPath(tmpFolder);
            //Debug.Log(fullPath);
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dir = new DirectoryInfo(fullPath);
                var files = dir.GetFiles("*", SearchOption.AllDirectories);
                for (var i = 0; i < files.Length; ++i)
                {
                    var fileInfo = files[i];
                    //显示进度条
                    EditorUtility.DisplayProgressBar("设置AssetBundleName名称", "正在设置AssetBundleName名称中...", 1.0f * i / files.Length);
                    foreach (string suffix in Filtersuffix)
                    {
                        if (fileInfo.Name.EndsWith(suffix))
                        {
                            string path = fileInfo.FullName.Replace('\\', '/').Substring(AssetBundleConfig.PROJECT_PATH.Length);
                            //资源导入器
                            var importer = AssetImporter.GetAtPath(path);
                            if (importer)
                            {
                                string name = path.Substring(fullPath.Substring(AssetBundleConfig.PROJECT_PATH.Length).Length + 1);
                                importer.assetBundleName = name.Substring(0, name.LastIndexOf('.')) + AssetBundleConfig.SUFFIX;
                            }
                        }
                    }
                }
            }

        }
        //删除所有未使用的assetBundle资产数据库名称
        AssetDatabase.RemoveUnusedAssetBundleNames();
        EditorUtility.ClearProgressBar();
    }


    //输出所有AssetBundleName
    [MenuItem("AssetBundle Editor/GetAllAssetBundleName")]

    static void GetAllAssetBundleName()
    {

        string[] names = AssetDatabase.GetAllAssetBundleNames();

        foreach (var name in names)
        {
            Debug.Log(name);
        }

    }


    [MenuItem("AssetBundle Editor/ClearAssetBundleName")]

    static void ClearResourcesAssetBundleName()
    {
        UnityEngine.Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.Assets | SelectionMode.ExcludePrefab);
        //此处添加需要清除的资源后缀名,注意大小写。
        string[] Filtersuffix = new string[] { ".prefab", ".mat", ".dds" }; 
        if (SelectedAsset.Length == 0) return;
        foreach (Object tmpFolder in SelectedAsset)
        {
            string fullPath = AssetBundleConfig.PROJECT_PATH + AssetDatabase.GetAssetPath(tmpFolder);
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dir = new DirectoryInfo(fullPath);
                var files = dir.GetFiles("*", SearchOption.AllDirectories);
                for (var i = 0; i < files.Length; ++i)
                {
                    var fileInfo = files[i];
                    EditorUtility.DisplayProgressBar("清除AssetBundleName名称", "正在清除AssetBundleName名称中...", 1.0f * i / files.Length);
                    foreach (string suffix in Filtersuffix)
                    {
                        if (fileInfo.Name.EndsWith(suffix))
                        {
                            string path = fileInfo.FullName.Replace('\\', '/').Substring(AssetBundleConfig.PROJECT_PATH.Length);
                            var importer = AssetImporter.GetAtPath(path);
                            if (importer)
                            {
                                importer.assetBundleName = null;
                            }
                        }
                    }
                }
            }
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.RemoveUnusedAssetBundleNames();
    }
}
