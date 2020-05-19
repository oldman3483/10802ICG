using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehave : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer m_Renderer;
    Entity m_Entity;
    public Entity Entity { get { return m_Entity; } }


    #region event handler
    void HandleOnSelected(Entity e) { m_Renderer.material.color = Color.yellow; }
    void HandleOnDeselected(Entity e) { m_Renderer.material.color = Color.white; }
    void HandleOnTaken(Entity e) { Destroy(this.gameObject); }

    #endregion


    void Start()
    {
        m_Renderer = this.GetComponent<MeshRenderer>();
    }


    public void UpdateEntity(Entity entity)
    {
        m_Entity = entity;
        m_Entity.OnSelected += HandleOnSelected;
        m_Entity.OnDeselected += HandleOnDeselected;
        m_Entity.OnTaken += HandleOnTaken;
    }

    private void OnDestroy()
    {
        if (m_Entity != null)
        {

            //destroy 要反註冊
            m_Entity.OnSelected -= HandleOnSelected;
            m_Entity.OnDeselected -= HandleOnDeselected;
            m_Entity.OnTaken -= HandleOnTaken;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
