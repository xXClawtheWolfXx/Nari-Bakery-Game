using Godot;
using System;

public partial class BowlStation : Station {

    [Export] private PackedScene bowlToSpawn;

    public void SpawnBowl(Player player) {
        Bowl bowl = (Bowl)bowlToSpawn.Instantiate();
        AddChild(bowl);
        player.PickUp(bowl);
    }

    public override void OnInteract(Node3D body) {
        if (body is Player player && player.AreHandsEmpty())
            SpawnBowl(player);

    }


}
