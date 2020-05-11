using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalStation : MonoBehaviour
{
    // Start is called before the first frame update
    float m_HorizontalAngle = 0;
    float m_VerticalAngle = 0;
    const float ROTATE_SPEED = 40;


    [SerializeField] GameObject m_base;
    [SerializeField] GameObject m_telescope;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            m_HorizontalAngle += ROTATE_SPEED * Time.deltaTime;


        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_HorizontalAngle += -ROTATE_SPEED * Time.deltaTime;
        }




        m_base.transform.localEulerAngles = new Vector3(0, 0, m_HorizontalAngle);

        if (Input.GetKey(KeyCode.UpArrow))
        {

            m_VerticalAngle += ROTATE_SPEED * Time.deltaTime;


        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_VerticalAngle += -ROTATE_SPEED * Time.deltaTime;
        }




        m_telescope.transform.localEulerAngles = new Vector3(m_VerticalAngle, 0, 0);
    }
}
