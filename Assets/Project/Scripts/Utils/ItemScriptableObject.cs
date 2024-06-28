using UnityEngine;

[CreateAssetMenu(fileName = "itemObject", menuName = "Item", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemValue;
    [SerializeField] private int itemQuantity;
    [SerializeField] private Sprite itemSprite;

    public string GetItemName() => itemName;
    public int GetItemValue() => itemValue;
    public int GetItemQuantity() => itemQuantity;
    public Sprite GetItemSprite() => itemSprite;
}
