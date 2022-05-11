using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectData : MonoBehaviour
{
    public static SpawnObjectData Create(Vector3 worldPos, Vector2Int origin, SpawnObjScriptable.Dir dir, SpawnObjScriptable sOS,Transform parent=null)
    {
        Transform SpawnedObjectT =
                    Instantiate(
                        sOS.prefab,
                        worldPos,
                        Quaternion.Euler(0, sOS.GetRotationAngle(dir), 0));
        SpawnObjectData sOD = SpawnedObjectT.GetComponent<SpawnObjectData>();
        sOD.sOS = sOS;
        sOD.origin = origin;
        sOD.dir = dir;
        if (parent!=null)
        {
            SpawnedObjectT.SetParent(parent);
        }
        return sOD;
    }
    private SpawnObjScriptable sOS;
    private Vector2Int origin;
    private SpawnObjScriptable.Dir dir;
    public List<Vector2Int> GetGridPositionList() {
        return sOS.GetGridPositionList(origin, dir);
    }
    public Vector2Int GetOrigin() { 
        return origin;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
