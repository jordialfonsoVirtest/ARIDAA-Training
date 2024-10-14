using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopUp : MonoBehaviour
{
    [SerializeField] GameObject UIHand;
    [SerializeField] UIPopUp uIPopUp;

    private bool isTutorialShown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialCheck()
    {
        if (!isTutorialShown)
        {
            UIHand.SetActive(false);
            uIPopUp.enabled = false;
            isTutorialShown = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
