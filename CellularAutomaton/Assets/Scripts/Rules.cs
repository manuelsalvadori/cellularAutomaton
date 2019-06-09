using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules
{
    public enum rules : int
    {
        Life = 0,
        Highlife = 1,
        DayNight = 2,
        Seeds = 3
    }

    static readonly List<int> bLife = new List<int> { 3 };
    static readonly List<int> sLife = new List<int> { 2, 3 };

    static readonly List<int> bHighLife = new List<int> { 3, 6 };
    static readonly List<int> sHighLife = new List<int> { 2, 3 };

    static readonly List<int> bDayNight = new List<int> { 3, 6, 7, 8 };
    static readonly List<int> sDayNight = new List<int> { 3, 4, 6, 7, 8 };

    static readonly List<int> bSeeds = new List<int> { 2 };
    static readonly List<int> sSeeds = new List<int> { };

    public static readonly List<List<int>> bRules = new List<List<int>>
    {
        bLife,
        bHighLife,
        bDayNight,
        bSeeds
    };

    public static readonly List<List<int>> sRules = new List<List<int>>
    {
        sLife,
        sHighLife,
        sDayNight,
        sSeeds
    };



}
