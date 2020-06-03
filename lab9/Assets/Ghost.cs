using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    
    [SerializeField] Player m_Player;
    [SerializeField] UnityEngine.AI.NavMeshAgent m_Agent;

    
    

    // Update is called once per frame

    const float TRACKING_RANGE = 64f;

    void Update()
    {
        if((m_Player.transform.position - this.transform.position).sqrMagnitude < TRACKING_RANGE)
        {
            m_WanderTimer = -1;
            
            if (m_Player.IsInvincible)
            {
                ///// run away from player
                if(m_EscapeTimer == -1 || m_EscapeTimer > ESCAPE_INTERVAL)
                {
                    Escape();
                }
                else
                {
                    m_EscapeTimer += Time.deltaTime;
                }
                

            }else
            {
                m_Agent.SetDestination(m_Player.transform.position);
            }

            m_WanderTimer = -1;
            //m_Agent.SetDestination(m_Player.transform.position);
            
        }
        else
        {
            if (m_WanderTimer == -1 || m_WanderTimer > WANDER_INTERVAL)
            {
                Wander();
            }
            else
            {
                m_WanderTimer += Time.deltaTime;
            }
            m_EscapeTimer = -1;
        }

    }

    const float WANDER_INTERVAL = 2f;
    const float WANDER_DISTANCE = 7f;
    float m_WanderTimer = -1;
    

    void Wander()//閒晃
    {
        UnityEngine.AI.NavMeshHit navHit;

        Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * WANDER_DISTANCE;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out navHit, WANDER_DISTANCE, -1))
        {
            m_Agent.SetDestination(navHit.position);
            m_WanderTimer = 0;
        }else
        {
            m_WanderTimer = -1;
        }
    }

    const float ESCAPE_INTERVAL = 1f;
    const float ESCAPE_DISTANCE = 7f;
    float m_EscapeTimer = -1;


    void Escape()
    {
        UnityEngine.AI.NavMeshHit navHit;

        Vector3 randomPoint = this.transform.position + (this.transform.position - m_Player.transform.position).normalized * ESCAPE_DISTANCE;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out navHit, ESCAPE_DISTANCE, -1)) 
        {
            m_Agent.SetDestination(navHit.position);
            m_EscapeTimer = 0;
        }else
        {
            m_EscapeTimer = -1;
        }
    }

}
