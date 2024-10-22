using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Oculus.Interaction.OneGrabTranslateTransformer;
using static UnityEngine.GraphicsBuffer;

public class Slicer : MonoBehaviour
{
    [SerializeField] private float shaderMin;
    [SerializeField] private float shaderMax;
    [SerializeField] private Material slicerMaterialOpaque;
    [SerializeField] private Material slicerMaterialTransparent;
    private Vector3 initialPosition;
    private float[] positionBoundaries = { 0.0f, 0.0f };
    private float[] shaderBoundaries = { 0.0f, 0.0f };
    private string axis;
    private string direction;

    private bool isTransparent = false;

    public Material GetOpaqueMaterial() { return slicerMaterialOpaque; }
    public Material GetTransparentMaterial() {  return slicerMaterialTransparent; }

    public void SetIsTransparent(bool isTransparent)
    {
        this.isTransparent = isTransparent;
    }


    public Material GetSlicerMaterial() {

        if (isTransparent)
        {
            return slicerMaterialTransparent;
        }
        else
        {
            return slicerMaterialOpaque;
        }
    }

    public float[] GetShaderBoundaries() { return shaderBoundaries; }

    public string GetDirection() { return direction; }

    public float[] GetPositionBoundaries() { return positionBoundaries; }

    public void SetPositionBoundaries()
    {
        switch (axis)
        {
            case "X":
                positionBoundaries[0] = this.gameObject.GetComponent<Oculus.Interaction.OneGrabTranslateTransformer>().Constraints.MinX.Value;
                positionBoundaries[1] = this.gameObject.GetComponent<Oculus.Interaction.OneGrabTranslateTransformer>().Constraints.MaxX.Value;
                break;
            case "Y":
                positionBoundaries[0] = this.gameObject.GetComponent<Oculus.Interaction.OneGrabTranslateTransformer>().Constraints.MinY.Value;
                positionBoundaries[1] = this.gameObject.GetComponent<Oculus.Interaction.OneGrabTranslateTransformer>().Constraints.MaxY.Value;
                break;
            case "Z":
                positionBoundaries[0] = this.gameObject.GetComponent<Oculus.Interaction.OneGrabTranslateTransformer>().Constraints.MinZ.Value;
                positionBoundaries[1] = this.gameObject.GetComponent<Oculus.Interaction.OneGrabTranslateTransformer>().Constraints.MaxZ.Value;
                break;
            default: break;
        }
    }

    public void ResetPosition() {

        this.gameObject.transform.localPosition = initialPosition;

    }

    
    void SwapCurrentSlicer()
    {
        if(this.transform.localPosition != initialPosition)
        {
            if (!SliceConfiguratorManager.Instance.GetCurrentSlicer())
            {
                SliceConfiguratorManager.Instance.SetCurrentSlicer(this.gameObject);
            }
            if (SliceConfiguratorManager.Instance.GetCurrentSlicer() != this.gameObject)
            {
                SliceConfiguratorManager.Instance.SetCurrentSlicer(this.gameObject);
            }
        } 
    }

    public float GetDistance()
    {
        switch (axis)
        {
            case "X":
                return Mathf.Abs(initialPosition.x - this.transform.localPosition.x);
            case "Y":
                return Mathf.Abs(initialPosition.y - this.transform.localPosition.y);
            case "Z":
                return Mathf.Abs(initialPosition.z - this.transform.localPosition.z);
            default:
                return -1;
        }
        
    }

    public float GetMaxDistance()
    {
        switch (axis)
        {
            case "X":
                switch (direction)
                {
                    case "Downwards":
                        return Mathf.Abs(initialPosition.x - positionBoundaries[1]);
                    case "Upwards":
                        return Mathf.Abs(initialPosition.x - positionBoundaries[0]);
                    default: return -1;
                }
                
            case "Y":
                switch (direction)
                {
                    case "Downwards":
                        return Mathf.Abs(initialPosition.y - positionBoundaries[1]);
                    case "Upwards":
                        return Mathf.Abs(initialPosition.y - positionBoundaries[0]);
                    default: return -1;
                }
                
            case "Z":
                switch (direction)
                {
                    case "Downwards":
                        return Mathf.Abs(initialPosition.z - positionBoundaries[1]);
                    case "Upwards":
                        return Mathf.Abs(initialPosition.z - positionBoundaries[0]);
                    default: return -1;
                }
                
            default:
                return -1;
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        initialPosition = this.transform.localPosition;
        axis = transform.name.Split("_")[0];
        direction = transform.name.Split("_")[1];

        SetPositionBoundaries();
        

        shaderBoundaries[0] = shaderMin;
        shaderBoundaries[1] = shaderMax;
    }

    // Update is called once per frame
    void Update()
    {
        SwapCurrentSlicer();
    }
}
