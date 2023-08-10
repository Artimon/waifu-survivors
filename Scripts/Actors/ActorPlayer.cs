using Godot;

namespace WaifuSurvivors;

public partial class ActorPlayer : ActorBase {
	public float _hits;
	public float _maxHits;

	[Export]
	public WaifuConfig _config;

	[Export]
	public AnimatedSprite2D _animatedSprite;

	[Export]
	public ActorCompHitsBar _hitsBar;

	public Vector2 InputDirection => Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

	public override void _EnterTree() {
		_hits = _maxHits = _config.hits;
	}

	public override void _Ready() {
		BodyEntered += _OnBodyEntered;
		BodyExited += _OnBodyExited;

		_UpdateHitsBar();
	}

	public override void _PhysicsProcess(double delta) {
		var targetVelocity = InputDirection * _speed;

		var isMoving = _movement.PhysicsMove(targetVelocity, Friction, delta);
		var isStopping = targetVelocity.Equals(Vector2.Zero);

		if (isStopping) {
			_StopAnimation();
		}
		else {
			_UpdateAnimation();
			_UpdateDisplayDirection();
		}
	}

	public void _StopAnimation() {
		_animatedSprite.Animation = "Idle";
	}

	public void _UpdateAnimation() {
		_animatedSprite.Animation = "Walk";
	}

	public void _UpdateHitsBar() {
		_hitsBar.SetHits(_hits, _maxHits);
	}

	public void ApplyDamage(float attack) {
		GD.Print("Applying damage: " + attack);

		_hits -= attack;
		_UpdateHitsBar();
	}

	public void _OnBodyEntered(Node otherBody) {
		if (otherBody is not ActorMob actor) {
			return;
		}

		actor.OnTouchPlayer(this);
	}

	public void _OnBodyExited(Node otherBody) {
		if (otherBody is not ActorMob actor) {
			return;
		}

		actor.OnUntouchPlayer();
	}
}

