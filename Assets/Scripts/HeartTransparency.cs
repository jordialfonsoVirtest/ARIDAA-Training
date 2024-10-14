using Meta.WitAi.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartTransparency : MonoBehaviour
{

    [SerializeField] Material X_Downwards_Transparent;
    [SerializeField] Material X_Upwards_Transparent;
    [SerializeField] Material Y_Downwards_Transparent;
    [SerializeField] Material Y_Upwards_Transparent; 
    [SerializeField] Material Z_Downwards_Transparent;
    [SerializeField] Material Z_Upwards_Transparent;
    [SerializeField] Material X_Downwards;
    [SerializeField] Material X_Upwards;
    [SerializeField] Material Y_Downwards;
    [SerializeField] Material Y_Upwards;
    [SerializeField] Material Z_Downwards;
    [SerializeField] Material Z_Upwards;

    [SerializeField] private GameObject slider;

    [SerializeField] private float[] sliderPositionRange = { -79.5f, 79.5f };
    [SerializeField] private float[] shaderTransparentRange = { 0f, 1f };

    [SerializeField] GameObject heart;
    // Start is called before the first frame update

    public void SetTransparentTexture(float value)
    {
        X_Downwards_Transparent.SetFloat("_Transparency", value);
        X_Upwards_Transparent.SetFloat("_Transparency", value);
        Y_Downwards_Transparent.SetFloat("_Transparency", value);
        Y_Upwards_Transparent.SetFloat("_Transparency", value);
        Z_Downwards_Transparent.SetFloat("_Transparency", value);
        Z_Upwards_Transparent.SetFloat("_Transparency", value);

    }

    public float GetTransparencyLevel()
    {
        float sliderPosition = slider.transform.localPosition.x;

        float distance = Mathf.Abs(sliderPosition - sliderPositionRange[0]);

        float maxDistance = Mathf.Abs(sliderPositionRange[1] - sliderPositionRange[0]);

        float distancePercentage = distance / (maxDistance / 100);

        float shaderDistance = Mathf.Abs(shaderTransparentRange[1] - shaderTransparentRange[0]);
        
        return (shaderDistance / 100) * distancePercentage;
    }

    public void ButtonPress(float value)
    {
        GameObject currentSlicer;
        string currentDirection = "";
        string currentAxis = "";
        if (SliceConfiguratorManager.Instance.GetCurrentSlicer())
        {
            currentSlicer = SliceConfiguratorManager.Instance.GetCurrentSlicer();
            currentDirection = SliceConfiguratorManager.Instance.GetCurrentDirection();
            currentAxis = currentSlicer.name.Split("_")[0];
        }
        else
        {
            currentSlicer = null;
        }

        

        if (value >= (shaderTransparentRange[1]-0.02f))
        {
            SliceConfiguratorManager.Instance.SetIsTransparent(false);
            if (currentSlicer)
            {
                switch (currentAxis)
                {
                    case "X":
                        switch (currentDirection)
                        {
                            case "Downwards":
                                heart.GetComponent<Renderer>().material = X_Downwards;
                                break;
                            case "Upwards":
                                heart.GetComponent<Renderer>().material = X_Upwards;
                                break;
                            default: break;
                        }
                        break;
                    case "Y":
                        switch (currentDirection)
                        {
                            case "Downwards":
                                heart.GetComponent<Renderer>().material = Y_Downwards;
                                break;
                            case "Upwards":
                                heart.GetComponent<Renderer>().material = Y_Upwards;
                                break;
                            default: break;
                        }
                        break;
                    case "Z":
                        switch (currentDirection)
                        {
                            case "Downwards":
                                heart.GetComponent<Renderer>().material = Z_Downwards;
                                break;
                            case "Upwards":
                                heart.GetComponent<Renderer>().material = Z_Upwards;
                                break;
                            default: break;
                        }
                        break;
                    default:break;
                }
            }
            else
            {
                heart.GetComponent<Renderer>().material = X_Downwards;
            }
            
        }
        else
        {
            SliceConfiguratorManager.Instance.SetIsTransparent(true);
            SetTransparentTexture(value);

            if (currentSlicer)
            {
                switch (currentAxis)
                {
                    case "X":
                        switch (currentDirection)
                        {
                            case "Downwards":
                                heart.GetComponent<Renderer>().material = X_Downwards_Transparent;
                                break;
                            case "Upwards":
                                heart.GetComponent<Renderer>().material = X_Upwards_Transparent;
                                break;
                            default: break;
                        }
                        break;
                    case "Y":
                        switch (currentDirection)
                        {
                            case "Downwards":
                                heart.GetComponent<Renderer>().material = Y_Downwards_Transparent;
                                break;
                            case "Upwards":
                                heart.GetComponent<Renderer>().material = Y_Upwards_Transparent;
                                break;
                            default: break;
                        }
                        break;
                    case "Z":
                        switch (currentDirection)
                        {
                            case "Downwards":
                                heart.GetComponent<Renderer>().material = Z_Downwards_Transparent;
                                break;
                            case "Upwards":
                                heart.GetComponent<Renderer>().material = Z_Upwards_Transparent;
                                break;
                            default: break;
                        }
                        break;
                    default: break;
                }
            }
            else
            {
                heart.GetComponent<Renderer>().material = X_Downwards_Transparent;
            }
        }
        if (currentSlicer)
        {
            SliceConfiguratorManager.Instance.UpdateShader();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonPress(GetTransparencyLevel());
    }
}
