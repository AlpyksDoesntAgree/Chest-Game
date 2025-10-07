using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _chestLogic.UpdateText();
        }
    }
}
