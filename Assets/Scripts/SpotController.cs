using UnityEngine;
using System.Collections;
using Enums;
using System.Linq;

public class SpotController : MonoBehaviour 
{
    private FoodData food;
    private GameObject visuals;

    public float Heat = 20f;
    private float currentHeat = 20f;
    private float TargetHeat = 20f;
    private float heatLerp = 0f;

    private MeshRenderer visualRenderer;

    public ProgressbarController ProgressBar;

    private float lastHeat = 0f;

    void Awake()
    {
        ProgressBar.gameObject.SetActive(false);
    }
    
	void Start () 
    {
	
	}
    public void ChangeHeat(float change)
    {
        heatLerp = 0f;
        TargetHeat += change;
    }
    public bool IsOk()
    {
        float vara = food.MaxHeat - food.GoodHeat;
        float ab = Mathf.Abs(food.GoodHeat - Heat);

        /*
        Debug.LogWarning(food.Type + " Heat: " + Heat + " vara: " + vara + " dif: " + 
            ab+" and "+(vara > ab)+" goodH: "+food.GoodHeat);
        */
        
        return vara > ab;
    }
    public bool IsBurnt()
    {
        return Heat > food.MaxHeat;
    }
    
	void Update () 
    {
        if (food == null)
            return;
        
        Heat = Mathf.Lerp(Heat, TargetHeat, heatLerp);

        if(!Mathf.Approximately(Heat, TargetHeat))
        {
            ProgressBar.gameObject.SetActiveIfNeeded(true);
            float progress = Mathf.Clamp(TargetHeat / food.MaxHeat, 0f, 1f);
            ProgressBar.SetProgress(progress);
            if (lastHeat != Heat)
            {
                lastHeat = Heat;
                //Debug.LogWarning("Set progress: " + food.Type + " at " + progress + " when " + Heat + "/" + food.MaxHeat);
            }
        }
        else
        {
            ProgressBar.gameObject.SetActiveIfNeeded(false);
        }

        //TargetHeat -= DataManager.Instance.FoodHeatDecay;
        
        Color currentColor = DataManager.Instance.Burnt;

        if (Heat < food.GoodHeat)
        {
            float t = Heat / food.GoodHeat;
            currentColor = Color.Lerp(DataManager.Instance.Cold, DataManager.Instance.Good, t);
        }
        else if (Heat >= food.GoodHeat && Heat <= food.MaxHeat)
        {
            float max = food.MaxHeat - food.GoodHeat;
            float cur = food.MaxHeat - Heat;

            float t = cur / max;
            currentColor = Color.Lerp(DataManager.Instance.Good, DataManager.Instance.Hot, t);
        }
        /*
        else if(Heat > food.MaxHeat)
        {
            currentColor = DataManager.Instance.Burnt;
        }
        */
        

        visualRenderer.material.color = currentColor;

        heatLerp += 0.05f;
        if (heatLerp > 1f)
            heatLerp = 1f;
	}
    public void HitWithHeat(float heatPowah)
    {
        EffectSystem.Instance.PlayEffect("HitEffect", this.transform.position, 1f);
        ChangeHeat(heatPowah);


        /*
        if (visuals != null)
        {
            Debug.LogWarning("Hit on: " + visuals.name+" with powah: "+heatPowah);
        }
        */
    }
    public void DestroyVisuals()
    {
        if (visuals != null)
        {
            GameObject.Destroy(visuals);
            visuals = null;
        }
    }
    public void CreateVisuals()
    {
        if (food != null)
        {
            GameObject go = GameObject.Instantiate(food.Gobject);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one * food.Scaling;
            visuals = go;
            visualRenderer = go.GetComponentsInChildren<MeshRenderer>().FirstOrDefault();
        }
    }
    public void SetFood(FoodType ft)
    {
        food = DataManager.Instance.GetFood(ft);
        Debug.LogWarning("Got: " + food + " for " + ft);
        CreateVisuals();
    }
    public FoodData GetFood() {
        return food;
    }
}
