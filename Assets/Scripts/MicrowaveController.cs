using UnityEngine;
using System.Collections;

public class MicrowaveController : MonoBehaviour 
{

    public enum MicroStatus { Running, Win, Fail }
    public MicroStatus Status = MicroStatus.Running;

    public PlateController Plate;
    public HeaterController[] Heaters;
    public Transform Middle;

    public static MicrowaveController Instance;

    void Awake()
    {
        Instance = this;
    }
	void Start () 
    {
        
	}
	

    public MicroStatus SolveStatus()
    {
        bool allOk = true;
        foreach (SpotController spot in Plate.Spots)
        {
            if (spot.GetFood() == null) continue;

            if (spot.IsBurnt())
            {
                
                HudController.Instance.WinPanel.Win(false);
                return MicroStatus.Fail;
            }

            if (!spot.IsOk())
            {
                allOk = false;
            }

        }

        if (allOk)
        {
            HudController.Instance.WinPanel.Win(true);
            return MicroStatus.Win;
        }

        return MicroStatus.Running;
    }
	void Update () 
    {
        if (Status != MicroStatus.Running)
            return;
        
        Status = SolveStatus();
        
	}
}
