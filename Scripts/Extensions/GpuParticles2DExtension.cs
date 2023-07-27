using Godot;

namespace WaifuSurvivors.Scripts;

public static class GpuParticles2DExtension {
	public static void Play(this GpuParticles2D particles) {
		particles.Emitting = true;
	}
}