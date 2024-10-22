using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using Unity.VisualScripting;

public class CentrelineRenderer : MonoBehaviour
{

    private List<CentrelineManager.Point> centrelinePoints = new List<CentrelineManager.Point>();
    private string filePathCentreline;
    [SerializeField] private LineRenderer lineRenderer;

    public string GetFilePathCentreline()
    {
        return filePathCentreline;
    }

    public void SetFilePathCentreline(string path)
    {
        filePathCentreline = path;
    }

    public List<CentrelineManager.Point> GetCentrelinePoints() { return centrelinePoints; }



    string ReadTextFile(string filePath)
    {
        StreamReader inp_stm = new StreamReader(filePath);

        string input = "";

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            input += inp_ln; 
        }

        inp_stm.Close();

        return input;
    }

    void ReadCentrelinePoints(string input)
    {
        centrelinePoints.Clear();

        string[] points = input.Split(" ");


        int coordaxis = 0;

        double coordx = 0;
        double coordy = 0;
        double coordz = 0;

        for (int i = 0; i < points.Length; i++)
        {

            switch (coordaxis)
            {
                case 0:
                    coordaxis++;
                    coordx = double.Parse(points[i], CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 1:
                    coordaxis++;
                    coordy = double.Parse(points[i], CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case 2:
                    coordaxis = 0;
                    coordz = double.Parse(points[i], CultureInfo.InvariantCulture.NumberFormat);
                    CentrelineManager.Point point = new CentrelineManager.Point(coordx, coordy, coordz);
                    centrelinePoints.Add(point);
                    break;
            }

        }

    }

    void RenderCentrelinePoints(string input)
    {
        ReadCentrelinePoints(input);

        lineRenderer.positionCount = centrelinePoints.Count;

        for (int i = 0;i < lineRenderer.positionCount; i++)
        {

            lineRenderer.SetPosition(i, new Vector3((float)centrelinePoints[i].coordx, (float)centrelinePoints[i].coordy, (float)centrelinePoints[i].coordz));
            
        }
        lineRenderer.Simplify(0.01f);
    }

    public void Render()
    {
        RenderCentrelinePoints(ReadTextFile(filePathCentreline));
        CentrelineManager.Instance.centrelinesList.Add(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void SetMaterial(Material material)
    {

        lineRenderer.material = material;
    }

    public void RenderHardcoded(string centreline)
    {
        RenderCentrelinePoints(centreline);
        CentrelineManager.Instance.centrelinesList.Add(this.gameObject);
        transform.localScale = new Vector3(-1, 1, 1);
        transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //this.gameObject.SetActive(false);
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
