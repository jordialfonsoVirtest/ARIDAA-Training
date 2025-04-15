using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System;

public class StreamlineRenderer : MonoBehaviour
{
    [SerializeField] public TextAsset multiLinePointsJSONs;
    [SerializeField] public TextAsset pointsJSONs;
    [SerializeField] public TextAsset velocityMagnitudesJSONs;
    [SerializeField] public List<PointVelocityData> pointVelocityDataList = new List<PointVelocityData>();
    [SerializeField] public Material defaultLineMaterial; // Assign a default line material in the inspector
    [SerializeField] private float lineWidth = 0.001f;
    //[SerializeField] private int lineWidth = 0.01f;



    [System.Serializable]
    public class Vector3Json
    {
        public float x;
        public float y;
        public float z;
    }

    public class PointData
    {
        public float x;
        public float y;
        public float z;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            PointData other = (PointData)obj;
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }
    }

    public class PointVelocityData
    {
        public Vector3 position;
        public float normalizedVelocity;

        public PointVelocityData(Vector3 pos, float vel)
        {
            position = pos;
            normalizedVelocity = vel;
        }
    }

    private bool AreVectorsEqual(Vector3 a, Vector3 b, float tolerance = 0.005f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance &&
               Mathf.Abs(a.y - b.y) < tolerance &&
               Mathf.Abs(a.z - b.z) < tolerance;
    }

    private void Start()
    {
        //GenerateColoredMultiLinesFromJSONs();
    }
    public void GenerateColoredMultiLinesFromJSONs()
    {
        ParsePointsAndVelocities();
        GenerateAndColorLineSegments();
    }

    private void ParsePointsAndVelocities()
    {
        List<PointData> points = new List<PointData>();
        List<float> velocityMagnitudes = new List<float>();

        // ... (rest of the ParsePointsAndVelocities function remains the same) ...
        if (pointsJSONs != null)
        {
            List<PointData> parsedPoints = JsonConvert.DeserializeObject<List<PointData>>(pointsJSONs.text);
            if (parsedPoints != null)
            {
                points.AddRange(parsedPoints);
            }
        }
        else
        {
            Debug.LogWarning("pointsJSONs list is null.");
        }

        if (velocityMagnitudesJSONs != null)
        {
            List<List<float>> parsedVelocities = JsonConvert.DeserializeObject<List<List<float>>>(velocityMagnitudesJSONs.text);
            if (parsedVelocities != null)
            {
                velocityMagnitudes.AddRange(parsedVelocities.SelectMany(v => v)); // Flatten the list of lists
            }
        }
        else
        {
            Debug.LogWarning("velocityMagnitudesJSONs list is null.");
        }

        if (points.Count == velocityMagnitudes.Count)
        {
            float minVelocity = velocityMagnitudes.Min();
            float maxVelocity = velocityMagnitudes.Max();
            minVelocity = 0.0f;
            maxVelocity = 0.20f;

            for (int i = 0; i < points.Count; i++)
            {
                if(velocityMagnitudes[i] > maxVelocity)
                {
                    pointVelocityDataList.Add(new PointVelocityData(new Vector3(points[i].x, points[i].y, points[i].z), 1f));
                }
                else
                {
                    float normalizedVelocity = (maxVelocity - minVelocity == 0) ? 0.5f : (velocityMagnitudes[i] - minVelocity) / (maxVelocity - minVelocity);
                    pointVelocityDataList.Add(new PointVelocityData(new Vector3(points[i].x, points[i].y, points[i].z), normalizedVelocity));
                }

            }
        }
        else
        {
            Debug.LogError("Number of points and velocity magnitudes do not match!");
        }

    }

    private List<List<Vector3>> ParseJsonToPointLists(string json)
    {
        List<List<Vector3>> allLines = new List<List<Vector3>>();
        try
        {
            List<List<Vector3Json>> jsonLines = JsonConvert.DeserializeObject<List<List<Vector3Json>>>(json);

            if (jsonLines != null)
            {
                foreach (List<Vector3Json> pointList in jsonLines)
                {
                    List<Vector3> linePoints = new List<Vector3>();
                    foreach (Vector3Json pointData in pointList)
                    {
                        linePoints.Add(new Vector3(pointData.x, pointData.y, pointData.z));
                    }
                    if (linePoints.Count > 0)
                    {
                        allLines.Add(linePoints);
                    }
                }
            }
            else
            {
                Debug.LogError("JSON parsing failed or JSON is null (multi-line points).");
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("JSON parsing error (multi-line points): " + e.Message);
        }
        return allLines;
    }

    private void GenerateAndColorLineSegments()
    {
        if (multiLinePointsJSONs == null)
        {
            Debug.LogWarning("Multi Line Points JSONs list is null.");
            return;
        }

        List<List<Vector3>> lines = ParseJsonToPointLists(multiLinePointsJSONs.text);
        foreach (List<Vector3> linePoints in lines)
        {
            if (linePoints.Count >= 2)
            {
                // Split the line into segments of max 8 points
                for (int i = 0; i < linePoints.Count; i += 8)
                {
                    List<Vector3> segmentPoints = linePoints.GetRange(i, Mathf.Min(8, linePoints.Count - i));
                    if (segmentPoints.Count >= 2)
                    {
                        CreateColoredLineSegmentRenderer(segmentPoints);
                    }
                    else if (segmentPoints.Count > 0)
                    {
                        Debug.LogWarning("Not enough points for a segment.");
                    }
                }
            }
            else if (linePoints.Count > 0)
            {
                Debug.LogWarning("Not enough points to create a line in JSON: " + multiLinePointsJSONs.name + ". Need at least 2 points.");
            }
        }
    }

    private void CreateColoredLineSegmentRenderer(List<Vector3> points)
    {
        GameObject lineObj = new GameObject("ColoredLineSegment");
        lineObj.transform.parent = transform;
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        if (defaultLineMaterial != null)
        {
            lineRenderer.material = defaultLineMaterial;
        }
        else
        {
            Debug.LogWarning("Default Line Material not assigned. Using default Unity material.");
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        if (points.Count > 0)
        {
            Color[] colors = new Color[points.Count];
            Gradient gradient = new Gradient();
            GradientColorKey[] colorKeys = new GradientColorKey[points.Count];
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                Vector3 currentPoint = points[i];
                PointVelocityData data = pointVelocityDataList.FirstOrDefault(pvd => AreVectorsEqual(pvd.position, currentPoint));

                if (data != null)
                {
                    Color pointColor = GetCoolWarmColor(data.normalizedVelocity);
                    colorKeys[i] = new GradientColorKey(pointColor, (float)i / (points.Count - 1));
                }
                else
                {
                    Debug.LogWarning($"Could not find velocity data for point: {currentPoint} in generated line segment.");
                    colorKeys[i] = new GradientColorKey(Color.gray, (float)i / (points.Count - 1)); // Default color
                }
                alphaKeys[i] = new GradientAlphaKey(1f, (float)i / (points.Count - 1));
            }

            gradient.SetKeys(colorKeys, alphaKeys);
            lineRenderer.colorGradient = gradient;
        }
    }

    /*private Color GetCoolWarmColor(float normalizedValue)
    {
        // Cool to Warm colormap (similar to matplotlib's coolwarm)
        Color coolColor = Color.blue; // Blue
        Color warmColor = Color.red; // Red
        Color midColor = Color.white;

        normalizedValue = Mathf.Clamp01(normalizedValue);

        if (normalizedValue < 0.5f)
        {
            return Color.Lerp(coolColor, midColor, normalizedValue * 2f);
        }
        else
        {
            return Color.Lerp(midColor, warmColor, (normalizedValue - 0.5f) * 2f);
        }
    }*/

    private Color GetCoolWarmColor(float normalizedValue)
    {
        // Jet colormap implementation
        normalizedValue = Mathf.Clamp01(normalizedValue);

        if (normalizedValue < 1f / 8f)
        {
            return Color.Lerp(Color.blue, new Color(0f, 0f, 1f, 1f), normalizedValue * 8f); // Blue to Deep Blue (can be just Blue)
        }
        else if (normalizedValue < 3f / 8f)
        {
            return Color.Lerp(new Color(0f, 0f, 1f, 1f), Color.cyan, (normalizedValue - 1f / 8f) * 4f); // Deep Blue to Cyan
        }
        else if (normalizedValue < 5f / 8f)
        {
            return Color.Lerp(Color.cyan, Color.yellow, (normalizedValue - 3f / 8f) * 4f); // Cyan to Yellow
        }
        else if (normalizedValue < 7f / 8f)
        {
            return Color.Lerp(Color.yellow, Color.red, (normalizedValue - 5f / 8f) * 4f); // Yellow to Red
        }
        else
        {
            return Color.Lerp(Color.red, new Color(0.5f, 0f, 0f, 1f), (normalizedValue - 7f / 8f) * 4f); // Red to Dark Red (can be just Red)
        }
    }
}