using UnityEngine;
using System.Collections;
using System;
using Enums;

[Serializable]
public class LevelData 
{
    public string Name;
    public FoodType[] Foods;
    public int Level;
    public float Timer;
    public GameObject Microwave;
}
