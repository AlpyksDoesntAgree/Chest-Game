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
    public GameObject TakeAnItemPanel;
    public TextMeshProUGUI TakeAnItemText;
    public Button[] InteractableBtns;
    private ChestScript _chestScript;
    private ButtonManager _btnManager;
    private void Start()
    {
        _chestScript = GameObject.Find("GenerateLoot").GetComponent<ChestScript>();
        _btnManager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
        TakeAnItemPanel.SetActive(false);
    }
    public void ChestClick()
    {
        _btnManager.ItemName = Loot.LootName;
        _btnManager.ChestID = Loot.ChestID;
        TakeAnItemPanel.SetActive(true);
        TakeAnItemText.text = $"Would you like to take {Loot.LootName} from chest {Loot.ChestID}";
        foreach(var item in InteractableBtns)
        {
            item.interactable = false;
        }
        foreach(var item in _chestScript.Chests)
        {
            item.GetComponent<Button>().interactable = false ;
        }
    }  
}
