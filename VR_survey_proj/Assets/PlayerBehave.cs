using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehave : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject instrument;
    [SerializeField] GameObject[] m_mirror = new GameObject [2];
    bool take = false;

    // Update is called once per frame
    void Update()
    {
        Vector3 InsPos = instrument.transform.localPosition;
        Vector3 PlayerPos = this.transform.localPosition;

        if (Input.GetKey(KeyCode.T))//take the ins
        {
            Debug.Log("take the instrument from "+ InsPos);

            take = true;
            

        }

        if (Input.GetKey(KeyCode.P) && take)// put the ins
        {
            
            take = false;
            Vector3 addZ = new Vector3(0f, -1.4f, 0f);
            instrument.transform.localPosition = PlayerPos + addZ;
            Debug.Log("put the instrument to " + InsPos);
        }

        if(take)
        {
            Vector3 addZ = new Vector3(1f, -1.4f, 0.3f);
            //Vector3 addZ = new Vector3(0,0,0);
            instrument.transform.localPosition = PlayerPos + addZ;
        }


        
    }


}
