using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowTorch : InteractableObjects
{
    // public float BurnTimer, MaxBurnedtimer = 1;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField] float burnStrength = 6;
    public bool BurnTheburger;
    public Transform pegboardPos;
    public bool resetPos,played;
    float timer;
    public AudioSource burningThePlace;




    public void Start()
    {
        transform.position = pegboardPos.position;
        transform.rotation = pegboardPos.rotation;
        GetComponent<Rigidbody>().isKinematic = true;
    }


    public void Update()
    {

        if (gameObject == null)
        {
            gameObject.transform.position = pegboardPos.position;
            transform.rotation = pegboardPos.rotation;
            GetComponent<Rigidbody>().isKinematic = true;

        }

        if (resetPos == true)
        {
             timer += Time.deltaTime;
            if (timer > 10)
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
        if (!played)
        {
            burningThePlace.Play();
            played = true;
        }
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<ParticleSystem>().Play();

        base.OnPickup(playerCon);
        StartCoroutine(Burn());

        transform.rotation = new Quaternion(0.396383733f,-0.60298115f,0.371423066f,0.584241927f);
        resetPos = false;
    }

    public override void OnRelease()
    {
        base.OnRelease();
        GetComponent<AudioSource>().Stop();
        GetComponentInChildren<ParticleSystem>().Stop();
        resetPos = true;

    }


    public IEnumerator Burn()
    {

        while(player != null)
        {
            Ray ourRay = new Ray(shootPoint.position, shootPoint.forward);
            RaycastHit[] hits = Physics.RaycastAll(ourRay, Mathf.Infinity, 1 << 0);

            Debug.Log("is On: " + hits.Length);

            foreach (RaycastHit hit in hits)
            {
                if(hit.collider.TryGetComponent(out IngredientObject component))
                {
                    if((component.Layers & (1 << 8)) !=0)
                    {
                        component.ingredient.burnProgress += burnStrength * Time.deltaTime;
                        if (!GetComponent<Renderer>())
                        {
                            BurnTheburger = true;
                            yield break; 
                        }

                        component.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.black, component.ingredient.burnProgress);

                        Debug.Log("is burning: " + hit.collider.gameObject.name);

                        // if(component.ingredient.burnProgress >= 1)
                        // {

                        // }
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}