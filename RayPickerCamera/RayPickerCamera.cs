using Godot;
using System;

public partial class RayPickerCamera : Camera3D
{
	public RayCast3D Ray { get; set; }
	[Export] public TurretManager TurretManager { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Ray = GetNode<RayCast3D>("RayCast3D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePosition = GetViewport().GetMousePosition();
		Vector3 target = ProjectLocalRayNormal(mousePosition);
		Ray.TargetPosition = target * 100;
		Ray.ForceRaycastUpdate();

		if (Ray.IsColliding() && Ray.GetCollider() is GridMap gridMap)
		{
			Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
			if (Input.IsActionPressed("click"))
			{
				Vector3 collisionPoint = Ray.GetCollisionPoint();
				Vector3I cell = gridMap.LocalToMap(collisionPoint);
				if (gridMap.GetCellItem(cell) == 0)
				{
					gridMap.SetCellItem(cell, 1);
					TurretManager.BuildTurret(gridMap.MapToLocal(cell));
				}
			}
		}
		else
		{
			Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
		}
	}
}
