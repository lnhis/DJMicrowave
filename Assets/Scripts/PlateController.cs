using UnityEngine;
using System.Collections;

public class PlateController : MonoBehaviour 
{
    public SpotController[] Spots;

    private LevelData Level;
    private float baseAngle = 0.0f;
    private bool mouseDown = false;

    private float rotateSpeed = 0f;

	void Start () 
    {
        rotateSpeed = 1f;


	}

	void Update () 
    {
        /*
        Vector3 rot = transform.localRotation.eulerAngles;
        Vector3 newRot = new Vector3(0f, rot.y + rotateSpeed, 0f);
        transform.localRotation = Quaternion.Euler(newRot);
        */

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.LogWarning("Flag 1");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, DataManager.Instance.PlateHit))
            {
                //Debug.LogWarning("Hit something: " + hit.transform.name); 
                mouseDown = true;

                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                pos = Input.mousePosition - pos;
                baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                baseAngle -= Mathf.Atan2(GameManager.Instance.RotationHelper.right.y, GameManager.Instance.RotationHelper.right.x) *Mathf.Rad2Deg;
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (mouseDown)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos = Input.mousePosition - pos;
            float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
            GameManager.Instance.RotationHelper.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
            this.transform.localRotation = Quaternion.Euler(new Vector3(0f, -GameManager.Instance.RotationHelper.transform.rotation.eulerAngles.z, 0f));

        }
	}
    private float previousAng;
    private Vector3 previousPos;
    /*
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos = Input.mousePosition - pos;
            baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
            baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) *Mathf.Rad2Deg;
                
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            pos = Input.mousePosition - pos;
            float ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
            transform.rotation = Quaternion.AngleAxis(ang, Vector3.down);
    */
    void OnMouseDown()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) *Mathf.Rad2Deg;
    }

    void OnMouseDrag()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos = Input.mousePosition - pos;
        float ang = Mathf.Atan2(pos.y, pos.x) *Mathf.Rad2Deg - baseAngle;
        transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
    }

    public void LoadLevel(LevelData level)
    {
        for (int i = 0; i < level.Foods.Length; i++)
        {
            Spots[i].SetFood(level.Foods[i]);
        }
    }
}
