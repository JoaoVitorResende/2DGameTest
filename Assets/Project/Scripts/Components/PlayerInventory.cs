using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory instance;

        [SerializeField] private int playerMoney = 30;
        [SerializeField] List<ItemScriptableObject> items = new List<ItemScriptableObject>();
        private bool isNewItemAdded = false;
        private void Start() => instance = this;
      
        public bool SetNewItemToInventory(ItemScriptableObject item)
        {
            if (playerMoney >= item.GetItemValue())
            {
                playerMoney -= item.GetItemValue();
                items.Add(item);
                SetIsNewItemAdded(true);
                return true;
            }

            return false;
        }

        public void RemoveItemFromList(string itemName)
        {
            for(int i = 0; i < items.Count;i++)
            {
                if (items[i].GetItemName().Equals(itemName))
                {
                    playerMoney += items[i].GetItemValue();
                    items.Remove(items[i]);
                }
            }
            items.RemoveAll(i => i == null);
        }

        public ItemScriptableObject GetItem(string itemName)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].GetItemName().Equals(itemName))
                {
                    return items[i];
                }
            }
            return null;
        }

        public ItemScriptableObject GetLastItem()
        {
            return items[items.Count - 1];
        }

        public bool GetIsNewItemAdded() => isNewItemAdded;

        public void SetIsNewItemAdded(bool isAdded) => isNewItemAdded = isAdded;

        public List<ItemScriptableObject> GetListOfPlayersItens() => items;
    }
}
