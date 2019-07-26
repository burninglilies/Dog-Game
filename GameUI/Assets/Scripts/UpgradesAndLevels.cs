using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesAndLevels
{
    [Header("Upgrade Spreadsheet List")]
    public int tier;
    public int category;
    public int index;
    public int cost;
    public int a1;
    public int a2;
    public int a3;
    public int a4;
    public int a5;

    [Header("Levels and Rewards List")]
    public int mTier;
    public int levelIndex;
    public int reward;
    public int a1req;
    public int a2req;
    public int a3req;
    public int a4req;
    public int a5req;

    [Header("User Stats")]
    public int balance = 0;
    public int gbalance = 0;
    public int currenta1 = 25;
    public int currenta2 = 25;
    public int currenta3 = 25;
    public int currenta4 = 25;
    public int currenta5 = 25;
}
