using System.Diagnostics;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Compilation;


namespace MLib
{
    
    public class MDebugWindow : EditorWindow
    {
        private SerializedObject soThis;
        [SerializeField] private EditorConfigSO editorConfig;

        GUIStyle titleStyle;

        #region Button style
        #endregion

        #region Time scale variables
        AnimBool animShowTimeScaleFields;
        float maxValue = 1f;
        float timeScale = 1f;
        #endregion

        #region Data variables
        string serializedData;
        bool showExpandData;
        string fileName = "SaveData.json";
        string pathFileData => Application.persistentDataPath + "/" + fileName;
        LocalData localData = new();
        AnimBool animShowDataFields;
        GUIStyle styleDataText = new();
        #endregion

        #region Define symbols
        private BuildTargetGroup buildTargetGroup = BuildTargetGroup.Android;
        private Dictionary<string, bool> symbolPairs = new Dictionary<string, bool>();
        #endregion

        #region others
        bool showOthers;
        #endregion

        [MenuItem("MLib/Debug Window")]
        public static void ShowExample()
        {
            MDebugWindow wnd = GetWindow<MDebugWindow>();
            wnd.titleContent = new GUIContent("Debug Window");
        }
        private void OnEnable()
        {
            titleStyle = new();
            titleStyle.normal.textColor = Color.white;
            titleStyle.fontSize = 20;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;

            animShowTimeScaleFields = new AnimBool(false);
            animShowTimeScaleFields.valueChanged.AddListener(Repaint);

            serializedData = JsonConvert.SerializeObject(localData, Formatting.Indented);
            animShowDataFields = new AnimBool(false);
            animShowDataFields.valueChanged.AddListener(Repaint);

            try
            {
                styleDataText = new GUIStyle(EditorStyles.textArea);
                styleDataText.wordWrap = true;
            }
            catch { }

            buildTargetGroup = BuildTargetGroup.Android;
            LoadDefineSymbols();
        }
        private void OnGUI()
        {
            SetEditorDataConfig();

            EditorGUILayout.Space(10f);
            SetEditorTimeScale();

            EditorGUILayout.Space(10f);
            SetEditorData();

            EditorGUILayout.Space(10f);
            showOthers = EditorGUILayout.Foldout(showOthers, "Others");
            if (showOthers)
            {
                SetEditorToggleDefineSymbols();
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];

            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = col;
            }

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        private void SetEditorDataConfig()
        {
            string editorConfigPath = GetEditorConfigPath();
            soThis = new SerializedObject(this);
            editorConfig = AssetDatabase.LoadAssetAtPath<EditorConfigSO>(editorConfigPath);

            EditorGUILayout.LabelField("General");

            if(editorConfig == null)
            {
                EditorGUILayout.HelpBox("Have to generate new Editor Config Asset", MessageType.Error);
            }

            SerializedProperty editorConfigProp = soThis.FindProperty("editorConfig");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(editorConfigProp, true);
            if (GUILayout.Button("Generate new asset"))
            {
                var instance = ScriptableObject.CreateInstance<EditorConfigSO>();
                AssetDatabase.CreateAsset(instance, editorConfigPath);
            }
            EditorGUILayout.EndHorizontal();
        }

        private string GetEditorConfigPath()
        {
            MonoScript script = MonoScript.FromScriptableObject(this);
            string scriptPath = AssetDatabase.GetAssetPath(script);
            string dirPath = System.IO.Path.GetDirectoryName(scriptPath);

            return $"{dirPath}/EditorConfig.asset";
        }

        private void SetEditorTimeScale()
        {
            animShowTimeScaleFields.target = EditorGUILayout.ToggleLeft("Enable edit time scale in realtime", animShowTimeScaleFields.target);
            if (EditorGUILayout.BeginFadeGroup(animShowTimeScaleFields.faded))
            {
                if(!Application.isPlaying)
                {
                    EditorGUILayout.HelpBox("Edit time scale only work in \"Playing mode\"", MessageType.Warning);
                }

                maxValue = EditorGUILayout.FloatField("Max", maxValue);
                timeScale = EditorGUILayout.Slider("Value", timeScale, 0f, maxValue);

                if (Application.isPlaying)
                {
                    Time.timeScale = timeScale;
                }
            }
            EditorGUILayout.EndFadeGroup();
        }

