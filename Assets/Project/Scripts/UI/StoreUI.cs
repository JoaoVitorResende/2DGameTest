using Store;
using UnityEngine;
using Util;
using UnityEngine.EventSystems;
using player;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace UserInterface
{
    public class StoreUI : MonoBehaviour
    {
        [SerializeField] private List<GameObject> storeButtons = new List<GameObject>();

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

        public void BuyItem()
        {
            ItemScriptableObject item = StoreData.instance.GetItem(EventSystem.current.currentSelectedGameObject.name);
            bool isItemSold = PlayerInventory.instance.SetNewItemToInventory(item);
            CheckItemQuantity(item, isItemSold);
        }

        private void CheckItemQuantity(ItemScriptableObject item, bool isItemSold)
        {
            GameObject objectOnStore = CheckObjectOnList(item.GetItemName());

            int itemQuantity = int.Parse(objectOnStore.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);

            if (itemQuantity == itemMinimunQuantity && isItemSold)
            {
                RemoveItemFromStore(item.GetItemName());
                UpdateItemOnStore(itemQuantity - itemMinimunQuantity);
            }
            else if(isItemSold)
                UpdateItemOnStore(itemQuantity - itemMinimunQuantity);
        }

        private void UpdateItemOnStore(int itemQuantity) => StoreData.instance.SetNewItemQuantity(itemQuantity, buttonPosition);
        
        private void RemoveItemFromStore(string itemName)
        {
            GameObject item = CheckObjectOnList(itemName);
            if (item != null)
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

