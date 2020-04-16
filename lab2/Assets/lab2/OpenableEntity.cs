using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenableEntity : Entity
{
    // Start is called before the first frame update
    string m_KeyIdentifier;

    public OpenableEntity(string name, string keyIdentifier):
        base(name)
    {
        m_KeyIdentifier = keyIdentifier;
    }

    protected virtual void Open(EscapeGame game)
    {
        Debug.Log("Succeed to open the item, but nothing happened.");
    }

    public override void Inspect()
    {
        if (string.IsNullOrEmpty(m_KeyIdentifier))
        {
            base.Inspect();
            return;
        }
        Debug.Log("Use the right key to open this.");
    }

    public override void Interact(EscapeGame game)
    {
        if (string.IsNullOrEmpty(m_KeyIdentifier))
        {
            Open(game);
            return;
        }
        KeyEntity key = game.TakenEntity as KeyEntity;
        if (key != null)
        {
            if(key.Identifier == m_KeyIdentifier)
            {
                Open(game);
            }
            else
            {
                Debug.Log(string.Format("This is item cannot be opened by the key " + "<color=white>{0}</color>", key.Name));
            }
        }
        else
        {
            Debug.Log("You need a key to open it.");
        }
    }
    
}
