using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntity : MonoBehaviour
{
    public GameObject wheelFrontRight;
    public GameObject wheelFrontLeft;
    public GameObject wheelBackRight;
    public GameObject wheelBackLeft;

    float m_FrontWheelAngle = 0;
    const float WHEEL_ANGLE_LIMIT = 40f;
    public float turnAngularVelocity = 20f;

    float m_Velocity=0;
    public float Velocity { get { return m_Velocity; } }
    public float acceleration = 3f;
    public float deceleration = 10f;
    public float maxVelocity = 20f;


    public int gear = 0; // 0=P 1=D 2=R
    public bool show_v = true;

    float m_DeltaMovement;

    const float WHEELDISTANCE = 1.02f;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Please use 'R', 'D', 'P'to Choose the Gear.\n" +
            "It's gear P now.");
    }

    void UpdateWheels()
    {
        Vector3 localEularAngles = new Vector3(0f, 0f, m_FrontWheelAngle);

        wheelFrontLeft.transform.localEulerAngles = localEularAngles;
        wheelFrontRight.transform.localEulerAngles = localEularAngles;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetColor();
            Debug.Log("Gear P.");
            gear = 0;
            m_Velocity = 0;
            Debug.Log("Velocity is 0.");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ResetColor();
            Debug.Log("Gear D");
            if (gear != 1)
            {
                m_Velocity = 0;
            }
            gear = 1;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeColor(Color.gray);
            Debug.Log("Gear R");

            if (gear != 2)
            {
                m_Velocity = 0;
            }
            gear = 2;
        }


        if (gear == 1)
        {
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_Velocity = Mathf.Min(maxVelocity, m_Velocity + deltaTime * acceleration);


                if (show_v)
                {
                    Debug.Log("Velocity is " + m_Velocity);
                }
                if (m_Velocity == maxVelocity)
                {
                    show_v = false;
                }
                else
                {
                    show_v = true;
                }

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_Velocity = Mathf.Max(0, m_Velocity - deltaTime * deceleration);

                if (show_v)
                {
                    Debug.Log("Velocity is " + m_Velocity);
                }

                if (m_Velocity == 0)
                {
                    show_v = false;
                }
                else
                {
                    show_v = true;
                }

            }
        }

        if (gear == 2)
        {
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_Velocity = Mathf.Min(0, m_Velocity + deltaTime * deceleration);
                if (show_v)
                {
                    Debug.Log("Velocity is " + -m_Velocity);
                }

                if (m_Velocity == 0)
                {
                    show_v = false;
                }
                else
                {
                    show_v = true;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_Velocity = Mathf.Max(-maxVelocity, m_Velocity - deltaTime * acceleration);
                if (show_v)
                {
                    Debug.Log("Velocity is " + -m_Velocity);
                }
                if (m_Velocity == -maxVelocity)
                {
                    show_v = false;
                }
                else
                {
                    show_v = true;
                }

            }
        }

        m_DeltaMovement = m_Velocity * deltaTime;
        


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle = m_FrontWheelAngle + deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle = m_FrontWheelAngle - deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        float bodyAngleDelta = 1 / WHEELDISTANCE * Mathf.Tan(Mathf.Deg2Rad *
            m_FrontWheelAngle) *
            m_DeltaMovement * Mathf.Rad2Deg;

        this.transform.Rotate(0f, 0f, bodyAngleDelta );
        this.transform.Translate(Vector3.right * m_DeltaMovement);
    }


    [SerializeField] SpriteRenderer[] m_Renderders = new SpriteRenderer[5];
    void ChangeColor(Color color)
    {
        foreach (SpriteRenderer r in m_Renderders)
        {
            r.color = color;
        }
    }
    private void ResetColor()
    {
        ChangeColor(Color.white);
    }

    void Stop()
    {
        m_Velocity = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stop();
        ChangeColor(Color.red);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Stop();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        ResetColor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint>();
        if (checkPoint != null)
        {
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 1f);
        }
    }

}
