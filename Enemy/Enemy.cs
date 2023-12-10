using Godot;
using System;

public partial class Enemy : PathFollow3D
{
	[Export] public float MoveSpeed { get; set; } = 5f;
	[Export] public int GoldValue { get; set; } = 15;
	public Base Base { get; set; }
	public AnimationPlayer AnimationPlayer { get; set; }

	[Export] public float MaxHealth { get; set; } = 2.0f;
	private float _currentHealth;
	public float CurrentHealth
	{
		get => _currentHealth;
		set
		{
			if (value < _currentHealth)
			{
				if (AnimationPlayer.IsPlaying()) { AnimationPlayer.Stop(); }
				AnimationPlayer.Play("TakeDamage");
			}
			_currentHealth = value;
			if (_currentHealth < 1)
			{
				(GetTree().GetFirstNodeInGroup("bank") as Bank).Gold += GoldValue;
				QueueFree();
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Base = GetTree().GetFirstNodeInGroup("base") as Base;
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_currentHealth = MaxHealth;
		GetNode<MeshInstance3D>("Barbarian/Armature/Skeleton3D/Barbarian_M_02/Highlight").Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += (float)delta * MoveSpeed;

		if (ProgressRatio >= 0.98)
		{
			Base.TakeDamage();
			QueueFree();
		}
	}
}
