using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OutlineInteractive : MonoBehaviour
{
    public Material outlineMaterial;
    public GameObject InteractableOBJ=null;
    public GameObject target=null;
    // Start is called before the first frame update
    void Start()
    {
        CreateOutline();
    }

    private void CreateOutline()
    {
        
        target = InteractableOBJ.GetComponentInChildren<MeshRenderer>().gameObject;
        GameObject outlineObject=Instantiate(target, InteractableOBJ.transform);

        outlineObject.GetComponent<Renderer>().material = outlineMaterial;

        outlineObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
    }
}
