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
    private GameObject verticePrefab;
    [SerializeField]
    private Transform verticeParent;
    [SerializeField]
    private Button generateButton;

    public void generateVertices()
    {
       foreach(Transform child in verticeParent)
        {
            Destroy(child.gameObject);
        }
        vertices = GetComponent<MeshFilter>().mesh.vertices;
        Color color = new Color(1, 0, 1, 1.0f);
        foreach(var vert in vertices)
        {
            color = new Color(1, vert.y / 4, 1, 1.0f);
            GameObject sphere = Instantiate(verticePrefab, vert, Quaternion.identity, verticeParent);
            sphere.GetComponent<MeshRenderer>().material.color = color;
        }
    }
    public void showHideVertices()
    {
        verticeParent.gameObject.SetActive(!verticeParent.gameObject.activeSelf);
        generateButton.interactable = verticeParent.gameObject.activeSelf;
    }
}
