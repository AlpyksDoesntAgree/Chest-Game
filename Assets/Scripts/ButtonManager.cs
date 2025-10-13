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
    [HideInInspector] public Loot LootFromChest;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private TMP_InputField _inputField;

    public GameObject TakeAnItemPanel;
    public TextMeshProUGUI TakeAnItemText;
    void Start()
    {
        if (GameObject.Find("GenerateLoot") != null)
        {
            _chestScript = GameObject.Find("GenerateLoot").GetComponent<ChestScript>();
            _userInventory = GameObject.Find("UserInventory").GetComponent<UserInventory>();
        }
        if (_textMeshProUGUI != null)
            SetText();
    }
    public void RegenerateChestsBtn()
    {
        for (int i = 0; i < _chestScript.Chests.Length; i++)
        {
            _chestLogic = _chestScript.Chests[i].GetComponent<ChestLogic>();
            Debug.Log(_chestLogic.Loot.ChestID);
            _chestLogic.Loot = _chestScript.GenerateLoot(_chestLogic.Loot.ChestID);
        }
    }
    public void CancelBtn()
    {
        //Close Panel & Turn On Btns
        foreach (var chest in _chestScript.Chests)
        {
            var chestLogic = chest.GetComponent<ChestLogic>();
            if (TakeAnItemPanel.activeInHierarchy)
            {
                TakeAnItemPanel.SetActive(false);
                chestLogic.InteractableBtn.interactable = true;
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
        for (int i = 0; i < LootFromChest.LootName.Count; i++)
        {
            string chestItem = LootFromChest.LootName[i];
            for (int j = 0; j < _userInventory.Inventory.Items.Length; j++)
            {
                if (_userInventory.Inventory.Items[j] == chestItem)
                {
                    _userInventory.Inventory.CountItems[j]++;
                    break;
                }
            }
        }

        _userInventory.SaveInventory(_userInventory.Inventory.UsName, _userInventory.Inventory.CountItems);
        SetText();

        //Close Panel & Turn On Btns
        foreach (var chest in _chestScript.Chests)
        {
            var chestLogic = chest.GetComponent<ChestLogic>();
            if (TakeAnItemPanel.activeInHierarchy)
            {
                TakeAnItemPanel.SetActive(false);
                chestLogic.InteractableBtn.interactable = true;
            }
        }
        foreach (var item in _chestScript.Chests)
        {
            item.GetComponent<Button>().interactable = true;
        }

        _chestScript.GenerateLoot(LootFromChest.ChestID);
    }
    public void SetText()
    {
        _textMeshProUGUI.text = "";
        for (int i = 0; i < _userInventory.Inventory.Items.Length; i++)
        {
            _textMeshProUGUI.text += _userInventory.Inventory.Items[i].ToString()
                + "-" + _userInventory.Inventory.CountItems[i].ToString() + '\n';
        }
    }
}
