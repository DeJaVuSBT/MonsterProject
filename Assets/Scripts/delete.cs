using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class delete : MonoBehaviour
{
    public bool doitnow = false;
    public bool add = false;
    [Range(0, 2)]
    public int t;
    public int Morality;
    public int Hunger;
    public Mesh mesh;
    void OnEnable()
    {
        if (doitnow)
        {


            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).GetChild(0).GetComponent<BoxCollider>());
            }
        }

        if (add)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                /*
                MoraEvents  a = transform.GetChild(i).gameObject.AddComponent<MoraEvents>();
                    a.SetInteractType(t);
                a.SetReward(Morality, Hunger);

                 transform.GetChild(i).gameObject.AddComponent<BoxCollider>();
                // meshc.center = new Vector3(0.5f,0.5f,0.5f);*/
                DestroyImmediate(transform.GetChild(i).GetComponent<SpawnObjectData>());
            }
            
        }
    }
}
