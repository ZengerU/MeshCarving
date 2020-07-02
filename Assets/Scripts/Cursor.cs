using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private float speed = .05f;
    void Update()
    {
        Vector3 tmp = transform.localPosition;
        tmp.x += (Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed);
        tmp.y += (Input.GetAxisRaw("Vertical") * Time.deltaTime * speed);
        transform.localPosition = tmp;
    }
}
