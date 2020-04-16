using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEntity : Entity
{
    string m_KeyIdentifier;
    public KeyEntity(string name, string keyIdentifier):
    base(name){
        m_KeyIdentifier = keyIdentifier;
    }

    public override void Inspect()
    {
        Debug.Log("A key for something. Maybe can be used later.");
    }
    public override void Interact(EscapeGame game)
    {
        game.Take(this);
    }

    public string Identifier
    {
        get
        {
            return m_KeyIdentifier;
        }
    }
}
