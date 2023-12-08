using System.ComponentModel;
using Valhalla.Awaitables;

namespace System.Threading;

public static class ThreadingAwaitExtensions
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static CancellationTokenAwaitable.CancellationTokenAwaiter GetAwaiter(this CancellationToken ct)
		=> new CancellationTokenAwaitable(true, false, ct).GetAwaiter();

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static CancellationTokenAwaitable ConfigureAwait(this CancellationToken ct, bool continueOnCapturedContext)
		=> new(continueOnCapturedContext, false, ct);

	public static CancellationTokenAwaitable ThrowIfCancel(this CancellationToken ct)
		=> new(true, true, ct);
}
