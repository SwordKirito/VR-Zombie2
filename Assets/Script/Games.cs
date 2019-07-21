using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Games : MonoBehaviour {

    SteamVR_TrackedObject trackedObj;

    //FixedJoint joint;

    bool Gun = false;

    public GameObject GunMesh;

	// Use this for initialization
	void Start () {

        trackedObj = this.transform.parent.GetComponent<SteamVR_TrackedObject>();

	}
	
	// Update is called once per frame
	void Update () {

        if (!Gun)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Projectile.Instance.ProjectileCube();
            }
           
        }
       
	}


    void OnTriggerStay(Collider collider)
    {
        if (collider.name != "Gun04")
        {
            return;
        }

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //joint = this.gameObject.AddComponent<FixedJoint>();
            //joint.connectedBody = collider.GetComponent<Rigidbody>();

            GunMesh.SetActive(true);
            this.gameObject.SetActive(false);
            collider.gameObject.SetActive(false); 

        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name != "Gun04")
        {
            return;
        }
        Gun = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.name != "Gun04")
        {
            return;
        }
        Gun = false;
    }
}
