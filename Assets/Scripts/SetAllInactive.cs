using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAllInactive : MonoBehaviour
{
    // Start is called before the first frame update

    public void SetAllGameObjectsInactive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