        private void SetEditorData()
        {
            animShowDataFields.target = EditorGUILayout.BeginFoldoutHeaderGroup(animShowDataFields.target, "Local Data");
            if (animShowDataFields.target)
            {
                fileName = EditorGUILayout.TextField("File name", fileName);

                showExpandData = EditorGUILayout.Foldout(showExpandData, "Edit JSON Data");

                if (showExpandData)
                {
                    // Display TextArea and update serializedData with its content
                    serializedData = EditorGUILayout.TextArea(serializedData, styleDataText);
                }

                if (GUILayout.Button("Save data into disk"))
                {
                    localData = JsonConvert.DeserializeObject<LocalData>(serializedData);
                    MHelper.SaveDataIntoFile(pathFileData, localData);
                }
                if (GUILayout.Button("Reload data from disk"))
                {
                    localData = MHelper.LoadDataFromFile<LocalData>(pathFileData);

                    serializedData = JsonConvert.SerializeObject(localData, Formatting.Indented);
                }
                if (GUILayout.Button("Clear all data"))
                {
                    localData = new();
                    PlayerPrefs.DeleteAll();
                    MHelper.SaveDataIntoFile(pathFileData, localData);

                    serializedData = JsonConvert.SerializeObject(localData, Formatting.Indented);
                }
                if (GUILayout.Button("OPEN SAVE FILE FOLDER EXPLORER"))
                {
                    OpenFolder(Application.persistentDataPath);
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void OpenFolder(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                string windowsPath = path.Replace("/", "\\");
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = windowsPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                UnityEngine.Debug.LogError("Folder path does not exist: " + path);
            }
        }


        private void SetEditorToggleDefineSymbols()
        {
            EditorGUILayout.LabelField("---Define symbols---", titleStyle);
            if (!editorConfig)
            {
                EditorGUILayout.HelpBox("Have to generate new Editor Config Asset", MessageType.Error);
                return;
            }

            buildTargetGroup = (BuildTargetGroup) EditorGUILayout.EnumFlagsField("Build Target", buildTargetGroup);

            foreach (var symbol in symbolPairs.ToList())
            {
                bool value = symbol.Value;
                symbolPairs[symbol.Key] = EditorGUILayout.ToggleLeft(symbol.Key, value);
            }

            if (GUILayout.Button("Reload Custom Symbols"))
            {
                LoadDefineSymbols();
            }
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("This action may take some time.", MessageType.None);
            if (GUILayout.Button("Apply"))
            {
                SaveDefineSymbols();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void LoadDefineSymbols()
        {
            if (!editorConfig)
                return;

            symbolPairs = new();
            foreach (string symbol in editorConfig.DefineSymbolsToggle)
                symbolPairs.TryAdd(symbol, false);

            string strSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            int count = 0;
            string curSymbol = string.Empty;
            while(count < strSymbols.Length)
            {
                char c = strSymbols[count];
                count++;

                if (!c.Equals(';'))
                {
                    curSymbol += c;
                    
                    if(count < strSymbols.Length) continue;
                }

                if (symbolPairs.ContainsKey(curSymbol))
                {
                    symbolPairs[curSymbol] = true;
                }

                // clear curSymbol
                curSymbol = string.Empty;

            }
        }

        private void SaveDefineSymbols()
        {
            if (!editorConfig)
                return;

            HashSet<string> setSymbols = new();
            string strSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            int count = 0;
            string curSymbol = string.Empty;
            while (count < strSymbols.Length)
            {
                char c = strSymbols[count];
                count++;

                if (!c.Equals(';'))
                {
                    curSymbol += c;

                    if (count < strSymbols.Length) continue;
                }
                
                setSymbols.Add(curSymbol);

                // clear curSymbol
                curSymbol = string.Empty;

            }

            foreach (var pair in symbolPairs)
            {
                if(pair.Value == true)
                    setSymbols.Add(pair.Key);
                else if(pair.Value == false && setSymbols.Contains(pair.Key)) 
                    setSymbols.Remove(pair.Key);
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                targetGroup: buildTargetGroup,
                defines: setSymbols.ToArray());

            CompilationPipeline.RequestScriptCompilation();
        }
    }
}
