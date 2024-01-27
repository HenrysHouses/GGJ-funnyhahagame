using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : InteractableObjects
{
    public float hitTimer, hitTimerReset = 1;
    [SerializeField]
    private bool hasbeenBeat;

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
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.4f, target.transform.localScale.y * 0.6f, target.transform.localScale.z * 1.4f);
                hasbeenBeat = true;

            }
            else if(target.transform.rotation == Quaternion.FromToRotation(Vector3.zero,Vector3.right))
            {
                gameObject.GetComponent<AudioSource>().Play();
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.4f, target.transform.localScale.y * 1.4f, target.transform.localScale.z * 0.6f);
                hasbeenBeat = true;

            }
            


            Debug.Log("Smash!!");
        }

    }

    internal override void OnCollisionEnter(Collision collision)
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
