using Godot;
using System;
using Godot.Collections;

public partial class PlayerMovement : CharacterBody3D {

    [Export] private Array<RayCast3D> raycasts = new Array<RayCast3D>();

    [ExportGroup("Movement")]
    [Export] private float normalSpeed = 600;
    [Export] private float runSpeed = 1200;
    [Export] private float rotationSpeed = 8f;
    [Export] private float jumpHeight = 3;
    [Export] private float apexDuration = 0.5f;
    [Export] private float fallGravity = 45;
    [Export] private Node3D meshRoot;

    [ExportGroup("Camera")]
    [Export] private SpringArm3D pitchPivot;
    [Export] private Node3D twistPivot;
    [Export] private float mouseSensitivity = 0.5f;
    [Export] private float zoomSensitivity = 1;
    [Export] private float zoomSpeed = 10;
    [Export] private float zoomMin = 2;
    [Export] private float zoomMax = 11;

    private float jumpGravity;
    private float zoom = 8;
    private float speed;
    private float twistInput;
    private float pitchInput;
    private Vector3 vel;

    public override void _Ready() {
        jumpGravity = fallGravity;
        pitchPivot.SpringLength = zoom = 8;
        speed = normalSpeed;
        pitchPivot.AddExcludedObject(GetRid());
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta) {
        /*
        //interaction goes here so we don't have more than one physics process
        foreach (RayCast3D raycast in raycasts)
            if (raycast.IsColliding() && raycast.GetCollider() is IInteractable interactable) {
                GD.PrintS("efkef");
                if (Input.IsActionJustPressed("interact"))
                    interactable.OnInteract((Node3D)GetParent());
                else if (Input.IsActionJustPressed("altInteract")) {
                    GD.PrintS("interacting alt");
                    interactable.OnAltInteract((Node3D)GetParent());
                }
            }
        */
        Misc();
        vel = Velocity;

        //gravity
        if (!IsOnFloor()) {
            if (vel.Y >= 0)
                vel.Y -= jumpGravity * (float)delta;
            else
                vel.Y -= fallGravity * (float)delta;
        }

        //jump
        if (Input.IsActionPressed("jump") && IsOnFloor()) {
            vel.Y = jumpHeight * 2 / apexDuration;
            jumpGravity = vel.Y / apexDuration;
        }

        //zoom
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        pitchPivot.SpringLength = Mathf.MoveToward(pitchPivot.SpringLength, zoom, zoomSpeed * (float)delta);

        //run
        if (Input.IsActionPressed("run")) {
            speed = runSpeed;
        } else {
            speed = normalSpeed;
        }

        //movement
        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Vector3 moveDir = (twistPivot.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        if (moveDir != Vector3.Zero) {
            vel.X = moveDir.X * speed * (float)delta;
            vel.Z = moveDir.Z * speed * (float)delta;

            float targetAngle = Mathf.Atan2(moveDir.X, moveDir.Z) - Rotation.Y;
            meshRoot.Rotation = new Vector3(
                meshRoot.Rotation.X,
                Mathf.LerpAngle(meshRoot.Rotation.Y, targetAngle, rotationSpeed * (float)delta),
                meshRoot.Rotation.Z
            );
        } else {
            vel.X = Mathf.MoveToward(vel.X, 0, speed);
            vel.Z = Mathf.MoveToward(vel.Z, 0, speed);
        }

        Velocity = vel;
        MoveAndSlide();
        twistInput = pitchInput = 0;
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseMotion mouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured) {
            twistInput = -mouseMotion.Relative.X * mouseSensitivity;
            pitchInput = -mouseMotion.Relative.Y * mouseSensitivity;

            twistPivot.RotateY(Mathf.DegToRad(twistInput));
            pitchPivot.RotateX(Mathf.DegToRad(pitchInput));
            pitchPivot.Rotation = new Vector3(
                Mathf.Clamp(pitchPivot.Rotation.X, Mathf.DegToRad(-30), Mathf.DegToRad(90)),
                pitchPivot.Rotation.Y,
                pitchPivot.Rotation.Z
            );
        }

        if (@event.IsActionPressed("zoomIn"))
            zoom -= zoomSensitivity;
        else if (@event.IsActionPressed("zoomOut"))
            zoom += zoomSensitivity;
    }

    private void Misc() {
        if (Input.IsActionJustPressed("quit"))
            GetTree().Quit();
        if (Input.IsActionJustPressed("alt")) {
            if (Input.MouseMode == Input.MouseModeEnum.Visible)
                Input.MouseMode = Input.MouseModeEnum.Captured;
            else
                Input.MouseMode = Input.MouseModeEnum.Visible;
        }

    }
}
