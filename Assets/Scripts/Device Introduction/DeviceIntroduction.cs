using Meta.WitAi.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class DeviceIntroduction : MonoBehaviour
{

    [SerializeField] private GameObject slider;

    [SerializeField] private float[] sliderPositionRange = { -0.24f, 0.24f };

    [SerializeField] GameObject centreline;

    [SerializeField] GameObject device;

    private int totalSteps;


    // Start is called before the first frame update

    public float GetSliderDistance()
    {
        float sliderPosition = slider.transform.localPosition.x;

        float distance = Mathf.Abs(sliderPosition - sliderPositionRange[0]);

        float maxDistance = Mathf.Abs(sliderPositionRange[1] - sliderPositionRange[0]);

        return distance / maxDistance;

    }

    public void UpdateIntroduction()
    {

        int IntroductionDistance = Mathf.FloorToInt(totalSteps * GetSliderDistance());

        if (IntroductionDistance < totalSteps)
        {
            if(IntroductionDistance != 0)
            {
                device.transform.position = centreline.transform.GetChild(IntroductionDistance).position;
                device.transform.LookAt(centreline.transform.GetChild(IntroductionDistance - 1).position);
            }
            else{
                device.transform.position = centreline.transform.GetChild(IntroductionDistance).position;
                device.transform.LookAt(centreline.transform.GetChild(IntroductionDistance + 1).position);
            }


        }
        else
        {
            device.transform.position = centreline.transform.GetChild(IntroductionDistance-1).position;
            device.transform.LookAt(centreline.transform.GetChild(IntroductionDistance - 2).position);
        }


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!centreline)
        {
            if (CentrelineManager.Instance.GetActiveCentreline())
            {
                centreline = CentrelineManager.Instance.GetActiveCentreline();
                totalSteps = centreline.transform.childCount;
            }
        }
        else
        {
            UpdateIntroduction();
        }

    }
}
