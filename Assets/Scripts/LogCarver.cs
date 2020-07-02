using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCarver : MonoBehaviour
{
    private Vector3[] vertices;
    [SerializeField]
    private InputField startInput = null, endInput = null, multInput = null;
    private MeshFilter filter;
    public void CarveLog(float heightStart, float heightEnd, float multiplier)
    {
        float max = 0;
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            if(vertices[i].y >= heightStart && vertices[i].y < heightEnd)
            {
                vertices[i].x *= multiplier;
                vertices[i].z *= multiplier;
            }
            max = Mathf.Max(max, vertices[i].y);
        }
        filter.mesh.vertices = vertices;
        Destroy(GetComponent<MeshCollider>()); 
        var collider = gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = filter.mesh;

    }
    public void CarveLog()
    {
        CarveLog(float.Parse(startInput.text), float.Parse(endInput.text), float.Parse(multInput.text));
    }
    public void CarveLog(Vector3 startPos, Vector3 endPos, float multiplier)
    {
        CarveLog(transform.InverseTransformPoint(startPos).y, transform.InverseTransformPoint(endPos).y, multiplier);
    }
    private void Start()
    {
        filter = GetComponent<MeshFilter>();
        CarveLog(1, 5, .8f);
    }
}
