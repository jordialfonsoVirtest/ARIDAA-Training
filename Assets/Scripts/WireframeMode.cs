using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeMode : MonoBehaviour
{
    [SerializeField] private GameObject pointRender;
    [SerializeField] private MeshRenderer meshRenderer;

    private bool isActive = false;

    public void ButtonPress()
    {
        if (!isActive)
        {
            pointRender.SetActive(true);
            meshRenderer.enabled = false;
            isActive = true;
        }
        else
        {
            pointRender.SetActive(false);
            meshRenderer.enabled = true;
            isActive = false;
        }

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
