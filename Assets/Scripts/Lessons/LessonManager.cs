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

    public GameObject laa;
    public GameObject ostium;
    public GameObject landingZone;
    public GameObject device;

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

        if (currentLessonNumber == 1)
        {
            laa.SetActive(true);
        }
        else
        {
            laa.SetActive(false);
        }

        if (currentLessonNumber < 2)
        {
            ostium.SetActive(false);
            landingZone.SetActive(false);
            device.SetActive(false);
        }
        else if (currentLessonNumber == 2)
        {
            ostium.SetActive(true);
            landingZone.SetActive(false);
            device.SetActive(false);
        }
        else if (currentLessonNumber == 3)
        {
            ostium.SetActive(true);
            landingZone.SetActive(true);
            device.SetActive(false);
        }else if (currentLessonNumber >= 4)
        {
            ostium.SetActive(true);
            landingZone.SetActive(true);
            device.SetActive(true);
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
