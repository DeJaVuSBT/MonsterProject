using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SpawnerGrid : MonoBehaviour
{
    public static SpawnerGrid Instance { get; private set; }

    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;

    [SerializeField] private SpawnObjScriptable[] spawnObjScriptable;
    [SerializeField]
    private int id = 0;
    private GridGeneric<SpawnObject> grid;
    private SpawnObjScriptable.Dir dir;
    private int number;
    [SerializeField] private Transform holder=null;
    [Header("GridSize")]
    [SerializeField] private int gridW = 100;
    [SerializeField]private int gridH = 100;
    [SerializeField] private float blockSize = 2f;
    [Header("DebugTime")]
    [SerializeField] private float debugTime = 1f;
    [SerializeField]
    private InputField a;

    private void Awake()
    {
 
        number = 1;
        Instance = this;
        grid = new GridGeneric<SpawnObject>(gridW, gridH, blockSize, Vector3.zero, (GridGeneric<SpawnObject> g, int x, int y) => new SpawnObject(g, x, y));

    }
    private void Start()
    {
        a = GameObject.Find("Canvas/InputField").GetComponent<InputField>();
    }
    private void Update()
    {
        //spawn
        if (Input.GetMouseButtonDown(0)&&number!=0)
        {
            grid.GetXY(Mouse3D.GetMouseWorldPosition(), out int x, out int y);

            spawn(x, y);
            
        }
        //delete
        if (Input.GetMouseButtonDown(1))
        {
            SpawnObject sO = grid.GetObject(Mouse3D.GetMouseWorldPosition());
            SpawnObjectData sOD = sO.GetSpawnObjectData();
            erase(sOD);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = ReturnObjectInList().GetNextDir(dir);
            UtilsClass.CreateWorldTextPopup(""+dir, Mouse3D.GetMouseWorldPosition());
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (number< spawnObjScriptable.Length)
            {
                number++;
            }
            else if (number==spawnObjScriptable.Length)
            {
                number = 1;
                
            }
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            number = int.Parse(a.text) +1;
            OnSelectedChanged?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            grid.ShowDebug(debugTime,2,Color.yellow);
        }

    }

    private void spawn(int x, int y) {
        List<Vector2Int> gridPosList = ReturnObjectInList().GetGridPositionList(new Vector2Int(x, y), dir);
        bool canSpawn = true;
        foreach (Vector2Int gridPos in gridPosList)
        {
            if (!grid.GetObject(gridPos.x, gridPos.y).CanSpawn())
            {
                canSpawn = false;
                break;
            }
        }
        if (canSpawn)
        {
            Vector2Int rotationOffset = ReturnObjectInList().GetRotationOffset(dir);
            Vector3 SpawnObjectWorldPosition = grid.GetWorldPos(x, y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetBlockSize();
            SpawnObjectData sOD = SpawnObjectData.Create(SpawnObjectWorldPosition, new Vector2Int(x, y), dir, ReturnObjectInList(),holder);

            foreach (Vector2Int gridPos in gridPosList)
            {
                grid.GetObject(gridPos.x, gridPos.y).SetSpawnObjectData(sOD);
            }
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
          
        }
        else
        {

            UtilsClass.CreateWorldTextPopup("Already something!", grid.GetWorldPos(x, y));
        }

    }
    private void erase(SpawnObjectData sOD)
    {
        if (sOD != null)
        {
            sOD.Destroy();
            List<Vector2Int> gridPosList = sOD.GetGridPositionList();
            foreach (Vector2Int gridPos in gridPosList)
            {
                grid.GetObject(gridPos.x, gridPos.y).ClearData();
            }

        }
    }

    public SpawnObjScriptable ReturnObjectInList() {
        return spawnObjScriptable[number-1];
    }
 

    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        grid.GetXY(mousePosition, out int x, out int y);

        if (spawnObjScriptable != null)
        {
            Vector2Int rotationOffset = ReturnObjectInList().GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPos(x, y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetBlockSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (spawnObjScriptable != null)
        {
            return Quaternion.Euler(0, ReturnObjectInList().GetRotationAngle(dir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }
    public GridGeneric<SpawnObject> ReturnGrid() { 
           return grid;
    }
}
public class SpawnObject
{

    private GridGeneric<SpawnObject> g;
    private int x;
    private int y;
    private SpawnObjectData sOD;

    public void ObjectImport(SpawnObject exporter)
    {
        if (CanSpawn())
        {
            SetSpawnObjectData(exporter.GetSpawnObjectData());
            exporter.ClearData();
        }
        else
        {
            Debug.Log("place is taken");
        }
    }

    public SpawnObject(GridGeneric<SpawnObject> g, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.g = g;
    }
    public SpawnObjectData GetSpawnObjectData()
    {

        return sOD;
    }
    public void SetSpawnObjectData(SpawnObjectData sOD)
    {
        this.sOD = sOD;
        g.TriggerGridObjectChanged(x, y);
    }
    public void ClearData()
    {
        sOD = null;
        g.TriggerGridObjectChanged(x, y);
    }
    public bool CanSpawn()
    {
        return sOD == null;
    }
    public override string ToString()
    {
        return x + "," + y + "\n" + sOD;
    }
    public Vector2Int ReturnXY()
    {
        return new Vector2Int(x, y);
    }
}
