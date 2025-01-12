using Godot;
using System;
using Godot.Collections;

public partial class RayPickerCamera : Camera3D {

    [Export] private RayCast3D raycast;
    [Export] private PlayerMovement playerMovement;
    [Export] private Player player;

    public override void _Ready() {
        //so we don't collide with ourself
        raycast.AddExceptionRid(playerMovement.GetRid());
    }

    public override void _Process(double delta) {
        Vector2 mousePos = GetViewport().GetMousePosition();
        raycast.TargetPosition = ProjectLocalRayNormal(mousePos) * 100;
        raycast.ForceRaycastUpdate();

        //if colliding with an interactable item, interact
        if (Input.IsActionJustPressed("interact") && raycast.IsColliding() && raycast.GetCollider() is IInteractable interactable) {
            interactable.OnInteract(player);
        }



    }
}
