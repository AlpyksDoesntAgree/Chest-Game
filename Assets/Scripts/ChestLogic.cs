using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Structs
[System.Serializable]
public struct Loot
{
    public int ChestID;
    public List<string> LootName;
}
[System.Serializable]
public struct Inventory
{
    public string UsName;
    public string[] Items;
    public int[] CountItems;
}


public class ChestLogic : MonoBehaviour
{
    public Loot Loot;
    private GameObject _takeAnItemPanel;
    private TextMeshProUGUI _takeAnItemText;
    [HideInInspector] public Button InteractableBtn;
    private ChestScript _chestScript;
    private ButtonManager _btnManager;
    private void Start()
    {
        _chestScript = GameObject.Find("GenerateLoot").GetComponent<ChestScript>();
        _btnManager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();

        InteractableBtn = GameObject.Find("RegenerateBtn").GetComponent<Button>();
        _takeAnItemPanel = _btnManager.TakeAnItemPanel;
        _takeAnItemText = _btnManager.TakeAnItemText;

        _takeAnItemPanel.SetActive(false);
    }
    public void ChestClick()
    {
        _btnManager.LootFromChest = Loot;
        _takeAnItemPanel.SetActive(true);
        _takeAnItemText.text = "";
        for (int i = 0; i < Loot.LootName.Count; i++)
        {
            if(i == Loot.LootName.Count-1)
                _takeAnItemText.text += $"{Loot.LootName[i]}.";
            else
                _takeAnItemText.text += $"{Loot.LootName[i]}, ";
        }
        InteractableBtn.interactable = false;
        foreach(var item in _chestScript.Chests)
        {
            item.GetComponent<Button>().interactable = false ;
        }
    }  
}
