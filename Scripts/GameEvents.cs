using Godot;
using System;
using System.Collections.Generic;

public partial class GameEvents : Node {
    public static event Action<List<RecipieR>> OnCookingRecipiesChosen;
    //public static event Action On

    public static void RaiseCookingRecipiesChosen(List<RecipieR> recipies)
      => OnCookingRecipiesChosen?.Invoke(recipies);


}
