using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandContinue : MonoBehaviour
{

    public GameObject Case210;
    public GameObject UIPointGesture;
    public GameObject UIPinchGesture;
    public GameObject MainUI;
    public GameObject Exit;
    public GameObject LessonManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait(float seconds)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);
        Case210.GetComponent<UIOnEnableLocation>().enabled = false;
        LessonManager.GetComponent<LessonManager>().SetCurrentLesson();
        this.transform.gameObject.SetActive(false);
    }

    public void ButtonPress()
    {
        Case210.SetActive(true);
        UIPointGesture.SetActive(false);
        UIPinchGesture.SetActive(true);
        MainUI.SetActive(false);
        Exit.SetActive(false);
        StartCoroutine(Wait(0.1f));

    }
}
