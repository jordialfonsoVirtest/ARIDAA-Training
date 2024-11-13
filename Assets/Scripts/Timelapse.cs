using Meta.WitAi.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class Timelapse : MonoBehaviour
{

    [SerializeField] private GameObject slider;

    [SerializeField] private float[] sliderPositionRange = { -0.24f, 0.24f };

    [SerializeField] GameObject timelapse3DModels;

    [SerializeField] GameObject text100;

    [SerializeField] GameObject textSlider;

    [SerializeField] GameObject textPauseButton;

    private int timelapseSteps;

    private int totalSteps;

    private bool isPaused = true;


    // Start is called before the first frame update

    public float GetSliderDistance()
    {
        float sliderPosition = slider.transform.localPosition.x;

        float distance = Mathf.Abs(sliderPosition - sliderPositionRange[0]);

        float maxDistance = Mathf.Abs(sliderPositionRange[1] - sliderPositionRange[0]);

        return distance / maxDistance;
        
    }

    public void UpdateTimelapse()
    {
        for(int i = 0; i < totalSteps; i++)
        {
            timelapse3DModels.transform.GetChild(i).gameObject.SetActive(false);
        }

        int timelapseDistance = Mathf.FloorToInt(timelapseSteps * GetSliderDistance());

        if(timelapseDistance < timelapseSteps)
        {
            timelapse3DModels.transform.GetChild(timelapseDistance).gameObject.SetActive(true);
            timelapse3DModels.transform.GetChild(timelapseDistance + (timelapseSteps * 1)).gameObject.SetActive(true);
            timelapse3DModels.transform.GetChild(timelapseDistance + (timelapseSteps * 2)).gameObject.SetActive(true);
            timelapse3DModels.transform.GetChild(timelapseDistance + (timelapseSteps * 3)).gameObject.SetActive(true);
            textSlider.GetComponent<TMP_Text>().text = (timelapseDistance).ToString();
        }
        else
        {
            timelapse3DModels.transform.GetChild(timelapseSteps-1).gameObject.SetActive(true);
            timelapse3DModels.transform.GetChild(timelapseSteps-1 + (timelapseSteps * 1)).gameObject.SetActive(true);
            timelapse3DModels.transform.GetChild(timelapseSteps-1 + (timelapseSteps * 2)).gameObject.SetActive(true);
            timelapse3DModels.transform.GetChild(timelapseSteps-1 + (timelapseSteps * 3)).gameObject.SetActive(true);
            textSlider.GetComponent<TMP_Text>().text = (timelapseSteps-1).ToString();
        }
        

    }

    public void PauseButton()
    {
        if (isPaused)
        {
            isPaused = false;
            textPauseButton.GetComponent<TMP_Text>().text = "Pause";
        }
        else
        {
            isPaused = true;
            textPauseButton.GetComponent<TMP_Text>().text = "Play";
        }
    }

    public void ReplayButton()
    {
        slider.transform.localPosition = new Vector3(sliderPositionRange[0], slider.transform.localPosition.y, slider.transform.localPosition.z);
    }

    void Start()
    {
        totalSteps = timelapse3DModels.transform.childCount;
        timelapseSteps = totalSteps / 4;
        text100.GetComponent<TMP_Text>().text = (timelapseSteps-1).ToString();
        PauseButton();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateTimelapse();
        if(!isPaused && slider.transform.localPosition.x <= sliderPositionRange[1])
        {
            slider.transform.localPosition = new Vector3(slider.transform.localPosition.x+0.0005f, slider.transform.localPosition.y, slider.transform.localPosition.z);
        }

    }
}
