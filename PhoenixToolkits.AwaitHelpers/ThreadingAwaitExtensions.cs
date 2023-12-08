using System.ComponentModel;
using Valhalla.Awaitables;

namespace System.Threading;

public static class ThreadingAwaitExtensions
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static CancellationTokenAwaitable.CancellationTokenAwaiter GetAwaiter(this CancellationToken ct)
		=> new CancellationTokenAwaitable(true, ct).GetAwaiter();

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static CancellationTokenAwaitable ConfigureAwait(this CancellationToken ct, bool continueOnCapturedContext)
		=> new(continueOnCapturedContext, ct);
}
