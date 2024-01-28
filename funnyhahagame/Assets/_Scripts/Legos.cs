using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legos : InteractableObjects
{

    bool played;
    public AudioSource playingwithegos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);
        playingwithegos.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
