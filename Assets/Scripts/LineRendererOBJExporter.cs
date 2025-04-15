using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LineRendererOBJExporter : MonoBehaviour
{
    public string exportFileName = "RuntimeLines.obj";
    public string materialFileName = "RuntimeLines.mtl";
    public bool exportOnKeyPressE = true;
    public KeyCode exportKey = KeyCode.E;

    private void Update()
    {
        if (exportOnKeyPressE && Input.GetKeyDown(exportKey))
        {
            ExportLines();
        }
    }

    public void ExportLines()
    {
        LineRenderer[] lineRenderers = GetComponentsInChildren<LineRenderer>();
        if (lineRenderers.Length == 0)
        {
            Debug.LogWarning("No LineRenderer components found in this GameObject or its children.");
            return;
        }

        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>(); // Minimal normals for basic rendering
        List<Color> vertexColors = new List<Color>();
        List<int> faceIndices = new List<int>();
        int vertexCount = 0;

        string filePath = Path.Combine(Application.persistentDataPath, exportFileName);
        string mtlFilePath = Path.Combine(Application.persistentDataPath, materialFileName);

        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.WriteLine("# Exported LineRenderers at Runtime");
            sw.WriteLine($"mtllib {materialFileName}");
            sw.WriteLine($"g {gameObject.name}_CombinedLines");
            sw.WriteLine("usemtl default");

            foreach (LineRenderer lr in lineRenderers)
            {
                if (lr.enabled && lr.positionCount >= 2)
                {
                    Gradient gradient = lr.colorGradient;
                    float startWidth = lr.startWidth;
                    float endWidth = lr.endWidth;
                    int segments = lr.positionCount - 1;
                    Vector3[] positions = new Vector3[lr.positionCount];
                    lr.GetPositions(positions);

                    for (int i = 0; i < segments; i++)
                    {
                        Vector3 p1 = lr.transform.TransformPoint(positions[i]);
                        Vector3 p2 = lr.transform.TransformPoint(positions[i + 1]);

                        Vector3 forward = (p2 - p1).normalized;
                        Vector3 up = Vector3.up; // Adjust if needed based on your scene
                        Vector3 right = Vector3.Cross(forward, up).normalized;

                        float width1 = Mathf.Lerp(startWidth, endWidth, (float)i / segments);
                        float width2 = Mathf.Lerp(startWidth, endWidth, (float)(i + 1) / segments);

                        Vector3 v1 = p1 + right * width1 * 0.5f;
                        Vector3 v2 = p1 - right * width1 * 0.5f;
                        Vector3 v3 = p2 + right * width2 * 0.5f;
                        Vector3 v4 = p2 - right * width2 * 0.5f;

                        float t1 = (float)i / segments;
                        float t2 = (float)(i + 1) / segments;
                        Color c1 = gradient.Evaluate(t1);
                        Color c2 = gradient.Evaluate(t2);

                        vertices.Add(v1);
                        vertices.Add(v2);
                        vertices.Add(v3);
                        vertices.Add(v4);

                        vertexColors.Add(c1);
                        vertexColors.Add(c1);
                        vertexColors.Add(c2);
                        vertexColors.Add(c2);

                        normals.Add(forward); // Basic forward normal
                        normals.Add(forward);
                        normals.Add(forward);
                        normals.Add(forward);

                        faceIndices.Add(vertexCount);
                        faceIndices.Add(vertexCount + 2);
                        faceIndices.Add(vertexCount + 1);

                        faceIndices.Add(vertexCount + 1);
                        faceIndices.Add(vertexCount + 2);
                        faceIndices.Add(vertexCount + 3);

                        vertexCount += 4;

                        sw.WriteLine($"v {v1.x} {v1.y} {v1.z}");
                        sw.WriteLine($"v {v2.x} {v2.y} {v2.z}");
                        sw.WriteLine($"v {v3.x} {v3.y} {v3.z}");
                        sw.WriteLine($"v {v4.x} {v4.y} {v4.z}");

                        sw.WriteLine($"vn {forward.x} {forward.y} {forward.z}");
                        sw.WriteLine($"vn {forward.x} {forward.y} {forward.z}");
                        sw.WriteLine($"vn {forward.x} {forward.y} {forward.z}");
                        sw.WriteLine($"vn {forward.x} {forward.y} {forward.z}");

                        sw.WriteLine($"vc {c1.r} {c1.g} {c1.b}");
                        sw.WriteLine($"vc {c1.r} {c1.g} {c1.b}");
                        sw.WriteLine($"vc {c2.r} {c2.g} {c2.b}");
                        sw.WriteLine($"vc {c2.r} {c2.g} {c2.b}");
                    }
                }
            }

            for (int i = 0; i < faceIndices.Count; i += 3)
            {
                sw.WriteLine($"f {faceIndices[i] + 1} {faceIndices[i + 1] + 1} {faceIndices[i + 2] + 1}");
            }
        }

        using (StreamWriter swMtl = new StreamWriter(mtlFilePath))
        {
            swMtl.WriteLine("# Material for runtime lines");
            swMtl.WriteLine("newmtl default");
            swMtl.WriteLine("Kd 1 1 1"); // Basic white diffuse - vertex colors will be the actual color
        }

        Debug.Log($"OBJ file exported to: {filePath}");
        Debug.Log($"MTL file exported to: {mtlFilePath}");
    }
}