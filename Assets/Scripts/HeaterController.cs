using UnityEngine;
using System.Collections;

public class HeaterController : MonoBehaviour 
{
    public GameObject heatTemplate;

    public HeatData[] HeatSequence;

    public float StartDelay = 1f;

    private float startTime = 0f;

    private int Current = 0;
    private bool started = false;

	void Start () 
    {
        startTime = Time.time;
        StartCoroutine(ExecuteAfterTime(StartDelay));
	}
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        TryCreateHeatWave(HeatSequence[Current]);
    }

	
	void Update () 
    {
        /*
        if (Time.time - startTime > StartDelay)
        {
            foreach (HeatData hd in HeatSequence)
                TryCreateHeatWave(hd);
        }
        */
	}
    private void TryCreateHeatWave(HeatData hd)
    {
        //if (Time.time - hd.LastShootTime > hd.Delay)
        {
            hd.LastShootTime = Time.time;
            GameObject go = GameObject.Instantiate(heatTemplate);
            go.transform.SetParent(this.transform);
            go.transform.position = this.transform.position;
            HeatWaveController wave = go.GetComponent<HeatWaveController>();
            wave.Heat = hd;
            wave.Origin = this.transform;

            if (MicrowaveController.Instance.Status == MicrowaveController.MicroStatus.Running)
            {
                StartCoroutine(ExecuteAfterTime(hd.Delay));
                MoveCursor();
            }

        }
    }
    public void MoveCursor()
    {
        Current++;
        if (Current >= HeatSequence.Length)
            Current = 0;
    }
}
