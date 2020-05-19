using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGameScene : MonoBehaviour {

    [SerializeField] GameUI m_GameUI;


	EscapeGame m_Game;
    const float SELECT_RANGE = 3;

    public EscapeGame Game { get { return m_Game; } }

    #region Msg event handlers
    void HandleOnMessageAdded(string message)
    {
        m_GameUI.ShowMessage(message);
    }

    #endregion

    #region Entity event handlers
    void HandleOnEntitySelected (Entity entity){
        m_GameUI.SetActionVisible(true);
    }
    void HandleOnEntityDeselected (Entity entity) {
        m_GameUI.SetActionVisible(false);
    }
    void HandleOnEntityTaken(Entity entity) {
        m_GameUI.SetActionVisible(false);
        m_GameUI.UpdateTakenEntities();
    }
    void HandleOnEntityReleased(Entity entity) {
        m_GameUI.UpdateTakenEntities();

        var entityBehav = GameObject.Instantiate(
            Resources.Load<EntityBehave>("prefabs/entity_cube"));
        entityBehav.transform.localPosition = entity.Position;
        entityBehav.UpdateEntity(entity);
    }
    void HandleOnEntityInteracted(Entity entity) { }
    void HandleOnEntityInspected(Entity entity) { }

    #endregion

    #region
    void HandleOnGameStarted (EscapeGame game)
    {
        foreach (var e in m_Game.Entities)
        {
            // Create GameObject by e
            var entityBehav = GameObject.Instantiate(
                Resources.Load<EntityBehave>("prefabs/entity_cube"));
            entityBehav.transform.localPosition = e.Position;
            entityBehav.UpdateEntity(e);
        }
    }
    void HandleOnGameFinished (EscapeGame game) { }

    void HandleOnGamedOver (EscapeGame game) { }

    #endregion

    void Awake()
    {
        m_Game = new EscapeGame();
        m_Game.OnGameStarted += HandleOnGameStarted;
        m_Game.OnGameFinished += HandleOnGameFinished;
        m_Game.OnGameOver += HandleOnGamedOver;

        m_Game.OnMessageAdded += HandleOnMessageAdded;

        m_Game.OnEntityInteracted += HandleOnEntityInteracted;
        m_Game.OnEntityInspected += HandleOnEntityInspected;
        m_Game.OnEntitySelected += HandleOnEntitySelected;
        m_Game.OnEntityDeselected += HandleOnEntityDeselected;
        m_Game.OnEntityTaken += HandleOnEntityTaken;
        m_Game.OnEntityReleased += HandleOnEntityReleased;

        m_Game.MakeGame();

        
    }

    void DetectEntity()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        bool detectEntity = false;

        RaycastHit raycastResult;
        if(Physics.Raycast(ray, out raycastResult))
        {
            if (raycastResult.distance < SELECT_RANGE)
            {
                var entityBehav = raycastResult.collider.GetComponent<EntityBehave>();
                if (entityBehav != null)
                {
                    detectEntity = true;
                    m_Game.SelectEntity(entityBehav.Entity);
                }
            }
        }
        if (!detectEntity)
        {
            SelectNothing();
        }
    }

    void SelectNothing()
    {
        m_Game.SelectEntity(null);
    }


    void Start () {
	
		
	}

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            DetectEntity();

        }

        /*
		if (Input.GetKeyDown (KeyCode.N)) {

			m_Game.SelectNext ();

		} else if (Input.GetKeyDown (KeyCode.Space)) {

			m_Game.Inspect ();

		} else if (Input.GetKeyDown (KeyCode.Return)) {

			m_Game.Interact ();

		} else if (Input.GetKeyDown (KeyCode.R)) {

			m_Game.PutBack ();
		}*/
    }
}
