using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnStartGame(string SceneName)
    {
        Application.LoadLevel(SceneName); // load scene
    }

}
