using Godot;
using System;

public partial class BowlStation : MixStation {

    public override void OnAltInteract(Node3D body) {
        if (ingredientRs.Count >= 2)
            ProcessIngredient();
    }

    /*
        public void SpawnBowl(Player player) {
            Bowl bowl = (Bowl)bowlToSpawn.Instantiate();
            AddChild(bowl);
            player.PickUp(bowl);
        }

        public override void OnInteract(Node3D body) {
            if (body is Player player && player.AreHandsEmpty())
                SpawnBowl(player);

        }
    */

}
