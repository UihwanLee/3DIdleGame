using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipalbe,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Health,
    Speed
}

[Serializable]
public class WeaponInfo
{
    public float baseDamage;
    public int enhanceLevel;
    public int enhancePrice;
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Weapon")]
    public WeaponInfo weaponInfo;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
