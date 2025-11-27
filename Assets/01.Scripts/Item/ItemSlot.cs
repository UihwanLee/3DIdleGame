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

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quantityText;
    [SerializeField] private Image _highlightBG;
    [SerializeField] private TextMeshProUGUI _equipIcon;

    public void Initialize(int index, InventoryUI inventory)
    {
        this.index = index;
        this.inventory = inventory;

        isSelected = false;
        _icon = transform.FindChild<Image>("Icon");
        _quantityText = transform.FindChild<TextMeshProUGUI>("Quntitiy");
        _highlightBG = transform.FindChild<Image>("HighlighBG");
        _equipIcon = transform.FindChild<TextMeshProUGUI>("Equip");

        _highlightBG.gameObject.SetActive(false);
        _quantityText.text = string.Empty;
        _equipIcon.gameObject.SetActive(false);

        quantity = 0;
    }

    public void SelectSlot()
    {
        _highlightBG.gameObject.SetActive(true);
    }

    public void ResetSlot()
    {
        _highlightBG.gameObject.SetActive(false);
    }

    public void SetItem(ItemData item)
    {
        this.item = item;
        _icon.sprite = item.data.icon;
        quantity++;
        _quantityText.text = quantity.ToString();
    }

    public void PlusCanStackItem()
    {
        quantity = Mathf.Min(item.data.maxStackAmount, quantity + 1);
        _quantityText.text = quantity.ToString();
    }

    public void SetEquip(bool equip)
    {
        _equipIcon.gameObject.SetActive(equip);
    }
}
