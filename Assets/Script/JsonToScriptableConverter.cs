#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

public class JsonToScriptableConverter : EditorWindow
{
    private string jsonFilePath = "";
    private string outputFolder = "Assets/ScriptableObjects/Items";
    bool createDatabase = true;

    [MenuItem("Tools/JSON to Scriptable Objects")]
    public static void ShowWindow()
    {
        GetWindow<JsonToScriptableConverter>("JSON to ScriptableObjecs");
    }

    private void OnGUI()
    {
        GUILayout.Label("Json to ScriptableObject Converter", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        if (GUILayout.Button("Select JSON File")) jsonFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");

        EditorGUILayout.LabelField("Selected File : ", outputFolder);
        EditorGUILayout.Space();
        outputFolder = EditorGUILayout.TextField("Output Folder : ", outputFolder);
        createDatabase = EditorGUILayout.Toggle("Create Database Asset", createDatabase);
        EditorGUILayout.Space();

        if (GUILayout.Button("Convert to Scriptable Objects"))
        {
            if (string.IsNullOrEmpty(jsonFilePath))
            {
                EditorUtility.DisplayDialog("Error", "Please Select a JSON file", "OK");
                return;
            }
            ConvertJsonToScriptableObjects();
        }
    }

    private void ConvertJsonToScriptableObjects()
    {
        if (!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);

        string jsonText = File.ReadAllText(jsonFilePath);

        try
        {
            List<ItemData> itemList = JsonConvert.DeserializeObject<List<ItemData>>(jsonText);

            List<ItemSO> createdItems = new List<ItemSO>();
            foreach (ItemData itemData in itemList)
            {
                ItemSO itemSO = ScriptableObject.CreateInstance<ItemSO>();
                itemSO.id = itemData.id;
                itemSO.itemName = itemData.itemName;
                itemSO.nameEng = itemData.nameEng;
                itemSO.description = itemData.description;

                if (System.Enum.TryParse(itemData.itemTypeString, out ItemType parsedType)) itemSO.itemType = parsedType;
                else Debug.LogWarning($"아이템 {itemData.itemName}의 유효하지 않은 타입 : {itemData.itemType}");

                itemSO.price = itemData.price;
                itemSO.power = itemData.power;
                itemSO.level = itemData.level;
                itemSO.description = itemData.description;
                itemSO.isStackable = itemData.isStackable;

                if (!string.IsNullOrEmpty(itemData.iconPath))
                {
                    itemSO.icon = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Resources/{itemData.iconPath}.png");

                    if (itemSO.icon == null) Debug.LogWarning($"아이템 {itemData.nameEng} 의 아이콘을 찾을 수 없음. : {itemData.iconPath}");
                }

                string assetPath = $"{outputFolder}/item_{itemData.id.ToString("D4")}_{itemData.nameEng}.asset";
                AssetDatabase.CreateAsset( itemSO, assetPath );
                itemSO.name = $"Item_{itemSO.id.ToString("D4")} + {itemData.nameEng}";
                createdItems.Add(itemSO);

                EditorUtility.SetDirty(itemSO);

                if (createDatabase && createdItems.Count > 0)
                {
                    ItemDataBaseSO dataBase = ScriptableObject.CreateInstance<ItemDataBaseSO>();
                    dataBase.items = createdItems;

                    AssetDatabase.CreateAsset(dataBase, $"{outputFolder}/ItemDatabase.asset");
                    EditorUtility.SetDirty(dataBase);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            EditorUtility.DisplayDialog("Sucess", $"Created {createdItems.Count} Scriptable Objects", "OK");
        }

        catch(System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", $"Failed to Convert JSON : {e.Message}", "OK");
            Debug.LogError("JSON을 변환하기가 좀 그랬습니다. : " + e);
        }
    }

}

#endif