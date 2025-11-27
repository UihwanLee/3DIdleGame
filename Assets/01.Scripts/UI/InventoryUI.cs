using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;

    [SerializeField] private GameObject _inventoryWindow;
    [SerializeField] private Transform _slotPanel;

    [Header("Select Item")]
    [SerializeField] private Image _selectedItemIcon;
    [SerializeField] private TextMeshProUGUI _selectedItemName;
    [SerializeField] private TextMeshProUGUI _selectedItemDescription;
    [SerializeField] private TextMeshProUGUI _curDamageStat;
    [SerializeField] private TextMeshProUGUI _curSpeedStat;
    [SerializeField] private GameObject _useButton;
    [SerializeField] private GameObject _enhanceButton;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private TextMeshProUGUI _enhancePrice;

    [Header("StartItem")]
    [SerializeField] private List<ItemData> _startItem;

    private ItemSlot _currentSelectedSlot;
    private ItemSlot _currentEquipSlot;

    private Player _player;

    private void Awake()
    {
        Initialize();
    }

    private void Reset()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.ItemGetEvent -= AddItem;
        }
    }

    private void Initialize()
    {
        _inventoryWindow = GameObject.Find("InventoryWindow");
        _slotPanel = transform.FindChild<Transform>("InventorySlots");

        _selectedItemIcon = transform.FindChild<Image>("CurItemImg");
        _selectedItemName = transform.FindChild<TextMeshProUGUI>("CurItemName");
        _selectedItemDescription = transform.FindChild<TextMeshProUGUI>("CurItemDesc");
        _curDamageStat = transform.FindChild<TextMeshProUGUI>("Damage Stat");
        _curSpeedStat = transform.FindChild<TextMeshProUGUI>("Speed Stat");
        _useButton = GameObject.Find("Interaction_consume");
        _enhanceButton = GameObject.Find("Interaction_enhacne");
        _equipButton = GameObject.Find("Interaction_equip");
        _enhancePrice = transform.FindChild<TextMeshProUGUI>("enhance_gold");
    }

    private void Start()
    {
        _player = GameManager.Instance.Player;
        _player.ItemGetEvent += AddItem;

        _inventoryWindow.SetActive(false);
        slots = new ItemSlot[_slotPanel.childCount];

        for(int i=0; i<slots.Length; i++)
        {
            int index = i;
            slots[i] = _slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].Initialize(index, this);
            slots[i].GetComponent<Button>().onClick.AddListener(()=> SelectedItem(index));
        }

        ClearSelectedItemWindow();
        GetStartEquipment();
    }

    public void ClearSelectedItemWindow()
    {
        _selectedItemIcon.gameObject.SetActive(false);
        _selectedItemName.text = string.Empty;
        _selectedItemDescription.text = string.Empty;
        //curDamageStat.text = string.Empty;
        //curSpeedStat.text = string.Empty;

        _enhancePrice.text = string.Empty;
        _useButton.SetActive(false);
        _enhanceButton.SetActive(false);
    }

    private void GetStartEquipment()
    {
        // 초반 시작에 검을 들고 시작
        foreach(ItemData item in _startItem)
        {
            AddItem(item);
        }

        // 무기 장착
        EquipItem(slots[0]);
    }

    public void SelectedItem(int index)
    {
        ClearSelectedItemWindow();

        slots[index].isSelected = !slots[index].isSelected;

        if(_currentSelectedSlot != null)
        {
            _currentSelectedSlot.ResetSlot();
        }

        if(_currentSelectedSlot == slots[index])
        {
            _currentSelectedSlot.ResetSlot();
            _currentSelectedSlot = null;
        }
        else
        {
            _currentSelectedSlot = slots[index];
            _currentSelectedSlot.SelectSlot();
        }

        if(_currentSelectedSlot != null)
        {
            ItemData item = _currentSelectedSlot.item;
            if (item != null) SetSelectedItme(item, slots[index]);
        }
    }

    private void SetSelectedItme(ItemData item, ItemSlot slot)
    {
        _selectedItemIcon.gameObject.SetActive(true);
        _selectedItemIcon.sprite = item.data.icon;
        _selectedItemName.text = item.data.displayName;
        _selectedItemDescription.text = item.data.description;
        
        // 아이템 타입에 따라 강화 or 소비 버튼 표시
        switch(item.data.type)
        {
            case ItemType.Equipalbe:
                _enhanceButton.SetActive(true);
                _enhancePrice.text = item.data.weaponInfo.enhancePrice.ToString();
                _selectedItemName.text += $" (+{item.data.weaponInfo.enhanceLevel}강)";
                _equipButton.GetComponentInChildren<Button>().onClick.AddListener(() => EquipItem(slot));
                _enhanceButton.GetComponentInChildren<Button>().onClick.AddListener(() => EnhacneItem(item));
                break;
            case ItemType.Consumable:
                _useButton.SetActive(true);
                _useButton.GetComponentInChildren<Button>().onClick.AddListener(() => UseItem(item));
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
            if (slot.item == null)
            {
                slot.SetItem(item);
                return;
            }
        }
    }

    public void EquipItem(ItemSlot slot)
    {
        // 슬롯 설정
        if(_currentEquipSlot != null)
        {
            _currentEquipSlot.SetEquip(false);
        }

        _currentEquipSlot = slot;
        _currentEquipSlot.SetEquip(true);

        // 플레이어에게 전달
        ItemData weapon = _currentEquipSlot.item;
        if(weapon != null)
        {
            _player.CurrentWeapon = weapon;

            UpdatePlayerInfo();
        }
    }

    public void EnhacneItem(ItemData item)
    {

    }

    public void UseItem(ItemData item)
    {

    }

    private void UpdatePlayerInfo()
    {
        float weaponAtk = _currentEquipSlot.item.data.weaponInfo.baseDamage;
        float playerAtk = _player.Condition.Atk;
        _curDamageStat.text = $"{playerAtk + weaponAtk} (+{weaponAtk})";
    }
}
