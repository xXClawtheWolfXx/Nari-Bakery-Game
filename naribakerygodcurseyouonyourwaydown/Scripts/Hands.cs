using Godot;
using System;

//Hands is a single-celled inventory whose ingredient exists in the game world
public partial class Hands : Node3D {

    private Ingredient ingredient;

    //returns whether or not hands is empty
    public bool IsEmpty() {
        return ingredient == null;
    }

    //Adds the specified Ingredient to inventory and repositions it
    public void PickUp(Ingredient ingIn) {
        if (!IsEmpty()) return;
        ingredient = ingIn;

        //instantiate
        ingIn.Reparent(this);
        ingIn.GlobalPosition = GlobalPosition;
    }

    //Gets rid of an ingredient from the inventory and destroys it or returns it
    public Ingredient PutDown(bool canDestroyItem) {
        if (IsEmpty()) return null;
        if (canDestroyItem) {
            ingredient.QueueFree();
            ingredient = null;
            return null;
        }

        Ingredient i = ingredient;
        ingredient = null;
        return i;
    }


}
