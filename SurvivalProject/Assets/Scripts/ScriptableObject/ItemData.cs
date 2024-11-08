using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resourse
}

public enum ConsumableType
{
    Health,
    Hunger
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu (fileName = "Item", menuName ="New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName; //아이템 이름
    public string description; //아이템 설명
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;
    
    [Header("Stacking")] //몇 개까지 소지 가능한지
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")] //
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
}
