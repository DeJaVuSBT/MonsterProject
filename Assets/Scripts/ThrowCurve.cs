using UnityEngine;
using System.Collections;

public class ThrowCurve : MonoBehaviour
{
    public Transform attacker;     // 起始点
    private Vector3 middlePoint;   // 中间点
    public float heightOffSet;
    public Transform playerPos;      // 终止点
    public Transform stone;       // 要移动的物体

    private float ticker = 0.0f;
    [SerializeField]
    private float attackTime = 2.0f;    // 假设要用2秒飞到目标点

    public static Vector3 GetCurvePoint(Vector3 _p0, Vector3 _p1, Vector3 _p2, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        float x = ((1 - t) * (1 - t)) * _p0.x + 2 * t * (1 - t) * _p1.x + t * t * _p2.x;
        float y = ((1 - t) * (1 - t)) * _p0.y + 2 * t * (1 - t) * _p1.y + t * t * _p2.y;
        float z = ((1 - t) * (1 - t)) * _p0.z + 2 * t * (1 - t) * _p1.z + t * t * _p2.z;
        Vector3 pos = new Vector3(x, y, z);
        return pos;
    }

    private void Update()
    {
        ticker += Time.deltaTime;
        float t = ticker / attackTime;  // 这里是计算当前已用时间占计划用时间的百分比，当作增量t
        t = Mathf.Clamp(t, 0.0f, 1.0f); 
        Vector3 p1 = attacker.position;
        Vector3 p3 = playerPos.position; 
        middlePoint = (p3 - p1) / 2+p1+new Vector3(0, heightOffSet, 0); 
        Vector3 p2 = middlePoint; 


        Vector3 currPos = GetCurvePoint(p1, p2, p3, t);
        stone.position = currPos;

        if (t == 1.0f)
        {
            // 到达目标点

        }
    }
}
