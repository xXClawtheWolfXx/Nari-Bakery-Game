using Godot;
using System;
using System.Collections.Generic;

public partial class Bowl : Item, IInteractable {

    [Export] private Node3D ingredientSpawnPosition;
    [Export] private MeshInstance3D mesh;

    private List<Ingredient> ingredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient) {
        ingredients.Add(ingredient);
        ingredient.Reparent(this);
        ingredient.GlobalPosition = ingredientSpawnPosition.GlobalPosition;


    }

    public Material GetMaterial() {
        return mesh.GetActiveMaterial(0);
    }

    public void OnAltInteract(Node3D body) {

    }

    public void OnInteract(Node3D body) {
        if (body is Player player) {
            if (!player.AreHandsEmpty() && player.GetItem() is Ingredient ing) {
                GD.PrintS(player.GetItem() is Ingredient, "ing");
                player.PutDown(false);
                AddIngredient(ing);
            }
        }
    }

    public void SetMaterial(Material mat) {
        mesh.SetSurfaceOverrideMaterial(0, mat);
    }

}