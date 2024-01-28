using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    // [SerializeField] Transform PlateDispenser;
    [SerializeField] GameObject PlatePrefab;
    [SerializeField] DishRecipe_ScriptableObject[] Dishes;

    // Start is called before the first frame update
    void Start()
    {
        if (Dishes.Count > 0)
            instantiatePlate(Dishes[0]);
    }

    private void instantiatePlate(DishRecipe_ScriptableObject recipe)
    {
        GameObject spawn = Instantiate(PlatePrefab);
        spawn.transform.position = transform.position;
        spawn.GetComponent<PlateController>().init(recipe.recipe);
    }
}
