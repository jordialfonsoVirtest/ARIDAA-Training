using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointStored : MonoBehaviour
{
    [SerializeField] private TMP_Text Point;
    [SerializeField] private TMP_Text D1;
    [SerializeField] private TMP_Text DMean;
    [SerializeField] private TMP_Text D2;
    [SerializeField] private TMP_Text PDMD;
    [SerializeField] private TMP_Text Device1;
    [SerializeField] private TMP_Text Device2;

    public void SetPointTexts(string Point, string D1, string DMean, string D2, string PDMD, string Device1, string Device2)
    {
        this.Point.text = Point;
        this.D1.text = D1;
        this.DMean.text = DMean;
        this.D2.text = D2;
        this.PDMD.text = PDMD;
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
