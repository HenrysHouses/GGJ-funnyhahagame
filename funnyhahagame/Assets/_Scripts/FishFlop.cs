using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlop : IngredientObject
{
    public float floppingForce;
    private float floppingtimer;
    [SerializeField]
    private int randomHit;
    private bool canfloop;
    private AudioSource sound;

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canfloop)
        Flopping();
    }

    private void Flopping()
    {
        floppingtimer += Time.deltaTime;
        if (floppingtimer > 1)
        {
            randomHit = Random.Range(0,5);
            if (randomHit == 0)
            {
                floppingForce = Random.Range(50, 300);

                GetComponent<Rigidbody>().AddForce(Vector3.up * floppingForce);
                sound.Play();
                
                canfloop = false;
            }

        }

    }


    internal override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        canfloop = true;
        floppingtimer = 0;
    }
}
