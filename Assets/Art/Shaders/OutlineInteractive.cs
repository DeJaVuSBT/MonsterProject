using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OutlineInteractive : MonoBehaviour
{
    public Material outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        CreateOutline();
    }

    private void CreateOutline()
    {
        GameObject outlineObject = new GameObject();

        outlineObject.AddComponent(typeof(MeshFilter));
        outlineObject.AddComponent(typeof(MeshRenderer));
        outlineObject.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;

        outlineObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        outlineObject.GetComponent<Renderer>().material = outlineMaterial;

        Debug.Log("damn bro ");
        outlineObject.GetComponent<Renderer>().shadowCastingMode = ShadowCastingMode.Off;
    }

    private void OnDestroy()
    {
        Destroy(outlineMaterial);
    }
}
