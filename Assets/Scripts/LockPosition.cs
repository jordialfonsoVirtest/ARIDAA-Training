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
    private bool isOn = false;

    public void SetButtonColor(Color color)
    {
        Oculus.Interaction.InteractableColorVisual.ColorState colorState = new Oculus.Interaction.InteractableColorVisual.ColorState() { Color = color };
        this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.InteractableColorVisual>().InjectOptionalNormalColorState(colorState);
        this.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.RoundedBoxProperties>().Color = color;
    }

    public void UpdateColor()
    {
        if (isOn)
        {
            SetButtonColor(new Color(0.78f, 0.78f, 0.78f, 0.2f));
        }
        else
        {
            SetButtonColor(new Color(0, 0.58f, 1.0f, 0.7f));
        }

        isOn = !isOn;
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
