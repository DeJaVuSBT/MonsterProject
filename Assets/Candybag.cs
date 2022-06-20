using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candybag : MonoBehaviour
{
    [SerializeField]
    TwinQuest tq;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            tq.GetCandy = true;
        }
    }
}
