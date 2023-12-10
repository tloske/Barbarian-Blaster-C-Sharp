using Godot;
using System;

public partial class Base : Node3D
{
	[Export] public int MaxHealth { get; set; } = 5;

	private int _currentHealth;
	public int CurrentHealth
	{
		get => _currentHealth;
		set
		{
			_currentHealth = value;
			HealthBar.Value = (float)_currentHealth / MaxHealth * 100;
			HealthLabel.Text = $"{_currentHealth}/{MaxHealth}";
			HealthLabel.Modulate = Colors.Red.Lerp(Colors.White, (float)_currentHealth / MaxHealth);
			if (_currentHealth < 1)
			{
				GetTree().ReloadCurrentScene();
			}
		}
	}

	public TextureProgressBar HealthBar { get; set; }
	public Label HealthLabel { get; set; }

	public override void _Ready()
	{
		base._Ready();
		HealthBar = GetNode<TextureProgressBar>("Sprite3D/SubViewport/HealthBar");
		HealthLabel = GetNode<Label>("Sprite3D/SubViewport/HealthBar/HealthLabel");
		CurrentHealth = MaxHealth;
	}

	public void TakeDamage() => CurrentHealth--;
}
