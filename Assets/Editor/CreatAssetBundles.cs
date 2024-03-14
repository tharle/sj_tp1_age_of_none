using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using static GameParameters;

public class CreatAssetBundles
{
    [MenuItem("Tharle/Builld AssetBundles")]
    static void BuildAllAssetBundles()
    {
        List<AssetBundleBuild> assetBundleDefinitionList = new();

        // FOR SFX MAIN MENU
        {
            AssetBundleBuild ab = new();
            ab.assetBundleName = BundleNames.SFX;
            ab.assetNames = RecursiveGetAllAssetsInDirectory(BundlePath.BUNDLE_ASSETS_INPUT, BundleExtension.SFX).ToArray();
            assetBundleDefinitionList.Add(ab);
        }
        
        // Create if not exist streaming Assets directory
        if (!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(Application.streamingAssetsPath);
        }

        // Build all bundles from 'BundleAssets' to streaming directory
        AssetBundleManifest manifest =  BuildPipeline.BuildAssetBundles(BundlePath.BUNDLE_ASSETS_OUTPUT, assetBundleDefinitionList.ToArray(), BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        
        // Look at the results
        if (manifest != null)
        {
            foreach (var bundleName in manifest.GetAllAssetBundles())
            {
                string projectRelativePath = BundlePath.BUNDLE_ASSETS_OUTPUT + "/" + bundleName;
                Debug.Log($"Size of AssetBundle {projectRelativePath} is {new FileInfo(projectRelativePath).Length}");
            }
        }
        else
        {
            Debug.Log("Build failed, see Console and Editor log for details");
        }
    }

    static List<string> RecursiveGetAllAssetsInDirectory(string path, params string[] extensions)
    {

        StringBuilder searchPattern = new StringBuilder();
        foreach(string s in extensions)
        {
            searchPattern.Append("\\.");
            searchPattern.Append(s);
            searchPattern.Append("|");
        }

        List<string> assets = new();
        foreach (string asset in Directory.EnumerateFiles(path, searchPattern.ToString(), SearchOption.AllDirectories))
                assets.Add(asset);
        return assets;
    }
}
