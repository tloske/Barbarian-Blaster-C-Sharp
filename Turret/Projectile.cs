using Godot;
using System;

public partial class Projectile : Area3D
{
	public Vector3 Direction { get; set; } = Vector3.Forward;

	[Export] public float Speed { get; set; } = 30.0f;


}
