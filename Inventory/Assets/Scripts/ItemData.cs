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

    public eType type = eType.None;             // ������ Ÿ��
    public string name = string.Empty;          // ������ �̸�
    public string description = string.Empty;   // ������ ����
    public int count = 1;                       // ������ ����
    public Sprite sprite = null;                // ������ �ؽ���
}