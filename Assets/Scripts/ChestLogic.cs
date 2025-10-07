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
    [SerializeField] private GameObject _lootName;
    [SerializeField] private Transform _textPos;
    private TextMeshPro _text;
    void Start()
    {
        Debug.Log(Loot.LootName);
    }
}
