using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Loot
{
    public int ChestID;
    public string LootName;
}
public class ChestLogic : MonoBehaviour
{
    public Loot Loot;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Transform _textPos;
    void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        if (_text != null)
        {
            _text.text = $"{Loot.LootName}";
        }
    }
}
