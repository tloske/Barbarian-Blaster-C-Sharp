using Godot;
using System;

public partial class Bank : MarginContainer
{
    [Export] public int StartingGold { get; set; } = 150;
    private Label GoldLabel { get; set; }
    public int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = Math.Max(value, 0);
            GoldLabel.Text = $"Gold: {gold}";
        }
    }

    public override void _Ready()
    {
        base._Ready();
        GoldLabel = GetNode<Label>("Label");
        Gold = StartingGold;
    }
}
