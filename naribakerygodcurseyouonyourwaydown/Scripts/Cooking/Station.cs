using Godot;
using System;
using System.Diagnostics;

public partial class Station : StaticBody3D, IInteractable {

    [Export] private MeshInstance3D mesh;

    protected Ingredient ingredientAdded;

    public bool HasIngredient() {
        return ingredientAdded is not null;
    }

    public virtual void AddIngredient(Ingredient ingredient) {
        if (HasIngredient()) return;
        ingredientAdded = ingredient;
    }

    public virtual Ingredient RemoveIngredient() {
        Ingredient ingredientAgain = ingredientAdded;
        ingredientAdded = null;
        return ingredientAgain;
    }

    public virtual void ProcessIngredient() {

    }


    public virtual void OnInteract(Node3D body) {
        GD.PrintS(body, "interacting with", Name);
    }
    public virtual void OnAltInteract(Node3D body) {
        GD.PrintS(body, "interacting alt with", Name);
    }
    public void SetMaterial(Material mat) {
        mesh.SetSurfaceOverrideMaterial(0, mat);
    }

    public Material GetMaterial() {
        return mesh.GetActiveMaterial(0);
    }


}
