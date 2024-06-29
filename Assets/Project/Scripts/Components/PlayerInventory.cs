using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory instance;

        [SerializeField] private int playerMoney = 30;
        [SerializeField] List<ItemScriptableObject> itens = new List<ItemScriptableObject>();

        private void Start() => instance = this;
      
        public bool SetNewItemToInventory(ItemScriptableObject item)
        {
            if (playerMoney >= item.GetItemValue())
            {
                playerMoney -= item.GetItemValue();
                itens.Add(item);
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

        public List<ItemScriptableObject> GetListOfPlayersItens() => itens;
    }
}
