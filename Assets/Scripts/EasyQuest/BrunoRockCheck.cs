using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrunoRockCheck : MonoBehaviour
{
    [SerializeField]
    BrunoQuest bq;
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="BrunoRock")
        {
            bq.RockCount--;
        }
    }
}
