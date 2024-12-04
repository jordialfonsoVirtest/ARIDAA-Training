using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LockPosition : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject rotatedObject;

    private bool isLocked = false;

    public void SetButtonColor(Color color)
    {
        Oculus.Interaction.InteractableColorVisual.ColorState colorState = new Oculus.Interaction.InteractableColorVisual.ColorState() { Color = color };
        this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.InteractableColorVisual>().InjectOptionalNormalColorState(colorState);
        this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.RoundedBoxProperties>().Color = color;
    }

    public void UpdateColor()
    {
        if (isLocked)
        {
            SetButtonColor(new Color(0, 100, 255));
        }
        else
        {
            SetButtonColor(new Color(0, 200, 255, 0.2f));
        }
    }
    public void LockObjectPosition()
    {
        if (!isLocked)
        {
            transform.gameObject.GetComponent<HandGrabInteractable>().enabled = false;
            isLocked = true;
        }
        else
        {
            transform.gameObject.GetComponent<HandGrabInteractable>().enabled = true;
            isLocked = false;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        rotatedObject.transform.LookAt(target.transform);
    }
}
