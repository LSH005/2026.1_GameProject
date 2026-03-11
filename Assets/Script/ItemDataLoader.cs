using UnityEngine;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ItemDataLoader : MonoBehaviour
{
    [SerializeField]
    private string jsonFileName = "items";
    private List<ItemData> itemList;

    private void Start()
    {
        LoadItemData();
    }


    void LoadItemData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile != null)
        {
            byte[] bytes = Encoding.Default.GetBytes(jsonFile.text);
            string currentText = Encoding.UTF8.GetString(bytes);

            itemList = JsonConvert.DeserializeObject<List<ItemData>>(currentText);
            Debug.Log($"로드된 아이템 수 : {itemList.Count}");

            foreach (ItemData item in itemList)
            {
                Debug.Log($"아이템 : {EncodeKorean(item.itemName)} // 설명 : {EncodeKorean(item.description)}");
            }
        }
        else
        {
            Debug.LogError($"JSON 파일 찾을 수 없음 : {jsonFileName}");
        }
    }
    private string EncodeKorean(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        byte[] bytes = Encoding.Default.GetBytes(text);
        return Encoding.UTF8.GetString(bytes);
    }
}
