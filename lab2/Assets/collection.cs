using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> myInts = new List<int>();

        myInts.Add(1);
        myInts.Add(2);

        myInts.Insert(2, 5);
        myInts.Insert(2, 7);
        myInts.Insert(0, 9);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


