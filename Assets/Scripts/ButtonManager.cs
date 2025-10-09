using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    private ChestScript _chestScript;
    private ChestLogic _chestLogic;
    private UserInventory _userInventory;
    [HideInInspector] public string ItemName;
    [HideInInspector] public int ChestID;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    private int[] _updatedItemCounts = new int[] { 0, 0, 0, 0, 0 };
    [SerializeField] private TMP_InputField _inputField;
    void Start()
    {
        if (GameObject.Find("GenerateLoot") != null)
        {
            _chestScript = GameObject.Find("GenerateLoot").GetComponent<ChestScript>();
            _userInventory = GameObject.Find("UserInventory").GetComponent<UserInventory>();
        }
        SetText();
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
    public void SignIn()
    {
        if (_inputField.text.Trim() == _inputField.text &&
            Regex.IsMatch(_inputField.text, "^[a-zA-Z0-9]+$"))
        {
            PlayerPrefs.SetString("username", _inputField.text);
            SceneManager.LoadScene("Chests");
        }
        else
        {
            Debug.Log("Спецсимволы или пробел");
        }
    }
    public void TakeBtn()
    {
        for (int i = 0; i < 5; i++)
        {
            if (_userInventory.Inventory.Items[i] == ItemName)
            {
                _updatedItemCounts[i] = _userInventory.Inventory.CountItems[i] + 1;
            }
            else
            {
                _updatedItemCounts[i] = _userInventory.Inventory.CountItems[i];
            }
        }

        Inventory updatedInv = new Inventory()
        {
            UsName = _userInventory.Inventory.UsName,
            Items = _userInventory.Inventory.Items,
            CountItems = _updatedItemCounts,
        };

        _userInventory.SaveInventory(updatedInv.UsName, updatedInv.CountItems);
        _userInventory.Inventory = updatedInv;

        SetText();

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

        _chestScript.GenerateLoot(ChestID);
    }
    public void SetText()
    {
        _textMeshProUGUI.text = "";
        for (int i = 0; i < _userInventory.Inventory.Items.Length; i++) {
            _textMeshProUGUI.text += _userInventory.Inventory.Items[i].ToString() 
                +"-"+ _userInventory.Inventory.CountItems[i].ToString() + '\n';
        }     
    }
}
