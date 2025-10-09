using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class UserInventory : MonoBehaviour
{
    public Inventory Inventory;
    private string _currentUser; 
    void Awake()
    {
        _currentUser = PlayerPrefs.GetString("username", "guest");
        Inventory = LoadInventory(_currentUser);
    }
    public Inventory SaveInventory(string userName, int[] count)
    {
        Inventory generatedInventory = new Inventory()
        {
            UsName = userName,
            Items = new string[] { "Gold", "Diamond", "Coal", "Boots", "Coin" },
            CountItems = count,
        };

        string json = JsonUtility.ToJson(generatedInventory);
        string path = Path.Combine(Application.persistentDataPath, $"userInv_{userName}.json");
        File.WriteAllText(path, json);

        return generatedInventory;
    }
    public Inventory LoadInventory(string userName)
    {
        string path = Path.Combine(Application.persistentDataPath, $"userInv_{userName}.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Inventory loadIn = JsonUtility.FromJson<Inventory>(json);
            return loadIn;
        }
        Inventory newUser = new Inventory()
        {
            UsName = userName,
            Items = new string[] { "Gold", "Diamond", "Coal", "Boots", "Coin" },
            CountItems = new int[] { 0, 0, 0, 0, 0 },
        };
        SaveInventory(userName, newUser.CountItems);
        return newUser;
    }
}
