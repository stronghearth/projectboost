using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trigger : MonoBehaviour
{
    public Audio a;

     //Start is called before the first frame update
    void Start()
    {
       
    }

    //Update is called once per frame
   void Update()
    {
    
    }
    public void OnCollisionEnter(Collision collision)
   {

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                //do nothing
                break;
            case "Finish":
                a.StopMusic();
                break;
           default:
                a.StopMusic();
               break;
        } 
   }
}
