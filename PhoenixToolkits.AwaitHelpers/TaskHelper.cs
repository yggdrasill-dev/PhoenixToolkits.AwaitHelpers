namespace Valhalla.Awaitables;

public static class TaskHelper
{
	public static NewThreadAwaitable NewThread() => NewThreadAwaitable.Default;
}
