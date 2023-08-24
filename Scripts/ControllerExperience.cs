using Godot;

namespace WaifuSurvivors;

public partial class ControllerExperience : Node2D {
	public const int MaxGemAmount = 10;

	public static ControllerExperience instance;

	[Export]
	public LevelContainer _levelContainer;

	[Export]
	public PackedScene _experienceGemPrefab;

	[Export]
	public AudioStreamLimiter _experienceAudio;

	public int _gemAmount;

	public float _experienceStack;

	public override void _EnterTree() {
		instance = this;
	}

	public void CreateExperience(ActorMob actor) {
		var experience = 5f; // @TODO Read from mob resource config.

		if (_gemAmount >= MaxGemAmount) {
			_experienceStack += experience;

			return;
		}

		var experienceGem = _levelContainer.Append<ExperienceGem>(_experienceGemPrefab, actor.GlobalPosition);

		experienceGem.experience = experience + _experienceStack;
		experienceGem.onCollect = _OnCollectGem;

		_experienceStack = 0;

		_gemAmount += 1;
	}

	public void _OnCollectGem() {
		_gemAmount -= 1;
		_experienceAudio.Play();
	}

	public override void _ExitTree() {
		instance = null;
	}
}