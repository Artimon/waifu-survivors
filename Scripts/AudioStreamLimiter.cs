using Godot;

namespace WaifuSurvivors;

public partial class AudioStreamLimiter : Node2D {
	[Export]
	public AudioStreamPlayer[] _audioStreamPlayers;

	public void Play() {
		foreach (var audioStreamPlayer in _audioStreamPlayers) {
			if (audioStreamPlayer.Playing) {
				continue;
			}

			audioStreamPlayer.Play();

			return;
		}
	}
}