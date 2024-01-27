using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : InteractableObjects
{
    public float hitTimer, hitTimerReset = 1;
    [SerializeField]
    private bool hasbeenBeat;



    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (hasbeenBeat)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitTimerReset)
            {
                hasbeenBeat = false;
                hitTimer = 0;

            }
        }


    }

    public void Smash(InteractableObjects target)
    {
        if (!hasbeenBeat && (target.Layers & (1 << 7)) != 0)
        {
            if(target.transform.rotation == Quaternion.identity)
            {
                gameObject.GetComponent<AudioSource>().Play();
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.2f, target.transform.localScale.y * 0.8f, target.transform.localScale.z * 1.2f);
                hasbeenBeat = true;

            }
            else if(target.transform.rotation == Quaternion.FromToRotation(Vector3.zero,Vector3.right))
            {
                gameObject.GetComponent<AudioSource>().Play();
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.2f, target.transform.localScale.y * 1.2f, target.transform.localScale.z * 0.8f);
                hasbeenBeat = true;

            }
            


            Debug.Log("Smash!!");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (player == null)
            return;
        InteractableObjects objectstointeract = collision.gameObject.GetComponent<InteractableObjects>();
   


        if (objectstointeract)
        {
            if (player.HandTarget.velocity.magnitude > 2.2f)
                Smash(objectstointeract);
        }
    }
}
