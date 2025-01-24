using Godot;
using System;

//Hands is a single-celled inventory whose ingredient exists in the game world
public partial class Hands : Node3D {

    private Item item;

    //returns whether or not hands is empty
    public bool IsEmpty() {
        return item == null;
    }

    public Item GetItem() {
        return item;
    }

    //Adds the specified Ingredient to inventory and repositions it
    public void PickUp(Item ingIn) {
        if (!IsEmpty()) return;
        item = ingIn;

        //instantiate
        ingIn.Reparent(this);
        ingIn.GlobalPosition = GlobalPosition;
    }

    //Gets rid of an ingredient from the inventory and destroys it or returns it
    public Item PutDown(bool canDestroyItem) {
        if (IsEmpty()) return null;
        if (canDestroyItem) {
            item.QueueFree();
            item = null;
            return null;
        }

        Item i = item;
        item = null;
        return i;
    }


}
