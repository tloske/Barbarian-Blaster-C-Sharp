using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export] public float MoveSpeed { get; set; } = 5f;
	public Base Base { get; set; }

	[Export] public int MaxHealth { get; set; } = 50;
	private int _currentHealth;
	public int CurrentHealth
	{
		get
		{
			return _currentHealth;
		}
		set
		{
			_currentHealth = value;
			if (_currentHealth < 1)
			{
				QueueFree();
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Base = GetTree().GetFirstNodeInGroup("base") as Base;
		_currentHealth = MaxHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += (float)delta * MoveSpeed;
		if (ProgressRatio >= 1)
		{
			Base.TakeDamage();
			ProgressRatio = 0;
		}
	}
}
