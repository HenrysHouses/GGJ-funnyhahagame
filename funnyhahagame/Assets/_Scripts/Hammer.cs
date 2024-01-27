using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
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

    public void Cut(InteractableObjects target)
    {
        if (!hasbeenBeat && (target.Layers & (1 << 7)) != 0)
        {
            gameObject.GetComponent<AudioSource>().Play();
            target.transform.localScale = new Vector3(target.transform.localScale.x * 0.6f, target.transform.localScale.y * 1.2f, target.transform.localScale.z * 1.1f);
            hasbeenBeat = true;

            Debug.Log("Smash!!");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        InteractableObjects objectstointeract = collision.gameObject.GetComponent<InteractableObjects>();
        if (objectstointeract)
        {
            Cut(objectstointeract);
        }
    }

}
