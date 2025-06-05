using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerToggle : MonoBehaviour
{
    [SerializeField] MeshRenderer m_Renderer;
    [SerializeField] BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleSlicer()
    {

        m_Renderer.enabled = !m_Renderer.enabled;
        boxCollider.enabled = !boxCollider.enabled;

    }
}
