using System;
using UnityEngine;

[Serializable]
public class ItemData {
    public enum eType {
        None,
        Equip,
        Consumable,
        Material
    }

    public eType type = eType.None;             // 아이템 타입
    public string name = string.Empty;          // 아이템 이름
    public string description = string.Empty;   // 아이템 설명
    public int count = 1;                       // 아이템 갯수
    public Sprite sprite = null;                // 아이템 텍스쳐
}
