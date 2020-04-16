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
    public float maxVelocity = 60f;

    float m_DeltaMovement;

    const float WHEELDISTANCE = 1.02f;

 
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Velocity = Mathf.Min(maxVelocity, m_Velocity + deltaTime * acceleration);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Velocity = Mathf.Max(0, m_Velocity - deltaTime * deceleration);
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
