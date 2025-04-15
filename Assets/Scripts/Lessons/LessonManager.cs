using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonManager : MonoBehaviour
{

    private static LessonManager _instance;

    public int currentLessonNumber;

    public GameObject currentLesson;
    public GameObject lessonList;

    public GameObject backButton;
    public GameObject continueButton;

    public GameObject lessonContentList;
    public GameObject previousLessonContent;

    public GameObject circumflex;
    public GameObject fossa;
    public GameObject laaMeasurements;
    public GameObject laaMeasurementsGrabbable;
    public GameObject centrelineManager;

    public GameObject watchmanFLX;
    public GameObject amplatzerAMULET;

    public GameObject deviceSelector;
    public GameObject compressionCases;
    public GameObject compressionCasesUI;

    public GameObject contourWarning;

    public bool ContourWarningCheck()
    {
        if(LAAMeasurementsManager.Instance.recommendedSizeValueAmplatzerAMULET != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool DeviceCheck()
    {
        if (LAAColliderManager.Instance.currentDeviceDisk && LAAColliderManager.Instance.currentDeviceLobe)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TimelapseCheck()
    {
        if (LAAColliderManager.Instance.previousTimelapseCase)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetDeviceVisibility(bool state)
    {
        LAAColliderManager.Instance.currentDeviceDisk.SetActive(state);
        LAAColliderManager.Instance.currentDeviceLobe.SetActive(state);
    }

    public void UpdateDeviceState(bool state)
    {
        if (DeviceCheck())
        {
            SetDeviceVisibility(state);
        }
    }

    public void UpdateTimelapseState(bool state)
    {
        if (TimelapseCheck())
        {
            LAAColliderManager.Instance.previousTimelapseCase.SetActive(state);
        }
    }
    public void NextLesson()
    {
        currentLessonNumber++;
    }

    public void PreviousLesson()
    {
        currentLessonNumber--;
    }
        
    public void SetCurrentLesson()
    {
        currentLesson.SetActive(false);
        int lessonListCount = lessonList.transform.childCount;
        currentLesson = lessonList.transform.GetChild(currentLessonNumber).gameObject;
        currentLesson.SetActive(true);
        
        if (!previousLessonContent)
        {
            previousLessonContent = lessonContentList.transform.GetChild(currentLessonNumber).gameObject;
            lessonContentList.transform.GetChild(currentLessonNumber).gameObject.SetActive(true);
        }
        else
        {
            previousLessonContent.SetActive(false);
            previousLessonContent = lessonContentList.transform.GetChild(currentLessonNumber).gameObject;
            lessonContentList.transform.GetChild(currentLessonNumber).gameObject.SetActive(true);
        }

        if (currentLessonNumber == 0 )
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
        if (currentLessonNumber == lessonListCount-1)
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }

        switch(currentLessonNumber)
        {
            case 0:
                circumflex.SetActive(true);
                fossa.SetActive(true);
                laaMeasurements.SetActive(false);                
                centrelineManager.SetActive(false);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = true;
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().isSet = false;
                break;

            case 1:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(false);
                amplatzerAMULET.SetActive(false);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();                
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;

            case 2:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(false);
                amplatzerAMULET.SetActive(false);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();                
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;

            case 3:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();                
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;

            case 4:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();               
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;

            case 5:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;

            case 6:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                deviceSelector.SetActive(true);
                contourWarning.SetActive(ContourWarningCheck());
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                DeviceSelectorManager.Instance.isCompressionLesson = true; 
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;
            case 7:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                contourWarning.SetActive(ContourWarningCheck());
                UpdateDeviceState(true);
                UpdateTimelapseState(true);
                DeviceSelectorManager.Instance.isCompressionLesson = false;
                deviceSelector.SetActive(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;
            case 8:
                laaMeasurements.SetActive(false);
                centrelineManager.SetActive(false);
                watchmanFLX.SetActive(false);
                amplatzerAMULET.SetActive(false);
                deviceSelector.SetActive(false);
                contourWarning.SetActive(false);
                UpdateDeviceState(false);
                UpdateTimelapseState(false);
                compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                compressionCasesUI.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().SetLocation();
                laaMeasurementsGrabbable.GetComponent<UIOnEnableLocation>().enabled = false;
                break;

            default:
                break;
        }

    }


    public static LessonManager Instance
    {
        get { return _instance; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        _instance = this;
    }
}
