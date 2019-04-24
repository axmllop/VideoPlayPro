using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowUnitMove :MonoBehaviour
{
    //public bool isLeft = true;

    public float speed = 0.5f;
    Vector3 start = Vector3.zero;
    float screenWidth = 0;
    float imgWidth;
    Image flow;
    float lLimit;
    float rLimit;
    float upLimit;
    float dLimit;


   
    void Start()
    {
        flow = GetComponent<Image>();
        screenWidth = Screen.width / 2;
        
        imgWidth = flow.preferredWidth;
       
        lLimit = -841f;
        rLimit = 841;

        upLimit = 388;
        dLimit = -388;
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            speed = +speed;
        }
        if (random == 1)
        {
            speed = -speed;
        }
        
     
       
    }
   
    
    float x = 5;
    float v = 2;
    void Update()
    {

        this.transform.localPosition += Vector3.left * speed;
        if (flow.rectTransform.localPosition.x <= lLimit || flow.rectTransform.localPosition.x >= rLimit)
        {
            speed = -speed;
        }
        //if (flow.rectTransform.localPosition.y < dLimit || flow.rectTransform.localPosition.y > upLimit)
        //{

        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            float fFlag = -1.0f;
            if (other.tag == "UpWall")
            {
            
                float lAngle = Vector3.Angle(transform.up, Vector3.right);
                transform.Rotate(Vector3.forward * 2.0f * lAngle * fFlag);
            }
            else if (other.tag == "DownWall")
            {
            
            float lAngle = Vector3.Angle(transform.up, Vector3.right);
                transform.Rotate(Vector3.forward * 2.0f * lAngle);
            }
            else if (other.tag == "LeftWall")
            {
            
            float lAngle = Vector3.Angle(transform.up, Vector3.up);
                transform.Rotate(Vector3.forward * 2.0f * lAngle * fFlag);
            }
            else if (other.tag == "RightWall")
            {
            
            float lAngle = Vector3.Angle(transform.up, Vector3.up);
                transform.Rotate(Vector3.forward * 2.0f * lAngle);
            }
        

    }
}
