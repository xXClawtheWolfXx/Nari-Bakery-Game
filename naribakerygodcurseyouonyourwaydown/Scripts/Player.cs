using Godot;
using System;

public partial class Player : Node3D {

    [Export] private Hands hands;

    public bool AreHandsEmpty() {
        return hands.IsEmpty();
    }

    public Item GetItem() {
        return hands.GetItem();
    }

    public void PickUp(Item ing) {
        hands.PickUp(ing);
    }

    public Item PutDown(bool canDestroyItem) {
        return hands.PutDown(canDestroyItem);
    }
}
