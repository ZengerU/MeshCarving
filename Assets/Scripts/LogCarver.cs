using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCarver : MonoBehaviour
{
    private Vector3[] vertices;
    [SerializeField]
    private InputField startInput, endInput, multInput;
    public void carveLog(float heightStart, float heightEnd, float multiplier)
    {
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            if(vertices[i].y >= heightStart && vertices[i].y < heightEnd)
            {
                vertices[i].x *= multiplier;
                vertices[i].z *= multiplier;
            }
        }
        GetComponent<MeshFilter>().mesh.vertices = vertices;
    }
    public void carveLog()
    {
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y >= float.Parse(startInput.text) && vertices[i].y < float.Parse(endInput.text))
            {
                vertices[i].x *= float.Parse(multInput.text);
                vertices[i].z *= float.Parse(multInput.text);
            }
        }
        GetComponent<MeshFilter>().mesh.vertices = vertices;
    }
}
