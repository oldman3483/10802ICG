﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitivesGenerator : MonoBehaviour
{

    [SerializeField] GameObject m_CubePrefab;

    [SerializeField] GameObject m_SpherePrefab;
    // Start is called before the first frame update
    void Start()
    {
        GeneratePrimitives(m_CubePrefab, 20);
        GeneratePrimitives(m_SpherePrefab, 20);
    }

    [SerializeField] Vector2 m_Dimension = new Vector2(5, 5);
    void GeneratePrimitives(GameObject primitive, int count)
    {
        for(int i=0; i<count; i++)
        {
            var primitiveIns = GameObject.Instantiate(primitive); //產出primitive 這個gameObject
            primitiveIns.transform.localPosition = new Vector3(
                Random.Range(-m_Dimension.x, m_Dimension.x), 
                3f,
                Random.Range(-m_Dimension.y, m_Dimension.y)
            );
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
