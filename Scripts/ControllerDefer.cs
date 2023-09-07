using System;
using Godot;

namespace WaifuSurvivors;

public partial class ControllerDefer : Node2D {
	public static event Action Delay;

	public override void _Process(double _) {
		if (Delay == null) {
			return;
		}

		Delay();
		Delay = null;
	}

	public override void _ExitTree() {
		Delay = null;
	}
}