/*using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MoveLineRenderersEditor : MonoBehaviour
{
    public bool applyOriginalTransforms = true; // Should the combined lines respect the original child transforms?

    [ContextMenu("Combine Child LineRenderers")]
    public void CombineLineRenderers()
    {
        List<LineRenderer> childLineRenderers = GetComponentsInChildren<LineRenderer>().Where(lr => lr.gameObject != gameObject).ToList();

        if (childLineRenderers.Count == 0)
        {
            Debug.LogWarning("No child LineRenderer components found to combine.", this);
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Mesh combinedMesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        List<Color> colors = new List<Color>();

        Material sharedMaterial = null; // Track the material to apply to the combined mesh
        bool materialsAreConsistent = true;

        int vertexOffset = 0;

        foreach (LineRenderer lr in childLineRenderers)
        {
            if (lr.positionCount < 2)
            {
                Debug.LogWarning($"LineRenderer on {lr.gameObject.name} has less than 2 points and will be skipped.", lr);
                continue;
            }

            int positionCount = lr.positionCount;
            Vector3[] positions = new Vector3[positionCount];
            lr.GetPositions(positions);
            Gradient gradient = lr.colorGradient;

            for (int i = 0; i < positions.Length - 1; i++)
            {
                Vector3 startPoint = positions[i];
                Vector3 endPoint = positions[i + 1];

                if (applyOriginalTransforms)
                {
                    startPoint = lr.transform.TransformPoint(startPoint);
                    endPoint = lr.transform.TransformPoint(endPoint);
                }

                vertices.Add(startPoint);
                vertices.Add(endPoint);

                indices.Add(vertexOffset + 0);
                indices.Add(vertexOffset + 1);

                // Handle gradient colors (sample at the start and end of each segment)
                float tStart = (float)i / (positions.Length - 1);
                float tEnd = (float)(i + 1) / (positions.Length - 1);

                colors.Add(gradient.Evaluate(tStart));
                colors.Add(gradient.Evaluate(tEnd));

                vertexOffset += 2;
            }

            // Check material consistency
            if (sharedMaterial == null)
            {
                sharedMaterial = lr.sharedMaterial;
            }
            else if (lr.sharedMaterial != sharedMaterial)
            {
                materialsAreConsistent = false;
                Debug.LogWarning($"Child LineRenderers have different materials. The material of the first LineRenderer will be used.", lr);
            }

            // Optionally disable the original LineRenderer component
            lr.enabled = false;
        }

        combinedMesh.vertices = vertices.ToArray();
        combinedMesh.colors = colors.ToArray();
        combinedMesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
        combinedMesh.RecalculateBounds();

        meshFilter.sharedMesh = combinedMesh;
        meshRenderer.sharedMaterial = sharedMaterial;

        Debug.Log($"Combined {childLineRenderers.Count} LineRenderers into a single mesh on {gameObject.name}.");

        // Optionally destroy the original child GameObjects with LineRenderers
        foreach (var lr in childLineRenderers)
        {
            if (lr != null && lr.gameObject != null)
            {
                Destroy(lr.gameObject);
            }
        }

    }

    public void Start()
    {
        CombineLineRenderers();
    }
}*/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor; // Required for AssetDatabase

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MoveLineRenderersEditor : MonoBehaviour
{
    /*public bool applyOriginalTransforms = true; // Should the combined lines respect the original child transforms?
    public string assetName = "CombinedLinesMesh"; // Name for the generated mesh asset

    [ContextMenu("Combine Child LineRenderers")]
    public void CombineLineRenderers()
    {
        List<LineRenderer> childLineRenderers = GetComponentsInChildren<LineRenderer>().Where(lr => lr.gameObject != gameObject).ToList();

        if (childLineRenderers.Count == 0)
        {
            Debug.LogWarning("No child LineRenderer components found to combine.", this);
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Mesh combinedMesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();
        List<Color> colors = new List<Color>();

        Material sharedMaterial = null; // Track the material to apply to the combined mesh
        bool materialsAreConsistent = true;

        int vertexOffset = 0;

        foreach (LineRenderer lr in childLineRenderers)
        {
            int positionCount = lr.positionCount;
            if (positionCount < 2)
            {
                Debug.LogWarning($"LineRenderer on {lr.gameObject.name} has less than 2 points and will be skipped.", lr);
                continue;
            }

            Vector3[] positions = new Vector3[positionCount];
            lr.GetPositions(positions); // Get all positions at once
            Gradient gradient = lr.colorGradient;

            for (int i = 0; i < positionCount - 1; i++)
            {
                Vector3 startPoint = positions[i];
                Vector3 endPoint = positions[i + 1];

                if (applyOriginalTransforms)
                {
                    startPoint = lr.transform.TransformPoint(startPoint);
                    endPoint = lr.transform.TransformPoint(endPoint);
                }

                vertices.Add(startPoint);
                vertices.Add(endPoint);

                indices.Add(vertexOffset + 0);
                indices.Add(vertexOffset + 1);

                // Handle gradient colors (sample at the start and end of each segment)
                float tStart = (float)i / (positionCount - 1);
                float tEnd = (float)(i + 1) / (positionCount - 1);

                colors.Add(gradient.Evaluate(tStart));
                colors.Add(gradient.Evaluate(tEnd));

                vertexOffset += 2;
            }

            // Check material consistency
            if (sharedMaterial == null)
            {
                sharedMaterial = lr.sharedMaterial;
            }
            else if (lr.sharedMaterial != sharedMaterial)
            {
                materialsAreConsistent = false;
                Debug.LogWarning($"Child LineRenderers have different materials. The material of the first LineRenderer will be used.", lr);
            }

            // Optionally disable the original LineRenderer component
            lr.enabled = false;
        }

        combinedMesh.vertices = vertices.ToArray();
        combinedMesh.colors = colors.ToArray();
        combinedMesh.SetIndices(indices.ToArray(), MeshTopology.Lines, 0);
        combinedMesh.RecalculateBounds();

        // Save the generated mesh as an asset
#if UNITY_EDITOR
        string assetPath = $"Assets/{assetName}_{gameObject.name}.asset";
        AssetDatabase.CreateAsset(combinedMesh, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);
#else
        meshFilter.sharedMesh = combinedMesh; // In build, the mesh is just assigned (not saved as asset)
#endif
        meshRenderer.sharedMaterial = sharedMaterial;

        Debug.Log($"Combined {childLineRenderers.Count} LineRenderers into a single mesh on {gameObject.name}. Mesh saved as asset: {assetPath}");

        // Optionally destroy the original child GameObjects with LineRenderers
        foreach (var lr in childLineRenderers)
        {
            if (lr != null && lr.gameObject != null)
            {
                DestroyImmediate(lr.gameObject); // Use DestroyImmediate in edit mode
            }
        }
    }

    public void Start()
    {
        CombineLineRenderers();
    }*/
}