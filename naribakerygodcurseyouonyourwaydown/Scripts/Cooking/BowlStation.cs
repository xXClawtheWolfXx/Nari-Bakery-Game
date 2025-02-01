using Godot;
using System;

public partial class BowlStation : PrepStation {

    [Export] private PackedScene bowlToSpawn;
    private bool finalDishSpawned = false;

    public override void AddIngredient(Ingredient ingredient) {
        spawnedIngredients.Add(ingredient);
        ingredientRs.Add(ingredient.GetIngredientR);
        base.AddIngredient(ingredient);
    }

    public override void ProcessIngredient() {
        base.ProcessIngredient();
        //if done, spawn choppable scene
        if (currStep >= maxStep && spawnedIngredients.Count > 1) {
            ResetProgressUI();
            foreach (Ingredient ing in spawnedIngredients)
                ing.QueueFree();
            currRecipie = FindBestRecipie();
            spawnedIngredients.Clear();
            ingredientRs.Clear();

            PackedScene finalDish = badDish;
            if (currRecipie is not null)
                finalDish = currRecipie.GetFinalDishScene;

            spawnedIngredients.Add(SpawnNewIngredient(finalDish));
            finalDishSpawned = true;
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

    public override Ingredient RemoveIngredient() {
        finalDishSpawned = false;
        spawnedIngredients.Clear();
        return base.RemoveIngredient();
    }

    public override void OnAltInteract(Node3D body) {
        if (ingredientRs.Count >= 2)
            ProcessIngredient();
    }

    protected override bool CanAcceptIngredient(Player player) {
        return !player.AreHandsEmpty() && player.GetItem() is Ingredient ing;
    }

    protected override bool CanRemoveIngredient(Player player) {
        return player.AreHandsEmpty() && finalDishSpawned;
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
