using Godot;
using System;

public partial class CounterStation : Station {

    public override void AddIngredient(Ingredient ingredient) {
        base.AddIngredient(ingredient);
        //spawn ingredient in the world
        ingredient.Reparent(this);
        ingredient.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
    }


    public override Ingredient RemoveIngredient() {
        return base.RemoveIngredient();
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player) {
            if (player.AreHandsEmpty() && HasIngredient())
                player.PickUp(RemoveIngredient());
            else if (!player.AreHandsEmpty() && !HasIngredient() && player.GetItem() is Ingredient ing) {
                player.PutDown(false);
                AddIngredient(ing);
            }
        }
        base.OnInteract(body);
    }

}
