using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyToggle : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private Material opaqueMaterial;
    [SerializeField] private Material transparentMaterial;
    private bool isTransparent = false;

    public void SetOpaqueMaterial(Material mat)
    {
        opaqueMaterial = mat;
    }

    public void SetTransparentMaterial(Material mat)
    {
        transparentMaterial = mat;
    }

    public void SetTransparency()
    {

        if (isTransparent)
        {
            heart.transform.gameObject.GetComponent<MeshRenderer>().material = opaqueMaterial;
            isTransparent = false;
            SliceConfiguratorManager.Instance.SetIsTransparent(false);
        }
        else
        {
            heart.transform.gameObject.GetComponent<MeshRenderer>().material = transparentMaterial;
            isTransparent = true;
            SliceConfiguratorManager.Instance.SetIsTransparent(true);
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
