using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class totalStation : MonoBehaviour
{
    
    /*
    [SerializeField] GameUI m_GameUI;
    #region Events
    public delegate void ShowCalculateResultEvent(string message);
    //public delegate void ShowCalculateResultBlackEvent(GameObject g);

    public event ShowCalculateResultEvent OnMessageAdded = (m) => { };
    //public event ShowCalculateResultBlackEvent OnResultBlack = (g) => { };


    #endregion
    */


    // Start is called before the first frame update
    float m_HorizontalAngle = 0;
    float m_VerticalAngle = 0;
    const float ROTATE_SPEED = 40f;
    const float ATTATCH_DISTANCE = 30f;
    float distance = 0;
    float OutAngle1 = 0;
    float OutAngle2 = 0;
    bool show = false;


    [SerializeField] GameObject m_ResultBlack;
    [SerializeField] GameObject m_base;
    [SerializeField] GameObject m_shootbase;
    [SerializeField] GameObject m_shootdir;
    [SerializeField] GameObject m_scopebase;
    [SerializeField] GameObject[] m_mirror = new GameObject[2];
    [SerializeField] GameObject m_rotatebase;
    [SerializeField] GameObject m_rotatetarget;
    GameObject m_DetectObject;
    [SerializeField] GameObject[] m_text = new GameObject[2];
    [SerializeField] Camera m_Player;


    bool mode = false; // the mode of the total station true means do not need mirror
    bool first = true;


    // Update is called once per frame
    void Update()
    {
        //DetectObject();
        m_ResultBlack.SetActive(show);
        float OutputAngle = CalculateAngle();
        m_text[1].GetComponent<Text>().text = "HORIZONTAL ANGLE IS : " + OutputAngle;
     

        if (Input.GetKey(KeyCode.LeftAlt))//set to zero
        {
            //press alt => display the UI of angle 
            SetAngleZero();
            
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_HorizontalAngle += -ROTATE_SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_HorizontalAngle += ROTATE_SPEED * Time.deltaTime;
        }
        
        m_base.transform.localEulerAngles = new Vector3(0, 0, m_HorizontalAngle);
        OutAngle2 =  m_base.transform.localEulerAngles.z%360;
        //Debug.Log("angle2  " + OutAngle2);


        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_VerticalAngle += -ROTATE_SPEED * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_VerticalAngle += ROTATE_SPEED * Time.deltaTime;
        }
        //var rotateQuaternion = Quaternion.Euler(m_VerticalAngle, 0f, 0f);
        m_scopebase.transform.localEulerAngles = new Vector3(m_VerticalAngle, 0, 0);


        /*
        m_rotatebase.transform.localEulerAngles = new Vector3(m_VerticalAngle, 0, 0);
        Debug.Log(m_rotatetarget.transform.position.z);
        //m_scopebase.transform.rotation = rotateQuaternion;
        
        //Debug.Log(m_rotatetarget.transform.position);

        Vector3 difVec = m_rotatetarget.transform.position - m_rotatebase.transform.position;
        Vector3 xzProjection = new Vector3(difVec.x, 0f, difVec.z);

        float xzProjectionLength = xzProjection.magnitude;
        var targetQ = Quaternion.FromToRotation(
            //new Vector3(-xzProjectionLength, 0f, 0f),
            //new Vector3(-xzProjectionLength, difVec.y, 0f)
            new Vector3(0f, 0f, xzProjectionLength),
            new Vector3(0, difVec.y, xzProjectionLength)
            );
        //m_rotatebase.transform.position,
        //m_rotatetarget.transform.position);


        m_scopebase.transform.localRotation = Quaternion.RotateTowards(
            m_scopebase.transform.localRotation,
            targetQ,
            360f * Time.deltaTime);
        Debug.DrawLine(m_scopebase.transform.position, m_rotatetarget.transform.position, Color.red);

        //m_scopebase.transform.localRotation = targetQ;
        */

        if (Input.GetKey(KeyCode.M))
        {
            mode = true;
            Debug.Log("Mode is no need mirror");
        }
        if (Input.GetKey(KeyCode.N))
        {
            mode = false;
            Debug.Log("Mode is need mirror");
        }


        if (Input.GetKey(KeyCode.Space))
        {

            show = true;
            DetectObject();


            if (first)
            {
                SetAngleZero();
                first = false;
            }


            Debug.Log("telescope pos is " + m_shootbase.transform.position);
            if (mode)
            {
                CalculateDistance();
                Debug.Log("The distance is " + distance);

            }
            else if (IsMirror())
            {
                CalculateDistance();
                Debug.Log("The distance is " + distance);
            }
            else
            {
                Debug.Log("No object is shoot!");

            }
            m_text[0].GetComponent<Text>().text = "DISTANCE IS : " + distance;
            
        }

        
        
    }


    void DetectObject()
    {
        Ray ray = new Ray(m_shootbase.transform.position, Vector3.forward); // down need to be changed to straight line
        RaycastHit RaycastResult;

        Vector3 dir = m_shootdir.transform.position - m_shootbase.transform.position;
        if (Physics.Raycast(m_shootbase.transform.position, dir , out RaycastResult, ATTATCH_DISTANCE))
        {
            Debug.DrawLine(m_shootbase.transform.position, RaycastResult.point, Color.red);
            if(m_DetectObject == RaycastResult.collider.gameObject)
            {
                return;
            }
            RecoverDectectObject();

            MeshRenderer meshRenderer = RaycastResult.collider.gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material.color = Color.yellow;
                m_DetectObject = RaycastResult.collider.gameObject;
            }
        }
        else
        {
            RecoverDectectObject();
        }

    }

    void RecoverDectectObject()
    {
        if (m_DetectObject != null)
        {
            m_DetectObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectObject = null;
        }
    }

    void CalculateDistance()
    {
        if(m_DetectObject != null)
        {
            float x_detect = m_DetectObject.transform.position.x;
            float y_detect = m_DetectObject.transform.position.y;
            float z_detect = m_DetectObject.transform.position.z;
            float x_this = m_scopebase.transform.position.x;
            float y_this = m_scopebase.transform.position.y;
            float z_this = m_scopebase.transform.position.z;

            distance = Mathf.Sqrt(Mathf.Pow((x_detect - x_this), 2) + Mathf.Pow((y_detect - y_this), 2) + Mathf.Pow((z_detect - z_this), 2));

        }
    }

    bool IsMirror()
    {
        if(m_DetectObject != m_mirror[0] && m_DetectObject != m_mirror[1])
        {
            return false;
        }

        return true;
    }

    float CalculateAngle()
    {
        float angle = 0;
        //Debug.Log(m_base.transform.localEulerAngles.z);
        if((OutAngle2 - OutAngle1)<0)
        {
            angle = OutAngle2 + 360 - OutAngle1;
        }
        else
        {
            angle = OutAngle2 - OutAngle1;
            //Debug.Log("ANgle___  "+angle);
        }

        return angle;
    }
    void SetAngleZero()
    {
        OutAngle1 = m_base.transform.localEulerAngles.z;
        
    }
}
