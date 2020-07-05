using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
