using Godot;
using System;

public partial class Projectile : Area3D
{
	public Vector3 Direction { get; set; } = Vector3.Forward;

	[Export] public float Speed { get; set; } = 30.0f;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Position += Direction * Speed * (float)delta;
	}

	public void OnVisibleOnScreenNotifierScreenExited() => QueueFree();

	public void OnAreaEntered(Area3D area)
	{
		if (area.IsInGroup("enemy_area"))
		{
			(area.Owner as Enemy).CurrentHealth--;
			QueueFree();
		}
	}
}
