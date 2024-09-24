using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameData
{
    public long money = 1000;
    public long coffeeBean = 100;
    public int level = 1;
    public int customerCount = 4;
    public int maxCustomerCount = 10;
    public int thumbDownCount = 0;
    public float bgmVol;
    public float sfxVol;
}
