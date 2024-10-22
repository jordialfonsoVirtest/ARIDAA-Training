using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SliceConfiguratorManager : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject currentSlicer;
    [SerializeField] private GameObject transparencyToggle;
    private float[] positionBoundaries = { 0.0f, 0.0f };
    private float[] shaderBoundaries = { 0.0f, 0.0f };
    private float shaderBoundariesDistance;
    private string currentDirection;

    private bool isTransparent = false;

    public void SetIsTransparent(bool isTransparent) { this.isTransparent = isTransparent; }

    public string GetCurrentDirection()
    {
        return currentDirection;
    }

    private static SliceConfiguratorManager _instance;
    public static SliceConfiguratorManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    public GameObject GetCurrentSlicer()
    {
        return currentSlicer;
    }


    public void SetCurrentSlicer(GameObject slicer)
    {
        ResetSlicer();

        currentSlicer = slicer;
        currentDirection = currentSlicer.GetComponent<Slicer>().GetDirection();

        positionBoundaries[0] = currentSlicer.GetComponent<Slicer>().GetPositionBoundaries()[0];
        positionBoundaries[1] = currentSlicer.GetComponent<Slicer>().GetPositionBoundaries()[1];

        shaderBoundaries[0] = currentSlicer.GetComponent<Slicer>().GetShaderBoundaries()[0];
        shaderBoundaries[1] = currentSlicer.GetComponent<Slicer>().GetShaderBoundaries()[1];

        shaderBoundariesDistance = Mathf.Abs(shaderBoundaries[0] - shaderBoundaries[1]);

        currentSlicer.GetComponent<Slicer>().SetIsTransparent(isTransparent);
        heart.GetComponent<Renderer>().material = currentSlicer.GetComponent<Slicer>().GetSlicerMaterial();
        transparencyToggle.GetComponent<TransparencyToggle>().SetOpaqueMaterial(currentSlicer.GetComponent<Slicer>().GetOpaqueMaterial());
        transparencyToggle.GetComponent<TransparencyToggle>().SetTransparentMaterial(currentSlicer.GetComponent<Slicer>().GetTransparentMaterial());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateShader()
    {
        float distance = currentSlicer.GetComponent<Slicer>().GetDistance();
        float maxDistance = currentSlicer.GetComponent<Slicer>().GetMaxDistance();

        float distancePercentage = distance / (maxDistance / 100);

        float shaderDistance = (shaderBoundariesDistance/100) * distancePercentage;

        switch (currentDirection)
        {
            case "Downwards":
                heart.GetComponent<Renderer>().material.SetFloat("_Offset", currentSlicer.GetComponent<Slicer>().GetShaderBoundaries()[1] - shaderDistance);
                break;
            case "Upwards":
                heart.GetComponent<Renderer>().material.SetFloat("_Offset", currentSlicer.GetComponent<Slicer>().GetShaderBoundaries()[0] + shaderDistance);
                break;
        }

    }

    void ResetSlicer()
    {     
        if (currentSlicer)
        {
            currentSlicer.GetComponent<Slicer>().ResetPosition();
            switch (currentDirection)
            {
                case "Downwards":
                    heart.GetComponent<Renderer>().material.SetFloat("_Offset", currentSlicer.GetComponent<Slicer>().GetShaderBoundaries()[1]);
                    break;
                case "Upwards":
                    heart.GetComponent<Renderer>().material.SetFloat("_Offset", currentSlicer.GetComponent<Slicer>().GetShaderBoundaries()[0]);
                    break;
                default: break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSlicer)
        {
            UpdateShader();
        }
        
    }
}
