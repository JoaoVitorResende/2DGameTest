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
            SetStoreItensOptionsBuy();
            instance = this;
        }

        private void SetStoreItensOptionsBuy()
        {
            for (int i = 0; i < itemScriptableObjects.Count; i++)
                InsertItemsData(buttons[i], itemScriptableObjects[i]);
        }

        public void SetStoreItensOptionSell(List<ItemScriptableObject> itensPlayers, List<GameObject> buttonsPlayerInventory)
        {
            for (int i = 0; i < itensPlayers.Count; i++)
                InsertItemsData(buttonsPlayerInventory[i], itensPlayers[i]);
        }

        private void InsertItemsData(GameObject bt, ItemScriptableObject item)
        {
            bt.transform.GetChild(itemName).GetComponent<TextMeshProUGUI>().text = item.GetItemName();
            bt.transform.GetChild(itemSprite).GetComponent<Image>().sprite = item.GetItemSprite();
            bt.transform.GetChild(itemValue).GetComponent<TextMeshProUGUI>().text = "Value: " + item.GetItemValue().ToString();
            bt.transform.GetChild(itemQuantity).GetComponent<TextMeshProUGUI>().text = item.GetItemQuantity().ToString();
            bt.name = item.GetItemName();
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


