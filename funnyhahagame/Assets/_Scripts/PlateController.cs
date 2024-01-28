using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : InteractableObjects
{
    private Recipe DesiredDish; 
    private Recipe platedDish;
    public bool completed;
    public bool handed_in;

    public Mesh bagMesh;
    public Material bagMaterial;


    public void init(Recipe _Recipe)
    {
        DesiredDish = _Recipe;
        platedDish = new Recipe();
        completed = false;

        Debug.Log("Expected dish: " + DesiredDish);
    }

    public void UpdateCompletion()
    {
        // Check plate state
        if (DesiredDish.Ingredients.Count == platedDish.Ingredients.Count)
        {
            Debug.Log("Des" + DesiredDish);
            Debug.Log("pla" + platedDish);
            if (CompareTwoRecipes(DesiredDish, platedDish))
            {
                completed = true;

                gameObject.GetComponent<MeshFilter>().mesh = bagMesh;
                gameObject.GetComponent<MeshRenderer>().material = bagMaterial;

                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }

                gameObject.transform.localScale = new Vector3(35, 20, 35);

                Debug.Log("Completed the dish!");
            }
            
        }

        if (platedDish != null)
            Debug.Log("Dish state: " + platedDish.ToString());
    }

    public bool CompareTwoRecipes(Recipe a, Recipe b)
    {
        int b_contains_a = 0;
        foreach (Ingredient ing in a.Ingredients)
        {
            if (b.Ingredients.Contains(ing))
            {
                b_contains_a++;
            }
        }

        int a_contains_b = 0;
        foreach (Ingredient ing in a.Ingredients)
        {
            if (b.Ingredients.Contains(ing))
            {
                a_contains_b++;
            }
        }

        if (a_contains_b == 0 || b_contains_a == 0) return false;

        return a_contains_b == b_contains_a;
    }

    public void addToPlate(IngredientObject target)
    {
        target.transform.SetParent(transform);

        platedDish.Ingredients.Add(target.ingredient);

        UpdateCompletion();
    }


}
