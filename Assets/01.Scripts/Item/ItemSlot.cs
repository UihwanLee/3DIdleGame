using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public InventoryUI inventory;

    public int index;
    public bool equipped;
    public int quantity;

    public bool isSelected;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Image highlightBG;

    public void Initialize(int index, InventoryUI inventory)
    {
        this.index = index;
        this.inventory = inventory;

        isSelected = false;
        icon = transform.FindChild<Image>("Icon");
        quantityText = transform.FindChild<TextMeshProUGUI>("Quntitiy");
        highlightBG = transform.FindChild<Image>("HighlighBG");

        highlightBG.gameObject.SetActive(false);
        quantityText.text = string.Empty;

        quantity = 0;
    }

    public void SelectSlot()
    {
        highlightBG.gameObject.SetActive(true);
    }

    public void ResetSlot()
    {
        highlightBG.gameObject.SetActive(false);
    }

    public void SetItem(ItemData item)
    {
        icon.sprite = item.data.icon;
        quantity++;
        quantityText.text = quantity.ToString();
    }

    public void PlusCanStackItem()
    {
        quantity = Mathf.Min(item.data.maxStackAmount, quantity + 1);
        quantityText.text = quantity.ToString();
    }
}
