using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarveController : MonoBehaviour
{
    private LogCarver carv;
    [SerializeField]
    private float mult = .1f;
    private void OnTriggerStay(Collider other)
    {
        carv.CarveLog(transform.TransformPoint(new Vector3(-0.5f, 0, 0)), transform.TransformPoint(new Vector3(0.5f, 0, 0)), mult);
    }
    private void Start()
    {
        carv = (LogCarver)FindObjectOfType(typeof(LogCarver));
        print("test");
    }
}
