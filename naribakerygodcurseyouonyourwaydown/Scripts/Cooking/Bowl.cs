using Godot;
using System;
using System.Collections.Generic;

public partial class Bowl : KitchenItem {
    /*
        public override void _Ready() {
            progressUI.Visible = false;
        }

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
                Ingredient finalDish = null;
                if (currRecipie is null)
                    finalDish = (Ingredient)badDish.Instantiate();
                else
                    finalDish = (Ingredient)currRecipie.GetFinalDishScene.Instantiate();

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
        public override void OnAltInteract(Node3D body) {
            if (ingredientRs.Count >= 2)
                ProcessIngredient();
        }

        public override void OnInteract(Node3D body) {
            if (body is Player player) {
                if (!player.AreHandsEmpty() && player.GetItem() is Ingredient ing) {
                    GD.PrintS(player.GetItem() is Ingredient, "ing");
                    player.PutDown(false);
                    AddIngredient(ing);
                }
            }
        }
    */

}