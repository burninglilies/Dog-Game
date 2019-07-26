using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSpreadSheets : MonoBehaviour
{
    List<UpgradesAndLevels> upgrades = new List<UpgradesAndLevels>();
    List<UpgradesAndLevels> levels = new List<UpgradesAndLevels>();

    Dictionary<string, Queue<UpgradesAndLevels>> upgradesByType = new Dictionary<string, Queue<UpgradesAndLevels>>();

    void Start()
    {
        //upgradesByType["attribute1"] = new Queue<UpgradesAndLevels>();
        //Queue<int> nextUpgrade = new Queue<int>();
        //nextUpgrade.Enqueue(5);
        loadUpgrades();
        loadLevels();
    }
    

    void loadUpgrades()
    {
        TextAsset upgradedata = Resources.Load<TextAsset>("upgradedata");

        string[] data = upgradedata.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            UpgradesAndLevels u = new UpgradesAndLevels();
            u.tier = int.Parse(row[0]);
            u.category = int.Parse(row[1]);
            u.index = int.Parse(row[2]);
            u.cost = int.Parse(row[3]);
            u.a1 = int.Parse(row[4]);
            u.a2 = int.Parse(row[5]);
            u.a3 = int.Parse(row[6]);
            u.a4 = int.Parse(row[7]);
            u.a5 = int.Parse(row[8]);
            upgrades.Add(u);
            upgradesByType["attribute1"].Enqueue(u);
        }
    }

    void loadLevels()
    {
        TextAsset leveldata = Resources.Load<TextAsset>("leveldata");
        string[] data = leveldata.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
            UpgradesAndLevels l = new UpgradesAndLevels();
            l.mTier = int.Parse(row[0]);
            l.levelIndex = int.Parse(row[1]);
            l.reward = int.Parse(row[2]);
            l.a1req = int.Parse(row[3]);
            l.a2req = int.Parse(row[4]);
            l.a3req = int.Parse(row[5]);
            l.a4req = int.Parse(row[6]);
            l.a5req = int.Parse(row[7]);
            levels.Add(l);
            upgradesByType["attribute1"].Enqueue(l);
        }
    }



    void Start1()
    {
        TextAsset upgradedata = Resources.Load<TextAsset>("upgradedata");

        string[] data = upgradedata.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            UpgradesAndLevels u = new UpgradesAndLevels();

            int.TryParse(row[0], out u.tier);
            int.TryParse(row[1], out u.category);
            int.TryParse(row[2], out u.index);
            int.TryParse(row[3], out u.cost);
            int.TryParse(row[4], out u.a1);
            int.TryParse(row[5], out u.a2);
            int.TryParse(row[6], out u.a3);
            int.TryParse(row[7], out u.a4);
            int.TryParse(row[8], out u.a5);
            upgrades.Add(u);
        }

        foreach (UpgradesAndLevels u in upgrades)
        {
            Debug.Log(u.tier);
        }
    }
}