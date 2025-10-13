using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private ChestLogic _lootFromChestLogic;
    [HideInInspector] public int Id;
    private List<string> _lootList = new List<string>();
    private int _randomAmountOfItems;
    private int _randomItem;
    [HideInInspector] public GameObject[] Chests;
    [SerializeField] private int _maxAmount;
    [SerializeField] private bool _isInfinite = true;

    void Start()
    {
        Chests = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < Chests.Length; i++)
        {
            Id = i;
            _lootFromChestLogic = Chests[i].GetComponent<ChestLogic>();
            _lootFromChestLogic.Loot = LoadChest(i);

            if (_lootFromChestLogic.Loot.LootName == null)
            {
                GenerateLoot(i);
            }
        }
    }

    public Loot GenerateLoot(int chestID)
    {
        //Generate List
        _lootList.Clear();
        List<string> lootList = new List<string>();
        if (_isInfinite)
        {
            _randomAmountOfItems = Random.Range(3, int.MaxValue);
        }
        else
        {
            _randomAmountOfItems = Random.Range(3, _maxAmount+1);
        }
        for (int i = 0; i < _randomAmountOfItems; i++)
        {
            _randomItem = Random.Range(0, 5);
            switch (_randomItem)
            {
                case 0:
                    lootList.Add("Gold");
                    break;
                case 1:
                    lootList.Add("Diamond");
                    break;
                case 2:
                    lootList.Add("Coal");
                    break;
                case 3:
                    lootList.Add("Boots");
                    break;
                case 4:
                    lootList.Add("Coin");
                    break;
            }
        }
        _lootList = lootList;

        foreach (var chest in Chests)
        {
            var chestLogic = chest.GetComponent<ChestLogic>();
            if (chestLogic.Loot.ChestID == chestID)
            {
                _lootFromChestLogic = chestLogic;
                break;
            }
        }

        _lootFromChestLogic.Loot = SaveChest(chestID);
        return _lootFromChestLogic.Loot;
    }
    private Loot SaveChest(int chestID)
    {
        List<string> lootCopy = new List<string>(_lootList);

        Loot generatedLoot = new Loot()
        {
            ChestID = chestID,
            LootName = lootCopy
        };

        string json = JsonUtility.ToJson(generatedLoot);
        string path = Path.Combine(Application.persistentDataPath, $"chest_{chestID}.json"); 
        File.WriteAllText(path, json);

        return generatedLoot;
    }
    private Loot LoadChest(int chestID)
    {
        string path = Path.Combine(Application.persistentDataPath, $"chest_{chestID}.json"); 
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Loot loadedLoot = JsonUtility.FromJson<Loot>(json);
            return loadedLoot;
        }
        return new Loot()
        {
            ChestID = chestID,
            LootName = null
        };
    }
}