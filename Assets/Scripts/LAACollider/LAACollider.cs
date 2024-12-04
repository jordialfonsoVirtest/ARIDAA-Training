using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LAACollider : MonoBehaviour
{
    [SerializeField] bool isCorrect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.transform.parent.name);
        if (LAAMeasurementsManager.Instance.recommendedSizeValueAmplatzerAMULET != 0)
        {
            if (other.tag == "AMULET")
            {
                int deviceSize = int.Parse(other.gameObject.transform.parent.name.Split('_')[3]);

                if (deviceSize == LAAMeasurementsManager.Instance.recommendedSizeValueAmplatzerAMULET) {

                    if (isCorrect) {

                        LAAColliderManager.Instance.displayTimelapseCase(0);

                    }
                    else
                    {

                        LAAColliderManager.Instance.displayTimelapseCase(3);

                    }

                }else if (deviceSize < LAAMeasurementsManager.Instance.recommendedSizeValueAmplatzerAMULET)
                {

                    if (isCorrect)
                    {

                        LAAColliderManager.Instance.displayTimelapseCase(1);

                    }
                    else
                    {

                        LAAColliderManager.Instance.displayTimelapseCase(4);

                    }

                }
                else
                {

                    if (isCorrect)
                    {

                        LAAColliderManager.Instance.displayTimelapseCase(2);

                    }
                    else
                    {

                        LAAColliderManager.Instance.displayTimelapseCase(5);

                    }

                }
            }
        }
    }*/

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "AMULET")
        {

            if (isCorrect)
            {

                LAAColliderManager.Instance.displayTimelapseCase(0);

            }
            else
            {

                LAAColliderManager.Instance.displayTimelapseCase(1);

            }
                
        }
    }

}
