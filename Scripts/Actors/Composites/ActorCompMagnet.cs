using Godot;

namespace WaifuSurvivors;

public partial class ActorCompMagnet : Area2D {
	public void _OnAreaEntered(Node otherBody) {
		if (otherBody is not ExperienceGem experienceGem) {
			return;
		}

		experienceGem.Collect(); // Start process to move towards player.
	}
}