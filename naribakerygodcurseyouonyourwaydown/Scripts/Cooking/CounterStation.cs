using Godot;
using System;

public partial class CounterStation : Station {

    [Export] protected Node3D ingredientSpawnPosition;

    public override void AddIngredient(Item item) {
        base.AddIngredient(item);
        //spawn ingredient in the world
        item.Reparent(this);
        item.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
    }


    public override Item RemoveIngredient() {
        return base.RemoveIngredient();
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player) {
            if (player.AreHandsEmpty() && HasIngredient())
                player.PickUp(RemoveIngredient());
            else if (!player.AreHandsEmpty() && !HasIngredient()) {
                AddIngredient(player.PutDown(false));
            }
        }
        base.OnInteract(body);
    }

}
