using Godot;

namespace WaifuSurvivors;

public partial class ActorCompMagnet : Area2D {
	[Export]
	public ActorPlayer _actor;

	public void _OnAreaEntered(Node otherBody) {
		if (otherBody is not ExperienceGem experienceGem) {
			return;
		}

		experienceGem.Collect(_actor); // Start process to move towards player.
	}
}