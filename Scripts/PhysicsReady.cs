using Godot;

namespace WaifuSurvivors;

public partial class PhysicsReady : Node2D {
	public int _physicsFrames;

	[Signal]
	public delegate void OnPhysicsReadyEventHandler();

	public override void _PhysicsProcess(double delta) {
		_physicsFrames += 1;
		if (_physicsFrames < 2) {
			return;
		}

		EmitSignal(SignalName.OnPhysicsReady);

		SetPhysicsProcess(false);
	}
}