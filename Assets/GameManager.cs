using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public int Level = 0;

    private PlateController Plate;

    public Transform RotationHelper;

    public static GameManager Instance { get; private set; }

    private GameObject CurrentLevel = null;

    private void DestroyCurrentLevel()
    {
        if(CurrentLevel!=null)
        {
            GameObject.Destroy(CurrentLevel);
            CurrentLevel = null;
        }
    }
    void Awake() 
    {
        Instance = this;
    }

	void Start () 
    {
        TryAgain();
    }
    public void TryAgain()
    {
        LoadLevel();
    }
	public void NextLevel()
    {
        Level++;
        LoadLevel();
    }
    private void LoadLevel()
    {
        DestroyCurrentLevel();
        LevelData level = DataManager.Instance.GetLevel(Level);

        CurrentLevel = GameObject.Instantiate(level.Microwave);
        MicrowaveController micro = CurrentLevel.GetComponent<MicrowaveController>();
        micro.Plate.LoadLevel(level);
    }
	void Update () 
    {
        // Debug.LogWarning("Flag 1: " + Input.mousePosition);
	}
}
