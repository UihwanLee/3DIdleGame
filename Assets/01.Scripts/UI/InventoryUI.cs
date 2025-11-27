using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("Select Item")]
    [SerializeField] private Image selectedItemIcon;
    [SerializeField] private TextMeshProUGUI selectedItemName;
    [SerializeField] private TextMeshProUGUI selectedItemDescription;
    [SerializeField] private TextMeshProUGUI curDamageStat;
    [SerializeField] private TextMeshProUGUI curSpeedStat;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject enhanceButton;
    [SerializeField] private TextMeshProUGUI enhancePrice;

    private ItemSlot currentSelectedSlot;
    private Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i=0; i<slots.Length; i++)
        {
            int index = i;
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].Initialize(index, this);
            slots[i].GetComponent<Button>().onClick.AddListener(()=> SelectedItem(index));
        }

        ClearSelectedItemWindow();
    }

    public void ClearSelectedItemWindow()
    {
        selectedItemIcon.gameObject.SetActive(false);
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        //curDamageStat.text = string.Empty;
        //curSpeedStat.text = string.Empty;

        enhancePrice.text = string.Empty;
        useButton.SetActive(false);
        enhanceButton.SetActive(false);
    }

    public void SelectedItem(int index)
    {
        ClearSelectedItemWindow();

        slots[index].isSelected = !slots[index].isSelected;

        if(currentSelectedSlot != null)
        {
            currentSelectedSlot.ResetSlot();
        }

        if(currentSelectedSlot == slots[index])
        {
            currentSelectedSlot.ResetSlot();
            currentSelectedSlot = null;
        }
        else
        {
            currentSelectedSlot = slots[index];
            currentSelectedSlot.SelectSlot();
        }

        if(currentSelectedSlot != null)
        {
            ItemData item = currentSelectedSlot.item;
            if (item != null) SetSelectedItme(item);
        }
    }

    private void SetSelectedItme(ItemData item)
    {
        selectedItemIcon.gameObject.SetActive(true);
        selectedItemIcon.sprite = item.data.icon;
        selectedItemName.text = item.data.displayName;
        selectedItemDescription.text = item.data.description;
        
        // 아이템 타입에 따라 강화 or 소비 버튼 표시
        switch(item.data.type)
        {
            case ItemType.Equipalbe:
                enhanceButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useButton.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void AddItem(ItemData item)
    {
        // 플레이어가 아이템 흭득 시 인벤토리에 추가
        foreach(var slot in slots)
        {
            if(slot.item != null)
            {
                // 아이템 타입이 Consumable이고 동일 타입이면 Stack 비교
                if (item.data.type == ItemType.Consumable && slot.item.data.type == ItemType.Consumable)
                {
                    if (item.data.consumables[0].type == slot.item.data.consumables[0].type)
                    {
                        slot.PlusCanStackItem();
                        return;
                    }
                }

                // 기타 아이템 확인
                if(item.data.type == ItemType.Resource && slot.item.data.type == ItemType.Resource)
                {
                    slot.PlusCanStackItem();
                    return;
                }
            }

            // 일치하지 않다면 빈 슬롯에 저장
            if (slot.item == null) slot.SetItem(item);
        }
    }
}
