using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tripod : MonoBehaviour
{

    [SerializeField] GameObject m_pivot01;
    [SerializeField] GameObject m_pivot02;
    [SerializeField] GameObject m_pivot03;

    float m_OpenAngle01 = 0;
    float m_OpenAngle02 = 0;
    float m_OpenAngle03 = 0;

    const float OPEN_SPEED = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            m_OpenAngle01 += OPEN_SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.P))
        {
            m_OpenAngle01 += -OPEN_SPEED * Time.deltaTime;
        }
        m_pivot01.transform.localEulerAngles = new Vector3(m_OpenAngle01, 0, 0);

        if (Input.GetKey(KeyCode.U))
        {
            m_OpenAngle02 += OPEN_SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.I))
        {
            m_OpenAngle02 += -OPEN_SPEED * Time.deltaTime;
        }
        m_pivot02.transform.localEulerAngles = new Vector3(m_OpenAngle02, m_OpenAngle02, 240);

        if (Input.GetKey(KeyCode.Y))
        {
            m_OpenAngle03 += OPEN_SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.T))
        {
            m_OpenAngle03 += -OPEN_SPEED * Time.deltaTime;
        }
        m_pivot03.transform.localEulerAngles = new Vector3(0, m_OpenAngle03, 120);
    }
}
