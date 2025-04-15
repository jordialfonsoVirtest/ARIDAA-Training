using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class StreamlineManager : MonoBehaviour
{
    [SerializeField] public List<TextAsset> velocityMagnitudesJSONs;
    [SerializeField] public List<TextAsset> pointsJSONs;
    [SerializeField] public List<TextAsset> multiLinePointsJSONs;
    [SerializeField] private GameObject StreamlineRenderer;
    [SerializeField] private Material StreamlineMaterial;

    [SerializeField] private GameObject Models3D;

    // Start is called before the first frame update
    void Start()
    {
        RenderAllStreamlines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RenderAllStreamlines()
    {


        for (int i = 0; i <= pointsJSONs.Count-1; i++)
        {
            GameObject streamlineRenderer = Instantiate(StreamlineRenderer);
            streamlineRenderer.name = pointsJSONs[i].name;
            streamlineRenderer.transform.parent = Models3D.transform;
            streamlineRenderer.GetComponent<StreamlineRenderer>().velocityMagnitudesJSONs = velocityMagnitudesJSONs[i];
            streamlineRenderer.GetComponent<StreamlineRenderer>().pointsJSONs = pointsJSONs[i];
            streamlineRenderer.GetComponent<StreamlineRenderer>().multiLinePointsJSONs = multiLinePointsJSONs[i];
            streamlineRenderer.GetComponent<StreamlineRenderer>().defaultLineMaterial = StreamlineMaterial;
            streamlineRenderer.GetComponent<StreamlineRenderer>().GenerateColoredMultiLinesFromJSONs();
            streamlineRenderer.SetActive(false);

        }

    }
}
