using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Player;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryObjects;
    [SerializeField] private GameObject EquippedObjects;
    [SerializeField] private GameObject invetoryCard;
    [SerializeField] private GameObject equippedCard;
    [SerializeField] List<GameObject> inventoryCards = new List<GameObject>();
    [SerializeField] List<SpriteRenderer> spritePositions = new List<SpriteRenderer>();

    private float inventoryOn = 1f;
    private float inventoryOff = 0f;
    private int itensEquipped = 0;

    public static InventoryUI isntance;

    private void Start() => isntance = this;
    private void CreateNewInventoryCard()
    {
        GameObject inventoryCard = Instantiate(invetoryCard);
        inventoryCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerInventory.instance.GetLastItem().GetItemName();
        inventoryCard.transform.GetChild(1).GetComponent<Image>().sprite = PlayerInventory.instance.GetLastItem().GetItemSprite();
        inventoryCard.transform.parent = inventoryObjects.transform;
        inventoryCard.name = inventoryCard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        inventoryCard.GetComponent<Button>().onClick.AddListener(delegate () { EquipItem(EventSystem.current.currentSelectedGameObject.name);});
        inventoryCards.Add(inventoryCard);
    }

    private void EquipItem(string itemName)
    {
        if (itensEquipped < 3)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
            GameObject equippedItem = Instantiate(equippedCard);
            equippedItem.transform.parent = EquippedObjects.transform;
            ItemScriptableObject item = PlayerInventory.instance.GetItem(itemName);
            equippedItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetItemName();
            equippedItem.transform.GetChild(1).GetComponent<Image>().sprite = item.GetItemSprite();
            equippedItem.transform.GetComponent<Button>().onClick.AddListener(delegate () { UnequipItem(); });
            equippedItem.name = itemName;
            SetSpritOnPlayer(item.GetItemSprite());
            itensEquipped++;
        }
    }

    private void SetSpritOnPlayer(Sprite Item)
    {
        spritePositions[itensEquipped].sprite = Item;
    }

    private void UpdateInventory(string itemName) => inventoryObjects.transform.Find(itemName).GetComponent<Button>().interactable = true;

    public void RemoveItemFromInventory(string itemName)
    {
        Destroy(inventoryObjects.transform.Find(itemName).gameObject);

        if(EquippedObjects.transform.Find(itemName) != null)
            Destroy(EquippedObjects.transform.Find(itemName).gameObject);
    }

    private void UnequipItem()
    {
        itensEquipped--;
        spritePositions[itensEquipped].sprite = null;
        string itemName = EventSystem.current.currentSelectedGameObject.name;
        UpdateInventory(itemName);
        Destroy(EventSystem.current.currentSelectedGameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            ControlInventoryModal();

        if(PlayerInventory.instance.GetIsNewItemAdded())
        {
            CreateNewInventoryCard();
            PlayerInventory.instance.SetIsNewItemAdded(false);
        }
    }

    private void ControlInventoryModal()
    {
        GetComponent<CanvasGroup>().alpha = GetComponent<CanvasGroup>().alpha == inventoryOff ? inventoryOn : inventoryOff;
        GetComponent<CanvasGroup>().interactable = !GetComponent<CanvasGroup>().interactable;
        GetComponent<CanvasGroup>().blocksRaycasts = !GetComponent<CanvasGroup>().blocksRaycasts;
    }
}
