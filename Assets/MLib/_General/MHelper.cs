using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MLib
{
    public static class MHelper
    {
        public static T LoadDataFromFile<T>(string path, bool createFileDefault = false) where T : new()
        {
            T result = new();

            try
            {
                if (File.Exists(path))
                {
                    string content = File.ReadAllText(path);
                    result = JsonConvert.DeserializeObject<T>(content);
                    Debug.LogWarning($"Read data <{typeof(T)}> from file: {path}");
                }
                else
                {
                    Debug.LogWarning($"Not exist file: {path}");

                    if (createFileDefault)
                    {
                        SaveDataIntoFile<T>(path, result);
                    }
                }
            }
            catch
            {
                Debug.LogError("Load file failure!");
            }

            return result;
        }

        public static void SaveDataIntoFile<T>(string path, T data) where T : new()
        {
            try
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

                File.WriteAllText(path, jsonData);
                Debug.LogWarning($"Game data <{typeof(T)}> saved to: " + path);
            }
            catch
            {
                Debug.LogError("Save file failure!");
            }

        }
        public static void FocusGameobject(GameObject target)
        {
#if UNITY_EDITOR
            Selection.activeGameObject = target;
#endif
        }
        public static bool CheckTouchUI()
        {

#if UNITY_EDITOR
            //if (Input.GetMouseButtonDown (0)) {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return true;
                //	}
            }

#elif UNITY_ANDROID || UNITY_IOS
		Touch[] lsttouch = Input.touches;
		if (lsttouch.Length > 0) {
		for (int i = 0; i < lsttouch.Length; i++) {
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (lsttouch [i].fingerId)) {
		return true;
		}
		}
		}
#endif
            return false;
        }

    }

}