using Godot;
using System;

public partial class Player : Node3D {

    [Export] private Hands hands;

    public bool AreHandsEmpty() {
        return hands.IsEmpty();
    }

    public void PickUp(Ingredient ing) {
        hands.PickUp(ing);
    }

    public Ingredient PutDown(bool canDestroyItem) {
        return hands.PutDown(canDestroyItem);
    }
}
