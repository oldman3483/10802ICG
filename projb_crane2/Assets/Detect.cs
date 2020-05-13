using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] MeshRenderer m_renderer;
    void ChangeColor(Color color)
    {
        m_renderer.material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float pos_x = gameObject.transform.localPosition.x;
        float pos_z = gameObject.transform.localPosition.y;


        if (pos_x <= 9.11 && pos_x >= -8.78 && pos_z > 7.92 && pos_z <= 23.96)
        {
            ChangeColor(Color.green);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        float pos_x = gameObject.transform.localPosition.x;
        float pos_z = gameObject.transform.localPosition.y;
        if (pos_x <= 9.11 && pos_x >= -8.78 && pos_z > 7.92 && pos_z <= 23.96)
        {
            
            ChangeColor(Color.green);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ChangeColor(Color.blue);

    }
}
