using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCar : MonoBehaviour
{
    // Start is called before the first frame update

    public CarEntity car1 = new CarEntity();

    public int chooseCar = 0; // 0 is no choose, 1 is yellow car. 2is orange car

    void Start()
    {
        Debug.Log("Please choose the Car \n Press 'A' to choose Yellow car, 'B' to orange car.");   
    }

    // Update is called once per frame
    void Update()
    {
       CarEntity car1 = new CarEntity();
        //CarEntity car2 = new CarEntity();
    }
}
