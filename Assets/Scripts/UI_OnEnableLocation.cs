using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOnEnableLocation : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private float distance = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        float xpos = 0;
        float ypos = 0;
        float zpos = 0;

        if (target.transform.forward.x > 0)
        {
            xpos = (float)(target.transform.position.x + distance);
        }else if (target.transform.forward.x < 0)
        {
            xpos = (float)(target.transform.position.x - distance);
        }
        else
        {
            xpos = (float)target.transform.position.x;
        }

        if (target.transform.forward.y > 0)
        {
            ypos = (float)(target.transform.position.y + distance);
        }
        else if (target.transform.forward.y < 0)
        {
            ypos = (float)(target.transform.position.y - distance);
        }
        else
        {
            ypos = (float)target.transform.position.y;
        }

        if (target.transform.forward.z > 0)
        {
            zpos = (float)(target.transform.position.z + distance);
        }
        else if (target.transform.forward.z < 0)
        {
            zpos = (float)(target.transform.position.z - distance);
        }
        else
        {
            zpos = (float)target.transform.position.z;
            
        }
        //this.transform.localRotation = new Quaternion(0, 0, 0, 0);
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.position = new Vector3(xpos, ypos, zpos);
        //this.transform.localPosition = new Vector3(xpos, ypos, zpos);
        Debug.Log("xpos: " + xpos + " ypos: " + ypos + " zpos: " + zpos);
        
        transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
    }
}
