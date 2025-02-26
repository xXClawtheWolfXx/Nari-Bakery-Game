using Godot;
using System;

public partial class ChoppableStation : PrepStation {

    public override void ProcessIngredient() {

        base.ProcessIngredient();

        //if done, spawn choppable scene
        if (currStep >= maxStep && ingAdded.GetChoppedScene != null) {
            SpawnNewIngredient(ingAdded.GetChoppedScene);
            ResetProgressUI();
            return;
        }
    }


}
