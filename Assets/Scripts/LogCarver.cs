using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCarver : MonoBehaviour
{
    static private float minDistance = .1f;
    private Vector3 minPointWorld = Vector3.one * 5000, maxPointWorld = Vector3.one * -5000, minPointLocal, maxPointLocal;
    private float min = float.MaxValue, max = float.MinValue;
    private Vector3[] vertices;
    private MeshFilter filter;
    //private Cursor curs;
    public void CarveLog(Bounds bound, float multiplier, float dist)
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 tmp = transform.TransformPoint(vertices[i]);
            bool checkx = tmp.x >= bound.center.x - bound.extents.x && tmp.x <= bound.center.x + bound.extents.x;
            float distance = Vector2.Distance(new Vector2(tmp.z, tmp.y), Vector2.zero);
            if(distance < minDistance)
            {
                cutLog(bound.center.x - bound.extents.x, bound.center.x + bound.extents.x);
                break;
            }
            if(distance >= dist && checkx)
            {
                vertices[i].x *= multiplier;
                vertices[i].z *= multiplier;
            }

        }
        filter.sharedMesh.vertices = vertices;
        Destroy(GetComponent<MeshCollider>());
        var collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = filter.mesh;
    }
    private void cutLog(float left, float right)
    {
        //Type 0 = single log, 1 = 2 logs, left neighbor, 2 = 2 logs, right neighbor

        int type = max < right ? 2 : (min > left ? 1 : 0);

        //TODO : 0 > duplicate this log, morph both. 1|2 > morph this and neighbor.
        switch(type)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
    private void morphLog()
    {

    }
    [SerializeField]
    private GameObject prefab;
    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        vertices = filter.sharedMesh.vertices;

        //TODO: Refactor this.
        List<Vector3> tmpList = new List<Vector3>();
        foreach(var child in vertices)
        {
            Vector3 tmp = transform.TransformPoint(child);
            if(Mathf.Abs(tmp.y) <= 0.01f && Mathf.Abs(tmp.z) <= 0.01f)
            {
                tmpList.Add(tmp);
            }
        }
        minPointWorld = Vector3.Min(tmpList[0], tmpList[1]);
        minPointLocal = transform.InverseTransformPoint(minPointWorld);
        maxPointWorld = Vector3.Max(tmpList[0], tmpList[1]);
        maxPointWorld = transform.InverseTransformPoint(maxPointWorld);
        Instantiate(prefab, minPointWorld, Quaternion.identity, transform);
        Instantiate(prefab, maxPointWorld, Quaternion.identity, transform);
        print($"{minPointWorld} and {maxPointWorld}");
    }
}
