using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DasEffect : EffectRunner 
{
    public int ID;

    private float lerpTime = 1f;
    private float currentLerpTime = 0f;

    public float moveDistance = 1f;

    private Vector2 startPos;
    private Vector2 endPos;

    private Text text;

    private float startTime = 0f;

    public void SetText(string t)
    {
        text.text = t;
    }
    void Awake()
    {
        text = this.GetComponentInChildren<Text>();
    }

    void Start () 
    {
        startTime = Time.time;
    }

    void Update () 
    {
        /*
        //increment timer once per frame
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //lerp!
        float perc = currentLerpTime / lerpTime;

        transform.position = Vector2.Lerp(startPos, endPos, perc);
        */



        if(Time.time - startTime > 2f)
        {
            DestroyObject();
        }
    }


    public override int AttachTo(Transform t)
    {
        throw new NotImplementedException();
    }

    public override void PassParameters(object[] parameters)
    {
    }
}
