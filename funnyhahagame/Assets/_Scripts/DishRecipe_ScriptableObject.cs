using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    Fish,
    TopBun,
    BottomBun,
    Patty,
    Brain,
    BabyPowder
}

[System.Serializable]
public class Recipe
{
    public List<Ingredient> Ingredients;

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
    }

    //public bool Equals(Recipe obj)
    //{
        
    //     return obj.Ingredients.Count == Ingredients.Count;
    //}

    //public static bool operator ==(Recipe a, Recipe b)
    //{
    //    return a.Equals(b);
    //}

    //public static bool operator !=(Recipe a, Recipe b)
    //{
    //    return !a.Equals(b);
    //}

    public override string ToString()
    {
        string stdout = "Recipe with Ingredients: ";
        foreach (Ingredient ing in Ingredients)
        {
            stdout += ing.ToString();
        }
        return stdout;
    }
}

[System.Serializable]
public struct Ingredient
{
    public IngredientType Type;
    public int timesCut;
    public int timesSmashed;
    public float burnProgress;

    public override string ToString()
    {
        string stdout = "Ingredient Details: ";

        stdout += Type + ", ";

        stdout += timesCut + ", ";

        stdout += timesSmashed + ", ";

        stdout += burnProgress + ". ";

        return stdout;

    }


    public bool Equals(Ingredient other)
    {
        return Type == other.Type
            && timesCut == other.timesCut
            && timesSmashed == other.timesSmashed
            && burnProgress == other.burnProgress;
    }
}

[CreateAssetMenu(menuName = "funnyhahagame/Recipe")]
public class DishRecipe_ScriptableObject : ScriptableObject
{
    [field:SerializeField] public Recipe recipe {get; private set;}
    public List<Ingredient> GetIngredients() => recipe.Ingredients;
}