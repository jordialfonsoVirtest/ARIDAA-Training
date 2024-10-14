using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using Unity.VisualScripting;

public class HeartLineRenderer : MonoBehaviour
{

    private List<CentrelineManager.Point> HeartPoints = new List<CentrelineManager.Point>();
    private string filePathHeartCoordinates;
    [SerializeField] private TextAsset heartCoordinates;
    [SerializeField] private Material cubeRender;

    public string GetHeartCoordinates()
    {
        return filePathHeartCoordinates;
    }

    public void SetfilePathHeartCoordinates(string path)
    {
        filePathHeartCoordinates = path;
    }

    public List<CentrelineManager.Point> GetHeartPoints() { return HeartPoints; }



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

    void ReadHeartPoints(string input)
    {
        HeartPoints.Clear();

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
                    HeartPoints.Add(point);
                    break;
            }

        }

    }

    void RenderHeartPoints(string input)
    {
        ReadHeartPoints(input);

        for (int i = 0;i < HeartPoints.Count; i++)
        {
            if (i % 6 == 0)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = transform;
                cube.transform.localPosition = new Vector3((float)HeartPoints[i].coordx, (float)HeartPoints[i].coordy, (float)HeartPoints[i].coordz);
                cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                cube.GetComponent<Renderer>().material = cubeRender;
            }
       
        }
    }

    public void Render()
    {
        RenderHeartPoints(ReadTextFile(filePathHeartCoordinates));
        this.gameObject.SetActive(false);
    }

    public void RenderHardcoded()
    {
        RenderHeartPoints(heartCoordinates.text);
        //transform.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        RenderHardcoded();
        //transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
