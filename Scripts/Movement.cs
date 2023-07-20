using Godot;

namespace WaifuSurvivors.Scripts;

public class Movement {
	public readonly RigidBody2D _characterBody;

	public Vector2 _velocity;

	public Vector2 Velocity => _velocity;

	public Movement(RigidBody2D characterBody) {
		_characterBody = characterBody;
	}

	public bool PhysicsMove(Vector2 targetVelocity, float friction, double delta) {
		var velocityChange = new Vector2 {
			X = (targetVelocity.X - _velocity.X) * friction * (float)delta,
			Y = (targetVelocity.Y - _velocity.Y) * friction * (float)delta
		};

		_velocity += velocityChange;

		var isZeroX = Mathf.IsEqualApprox(_velocity.X, 0f, 0.01f);
		if (isZeroX) {
			_velocity.X = 0f;
		}

		var isZeroY = Mathf.IsEqualApprox(_velocity.Y, 0, 0.01f);
		if (isZeroY) {
			_velocity.Y = 0f;
		}

		var isStanding = _velocity.Equals(Vector2.Zero);
		if (isStanding) {
			_velocity = Vector2.Zero;
		}

		return !isStanding;
	}
}