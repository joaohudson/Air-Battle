using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HEAL, BUFF
}

[CreateAssetMenu(menuName = "SO/Item")]
public class ItemInfo : ScriptableObject
{
    public ItemType itemType;
    [Header("Heal Type")]
    public int healAmount = 0;
    [Header("Buff Type")]
    public CharacterBuff buff = CharacterBuff.NONE;
}
