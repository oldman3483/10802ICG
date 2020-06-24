using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleCam : MonoBehaviour
{
    [SerializeField] GameObject m_telescope;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float[] TeleAngleX = new float[1000];
    // Update is called once per frame
    int TeleIndex = 0;
    void Update()
    {

        
        
        TeleAngleX[TeleIndex] = m_telescope.transform.localEulerAngles.x;
        //Debug.Log(TeleAngleX[TeleIndex]);


        if (TeleIndex >1)
        {
            //Debug.Log(m_telescope.transform.localEulerAngles.x);
            //Debug.Log(TeleAngleX[TeleIndex-1]);
            //Debug.Log(TeleIndex - 1);
            if ((TeleAngleX[TeleIndex-1]>270 )&& (TeleAngleX[TeleIndex]-270 < 20))
            {
                Debug.Log(string.Format("<color=red>OK!!!! </color>"));
                this.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
            }
            else if ((TeleAngleX[TeleIndex] > 270)&& (TeleAngleX[TeleIndex-1]-270 < 20))
            {
                Debug.Log(string.Format("<color=white>rotate it! </color>"));
                this.transform.localEulerAngles= new Vector3(90f, 0f, 180f);
            }
        }

        TeleIndex++;

        if(TeleIndex == 1000)
        {
            TeleIndex = 0;
        }
        
    }
}
