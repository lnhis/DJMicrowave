using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HeatWaveController : MonoBehaviour 
{
    public HeatData Heat { get; set; }
    public Transform Origin { get; set; }

    private MeshRenderer rend;

    private Vector3 Direction;

    private float currentHeat = 0f;

    private HashSet<SpotController> HitSpots = new HashSet<SpotController>();

	void Start () 
    {
        rend = this.transform.FindChild("Sphere").GetComponent<MeshRenderer>();

        Direction = (MicrowaveController.Instance.Middle.position-this.transform.position).normalized;

        //transform.LookAt(MicrowaveController.Instance.Middle.transform);

        currentHeat = Heat.Powah;
	}
	

	void Update () 
    {
        float p = currentHeat / Heat.Powah;
        rend.material.color = new Color(1f, 1f, 1f, p);

        float distanceFromOrigin = Vector3.Distance(Origin.position, this.transform.position);
        float percentage = distanceFromOrigin / Heat.Distance;

        this.transform.position += Direction*DataManager.Instance.HeatSpeed;
        //currentHeat -= DataManager.Instance.HeatDecay;
        currentHeat = Heat.Powah * (1-percentage);
        if (distanceFromOrigin >= Heat.Distance)
        {
            GameObject.Destroy(this.gameObject);
        }

        Vector3 fwd = transform.TransformDirection(Vector3.right);
        Debug.DrawRay(transform.position, fwd * 1f, Color.green);
        RaycastHit objectHit;
        Ray ray = new Ray();
        ray.origin = this.transform.position;
        ray.direction = fwd;
        //Physics.Raycast(transform.position, out objectHit, 1f, DataManager.Instance.FoodHit)
        //if (Physics.Raycast(transform.position, out objectHit, fwd, 1f, DataManager.Instance.FoodHit))
        if(Physics.Raycast(ray, out objectHit, 1f, DataManager.Instance.FoodHit))
        {
            // Debug.LogWarning("Flag 0: " + objectHit);
            SpotController spot = objectHit.transform.parent.transform.GetComponent<SpotController>();
            if (!HitSpots.Contains(spot))
            {
                spot.HitWithHeat(currentHeat);
                HitSpots.Add(spot);

                SoundSystem.Instance.PlaySound(Heat.Sound);
            }
        }
    }

}
