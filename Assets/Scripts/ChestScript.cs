using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private ChestLogic _lootFromChestLogic;
    [HideInInspector] public int Id;
    private string _lootName;
    private int _random;
    [HideInInspector] public GameObject[] Chests;
    void Start()
    {
        Chests = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < Chests.Length; i++)
        {
            Id = i;
            _lootFromChestLogic = Chests[i].GetComponent<ChestLogic>();
            _lootFromChestLogic.Loot = LoadChest();

            if (_lootFromChestLogic.Loot.LootName == null)
            {
                GenerateLoot();
            }
        }
    }
    public Loot GenerateLoot()
    {
        _random = Random.Range(0, 5);
        switch (_random)
        {
            case 0:
                _lootName = "Gold";
                break;
            case 1:
                _lootName = "Coal";
                break;
            case 2:
                _lootName = "Boots";
                break;
            case 3:
                _lootName = "Diamond";
                break;
            case 4:
                _lootName = "Coin";
                break;
        }

        _lootFromChestLogic.Loot = SaveChest();
        return _lootFromChestLogic.Loot;
    }
    private Loot SaveChest()
    {
        Loot generatedLoot = new Loot()
        {
            ChestID = Id,
            LootName = _lootName
        };

        string json = JsonUtility.ToJson(generatedLoot);
        string path = Path.Combine(Application.persistentDataPath, $"chest_{Id}.json");
        File.WriteAllText(path, json);

        return generatedLoot;
    }
    private Loot LoadChest()
    {
        string path = Path.Combine(Application.persistentDataPath, $"chest_{Id}.json");
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Loot loadedLoot = JsonUtility.FromJson<Loot>(json);
            return loadedLoot;
        }
        return new Loot()
        {
            ChestID = Id,
            LootName = null
        };
    }
}
