using Godot;
using System;

public partial class VictoryLayer : CanvasLayer
{
    public TextureRect Star1 { get; set; }
    public TextureRect Star2 { get; set; }
    public TextureRect Star3 { get; set; }
    public Label HealthLabel { get; set; }
    public Label GoldLabel { get; set; }
    public Base Base { get; set; }
    public Bank Bank { get; set; }

    public override void _Ready()
    {
        Star1 = GetNode<TextureRect>("%Star1");
        Star2 = GetNode<TextureRect>("%Star2");
        Star3 = GetNode<TextureRect>("%Star3");
        HealthLabel = GetNode<Label>("%HealthLabel");
        GoldLabel = GetNode<Label>("%GoldLabel");
        Base = (Base)GetTree().GetFirstNodeInGroup("base");
        Bank = (Bank)GetTree().GetFirstNodeInGroup("bank");
    }

    public void Victory()
    {
        Show();
        if (Base.MaxHealth == Base.CurrentHealth)
        {
            Star2.Modulate = Colors.White;
            HealthLabel.Show();
        }
        if (Bank.Gold >= 500)
        {
            Star3.Modulate = Colors.White;
            GoldLabel.Show();
        }
    }

    public void OnRetryButtonPressed() => GetTree().ReloadCurrentScene();
    public void OnQuitButtonPressed() => GetTree().Quit();
}
