using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory instance;

        [SerializeField] private int playerMoney = 30;
        [SerializeField] List<ItemScriptableObject> itens = new List<ItemScriptableObject>();

        private void Start()
        {
            instance = this;
        }

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
            foreach (ItemScriptableObject item in itens)
            {
                if (item.GetItemName().Equals(itemName))
                {
                    playerMoney = item.GetItemValue();
                    itens.Remove(item);
                }
            }
        }

        public List<ItemScriptableObject> GetListOfPlayersItens() => itens;
    }
}
