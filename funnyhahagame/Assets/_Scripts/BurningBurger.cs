using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningBurger : MonoBehaviour
{

    private IngredientObject ingredient;
    private ParticleSystem VFXs;
    // Start is called before the first frame update
    void Start()
    {
        ingredient = GetComponent<IngredientObject>();
        VFXs = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if( ingredient.ingredient.burnProgress > 1)
        {
            VFXs.Play();
        }



    }
}
