using Godot;
using System;
using System.Collections.Generic;

public partial class Bowl : Item, IInteractable {

    [Export] private Node3D ingredientSpawnPosition;
    [Export] private MeshInstance3D mesh;
    [Export] private Node3D progressUI;
    [Export] private ProgressBar progressBar;

    private float currStep = 0;
    private float maxStep = 1;
    private bool hasSpawned = false;

    private RecipieR currRecipie;
    private RecipieR recipieInProgress;

    public RecipieR GetCurrRecipie { get => currRecipie; }
    public RecipieR GetRecipieInProgress { get => recipieInProgress; }

    public override void _Ready() {
        progressUI.Visible = false;
    }

    private List<IngredientR> ingredientRs = new List<IngredientR>();
    private List<Ingredient> spawnedIngredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient) {
        spawnedIngredients.Add(ingredient);
        ingredientRs.Add(ingredient.GetIngredientR);
        ingredient.Reparent(this);
        ingredient.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
        currStep = 0;
        maxStep = GD.RandRange(3, 10);
    }


    public void ProcessIngredient() {
        //find recipie, max step from recipie
        //mix
        //finish-> return final dish
        //rip = currRecipie
        //
        progressUI.Visible = true;
        currStep++;
        progressBar.Value = currStep / maxStep;
        //if done, spawn choppable scene
        if (currStep >= maxStep && spawnedIngredients.Count > 1) {
            progressUI.Visible = false;
            foreach (Ingredient ing in spawnedIngredients)
                ing.QueueFree();
            currRecipie = FindBestRecipie();
            spawnedIngredients.Clear();
            ingredientRs.Clear();
            Ingredient finalDish = (Ingredient)currRecipie.GetFinalDishScene.Instantiate();

            spawnedIngredients.Add(finalDish);
            AddChild(finalDish);
            finalDish.GlobalPosition = ingredientSpawnPosition.GlobalPosition;
            currStep = 0;
            return;
        }
    }

    private RecipieR FindBestRecipie() {
        foreach (RecipieR recipieR in CookingManager.Instance.GetAllRecipies) {
            if (recipieR.HasAllIngredients(ingredientRs)) {
                return recipieR;
            }
        }
        return null;

    }
    public void OnAltInteract(Node3D body) {
        if (ingredientRs.Count >= 2)
            ProcessIngredient();
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

    public Material GetMaterial() {
        return mesh.GetActiveMaterial(0);
    }

    public void SetMaterial(Material mat) {
        mesh.SetSurfaceOverrideMaterial(0, mat);
    }

}