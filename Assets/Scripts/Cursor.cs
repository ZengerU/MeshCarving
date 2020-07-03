using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private float speed = .05f;
    public bool carving = false;
    void Update()
    {
        Vector3 tmp = transform.position;
        tmp.x += (Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed);
        tmp.z += (Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
        transform.position = tmp;
    }
}
