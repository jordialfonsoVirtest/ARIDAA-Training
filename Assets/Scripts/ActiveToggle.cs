using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    [SerializeField] private GameObject tutorialObject;
    private bool isTutorialShown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnabledToggle()
    {
        if(tutorialObject != null)
        {
            if(isTutorialShown)
            {
                this.transform.gameObject.SetActive(!this.transform.gameObject.activeSelf);
            }
            else
            {
                isTutorialShown=true;
            }
            
        }
        else
        {
            this.transform.gameObject.SetActive(!this.transform.gameObject.activeSelf);
        }
        
    }
}
