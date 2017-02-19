using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EffectSystem : MonoBehaviour 
{
    private static int effectCounter = 0;
    public static int GetEffectID() { effectCounter++; return effectCounter;}
    public GameObject[] effects;

    private Transform container;
    
    private Dictionary<int, EffectRunner> liveEffects = new Dictionary<int, EffectRunner>();

    public void StopAllEffects(string effectName)
    {
        bool hadEffect = false;
        do
        {
            hadEffect = false;
            foreach (EffectRunner o in liveEffects.Values)
            {
                if (o.effectName == effectName)
                {
                    this.RemoveEffect(o.effectId);
                    hadEffect = true;
                    break;
                }
            }
        }
        while (hadEffect);
    }
    
    private EffectRunner CreateEffect(string effectName, Vector3? position = null, object[] parameters = null, Transform attachTo = null)
    {
        GameObject o = null;
        foreach (GameObject to in effects)
        {
            if(to.name == effectName)
            {
                o = to;
            }
        }
        if(o == null)
        {
            Debug.LogError("Failed to start effect: " + effectName);
            return null;
        }
        GameObject newObj = GameObject.Instantiate(o);
        newObj.name = effectName;
        EffectRunner control = newObj.AddComponent<DasEffect>();
        // Debug.LogWarning("CONTROL: " + control);
        control.effectId = GetEffectID();
        if(attachTo!=null)
            control.AttachTo(attachTo);

        if (parameters != null)
            control.PassParameters(parameters);

        if(position.HasValue)
        {
            if(attachTo!=null)
            {
                control.transform.localPosition = position.Value;
            }
            else
            {
                control.transform.position = position.Value;
            }
        }

        newObj.transform.SetParent(container);
        liveEffects.Add(control.effectId, control);
        return control;
    }

    public EffectRunner PlayResult(int winnerTeam)
    {
        object[] parameters = new object[1];
        parameters[0] = winnerTeam;
        EffectRunner newObj = CreateEffect("ResultEffect", Vector2.zero, parameters, null);
        return newObj;
    }
    public EffectRunner PlayFloater(Vector2 position, string text, Vector2? endPos = null)
    {
        object[] parameters;
        if (endPos.HasValue)
        {
            parameters = new object[3];
            parameters[2] = endPos.Value;
        }
        else
        {
            parameters = new object[2];
        }
        
        parameters[0] = text;
        parameters[1] = position;

        EffectRunner newObj = CreateEffect("TextFloater", position, parameters, null);
        return newObj;
    }
    public EffectRunner PlayEffect(string effectName, Vector3 position, float power)
    {
        object[] parameters = new object[1];
        parameters[0] = power;
        EffectRunner newObj = CreateEffect(effectName, position, parameters, null);
        return newObj;
    }
    public EffectRunner PlaySmokeEffect(Vector2 position)
    {
        object[] parameters = new object[1];
        parameters[0] = 1.5f;
        EffectRunner newObj = CreateEffect("SmokeEffect", position, parameters, null);
        Vector3 v3 = newObj.transform.position;
        v3.z = -0.5f;
        newObj.transform.position = v3;
        return newObj;
    }
    public void PlayProgress(Vector2 position, float start, float end)
    {
        object[] parameters = new object[3];
        parameters[0] = 1f;
        parameters[1] = start;
        parameters[2] = end;
        EffectRunner newObj = CreateEffect("ProgressBarFloater", position, parameters, null);
    }
    public void PlayPoisonEffect(Vector2 position)
    {
        EffectRunner newObj = CreateEffect("PoisonEffect", position, null, null);
        Vector3 v3 = newObj.transform.position;
        v3.z = -0.5f;
        newObj.transform.position = v3;
    }
    public void PlayBloodEffect(Vector2 position)
    {
        object[] parameters = new object[1];
        parameters[0] = 1.5f;
        EffectRunner newObj = CreateEffect("BloodEffect", position, parameters, null);
        Vector3 v3 = newObj.transform.position;
        v3.z = -1f;
        newObj.transform.position = v3;
    }
    void Awake()
    {
        container = this.transform.FindChild("EffectContainer");
    }
	void Start () {
	}
	
	void Update () 
    {
	
	}

    public bool RemoveEffect(int ID)
    {
        if(liveEffects.ContainsKey(ID))
        {
            GameObject.Destroy(liveEffects[ID].gameObject);
            liveEffects.Remove(ID);
            return true;
        }
        return false;
    }

    private static EffectSystem instance = null;
    public static EffectSystem Instance
    {
        get 
        {
            if (instance != null)
                return instance;

            GameObject goTemplate = Resources.Load ("EffectSystem") as GameObject;
            GameObject go = GameObject.Instantiate (goTemplate);
            go.transform.name = "EffectSystem";
            instance = go.GetComponent<EffectSystem>();
            return instance;
        }
    }
    
}
