using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory instance;

        [SerializeField] private int playerMoney = 30;
        [SerializeField] List<ItemScriptableObject> itens = new List<ItemScriptableObject>();
        private bool isNewItemAdded = false;
        private void Start() => instance = this;
      
        public bool SetNewItemToInventory(ItemScriptableObject item)
        {
            if (playerMoney >= item.GetItemValue())
            {
                playerMoney -= item.GetItemValue();
                itens.Add(item);
                SetIsNewItemAdded(true);
                return true;
            }

            return false;
        }

        public void RemoveItemFromList(string itemName)
        {
            for(int i = 0; i < itens.Count;i++)
            {
                if (itens[i].GetItemName().Equals(itemName))
                {
                    playerMoney += itens[i].GetItemValue();
                    itens.Remove(itens[i]);
                }
            }
            itens.RemoveAll(i => i == null);
        }

        public ItemScriptableObject GetItem(string itemName)
        {
            for (int i = 0; i < itens.Count; i++)
            {
                if(itens[i].GetItemName().Equals(itemName))
                {
                    return itens[i];
                }
            }
            return null;
        }

        public ItemScriptableObject GetLastItem()
        {
            return itens[itens.Count - 1];
        }

        public bool GetIsNewItemAdded() => isNewItemAdded;

        public void SetIsNewItemAdded(bool isAdded) => isNewItemAdded = isAdded;

        public List<ItemScriptableObject> GetListOfPlayersItens() => itens;
    }
}
