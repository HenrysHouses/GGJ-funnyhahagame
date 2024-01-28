using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : InteractableObjects
{
    public Light[] lights;
    public bool lightsOn;
    // Start is called before the first frame update
    void Start()
    {
        TurnOnLights();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            TurnOnLights();
        }
    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);

        TurnOnLights();

        GetComponent<AudioSource>().Play();
    }


    public void TurnOnLights()
    {
        foreach (Light light in lights)
        {
            Light light1 = light.GetComponent<Light>();
            light1.enabled = !light1.enabled;
            if (light1.enabled)
                lightsOn = true;
            else
                lightsOn = false;
        }
    }



}
