using Godot;
using System;
using Godot.Collections;

public partial class RayPickerCamera : Camera3D {

    [Export] private RayCast3D raycast;
    [Export] private PlayerMovement playerMovement;
    [Export] private Player player;

    [Export] private Material whiteMaterial;

    private Material previousMaterial;
    private IInteractable currInteractable;

    public override void _Ready() {
        //so we don't collide with ourself
        raycast.AddExceptionRid(playerMovement.GetRid());
    }

    public override void _Process(double delta) {
        Vector2 mousePos = GetViewport().GetMousePosition();
        raycast.TargetPosition = ProjectLocalRayNormal(mousePos) * 100;
        raycast.ForceRaycastUpdate();

        //if colliding with an interactable item, interact
        if (raycast.IsColliding() && raycast.GetCollider() is IInteractable interactable) {
            currInteractable?.SetMaterial(previousMaterial);
            currInteractable = interactable;
            //bool hover = typeof(IInteractable).IsDefined(typeof(HoverableAttribute), false);
            previousMaterial = interactable.GetMaterial();
            interactable.SetMaterial(whiteMaterial);

            if (Input.IsActionJustPressed("interact"))
                interactable.OnInteract(player);
            else if (Input.IsActionJustPressed("altInteract")) {
                interactable.OnAltInteract((Node3D)GetParent());
            }
        } else {
            currInteractable?.SetMaterial(previousMaterial);
        }



    }
}
