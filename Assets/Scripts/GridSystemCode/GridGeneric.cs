using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

//if using 3d need to change the vector 3 

//Grid Generic passing an object 
public class GridGeneric<GType>
{

    public event EventHandler<OnGridObjectChangedEArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEArgs : EventArgs
    {
        public int x;
        public int y;
    }
    private int width;
    private int height;
    private float blockSize;
    private Vector3 originPos;
    private GType[,] gridArray;
    private TextMesh[,] debugTextArray;
    //Func passing a function ---()=> new object
    public GridGeneric(int width, int height, float blockSize, Vector3 originPos, Func<GridGeneric<GType>, int, int, GType> gTypeCreate)
    {
        this.width = width;
        this.height = height;
        this.blockSize = blockSize;
        this.originPos = originPos;
        gridArray = new GType[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = gTypeCreate(this, x, y);
            }
        }


     /* debugTextArray = new TextMesh[width, height];
        //lambda expression this is set on start add event 
        OnGridObjectChanged += (object sender, OnGridObjectChangedEArgs args) =>
            {
                debugTextArray[args.x, args.y].text = gridArray[args.x, args.y]?.ToString();
            };
      */

    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * blockSize + originPos;
    }
    public void ShowDebug(float showTime,int fontSize, Color color)
    {
        
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
             //   debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(blockSize, 0, blockSize) * 0.5f, fontSize, color, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.green, showTime);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.green, showTime);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.green, showTime);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.green, showTime);

    }



    // 赋值给multidimantion array 
    public void SetObject(int x, int y, GType Object)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = Object;
            OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEArgs { x = x, y = y });
            //debugTextArray[x, y].text = gridArray[x, y].ToString();
        }

    }

    public void TriggerGridObjectChanged(int x, int y) {

        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEArgs { x = x, y = y });
    }

    //运用out 把x y 两个值带出
    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - originPos).x / blockSize);
        y = Mathf.FloorToInt((worldPos - originPos).z / blockSize);
    }
    //生成两个函数 接收out 
    public void SetObject(Vector3 worldPos, GType Object)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetObject(x, y, Object);
    }
   
    public GType GetObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else { return default(GType); }
    }
    public GType GetObject(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetObject(x, y);
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x, 0,y) * blockSize + originPos;
    }
    public float GetBlockSize()
    {
        return blockSize;
    }

}