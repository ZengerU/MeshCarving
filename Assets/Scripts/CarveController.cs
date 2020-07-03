using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarveController : MonoBehaviour
{
    [SerializeField]
    private float mult = .99f, timerStart =.5f;
    private Bounds area;
    [HideInInspector]
    public int collidingObjectCount = 0;
    private float timer = .5f;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        
        timer = timerStart;
        area = GetComponent<MeshRenderer>().bounds;
        other.GetComponent<LogCarver>().CarveLog(area, mult, Mathf.Abs(area.center.z) - area.extents.z);
        print("enter trigger");
        collidingObjectCount++;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
    }
}
