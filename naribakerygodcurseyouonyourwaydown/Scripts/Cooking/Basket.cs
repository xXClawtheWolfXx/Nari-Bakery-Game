using Godot;
using System;

public partial class Basket : StaticBody3D, IInteractable {

    [Export] MeshInstance3D mesh;
    [Export] private PackedScene ingredientToSpawn;

    public void SpawnIngredient(Player player) {
        Ingredient ing = (Ingredient)ingredientToSpawn.Instantiate();
        AddChild(ing);
        player.PickUp(ing);
    }

    public void OnInteract(Node3D body) {
        if (body is Player player && player.AreHandsEmpty())
            SpawnIngredient(player);

    }

    public void SetMaterial(Material mat) {
        mesh.SetSurfaceOverrideMaterial(0, mat);
    }

    public Material GetMaterial() {
        return mesh.GetActiveMaterial(0);
    }

    public void OnAltInteract(Node3D body) { }
}
