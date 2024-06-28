using Store;
using UnityEngine;
using Util;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class StoreUI : MonoBehaviour
    {
        private bool playerStillInStore = false;

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
            GetComponent<CanvasGroup>().alpha = GetComponent<CanvasGroup>().alpha == 0 ? 1f : 0f;
            GetComponent<CanvasGroup>().interactable = !GetComponent<CanvasGroup>().interactable;
            GetComponent<CanvasGroup>().blocksRaycasts = !GetComponent<CanvasGroup>().blocksRaycasts;
        }
    
        public void BuyItem()
        {
            ItemScriptableObject item = StoreData.instance.GetItem(EventSystem.current.currentSelectedGameObject.name);
            Debug.Log(item.name);
        }
    }
}

