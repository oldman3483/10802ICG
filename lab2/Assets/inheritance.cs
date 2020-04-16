using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inheritance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Parent
{
    string m_ParentString;
    public Parent()
    {
        Debug.Log("Parent Constructor");
    }
    public Parent(string mystring)
    {
        m_ParentString = mystring;
        Debug.Log(m_ParentString);

    }
    public void print()
    {
        Debug.Log("parent print");
    }
}
public class Child : Parent
{
    public Child():base("From Derived")
    {
        Debug.Log("Child Constructor");
    }
    public new void print()
    {
        base.print();
        Debug.Log("child print");
    }
}
