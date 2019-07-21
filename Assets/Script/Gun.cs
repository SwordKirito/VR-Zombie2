using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;

    public Transform GunPoint;

    public LineRenderer Line;

    public GameObject RayPoint;

	// Use this for initialization
	void Start () {
        trackedObj = this.transform.parent.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(GunPoint.position, GunPoint.forward);
        Line.SetPosition(0, ray.origin);
       
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            RayPoint.SetActive(true);
            RayPoint.transform.position = hit.point;
            Line.SetPosition(1, hit.point);


            if(hit.transform.tag == "Cube")
            {
                SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
                if (device.GetTouchDown((SteamVR_Controller.ButtonMask.Trigger)))
                {
                     hit.transform.GetComponent<Rigidbody>().AddForce(ray.direction * 8000);
                }
            }


        }
        else
        {
            RayPoint.SetActive(false);
            Line.SetPosition(1, ray.direction * 100);
        }
	}
}
