using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressbarController : MonoBehaviour
{
    private Image healthBar;

    private float currentLerpTime;
    private float lerpTime = 1f;
    private bool finished = true;

    public Color[] coloringStages;

    private float targetHealthProgress = 1f;
    private float currentHealthProgress = 1f;

    public bool IsProgressRunning { get; private set; }

    void Awake()
    {
        healthBar = this.transform.FindChild("FullBar").GetComponent<Image>();
    }
    void Start ()
    {
        
	}

    public Color GetColor()
    {
        int imageIndex = (int)((coloringStages.Length - 1) * currentHealthProgress);
        
        return coloringStages[Mathf.Clamp(imageIndex, 0, coloringStages.Length-1)];
    }
	
	void Update ()
    {
        /*
        IsProgressRunning = currentHealthProgress != targetHealthProgress;
        Debug.LogWarning("WHYYY?: " + currentHealthProgress + " vs " + targetHealthProgress);
        if (IsProgressRunning)
        {
            currentHealthProgress += (targetHealthProgress - currentHealthProgress) * 0.02f;
            healthBar.fillAmount = currentHealthProgress;
            healthBar.color = GetColor();
            Debug.LogWarning("Target: " + targetHealthProgress + " we at " + currentHealthProgress);
        }
        */

        this.transform.LookAt(Camera.main.transform);
    }



    public void SetProgress(float targetHealthProgress)
    {
        this.targetHealthProgress = targetHealthProgress;
        healthBar.fillAmount = targetHealthProgress;
    }
}
