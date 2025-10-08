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
    public GameObject TakeAnItemPanel;
    public TextMeshProUGUI TakeAnItemText;
    public Button[] InteractableBtns;
    private ChestScript _chestScript;
    private void Start()
    {
        _chestScript = GameObject.Find("GenerateLoot").GetComponent<ChestScript>();
        TakeAnItemPanel.SetActive(false);
    }
    public void ChestClick()
    {
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
