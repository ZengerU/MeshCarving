using UnityEngine;


public class DisplayVertices : MonoBehaviour
{
    private Vector3[] vertices;
    [SerializeField]
    private GameObject vertexPrefab = null;
    [SerializeField]
    private Transform vertexParent = null;

    private void GenerateVertices()
    {
       foreach(Transform child in vertexParent) Destroy(child.gameObject);
       vertices = GetComponent<MeshFilter>().mesh.vertices;
        Color color;
        for(var i = 0; i < vertices.Length; i++)
        {
            color = new Color(i, i, i, 1.0f);
            var sphere = Instantiate(vertexPrefab, vertexParent.TransformPoint(vertices[i]), Quaternion.identity, vertexParent);
            sphere.GetComponent<MeshRenderer>().material.color = color;
        }
    }
    private void Start()
    {
        GenerateVertices();
    }
}
