using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEntity : MonoBehaviour
{
    [SerializeField] GameObject m_Target;
    [SerializeField] GameObject m_Base;
    [SerializeField] GameObject m_Gun;
    const float MAX_GUN_ROTATION_VELOCITY = 90f;
    const float MAX_BASE_ROTATION_VELOCITY = 360f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diffVec = m_Target.transform.position - this.transform.position;
        Vector3 xzProjection = new Vector3(diffVec.x,0, diffVec.z);

        var targetBaseQuaternion =
            Quaternion.FromToRotation(
                Vector3.left, xzProjection);
        //m_Base.transform.localRotation = targetBaseQuaternion;

        float xzProjectedLength = xzProjection.magnitude;

        var targerGunQuaterion =
            Quaternion.FromToRotation(
                new Vector3(-xzProjectedLength, 0f, 0f),
                new Vector3(-xzProjectedLength, diffVec.y, 0f));

        //m_Gun.transform.localRotation = targerGunQuaterion;


        m_Base.transform.localRotation = Quaternion.RotateTowards(
            m_Base.transform.rotation,
            targetBaseQuaternion,
            MAX_BASE_ROTATION_VELOCITY * Time.deltaTime) ; 



        

        m_Gun.transform.localRotation = Quaternion.RotateTowards(
            m_Gun.transform.localRotation,
            targerGunQuaterion,
            MAX_GUN_ROTATION_VELOCITY * Time.deltaTime);
    }
}
