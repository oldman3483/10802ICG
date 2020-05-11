using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jib : MonoBehaviour
{
    float m_HorizontalAngle = 0;
    const float ROTATE_SPEED = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {

            m_HorizontalAngle += ROTATE_SPEED * Time.deltaTime;


        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_HorizontalAngle += -ROTATE_SPEED * Time.deltaTime;
        }




        this.transform.localEulerAngles = new Vector3(0, m_HorizontalAngle, 0);
    }
}
