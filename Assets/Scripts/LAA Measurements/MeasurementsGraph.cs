using Oculus.Interaction.DebugTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MeasurementsGraph : MonoBehaviour
{

    [SerializeField] private LineRenderer D1Line;
    [SerializeField] private LineRenderer D2Line;
    [SerializeField] private LineRenderer DMeanLine;
    [SerializeField] private LineRenderer PDMDLine;

    [SerializeField] private GameObject separatorLinePrefab;
    [SerializeField] private GameObject separatorLines;

    [SerializeField] private GameObject MaxText;
    [SerializeField] private GameObject MinText;

    private double GraphXDistance = 0.16f;
    private double GraphYDistance = 0.285f;

    private float maxValue = -9999f;
    private float minValue = 9999f;

    private float minPoint = 0.055f;
    private float maxPoint = 0.34f;

    public void ResetMeasurementsGraph()
    {
        D1Line.positionCount = 0;
        D2Line.positionCount = 0;
        DMeanLine.positionCount = 0;
        PDMDLine.positionCount = 0;
        DestroyAllSeparators();
    }

    public void DestroyAllSeparators()
    {
        GameObject[] others = GameObject.FindGameObjectsWithTag("SeparatorLine");
        foreach (GameObject go in others)
        { 
            Destroy(go);
        }
    }


    public float CalculateYPosition(float value)
    {

        float distance = Mathf.Abs(value-minValue);

        float maxDistance = Mathf.Abs(maxValue - minValue);

        float distancePercentage = distance / (maxDistance / 100);

        return (float)(minPoint + (GraphYDistance / 100) * distancePercentage);
       
        

    }
    public void CalculateMaxAndMin(int count, List<LAAMeasurementsManager.Contours> centrelineContours)
    {
        List<float> D1Values = new List<float>();
        List<float> D2Values = new List<float>();
        List<float> DMeanValues = new List<float>();
        List<float> PDMDValues = new List<float>();

        for (int i = 0; i < count; i++)
        {
            D1Values.Add(centrelineContours[i].measurements.D1);
            D2Values.Add(centrelineContours[i].measurements.D2);
            DMeanValues.Add(centrelineContours[i].measurements.Dmean);
            PDMDValues.Add(centrelineContours[i].measurements.PDMD);
        }

        List<float> maxValues = new List<float>();
        List<float> minValues = new List<float>();

        Debug.Log(D2Values.Min());

        maxValues.Add(D1Values.Max());
        maxValues.Add(D2Values.Max());
        maxValues.Add(DMeanValues.Max());
        maxValues.Add(PDMDValues.Max());

        minValues.Add(D1Values.Min());
        minValues.Add(D2Values.Min());
        minValues.Add(DMeanValues.Min());
        minValues.Add(PDMDValues.Min());

        maxValue = maxValues.Max();
        minValue = minValues.Min();

    }

    public void RenderMeasurementsGraph()
    {
        ResetMeasurementsGraph();

        if (LAAMeasurementsManager.Instance.GetCurrentCentrelineMeasures() != null) {

            LAAMeasurementsManager.CentrelineMeasurement centrelineMeasures = LAAMeasurementsManager.Instance.GetCurrentCentrelineMeasures();
            List<LAAMeasurementsManager.Contours> centrelineContours = centrelineMeasures.contours;

            int contourCount = centrelineContours.Count;

            CalculateMaxAndMin(contourCount, centrelineContours);
            MinText.GetComponent<TMP_Text>().text = Mathf.Round(minValue).ToString();
            MaxText.GetComponent<TMP_Text>().text = Mathf.Round(maxValue).ToString();

            double xDistance = GraphXDistance / (contourCount-1);

            D1Line.positionCount = contourCount;
            D2Line.positionCount = contourCount;
            DMeanLine.positionCount = contourCount; 
            PDMDLine.positionCount = contourCount;

            for (int i = 0; i < contourCount; i++)
            {

                D1Line.SetPosition(i, new Vector3( (float)(i * xDistance), CalculateYPosition(centrelineContours[i].measurements.D1), 0));
                D2Line.SetPosition(i, new Vector3((float)(i * xDistance), CalculateYPosition(centrelineContours[i].measurements.D2), 0));
                DMeanLine.SetPosition(i, new Vector3((float)(i * xDistance), CalculateYPosition(centrelineContours[i].measurements.Dmean), 0));
                PDMDLine.SetPosition(i, new Vector3((float)(i * xDistance), CalculateYPosition(centrelineContours[i].measurements.PDMD), 0));

                GameObject separator = Instantiate(separatorLinePrefab);
                separator.transform.SetParent(separatorLines.transform);
                separator.GetComponent<RectTransform>().localPosition = D1Line.transform.GetComponent<RectTransform>().localPosition;
                separator.GetComponent<RectTransform>().sizeDelta = D1Line.transform.GetComponent<RectTransform>().sizeDelta;
                separator.GetComponent<RectTransform>().rotation = D1Line.transform.GetComponent<RectTransform>().rotation;
                separator.GetComponent<RectTransform>().localScale = D1Line.transform.GetComponent<RectTransform>().localScale;

                separator.GetComponent<LineRenderer>().positionCount = 2;
                separator.GetComponent<LineRenderer>().SetPosition(0, new Vector3((float)(i * xDistance), minPoint, 0));
                separator.GetComponent<LineRenderer>().SetPosition(1, new Vector3((float)(i * xDistance), maxPoint, 0));

            }

            

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
