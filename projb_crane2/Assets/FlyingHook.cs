﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingHook : MonoBehaviour
{

    const float MOVE_SPEED = 2f;
    const float ATTATCH_DISTANCE = 3f;
    GameObject m_DetectedObject;
    ConfigurableJoint m_JointForObject;
    [SerializeField] GameObject m_Jointbody;
    [SerializeField] LineRenderer m_Cable;
    [SerializeField] Transform m_hook;
    [SerializeField] Transform m_trolley;


    // Start is called before the first frame update
    void Start()
    {
       



    }

    // Update is called once per frame
    void Update()
    {
        float pos_z = gameObject.transform.localPosition.z;

        if(pos_z <= 0 && pos_z >= -11.8)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.Translate(0, 0, MOVE_SPEED * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                this.transform.Translate(0, 0, -MOVE_SPEED * Time.deltaTime);
            }
        }
        else if(pos_z >= 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                this.transform.Translate(0, 0, -MOVE_SPEED * Time.deltaTime);
            }
        }
        else if(pos_z <= -11.8)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.Translate(0, 0, MOVE_SPEED * Time.deltaTime);
            }
        }


 

        if(m_JointForObject == null)
        {
            DetectObjects();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttachOrDetatch();
        }

        UpdateCable();
    }

    void UpdateCable()
    {
        m_Cable.enabled = m_JointForObject != null;
        
        if (m_Cable.enabled)
        {
            m_Cable.SetPosition(0, this.transform.position); // 本身東西的位置
            //m_Cable.SetPosition(1, m_JointForObject.connectedBody.transform.position); // 吊的東西的位置

            var connectedBodyTransfor = m_JointForObject.connectedBody.transform;
            m_Cable.SetPosition(1, connectedBodyTransfor.TransformPoint(m_JointForObject.connectedAnchor));    
        }
    }

    void AttachOrDetatch()
    {
        if(m_JointForObject == null)
        {
            if(m_DetectedObject != null)
            {
                var joint = m_Jointbody.AddComponent<ConfigurableJoint>();
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;

                joint.angularXMotion = ConfigurableJointMotion.Free;
                joint.angularYMotion = ConfigurableJointMotion.Free;
                joint.angularZMotion = ConfigurableJointMotion.Free;

                var limit = joint.linearLimit;
                limit.limit = 4;

                joint.linearLimit = limit;

                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = new Vector3(0f, 0.5f, 0f); //接的地方，吊的位置 吊在物體的上方0.5
                joint.anchor = new Vector3(0f, 0f, 0f);

                joint.connectedBody = m_DetectedObject.GetComponent<Rigidbody>(); //接的物體 

                m_JointForObject = joint;

                m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                m_DetectedObject = null;

            }
        }
        else
        {
            m_JointForObject.connectedBody.GetComponent<MeshRenderer>().material.color = Color.blue;
            GameObject.Destroy(m_JointForObject);
            m_JointForObject = null;
        }
    }

    [SerializeField] MeshRenderer m_buliding;
    void DetectObjects()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ATTATCH_DISTANCE)) //Physics.Raycast(ray, out hit) 會回傳bool 代表ray射出後的hit有沒有打到東西 
        {
            if(m_DetectedObject == hit.collider.gameObject)
            {
                return;
            }
            RecoverClickedObject();
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();

            if (renderer != null && renderer != m_buliding)
            {
                renderer.material.color = Color.yellow;
            }
            m_DetectedObject = hit.collider.gameObject;
        }
        else
        {
            
            RecoverClickedObject();
        }

    }
    void RecoverClickedObject()
    {
        if (m_DetectedObject != null && m_DetectedObject.GetComponent<MeshRenderer>() != m_buliding)
        {
            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.blue;
            m_DetectedObject = null;
        }

    }
}
