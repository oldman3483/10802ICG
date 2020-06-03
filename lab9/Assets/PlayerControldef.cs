using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControldef : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    [SerializeField] GameObject m_PillPrefab;

    const float PILL_INTERVAL = 10f;
    const float PILL_DISTANCE = 10f;
    float m_PillTimer = 0;
    
    void UpdatePill()
    {
        if(m_PillTimer > PILL_INTERVAL)
        {
            NavMeshHit navHit;

            while(!NavMesh.SamplePosition(
                Random.insideUnitSphere*Random.Range(0f, PILL_DISTANCE), out navHit, 5, -1)) { }

            Vector3 position = navHit.position;
            position.y += 1.25f;
            GameObject.Instantiate(m_PillPrefab).transform.position = position;

            m_PillTimer = 0;

        }
        else
        {
            m_PillTimer += Time.deltaTime;
        }


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        UpdatePill();
    }
}
