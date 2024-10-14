using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DebugUIBuilder;

public class CentrelineButton : MonoBehaviour
{
    private GameObject centrelineRender;
    private bool isToggle = false;
    //[SerializeField] RuntimeAnimatorController animatorController;

    public int lessonNumber;
    public void SetButtonColor(Color color)
    {
        Oculus.Interaction.InteractableColorVisual.ColorState colorState = new Oculus.Interaction.InteractableColorVisual.ColorState() { Color = color };
        this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.InteractableColorVisual>().InjectOptionalNormalColorState(colorState);
        this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.RoundedBoxProperties>().Color = color;
    }


    public void SetIsToggle(bool toggle) {  isToggle = toggle; }

    public void SetCentrelineRender(GameObject render) { centrelineRender = render;}

    public void SetActiveCentrelineRender()
    {
        CentrelineManager.Instance.SetActiveCentreline(centrelineRender);
        transform.parent.gameObject.GetComponent<CentrelineToolbox>().RegenerateButtons();
    }

    public void ToggleActiveCentrelineRender()
    {
        centrelineRender.SetActive(!centrelineRender.activeSelf);
        if (centrelineRender.activeSelf)
        {
            SetButtonColor(new Color(0, 200, 255, 0.2f));
        }
        else
        {
            SetButtonColor(new Color(0, 100, 255, 0.2f));
            if (CentrelineManager.Instance.GetActiveCentreline() == centrelineRender)
            {
                CentrelineManager.Instance.SetActiveCentreline(null);
            }
        }
    }

    public void SetLesson()
    {
        LessonManager.Instance.currentLessonNumber = lessonNumber;
        LessonManager.Instance.SetCurrentLesson();
        transform.parent.gameObject.GetComponent<CentrelineToolbox>().RegenerateButtonsLessons();
    }

    public void ButtonPress()
    {
        if(isToggle)
        {
            ToggleActiveCentrelineRender();
        }
        else
        {
            SetActiveCentrelineRender();
        }
    }

    public void LessonButtonPress()
    {
        SetLesson();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
