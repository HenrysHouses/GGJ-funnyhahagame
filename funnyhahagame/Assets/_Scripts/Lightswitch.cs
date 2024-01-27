using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public Light[] lights;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            TurnOnLights();
        }
    }


    public void TurnOnLights()
    {
        foreach (Light light in lights)
        {
            Light light1 = light.GetComponent<Light>();
            light1.enabled = !light1.enabled;
        }
    }



}
