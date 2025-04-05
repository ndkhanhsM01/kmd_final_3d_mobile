/*
 * by https://discussions.unity.com/t/please-include-a-copy-path-when-right-clicking-a-game-object/638839/5
 */

using System;
using UnityEditor;
using UnityEngine;

namespace Khanhnd
{
    public static class AssetPathToCopy
    {
        private const string MenuAssetPath = "Assets/";
        private const int Priority = 2000;

        [MenuItem(MenuAssetPath + "Copy Full Path %#2", false, Priority + 2)]
        private static void CopyFullPath()
        {
            var guids = Selection.assetGUIDs;

            var assetPath = System.IO.Path.Combine(Application.dataPath,
                AssetDatabase.GUIDToAssetPath(guids[0]).Replace("Assets/", string.Empty));
            EditorGUIUtility.systemCopyBuffer = assetPath;
        }

        [MenuItem(MenuAssetPath + "Copy Full Path %#2", true, Priority + 2)]
        private static bool CopyFullPathValidation()
        {
            return Selection.assetGUIDs.Length == 1;
        }

        [MenuItem(MenuAssetPath + "Copy Path In Assets %#1", false, Priority + 1)]
        private static void CopyPathInAssets()
        {
            var guids = Selection.assetGUIDs;

            var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            EditorGUIUtility.systemCopyBuffer = assetPath;
        }

        [MenuItem(MenuAssetPath + "Copy Path In Assets %#1", true, Priority + 1)]
        private static bool CopyPathInAssetsValidation()
        {
            return Selection.assetGUIDs.Length == 1;
        }

        [MenuItem(MenuAssetPath + "Copy Path In Resources %#0", false, Priority)]
        private static void CopyPathInResources()
        {
            var guids = Selection.assetGUIDs;

            var assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            assetPath = assetPath.Substring(assetPath.IndexOf("Resources/", StringComparison.Ordinal) +
                                            "Resources/".Length);
            var extensionPos = assetPath.LastIndexOf(".", StringComparison.Ordinal);
            if (extensionPos >= 0)
                assetPath = assetPath.Substring(0, extensionPos);

            EditorGUIUtility.systemCopyBuffer = assetPath;
        }

        [MenuItem(MenuAssetPath + "Copy Path In Resources %#0", true, Priority)]
        private static bool CopyPathInResourcesValidation()
        {
            return Selection.assetGUIDs.Length == 1 &&
                   AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]).Contains("Resources");
        }
    }
}