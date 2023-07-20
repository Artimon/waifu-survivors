using Godot;

namespace WaifuSurvivors.Scripts;

public partial class ActorPlayer : ActorBase {
	[Export]
	public AnimatedSprite2D _animatedSprite;

	public Vector2 InputDirection => Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

	public override void _Ready() {
		BodyEntered += _OnBodyEntered;
		BodyExited += _OnBodyExited;
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

	public void ApplyDamage(float attack) {
		GD.Print("Applying damage: " + attack);
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

