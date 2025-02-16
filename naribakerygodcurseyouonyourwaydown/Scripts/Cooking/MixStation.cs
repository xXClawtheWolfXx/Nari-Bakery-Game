using Godot;
using System.Collections.Generic;

public abstract partial class MixStation : PrepStation {
    [Export] private Node3D appliance;

    protected RecipieR currRecipie;
    protected List<IngredientR> ingredientRs = new List<IngredientR>();
    protected List<Ingredient> spawnedIngredients = new List<Ingredient>();

    private bool finalDishSpawned = false;

    public override void AddIngredient(Ingredient ingredient) {
        spawnedIngredients.Add(ingredient);
        ingredientRs.Add(ingredient.GetIngredientR);
        base.AddIngredient(ingredient);
    }

    public override void ProcessIngredient() {
        base.ProcessIngredient();
        //if done, spawn finished recipie
        FinishIngredient();
    }

    protected void FinishIngredient() {
        if (currStep >= maxStep) {
            currStep = -10;
            SpawnFinalDish();
            ResetProgressUI();

        }
    }

    public override Ingredient RemoveIngredient() {
        finalDishSpawned = false;
        spawnedIngredients.Clear();
        return base.RemoveIngredient();
    }

    protected RecipieR FindBestRecipie() {
        foreach (RecipieR recipieR in Globals.Instance.CookingManager.GetAllRecipies) {
            if (recipieR.HasAllIngredients(ingredientRs)) {
                return recipieR;
            }
        }
        return null;
    }

    protected void SpawnFinalDish() {
        currRecipie = FindBestRecipie();
        DespawnIngredients();

        PackedScene finalDish = badDish;
        if (currRecipie is not null)
            finalDish = currRecipie.GetFinalDishScene;
        spawnedIngredients.Add(SpawnNewIngredient(finalDish));
        finalDishSpawned = true;
    }


    protected void DespawnIngredients() {
        foreach (Ingredient ing in spawnedIngredients)
            ing.QueueFree();
        spawnedIngredients.Clear();
        ingredientRs.Clear();
    }

    protected override bool CanAcceptIngredient(Player player) {
        return !player.AreHandsEmpty() && player.GetItem() is Ingredient;
    }

    protected override bool CanRemoveIngredient(Player player) {
        return player.AreHandsEmpty() && finalDishSpawned;
    }

}
