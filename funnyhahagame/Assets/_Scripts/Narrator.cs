using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{

    private GameManager gm;
    private Lightswitch lightswitch;
    private float timerForLights;
    public AudioSource[] soundsToPlay;
    public Lightswitch light;
    
    // Start is called before the first frame update
    void Start()
    {
        


    }

    void Update()
    {
        if(light.lightsOn)
        {
            soundsToPlay[4].Play();
        }
   
  
    }
}  
