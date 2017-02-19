using UnityEngine;
using System.Collections;
using Enums;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataManager : MonoBehaviour 
{
    public LayerMask PlateHit;
    public LayerMask FoodHit;

    public Color Burnt;
    public Color Hot;
    public Color Good;
    public Color Cold;

    public float HeatSpeed = 1f;
    public float HeatDecay = 1f;
    public float FoodHeatDecay = 0.01f;
        
    public LevelData[] Levels;
    public FoodData[] Foods;

    public FoodData GetFood(FoodType ft)
    {
        return Foods.FirstOrDefault(xs => xs.Type == ft);
    }
    public LevelData GetLevel(int level)
    {
        return Levels.FirstOrDefault(xs => xs.Level == level);
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}
    public void Refresh()
    {
        foreach (FoodData fd in Foods)
            fd.Name = fd.Type.ToString();
        
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(DataManager))]
    public class UpdateTextures : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DataManager myScript = (DataManager)target;

            if (GUILayout.Button("Refresh"))
            {
                myScript.Refresh();
            }
        }
    }
    #endif
    private static DataManager instance = null;
    public static DataManager Instance
    {
        get 
        {
            if (instance != null)
                return instance;

            GameObject goTemplate = Resources.Load ("DataManager") as GameObject;
            GameObject go = GameObject.Instantiate (goTemplate);
            go.transform.name = "DataManager";
            instance = go.GetComponent<DataManager>();
            return instance;
        }
    }
}
