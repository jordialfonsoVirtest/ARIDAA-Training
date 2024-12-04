using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceSelectorManager : MonoBehaviour
{
    public GameObject leftButtonTransform;
    public GameObject centerButtonTransform;
    public GameObject rightButtonTransform;

    public GameObject currentLeftButton;
    public GameObject currentCenterButton;
    public GameObject currentRightButton;

    public GameObject contourWarning;
    public GameObject compressionCases;

    public GameObject buttonList;
    public int buttonListSize = 0;

    public int currentRecommendedSize = 0;

    public bool isCompressionLesson = true;

    public static DeviceSelectorManager _instance;

    public static DeviceSelectorManager Instance
    {
        get { return _instance; }
    }

    public void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        buttonListSize = buttonList.transform.childCount;
    }

    public void UpdateRecommendedSize()
    {
        if (currentRecommendedSize != LAAMeasurementsManager.Instance.recommendedSizeValueAmplatzerAMULET)
        {
            currentRecommendedSize = (int)LAAMeasurementsManager.Instance.recommendedSizeValueAmplatzerAMULET;
        }
    }

    public void UpdateButtons()
    {
        UpdateRecommendedSize();
        if (currentRecommendedSize != 0)
        {

            contourWarning.SetActive(false);

            foreach (Transform button in buttonList.transform)
            {

                if (button.name == currentRecommendedSize.ToString())
                {

                    if (currentCenterButton)
                    {

                        if (currentCenterButton != button.gameObject)
                        {
                            currentCenterButton.SetActive(false);
                            currentCenterButton = button.gameObject;
                        }

                    }
                    else
                    {
                        currentCenterButton = button.gameObject;
                    }

                }

            }

            if (isCompressionLesson)
            {

                if (buttonList.transform.GetChild(buttonListSize - 1).name == currentRecommendedSize.ToString())
                {
                    if (currentRightButton)
                    {
                        currentRightButton.SetActive(false);
                        currentLeftButton.SetActive(false);
                        currentCenterButton.SetActive(false);
                        currentRightButton = null;
                        currentLeftButton = buttonList.transform.GetChild(buttonListSize - 2).gameObject;

                    }
                    else if (!currentLeftButton && !currentCenterButton)
                    {

                        currentLeftButton.SetActive(false);
                        currentCenterButton.SetActive(false);
                        currentLeftButton = buttonList.transform.GetChild(buttonListSize - 2).gameObject;

                    }
                }
                else if (buttonList.transform.GetChild(0).name == currentRecommendedSize.ToString())
                {

                }
                else
                {
                    for (int i = 1; i < buttonListSize - 1; i++)
                    {
                        Transform button = buttonList.transform.GetChild(i);
                        Transform nextButton = buttonList.transform.GetChild(i + 1);
                        Transform prevButton = buttonList.transform.GetChild(i - 1);
                        if (button.name == currentRecommendedSize.ToString())
                        {

                            if (currentRightButton)
                            {

                                if (currentRightButton != nextButton.gameObject)
                                {
                                    currentRightButton.SetActive(false);
                                    currentRightButton = nextButton.gameObject;
                                }

                            }
                            else
                            {
                                currentRightButton = nextButton.gameObject;
                            }
                            if (currentLeftButton)
                            {

                                if (currentLeftButton != prevButton.gameObject)
                                {
                                    currentLeftButton.SetActive(false);
                                    currentLeftButton = prevButton.gameObject;
                                }

                            }
                            else
                            {
                                currentLeftButton = prevButton.gameObject;
                            }

                        }
                    }

                }

            }
            else
            {
                if (currentLeftButton)
                {

                    currentLeftButton.SetActive(false);
                    currentLeftButton = null;

                }

                if (currentRightButton)
                {

                    currentRightButton.SetActive(false);
                    currentRightButton = null;

                }

            }

            if (currentCenterButton)
            {

                currentCenterButton.transform.localPosition = centerButtonTransform.transform.localPosition;
                currentCenterButton.SetActive(true);

            }

            if (currentLeftButton)
            {

                currentLeftButton.transform.localPosition = leftButtonTransform.transform.localPosition;
                currentLeftButton.SetActive(true);

            }

            if (currentRightButton)
            {

                currentRightButton.transform.localPosition = rightButtonTransform.transform.localPosition;
                currentRightButton.SetActive(true);

            }
        }
        else
        {
            if (currentCenterButton)
            {
                currentLeftButton.SetActive(false);
                currentLeftButton = null;
            }

            if (currentRightButton)
            {
                currentRightButton.SetActive(false);
                currentRightButton = null;
            }

            if (currentLeftButton)
            {
                currentCenterButton.SetActive(false);
                currentRightButton = null;
            }

            contourWarning.SetActive(true);
        } 

    }

    public void CompressionCheck()
    {

        if (isCompressionLesson) 
        {

            InactiveAllCompressionCases();
            int currentDeviceSize = int.Parse(LAAColliderManager.Instance.currentDeviceLobe.name);
            if (currentDeviceSize == currentRecommendedSize)
            {

                compressionCases.transform.GetChild(0).gameObject.SetActive(true);

            }else if (currentDeviceSize < currentRecommendedSize)
            {

                compressionCases.transform.GetChild(1).gameObject.SetActive(true);

            }else if(currentDeviceSize > currentRecommendedSize)
            {

                compressionCases.transform.GetChild(2).gameObject.SetActive(true);

            }

            LAAColliderManager.Instance.currentDeviceLobe.SetActive(false);
            LAAColliderManager.Instance.currentDeviceDisk.SetActive(false);


        }
    }

    public void InactiveAllCompressionCases()
    {
        compressionCases.GetComponent<SetAllInactive>().SetAllGameObjectsInactive();
    }

    // Update is called once per frame
    void Update()
    {

        UpdateButtons();

    }

    /*private void OnEnable()
    {

        
    }*/
}
