using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Util
{
    public class StoreData : MonoBehaviour
    {
        [SerializeField] private List<ItemScriptableObject> itemScriptableObjects = new List<ItemScriptableObject>();
        [SerializeField] private List<GameObject> buttons = new List<GameObject>();
        public static StoreData instance;
        private int itemName = 0;
        private int itemSprite = 1;
        private int itemValue = 2;
        private int itemQuantity = 3;

        private void Start()
        {
            SetStoreItensOptions();
            instance = this;
        }

        private void SetStoreItensOptions()
        {
            for (int i = 0; i < itemScriptableObjects.Count; i++)
            {
                buttons[i].transform.GetChild(itemName).GetComponent<TextMeshProUGUI>().text = itemScriptableObjects[i].GetItemName();
                buttons[i].transform.GetChild(itemSprite).GetComponent<Image>().sprite = itemScriptableObjects[i].GetItemSprite();
                buttons[i].transform.GetChild(itemValue).GetComponent<TextMeshProUGUI>().text = "Value: " + itemScriptableObjects[i].GetItemValue().ToString();
                buttons[i].transform.GetChild(itemQuantity).GetComponent<TextMeshProUGUI>().text = itemScriptableObjects[i].GetItemQuantity().ToString();
                buttons[i].name = itemScriptableObjects[i].GetItemName();
            }
        }

        public void SetNewItemQuantity(int newQuantity, int buttonPosition) => buttons[buttonPosition].transform.GetChild(itemQuantity).GetComponent<TextMeshProUGUI>().text = newQuantity.ToString();

        public ItemScriptableObject GetItem(string itemname)
        {
            foreach (ItemScriptableObject item in itemScriptableObjects)
            {
                if (item.GetItemName() == itemname)
                    return item;
            }

            return null;
        }
    }
}


