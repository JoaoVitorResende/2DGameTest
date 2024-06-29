using Store;
using UnityEngine;
using Util;
using UnityEngine.EventSystems;
using Player;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace UserInterface
{
    public class StoreUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> storeButtons = new List<GameObject>();
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private GameObject storeSellObjects;
        private bool playerStillInStore = false;
        private int itemMinimunQuantity = 1;
        private float storeOn = 1f;
        private float storeOff = 0f;
        private int buttonPosition = 0;

        private void FixedUpdate() => CheckIsPlayerOnStore();

        private void CheckIsPlayerOnStore()
        {
            if (StoreInteraction.instance.GetIsOnRangeToBuy() && !playerStillInStore)
            {
                playerStillInStore = true;
                ControlStoreModal();
            }
            else if (!StoreInteraction.instance.GetIsOnRangeToBuy() && playerStillInStore)
            {
                playerStillInStore = false;
                ControlStoreModal();
            }
        }

        private void ControlStoreModal()
        {
            GetComponent<CanvasGroup>().alpha = GetComponent<CanvasGroup>().alpha == storeOff ? storeOn : storeOff;
            GetComponent<CanvasGroup>().interactable = !GetComponent<CanvasGroup>().interactable;
            GetComponent<CanvasGroup>().blocksRaycasts = !GetComponent<CanvasGroup>().blocksRaycasts;
        }

        public void EnterOnSellingMode()
        {
            List<GameObject> buttonsPlayer = new List<GameObject>();

            foreach (ItemScriptableObject item in PlayerInventory.instance.GetListOfPlayersItens())
            {
                GameObject itemToSell = Instantiate(itemPrefab);
                itemToSell.transform.parent = storeSellObjects.transform;
                itemToSell.GetComponent<Button>().onClick.AddListener(delegate() { SellItem(); });
                buttonsPlayer.Add(itemToSell);
            }

            StoreData.instance.SetStoreItensOptionSell(PlayerInventory.instance.GetListOfPlayersItens(), buttonsPlayer);
        }

        public void ExitSellingMode()
        {
            for (int i = storeSellObjects.transform.childCount - 1; i >= 0; i--)
                Destroy(storeSellObjects.transform.GetChild(i).gameObject);
        }

        public void BuyItem()
        {
            ItemScriptableObject item = StoreData.instance.GetItem(EventSystem.current.currentSelectedGameObject.name);
            bool isItemSold = PlayerInventory.instance.SetNewItemToInventory(item);
           
            CheckItemQuantity(item, isItemSold);
        }

        public void SellItem()
        {
            string itemName = EventSystem.current.currentSelectedGameObject.name;
            GameObject itemOnStore = CheckObjectOnList(itemName);
            PlayerInventory.instance.RemoveItemFromList(itemName);
            int newItemQuantity = int.Parse(itemOnStore.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);
            UpdateItemOnStore(newItemQuantity + itemMinimunQuantity, true);
            if (newItemQuantity == 0)
            {
                Destroy(GameObject.Find(itemName));
            }
        }

        private void CheckItemQuantity(ItemScriptableObject item, bool isItemSold)
        {
            GameObject objectOnStore = CheckObjectOnList(item.GetItemName());

            int itemQuantity = int.Parse(objectOnStore.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);

            if (itemQuantity == itemMinimunQuantity && isItemSold)
            {
                RemoveItemFromStore(item.GetItemName());
                UpdateItemOnStore(itemQuantity - itemMinimunQuantity, false);
            }
            else if (isItemSold)
                UpdateItemOnStore(itemQuantity - itemMinimunQuantity, false);
        }

        private void UpdateItemOnStore(int itemQuantity, bool isSelling)
        {
            StoreData.instance.SetNewItemQuantity(itemQuantity, buttonPosition);

            if(isSelling)
                storeButtons[buttonPosition].GetComponent<Button>().interactable = true;
        }

        private void RemoveItemFromStore(string itemName)
        {
            GameObject item = CheckObjectOnList(itemName);
            
            item.GetComponent<Button>().interactable = false;
        }

        private GameObject CheckObjectOnList(string itemName)
        {
            for(int i = 0; i < storeButtons.Count; i++)
            {
                if (storeButtons[i].name.Equals(itemName))
                {
                    buttonPosition = i;
                    return storeButtons[i];
                }
            }
            return null;
        }
    }
}

