using Godot;

namespace WaifuSurvivors.Scripts;

public static class NodeExtension {
	public static void Remove(this Node node) {
		node.GetParent().RemoveChild(node);
		node.QueueFree();
	}
}