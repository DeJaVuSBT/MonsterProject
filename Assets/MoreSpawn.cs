using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoreSpawn : MonoBehaviour
{
    public  ScriptableObject[] sp = new ScriptableObject[5];
    public ScriptableObject[] sa = new ScriptableObject[5];
    public List<ScriptableObject[]> a= new List<ScriptableObject[]>();

    private void Start()
    {
        a.Add(sp);
        a.Add(sa);
    }
}
