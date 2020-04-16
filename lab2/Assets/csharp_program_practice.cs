using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csharp_program_practice : MonoBehaviour
{

    public enum Volume
    {
        Low,
        Medium,
        High
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int test = 3;

        string[] names = { "C", "J", "M", "R" };
        string instr = "back";
        Volume myVolume = Volume.Medium;

        switch (test) {
            case 1:
                switch (instr)
                {
                    case "back":
                        Debug.Log("it's back");
                        break;
                    case "for":
                        Debug.Log("it's front");
                        break;
                    case "turn left":
                    default:
                        Debug.Log("default");
                        break;
                }
                break;
            case 2:
                foreach(string person in names)
                {
                    Debug.Log("names" + person);
                }
                break;
            case 3:
                switch (myVolume)
                {
                    case Volume.Low:
                        Debug.Log(Volume.Low);
                        break;
                    case Volume.Medium:
                        Debug.Log(Volume.Medium);
                        break;
                    case Volume.High:
                        Debug.Log(Volume.Medium);
                        break;
                }
                break;
        }
    }
}

public class Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    { 
        this.x = x;
        this.y = y;
    }
}
