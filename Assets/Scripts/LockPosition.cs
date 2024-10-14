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
