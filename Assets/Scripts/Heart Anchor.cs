using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject heartPivot;
    [SerializeField] private GameObject heart;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = heartPivot.transform.position;
        this.transform.localScale = heart.transform.localScale;

    }
}
