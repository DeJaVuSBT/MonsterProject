using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableOBJLayerSorting : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> renderers;
    private void Start()
    {
        
        GameObject[] list = GameObject.FindGameObjectsWithTag("Moveable");
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i].GetComponent<SpriteRenderer>())
            {
                renderers.Add(list[i].GetComponent<SpriteRenderer>());
                list[i].GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            else
            {
                renderers.Add(list[i].transform.GetChild(0).GetComponent<SpriteRenderer>());
                list[i].transform.GetChild(0).GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            
        }
        renderers.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>());
    }

    private void LateUpdate()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].sortingOrder =(int)(renderers[i].gameObject.transform.position.z * -200);
        }
    }
}
