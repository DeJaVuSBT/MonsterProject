using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineInteractive : MonoBehaviour
{
    Material outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        GameObject outlineObject = new GameObject();

        outlineObject.AddComponent(typeof(MeshFilter));
        outlineObject.AddComponent(typeof(MeshRenderer));
        outlineObject.GetComponent<MeshFilter>().mesh = this.GetComponent<MeshFilter>().mesh;

        outlineObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        outlineMaterial = GetComponent<Renderer>().material;
        outlineMaterial.color = Color.red;

        outlineObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        //Instantiate(outlineObject, outlineObject.transform.position, Quaternion.identity);
        Debug.Log("damn bro ");
    }

    private void OnDestroy()
    {
        Destroy(outlineMaterial);
    }
}
