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
            {
                buttons[i].transform.GetChild(itemName).GetComponent<TextMeshProUGUI>().text = itemScriptableObjects[i].GetItemName();
                buttons[i].transform.GetChild(itemSprite).GetComponent<Image>().sprite = itemScriptableObjects[i].GetItemSprite();
                buttons[i].transform.GetChild(itemValue).GetComponent<TextMeshProUGUI>().text = "Value: " + itemScriptableObjects[i].GetItemValue().ToString();
                buttons[i].transform.GetChild(itemQuantity).GetComponent<TextMeshProUGUI>().text = itemScriptableObjects[i].GetItemQuantity().ToString();
                buttons[i].name = itemScriptableObjects[i].GetItemName();
            }
        }

        public void SetStoreItensOptionSell(List<ItemScriptableObject> itensPlayers, List<GameObject> buttonsPlayerInventory)
        {
            for (int i = 0; i < itensPlayers.Count; i++)
            {
                buttonsPlayerInventory[i].transform.GetChild(itemName).GetComponent<TextMeshProUGUI>().text = itensPlayers[i].GetItemName();
                buttonsPlayerInventory[i].transform.GetChild(itemSprite).GetComponent<Image>().sprite = itensPlayers[i].GetItemSprite();
                buttonsPlayerInventory[i].transform.GetChild(itemValue).GetComponent<TextMeshProUGUI>().text = "Value: " + itensPlayers[i].GetItemValue().ToString();
                buttonsPlayerInventory[i].transform.GetChild(itemQuantity).GetComponent<TextMeshProUGUI>().text = itensPlayers[i].GetItemQuantity().ToString();
                buttonsPlayerInventory[i].name = itemScriptableObjects[i].GetItemName();
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


