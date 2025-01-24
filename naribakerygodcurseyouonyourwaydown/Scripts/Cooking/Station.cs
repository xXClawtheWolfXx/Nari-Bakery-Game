using Godot;
using System;
using System.Diagnostics;

public partial class Station : StaticBody3D, IInteractable {

    [Export] private MeshInstance3D mesh;

    protected Item itemAdded;
    protected Ingredient ingAdded;

    public bool HasIngredient() {
        return itemAdded is not null;
    }

    public virtual void AddIngredient(Item item) {
        if (HasIngredient()) return;
        itemAdded = item;
    }

    public virtual void AddIngredient(Ingredient ingredient) {
        if (HasIngredient()) return;
        itemAdded = ingredient;
        ingAdded = ingredient;
    }

    public virtual Item RemoveIngredient() {
        Item ingredientAgain = itemAdded;
        itemAdded = null;
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
