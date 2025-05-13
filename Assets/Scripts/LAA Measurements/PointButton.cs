using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointButton : MonoBehaviour
{
    [SerializeField] private LAAMeasurementsManager.Contours contour;
    [SerializeField] private GameObject PointStoredPrefab;

    [SerializeField] private GameObject PointStored;

    [SerializeField] private GameObject ContourRender;

    [SerializeField] private bool isActive = false;

    [SerializeField] private bool isToggle = true;

    public void SetIsToggle(bool isToggle)
    {
        this.isToggle = isToggle;
    }

    public void DeactivateBackground()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ToggleIsActive()
    {
        isActive = !isActive;
    }

    public void SetContour(LAAMeasurementsManager.Contours contour)
    {
        this.contour = contour;
    }

    public void SetContourRender(GameObject contour)
    {
        ContourRender = contour;
    }

    public void SetText(string text)
    {
        transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = text;
    }

    public void SpawnPointStored()
    {
        GameObject point = Instantiate(PointStoredPrefab);
        point.transform.SetParent(LAAMeasurementsManager.Instance.PointSlider.transform);
        point.GetComponent<PointStored>().SetPointTexts(name,
                                                        (Mathf.Round(contour.measurements.D1 * 100.0f) * 0.1f).ToString(),
                                                        (Mathf.Round(contour.measurements.Dmean * 100.0f) * 0.1f).ToString(),
                                                        (Mathf.Round(contour.measurements.D2 * 100.0f) * 0.1f).ToString(),
                                                        (Mathf.Round(contour.measurements.PDMD * 100.0f) * 0.1f).ToString(),
                                                        "0",
                                                        "0");
        point.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        point.GetComponent<RectTransform>().localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        point.GetComponent<RectTransform>().localRotation = new Quaternion(0.0f,0.0f,0.0f,0.0f);

        PointStored = point;
    }

    public void SetContourActive()
    {
         ContourRender.SetActive(!ContourRender.activeSelf);
    }
    public void DestroyPointStored()
    {
        Destroy(PointStored);
    }

    public void SetCurrentContour(LAAMeasurementsManager.Contours contours)
    {
        LAAMeasurementsManager.Instance.currentContour = contours;
    }

    public void ButtonPress()
    {
        if (this.gameObject.GetComponent<Toggle>().interactable)
        {
            if (isToggle)
            {
                if (isActive)
                {
                    if (LAAMeasurementsManager.Instance.currentContourRender == ContourRender)
                    {
                        LAAMeasurementsManager.Instance.currentContourRender.GetComponent<ContourRenderer>().SetMaterial(LAAMeasurementsManager.Instance.contourUnactiveMaterial);
                        LAAMeasurementsManager.Instance.currentContour = null;
                        LAAMeasurementsManager.Instance.currentContourRender = null;
                        LAAMeasurementsManager.Instance.ResetContour2D();
                        LAAMeasurementsManager.Instance.CalculateRecommendedSizes();
                    }
                    DestroyPointStored();
                    ToggleIsActive();
                    SetContourActive();

                }
                else
                {

                    SpawnPointStored();
                    ToggleIsActive();
                    SetContourActive();
                    SetCurrentContour(contour);
                    LAAMeasurementsManager.Instance.RenderContour2D();

                    if (LAAMeasurementsManager.Instance.currentContourRender)
                    {
                        LAAMeasurementsManager.Instance.currentContourRender.GetComponent<ContourRenderer>().SetMaterial(LAAMeasurementsManager.Instance.contourUnactiveMaterial);
                    }

                    LAAMeasurementsManager.Instance.currentContourRender = ContourRender;
                    LAAMeasurementsManager.Instance.currentContourRender.GetComponent<ContourRenderer>().SetMaterial(LAAMeasurementsManager.Instance.contourActiveMaterial);

                    if (LAAMeasurementsManager.Instance.PointValues)
                    {
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = name;
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.D1 * 100.0f) * 0.01f).ToString();
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.Dmean * 100.0f) * 0.01f).ToString();
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.D2 * 100.0f) * 0.01f).ToString();
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(4).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.PDMD * 100.0f) * 0.01f).ToString();
                    }

                    LAAMeasurementsManager.Instance.CalculateRecommendedSizes();
                }
            }
            else
            {
                if (ContourRender.activeSelf)
                {
                    if (LAAMeasurementsManager.Instance.currentContourRender)
                    {
                        LAAMeasurementsManager.Instance.currentContourRender.GetComponent<ContourRenderer>().SetMaterial(LAAMeasurementsManager.Instance.contourUnactiveMaterial);
                    }
                    LAAMeasurementsManager.Instance.currentContourRender = ContourRender;
                    LAAMeasurementsManager.Instance.currentContourRender.GetComponent<ContourRenderer>().SetMaterial(LAAMeasurementsManager.Instance.contourActiveMaterial);

                    if (LAAMeasurementsManager.Instance.PointValues)
                    {
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = name;
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.D1 * 100.0f) * 0.01f).ToString();
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.Dmean * 100.0f) * 0.01f).ToString();
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.D2 * 100.0f) * 0.01f).ToString();
                        LAAMeasurementsManager.Instance.PointValues.transform.GetChild(4).gameObject.GetComponent<TMP_Text>().text = (Mathf.Round(contour.measurements.PDMD * 100.0f) * 0.01f).ToString();
                    }
                    LAAMeasurementsManager.Instance.CalculateRecommendedSizes();
                    SetCurrentContour(contour);
                    LAAMeasurementsManager.Instance.RenderContour2D();

                    if (LAAMeasurementsManager.Instance.currentContourActiveButton)
                    {
                        LAAMeasurementsManager.Instance.currentContourActiveButton.GetComponent<Toggle>().isOn = false;

                        LAAMeasurementsManager.Instance.currentContourActiveButton = this.gameObject;
                    }

                }


            }

            DeviceSelectorManager.Instance.InactiveAllCompressionCases();
        }
    }

    /*public void SetButtonColor(Color color)
    {
        Oculus.Interaction.InteractableColorVisual.ColorState colorState = new Oculus.Interaction.InteractableColorVisual.ColorState() { Color = color };
        this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.InteractableColorVisual>().InjectOptionalNormalColorState(colorState);
        this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Oculus.Interaction.RoundedBoxProperties>().Color = color;

        this.transform.gameObject.GetComponent<Toggle>().Invoke();
    }*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PointStored)
        {
            PointStored.GetComponent<RectTransform>().localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }

        if (!isToggle && isActive)
        {
            this.gameObject.GetComponent<Toggle>().interactable = true;

            if (LAAMeasurementsManager.Instance.currentContourActiveButton)
            {
                if (LAAMeasurementsManager.Instance.currentContourActiveButton != this.gameObject)
                {
                    this.gameObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    this.gameObject.GetComponent<Toggle>().isOn = true;
                }
            }
            if (!ContourRender.activeSelf)
            {
                isActive = false;
            }

        }
        else if(!isToggle && !isActive)
        {
            this.gameObject.GetComponent<Toggle>().interactable = false;

            if (ContourRender.activeSelf)
            {
                isActive = true;
            }
        }

        if (isToggle)
        {
            if (isActive)
            {
                this.gameObject.GetComponent<Toggle>().isOn = true;
            }
            else
            {
                this.gameObject.GetComponent<Toggle>().isOn = false;
            }
        }
        

        /*if(!isToggle)
        {
            if(contour == LAAMeasurementsManager.Instance.currentContour)
            {
                SetButtonColor(new Color(0f, 0.55f, 1f));
            }
        }*/
    }
}
