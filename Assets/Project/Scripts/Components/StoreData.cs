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

        private void Start()
        {
            SetStoreItensOptions();
            instance = this;
        }

        private void SetStoreItensOptions()
        {
            for (int i = 0; i < itemScriptableObjects.Count; i++)
            {
                buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemScriptableObjects[i].GetItemName();
                buttons[i].transform.GetChild(1).GetComponent<Image>().sprite = itemScriptableObjects[i].GetItemSprite();
                buttons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Value: "+itemScriptableObjects[i].GetItemValue().ToString();
                buttons[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Quantity: " + itemScriptableObjects[i].GetItemQuantity().ToString();
                buttons[i].name = itemScriptableObjects[i].GetItemName();
            }
        }

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


