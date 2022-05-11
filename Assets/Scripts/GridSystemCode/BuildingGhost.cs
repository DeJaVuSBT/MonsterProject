using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildingGhost : MonoBehaviour {

    [SerializeField]private Transform visual;

    private void Start() {
        RefreshVisual();

        SpawnerGrid.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }

    private void LateUpdate() {
        Vector3 targetPosition = SpawnerGrid.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 0.2f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, SpawnerGrid.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        SpawnObjScriptable spawnObjScriptable = SpawnerGrid.Instance.ReturnObjectInList();

        if (spawnObjScriptable != null) {
            visual = Instantiate(spawnObjScriptable.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 11);
        }
    }
    //set layer of spawned obj and its child
    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

}

