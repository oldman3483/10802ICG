using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trolley : MonoBehaviour
{
    const float MOVE_SPEED = 2f;
    const float ATTATCH_DISTANCE = 3f;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        float m_position_y = gameObject.transform.localPosition.y;
        if (m_position_y >= -17.2 && m_position_y <= 0)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
            }
        }else if (m_position_y <= -17.2f)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
            }
        }else if(m_position_y >= 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
            }
        }
    }

   

   
}
