using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUpgrade : MonoBehaviour
{
     List<Upgrades> upgrades = new List<Upgrades>();

     void Start()
     {
        TextAsset upgradedata = Resources.Load<TextAsset>("upgradedata");

        string[] data = upgradedata.text.Split(new char[] {char.Parse(",") });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] {char.Parse(",") });
            Upgrades u = new Upgrades();
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

        foreach (Upgrades u in upgrades)
        {
            Debug.Log(u.tier);
        } 
    }

    /*void Start2()
    {
        TextAsset upgradedata = Resources.Load<TextAsset>("upgradedata");

        string[] data = upgradedata.text.Split(new char[] { char.Parse(",") });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { char.Parse(",") });
            Upgrades u = new Upgrades();
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
        }

        foreach (Upgrades u in upgrades)
        {
            Debug.Log(u.tier);
        }
    } */

}

