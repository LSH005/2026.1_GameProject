using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public string nameEng;
    public string description;

    public ItemType itemType;
    public int price;
    public int power;
    public int level;
    public bool isStackable;
    public Sprite icon;

    public override string ToString()
    {
        return $"[{id}] {itemName} ({itemType}) - Cost : {price}Gold // Type : {power}";
    }

    public string DisplayName { get { return string.IsNullOrEmpty(nameEng) ? itemName : nameEng; } }
}
