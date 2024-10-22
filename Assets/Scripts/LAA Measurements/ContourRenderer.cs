using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using static LAAMeasurementsManager;

public class ContourRenderer : MonoBehaviour
{

    private List<Points_3D> contourPoints = new List<Points_3D>();

    [SerializeField] private LineRenderer lineRenderer;

    void SetContourPoints(Contours contour)
    {
        contourPoints.Clear();
        contourPoints = contour.points_3D;

    }

    void RenderContourPoints(Contours contour)
    {
        SetContourPoints(contour);

        lineRenderer.positionCount = contourPoints.Count;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {

            lineRenderer.SetPosition(i, new Vector3((float)contourPoints[i].x, (float)contourPoints[i].y, (float)contourPoints[i].z));

        }

    }

    public void Render(Contours contour)
    {
        RenderContourPoints(contour);
        transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.SetActive(false);
    }

    public void SetMaterial(Material material)
    {

        lineRenderer.material = material;
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
