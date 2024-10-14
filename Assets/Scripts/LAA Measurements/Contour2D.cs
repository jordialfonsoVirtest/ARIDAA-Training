using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contour2D : MonoBehaviour
{

    [SerializeField] private LineRenderer D1Line;
    [SerializeField] private LineRenderer D2Line;
    [SerializeField] private LineRenderer ContourLine;

    [SerializeField] private float scaley = 0.01f;
    [SerializeField] private float scalex = 0.01f;

    public void ResetLines()
    {
        D1Line.positionCount = 0;
        D2Line.positionCount = 0;
        ContourLine.positionCount = 0;
    }

    public void RenderContour2D()
    {
        ResetLines();

        if (LAAMeasurementsManager.Instance.currentContour != null) {

            List<LAAMeasurementsManager.Points_2D> contourPoints = LAAMeasurementsManager.Instance.currentContour.points_2D;
            LAAMeasurementsManager.Lines lines = LAAMeasurementsManager.Instance.currentContour.lines;
            LAAMeasurementsManager.Line2D D1Points = lines.D1_2D;
            LAAMeasurementsManager.Line2D D2Points = lines.D2_2D;

            D1Line.positionCount = 2;
            D1Line.SetPosition(0, new Vector3(D1Points.start.x * scalex, D1Points.start.y * scaley, 0));
            D1Line.SetPosition(1, new Vector3(D1Points.end.x * scalex, D1Points.end.y * scaley, 0));

            D2Line.positionCount = 2;
            D2Line.SetPosition(0, new Vector3(D2Points.start.x * scalex, D2Points.start.y * scaley, 0));
            D2Line.SetPosition(1, new Vector3(D2Points.end.x * scalex, D2Points.end.y * scaley, 0));

            ContourLine.positionCount = contourPoints.Count;
            for (int i = 0; i < ContourLine.positionCount; i++)
            {

                ContourLine.SetPosition(i, new Vector3((float)contourPoints[i].x * scalex, (float)contourPoints[i].y * scaley, 0));
                

            }

            ContourLine.Simplify(0.001f);
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
