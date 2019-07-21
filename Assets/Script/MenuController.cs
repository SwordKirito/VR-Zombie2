using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public Renderer BtnRender;
    public Material BtnMat;
    public Material BtnMat2;

    void OnTriggerStay(Collider collider)
    {
        StartButton StartBtn = collider.GetComponent<StartButton>();
        if (StartBtn != null)
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
            if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                StartBtn.BtnDown();
            }
        }
     }
    void OnTriggerEnter(Collider collider)
    {
        BtnRender.material = BtnMat2;
    }

    void OnTriggerExit(Collider collider)
    {
        BtnRender.material = BtnMat;
    } 
}
