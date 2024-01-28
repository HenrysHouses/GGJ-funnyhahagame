using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : InteractableObjects
{
    public AudioSource NarraterAudio0, NarraterAudio1, NarraterAudio2, NarraterAudio3;
    public Light[] lights;
    public bool lightsOn,played1,played2,played3;
    public float lighttimer;
    
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

        if(!lightsOn)
        {
            lighttimer += Time.deltaTime;
            if(lighttimer > 17)
            {
                if(!played1)
                {
                    NarraterAudio1.Play();
                    played1 = true;

                }
            }

             if (lighttimer > 30)
            {
                if (!played2)
                {
                    NarraterAudio2.Play();
                    played2 = true;

                }
            }

             if (lighttimer > 60)
            {
                if (!played3)
                {
                    NarraterAudio1.Play();
                    played3 = true;

                }

                TurnOnLights();
                GetComponent<AudioSource>().Play();


            }
        }
    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);

        TurnOnLights();

        GetComponent<AudioSource>().Play();
        NarraterAudio0.Play();
    }


    public void TurnOnLights()
    {
        lighttimer = 0;
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
