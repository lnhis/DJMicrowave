using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    public WinPanelController WinPanel;

    public static HudController Instance = null;

    void Awake()
    {
        WinPanel.gameObject.SetActive(false);
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
