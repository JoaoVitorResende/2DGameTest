using UnityEngine;

namespace Store
{
    public class StoreInteraction : MonoBehaviour
    {
        public static StoreInteraction instance;

        private bool isOnRangeToBuy = false;

        private void Start() => instance = this;
       
        private void OnTriggerEnter2D(Collider2D collision) => ChangeIsOnRangeStatus(true);
       
        private void OnTriggerExit2D(Collider2D collision) => ChangeIsOnRangeStatus(false);

        private void ChangeIsOnRangeStatus(bool isOnRange) => isOnRangeToBuy = isOnRange;

        public bool GetIsOnRangeToBuy() => isOnRangeToBuy;
    }
}
