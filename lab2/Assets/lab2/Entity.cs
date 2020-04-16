using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    protected string m_Name;
    public string Name {get { return m_Name; } }

    public Entity (string name)
    {
        m_Name = name;
    }
    public virtual void Inspect()
    {
        Debug.Log("Hmm...nothing special.");
    }
    public virtual void Interact (EscapeGame game)
    {
        Debug.Log("Nothing happened");
    }
}