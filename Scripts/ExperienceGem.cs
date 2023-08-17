using System;
using Godot;
using WaifuSurvivors.Extensions;

namespace WaifuSurvivors;

public partial class ExperienceGem : Node2D {
	public const double MoveSpeed = 128d;
	public const float MaxAngle = Mathf.Pi * 1.5f;
	public const float CollectRadiusSquared = 4f * 4f;

	[Export]
	public float experience;

	public Action onCollect;

	public ActorPlayer _actor;

	public double _angle;

	public override void _Ready() {
		SetProcess(false);
	}

	public override void _Process(double delta) {
		var direction = _actor.GlobalPosition - GlobalPosition;
		if (direction.LengthSquared() < CollectRadiusSquared) {
			_ActuallyCollect();

			return;
		}

		var speed = MoveSpeed;

		if (_angle < MaxAngle) {
			_angle += 16d * delta;
			speed *= -Mathf.Sin(_angle);
		}

		GlobalPosition += direction.Normalized() * (float)(speed * delta);
	}

	public void Collect(ActorPlayer actor) {
		_actor = actor;

		SetProcess(true);
	}

	public void _ActuallyCollect() {
		onCollect?.Invoke();

		this.Remove();
	}
}