using Godot;
using WaifuSurvivors.Extensions;

namespace WaifuSurvivors;

public partial class EffectDamageNumber : Node2D {
	[Export]
	public PixelPerfectNumber _pixelPerfectNumber;

	[Export]
	public AnimationPlayer _animationPlayer;

	public override void _Ready() {
		_animationPlayer.Play("FlyOff");
	}

	public void SetNumber(int damage) {
		_pixelPerfectNumber.SetNumber(damage);
	}

	public void _OnAnimationFinished(string _) {
		this.Remove();
	}
}