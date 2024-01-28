using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : InteractableObjects
{
    public float hitTimer, hitTimerReset = 1;
    [SerializeField]
    private bool hasBeenBeat;
    public GameObject SpashVFX;

    // Update is called once per frame
    void Update()
    {
        if (hasBeenBeat)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer >= hitTimerReset)
            {
                hasBeenBeat = false;
                hitTimer = 0;
            }
        }
    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);
        transform.rotation = new Quaternion(0.517273605f,-0.478444576f,-0.54497844f,-0.454441905f);
    }

    public void Smash(InteractableObjects target)
    {
        if (!hasBeenBeat && (target.Layers & (1 << 7)) != 0)
        {
            
                Destroy(Instantiate(SpashVFX, target.transform.position, Quaternion.identity), 3);
                gameObject.GetComponent<AudioSource>().Play();
                target.transform.localScale = new Vector3(target.transform.localScale.x * 1.4f, target.transform.localScale.y * 0.6f, target.transform.localScale.z * 1.4f);
                hasBeenBeat = true;
                //Debug.Log("Smash!!");

                IngredientObject _ingredientObject = target as IngredientObject;

            if(_ingredientObject != null)
            {
                _ingredientObject.ingredient.timesSmashed++;
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player == null)
            return;
        
        InteractableObjects interactionObject = collision.gameObject.GetComponent<InteractableObjects>();
        if (interactionObject)
        {
            if (player.HandTarget.velocity.magnitude > 1)
            {
                Smash(interactionObject);
              //  Debug.Log("Smash0");
            }
        }
    }
}
