using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

public partial class CookBookUI : Control {

    [Export] private Array<Label> recipieBlockLabels;

    private List<RecipieR> recipies;

    public override void _Ready() {
        GameEvents.OnCookingRecipiesChosen += HandleCookingRecipiesChosen;
    }

    private void HandleCookingRecipiesChosen(List<RecipieR> recipies) {
        //fill in label information
        GD.PrintS("Recipie count", recipies.Count);
        for (int i = 0; i < recipieBlockLabels.Count; i++) {

            if (i >= recipies.Count) {
                ((Control)recipieBlockLabels[i].GetParent()).Visible = false;
                continue;
            }
            recipieBlockLabels[i].Text = recipies[i].GetRecipieName;
        }
    }

    public void HandleRecipieButtonPressed(int buttonNum) {
        //make UI appear and follow directions
        GD.PrintS("Button", buttonNum, "works");
    }

    public void HandleXButtonPressed() {
        Visible = false;
    }

}
