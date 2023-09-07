using Godot;

namespace WaifuSurvivors;

public partial class ControllerDamageNumbers : Node2D {
	public static ControllerDamageNumbers instance;

	[Export]
	public PackedScene _damageNumberPrefab;

	public override void _EnterTree() {
		instance = this;
	}

	public void Create(float damage, Vector2 position) {
		var visualDamage = (int)Mathf.Max(1f, damage);

		LevelContainer.Instance
			.Append<EffectDamageNumber>(_damageNumberPrefab, position)
			.SetNumber(visualDamage);
	}
}