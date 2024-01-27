using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : InteractableObjects
{
    private Recipe DesiredDish; 
    private Recipe platedDish;

    public void init(Recipe _Recipe)
    {
        DesiredDish = _Recipe;
    }

    public void addToPlate(Transform target)
    {
        target.SetParent(transform);
    }
}
