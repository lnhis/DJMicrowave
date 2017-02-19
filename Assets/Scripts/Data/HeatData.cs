using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HeatData 
{
    public float Delay;
    public float Powah;
    public float Distance;
    public SoundSystem.SoundTypes Sound;

    public float LastShootTime { get; set; }
}
