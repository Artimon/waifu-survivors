﻿using Godot;

namespace WaifuSurvivors;

public partial class ControllerExperience : Node2D {
	public static ControllerExperience instance;

	[Export]
	public LevelContainer _levelContainer;

	[Export]
	public PackedScene _experienceGemPrefab;

	public override void _EnterTree() {
		instance = this;
	}

	public void CreateExperience(ActorMob actor) {
		var experienceGem = _levelContainer.Append<ExperienceGem>(_experienceGemPrefab, actor.GlobalPosition);

		experienceGem.experience = 5f; // Create via resource config.
	}

	public override void _ExitTree() {
		instance = null;
	}
}