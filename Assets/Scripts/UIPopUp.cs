using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool _menuPrev;
    private void Update()
    {
        var state = OVRPlugin.GetControllerState4((uint)OVRInput.Controller.Hands);
        bool menuGesture = (state.Buttons & (uint)OVRInput.RawButton.Start) > 0;
        if (menuGesture && !_menuPrev)
        {
            this.transform.GetChild(0).gameObject.SetActive(!this.transform.GetChild(0).gameObject.activeSelf);
        }
        _menuPrev = menuGesture;
    }
}
