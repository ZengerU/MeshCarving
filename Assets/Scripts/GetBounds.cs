﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(GetComponent<MeshRenderer>().bounds);        
    }
}
