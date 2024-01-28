using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : InteractableObjects
{
    public float hitTimer, hitTimerReset = 1;
    [SerializeField]
    private bool hasBeenBeat;
    public GameObject SpashVFX;
    public Transform pegboardPos;
    public bool resetPos;
    private float timer;
    // Update is called once per frame



    private void Start()
    {
        transform.position = pegboardPos.position;
        GetComponent<Rigidbody>().isKinematic = true;
    }
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


        if(gameObject == null)
        {
            gameObject.transform.position = pegboardPos.position;
            GetComponent<Rigidbody>().isKinematic = true;

        }

        if (resetPos == true)
        {
            timer += Time.deltaTime;
            if(timer > 10)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                gameObject.transform.position = pegboardPos.position;
                transform.rotation = pegboardPos.rotation;
                resetPos = false;
                timer = 0;
            }
        }
    }

    public override void OnPickup(PlayerController playerCon)
    {
        base.OnPickup(playerCon);
        transform.rotation = new Quaternion(0.517273605f,-0.478444576f,-0.54497844f,-0.454441905f);
        resetPos = false;

    }

    public override void OnRelease()
    {
        base.OnRelease();
        resetPos = true;
    }

    public void Smash(IngredientObject target)
    {
        if (!hasBeenBeat && (target.Layers & (1 << 7)) != 0)
        {
            
            Destroy(Instantiate(SpashVFX, target.transform.position, Quaternion.identity), 3);
            gameObject.GetComponent<AudioSource>().Play();
            target.transform.localScale = new Vector3(target.transform.localScale.x * 1.4f, target.transform.localScale.y * 0.6f, target.transform.localScale.z * 1.4f);
            hasBeenBeat = true;
            //Debug.Log("Smash!!");

            target.ingredient.timesSmashed++;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (player == null)
            return;    
        
        IngredientObject interactionObject = collision.gameObject.GetComponent<IngredientObject>();
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
