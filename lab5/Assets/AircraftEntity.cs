using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftEntity : MonoBehaviour
{

    float m_Velocity = 10;
    const float PLANE_ACCELERATION = 10f;
    const float PLANE_DECELERATION = 10f;
    const float PLANE_MIN_VELOCITY = 10f;
    const float PLANE_MAX_VELOCITY = 10f;

    float m_RollVelocity;
    const float ROLL_ACCELERATION = 100f;
    const float ROLL_DECELERATION = 100f;
    const float MAX_ROLL_VELOCITY = 100f;

    float m_PitchVelocity;
    const float PITCH_ACCELERATION = 100f;
    const float PITCH_DECELERATION = 100f;
    const float MAX_PITCH_VELOCITY = 100f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField] Transform m_PlaneBodyTrans;
    private void FixedUpdate()
    {
        this.transform.Translate(m_Velocity * Time.fixedDeltaTime * Vector3.right);
        this.transform.Rotate(m_RollVelocity * Time.fixedDeltaTime, 0, 0);
        this.transform.Rotate(0, 0, m_PitchVelocity * Time.fixedDeltaTime);

        m_PlaneBodyTrans.localEulerAngles = new Vector3(
            m_RollVelocity / MAX_ROLL_VELOCITY * 30f, 0f,
            m_PitchVelocity / MAX_PITCH_VELOCITY * 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Velocity = Mathf.Max(m_Velocity - PLANE_DECELERATION * Time.deltaTime, PLANE_MIN_VELOCITY);
        }else if (Input.GetKey(KeyCode.Space))
        {
            m_Velocity = Mathf.Min(m_Velocity + PLANE_ACCELERATION * Time.deltaTime, PLANE_MAX_VELOCITY);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            m_RollVelocity = Mathf.Min(MAX_ROLL_VELOCITY, m_RollVelocity + ROLL_ACCELERATION * Time.deltaTime);
        }else if(Input.GetKey(KeyCode.RightArrow))
        {
            m_RollVelocity = Mathf.Max(-MAX_ROLL_VELOCITY, m_RollVelocity - ROLL_ACCELERATION * Time.deltaTime);
        }
        else
        {
            m_RollVelocity = m_RollVelocity > 0 ?
                Mathf.Max(0, m_RollVelocity - ROLL_ACCELERATION * Time.deltaTime):
                Mathf.Min(0, m_RollVelocity + ROLL_ACCELERATION * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_PitchVelocity = Mathf.Max(-MAX_PITCH_VELOCITY, m_PitchVelocity - PITCH_ACCELERATION * Time.deltaTime);
        }else if(Input.GetKey(KeyCode.DownArrow))
        {
            m_PitchVelocity = Mathf.Min(MAX_PITCH_VELOCITY, m_PitchVelocity + PITCH_ACCELERATION * Time.deltaTime);
        }
        else
        {
            m_PitchVelocity = m_PitchVelocity > 0 ?
                Mathf.Max(0, m_PitchVelocity - PITCH_ACCELERATION * Time.deltaTime) :
                Mathf.Min(0, m_PitchVelocity + PITCH_ACCELERATION * Time.deltaTime);
        }
    }
}
