using System;
using Godot;
using WaifuSurvivors.Extensions;

namespace WaifuSurvivors;

public partial class ExperienceGem : Node2D {
	[Export]
	public float experience;

	public Action onCollect;

	public void Collect() {
		onCollect?.Invoke();

		this.Remove();
	}
}