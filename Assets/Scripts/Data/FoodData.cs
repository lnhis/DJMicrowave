using UnityEngine;
using System.Collections;
using System;
using Enums;

[Serializable]
public class FoodData 
{
    public string Name;
    public FoodType Type;
    public GameObject Gobject;
    public float MaxHeat;
    public float GoodHeat;
    public float Scaling = 1f;
}
