using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVertices : MonoBehaviour
{
    private Vector3[] vertices;
    [SerializeField]
    private GameObject verticePrefab = null;
    [SerializeField]
    private Transform verticeParent = null;

    public void generateVertices()
    {
       foreach(Transform child in verticeParent)
        {
            Destroy(child.gameObject);
        }
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        Color color = new Color(1, 0, 1, 1.0f);
        for(int i = 0; i < vertices.Length; i++)
        {
            color = new Color(i, i, i, 1.0f);
            GameObject sphere = Instantiate(verticePrefab, verticeParent.TransformPoint(vertices[i]), Quaternion.identity, verticeParent);
            sphere.GetComponent<MeshRenderer>().material.color = color;
        }
    }
    private void Start()
    {
        generateVertices();
        //curs = GetComponent<Cursor>();
    }
}
