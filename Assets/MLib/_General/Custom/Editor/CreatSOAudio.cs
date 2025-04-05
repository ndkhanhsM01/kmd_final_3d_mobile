/*
 * by https://discussions.unity.com/t/please-include-a-copy-path-when-right-clicking-a-game-object/638839/5
 */

using System;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace Khanhnd
{
    public static class CreatSOAudio
    {
        [MenuItem("Assets/SOAudio from AudioClip", true)]
        private static bool ValidateCreateSOAudio()
        {
            return Selection.activeObject is AudioClip;
        }

        [MenuItem("Assets/SOAudio from AudioClip")]
        private static void CreateSOAudio()
        {
            AudioClip selectedClip = Selection.activeObject as AudioClip;

            if (selectedClip == null) return;

            SOAudio soAudio = ScriptableObject.CreateInstance<SOAudio>();
            soAudio.Editor_Init(selectedClip);

            string path = AssetDatabase.GetAssetPath(selectedClip);
            string directory = Path.GetDirectoryName(path);
            string assetName = Path.GetFileNameWithoutExtension(path);
            string newAssetPath = Path.Combine(directory, $"SOAudio_{assetName}.asset");

            AssetDatabase.CreateAsset(soAudio, newAssetPath);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow(); 
            EditorGUIUtility.PingObject(soAudio);
        }
    }
}