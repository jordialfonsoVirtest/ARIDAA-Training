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
    public GameObject centrelineManager;

    public GameObject watchmanFLX;
    public GameObject amplatzerAMULET;
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
                break;

            case 1:
                laaMeasurements.SetActive(true);
                centrelineManager.SetActive(true);
                watchmanFLX.SetActive(false);
                amplatzerAMULET.SetActive(false);
                break;

            case 2:
                laaMeasurements.SetActive(true);
                watchmanFLX.SetActive(false);
                amplatzerAMULET.SetActive(false);
                break;

            case 3:
                laaMeasurements.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                break;

            case 4:
                laaMeasurements.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                break;

            case 5:
                laaMeasurements.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
                break;

            case 6:
                laaMeasurements.SetActive(true);
                watchmanFLX.SetActive(true);
                amplatzerAMULET.SetActive(true);
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
