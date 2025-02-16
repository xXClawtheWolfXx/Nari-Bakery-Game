using Godot;
using System;
using System.Diagnostics;

public abstract partial class Station : StaticBody3D, IInteractable {

    [Export] private MeshInstance3D mesh;
    [Export] protected Node3D ingredientSpawnPosition;

    protected Ingredient ingAdded;
    protected int maxStep;

    public bool HasIngredient() {
        return ingAdded is not null;
    }

    public virtual void AddIngredient(Ingredient ingredient) {
        if (HasIngredient()) return;
        ingAdded = ingredient;
    }

    public virtual Ingredient RemoveIngredient() {
        Ingredient ingredientAgain = ingAdded;
        ingAdded = null;
        return ingredientAgain;
    }

    protected virtual int GetMaxSteps() {
        return GD.RandRange(3, 10);
    }

    public virtual void ProcessIngredient() { }


    public virtual void OnInteract(Node3D body) { }
    public virtual void OnAltInteract(Node3D body) { }

    public void SetMaterial(Material mat) {
        mesh.SetSurfaceOverrideMaterial(0, mat);
    }

    public Material GetMaterial() {
        return mesh.GetActiveMaterial(0);
    }


}
