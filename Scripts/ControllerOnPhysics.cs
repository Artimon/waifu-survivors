using System;
using Godot;

namespace WaifuSurvivors;

public partial class ControllerOnPhysics : Node2D {
	public static event Action Defer;

	public override void _Process(double _) {
		if (Defer == null) {
			return;
		}

		Defer();
		Defer = null;
	}

	public override void _ExitTree() {
		Defer = null;
	}
}