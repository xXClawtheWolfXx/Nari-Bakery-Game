using Godot;
using System;

public partial class UICooking : Control {
    [Export] private Label orderLabel;
    [Export] private TextureRect ingredientImage;

    public void LoadNextImage(string orderText, Texture2D ingredientSprite) {
        orderLabel.Text = orderText;
        ingredientImage.Texture = ingredientSprite;
    }

}
