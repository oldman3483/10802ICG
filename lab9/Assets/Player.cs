using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] bool m_Invincible = false;
    public bool IsInvincible { get { return m_Invincible; } }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_InvincibleTimer > INVINNIBLE_INTERVAL)
        {
            m_Invincible = false;
            this.GetComponent<MeshRenderer>().material.color =
                new Color(67f / 255f, 167f / 255f, 59f / 255f);
        }
        m_InvincibleTimer += Time.deltaTime;
    }

    const float INVINNIBLE_INTERVAL = 10f;
    float m_InvincibleTimer;

    void OnCollisionEnter(Collision collision)
    {

        var pill = collision.collider.gameObject.GetComponent<Pill>();

        if (pill != null)
        {
            Destroy(pill.gameObject);
            m_Invincible = true;
            this.GetComponent<MeshRenderer>().material.color = Color.yellow;

            m_InvincibleTimer = 0;
        }

        var ghost = collision.collider.gameObject.GetComponent<Ghost>();
        if (ghost != null)
        {
            if (m_Invincible)
            {
                GameObject.Destroy(ghost.gameObject);
                Debug.Log("You win");
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                Debug.Log("Game Over");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
