using Godot;
using WaifuSurvivors.Extensions;

namespace WaifuSurvivors;

public partial class WeaponLoveChainSlash : PhysicsReady {
	[Export]
	public AudioStreamPlayer _audioPlayer;

	[Export]
	public AnimatedSprite2D _animation;

	[Export]
	public GpuParticles2D _particlesHearts;

	[Export]
	public GpuParticles2D _particlesSparks;

	[Export]
	public AttackArea _attackArea;

	public void _OnPhysicsReady() {
		_audioPlayer.Play();
		_animation.Play();
		_particlesHearts.Play();
		_particlesSparks.Play();

		var mobs = _attackArea.GetMobs();

		GD.Print($"Mobs detected: {mobs.Length}");

		foreach (var actorMob in mobs) {
			actorMob.ApplyDamage(2f);
		}
	}
}
