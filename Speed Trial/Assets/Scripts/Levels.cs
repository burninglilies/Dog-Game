using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public int createLevels()
    {

        var lines = System.IO.File.ReadAllText("Levels.csv").Replace(" ", "").ToCharArray();

        int[,] levels = new int[61, 8];

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 61; y++)
            {
                levels[x, y] = int.Parse(lines[x].ToString());
            }
        }

        return levels[0, 0];
    }


    void Start()
    {
        System.Console.Write(createLevels());
    }

    void Update()
    {
        
    }
}
