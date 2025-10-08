using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private ChestScript _chestScript;
    private ChestLogic _chestLogic;
    void Start()
    {
        _chestScript = GameObject.Find("GenerateLoot").GetComponent<ChestScript>();
    }
    public void RegenerateChestsBtn()
    {
        for (int i = 0; i < _chestScript.Chests.Length; i++)
        {
            _chestLogic = _chestScript.Chests[i].GetComponent<ChestLogic>();
            _chestScript.Id = i;
            _chestLogic.Loot = _chestScript.GenerateLoot();
        }
    }
    public void CancelBtn()
    {
        foreach (var chest in _chestScript.Chests)
        {
            var chestLogic = chest.GetComponent<ChestLogic>();
            if (chestLogic.TakeAnItemPanel.activeInHierarchy)
            {
                chestLogic.TakeAnItemPanel.SetActive(false);

                foreach (var item in chestLogic.InteractableBtns)
                {
                    item.interactable = true;
                }
            }
        }
        foreach (var item in _chestScript.Chests)
        {
            item.GetComponent<Button>().interactable = true;
        }
    }
}
