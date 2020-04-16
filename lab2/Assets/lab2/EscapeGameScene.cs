using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGameScene : MonoBehaviour
{
    EscapeGame m_Game;
    // Start is called before the first frame update
    void Start()
    {
        m_Game = new EscapeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            m_Game.SelectNext();
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Game.Inspect();
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            m_Game.Interact();
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            m_Game.Putback();
        }


    }

    
}
