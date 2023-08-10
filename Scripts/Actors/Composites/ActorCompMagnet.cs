using Godot;
using WaifuSurvivors.Extensions;

namespace WaifuSurvivors;

public partial class ActorCompMagnet : Area2D {
	public void _OnAreaEntered(Node otherBody) {
		if (otherBody is not ExperienceGem experienceGem) {
			return;
		}

		experienceGem.Remove(); // Start process to move towards player.

		ControllerExperience.instance.PlayExperienceSound();
	}
}