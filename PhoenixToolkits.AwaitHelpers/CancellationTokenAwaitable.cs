using System.Runtime.CompilerServices;

namespace Valhalla.Awaitables;

public readonly struct CancellationTokenAwaitable
{
	private readonly CancellationToken m_CancellationToken;
	private readonly bool m_ContinueOnCapturedContext;

	public CancellationTokenAwaitable(bool continueOnCapturedContext, CancellationToken cancellationToken)
	{
		m_CancellationToken = cancellationToken;
		m_ContinueOnCapturedContext = continueOnCapturedContext;
	}

	public CancellationTokenAwaitable ConfigureAwait(bool continueOnCapturedContext)
		=> new(continueOnCapturedContext, m_CancellationToken);

	public CancellationTokenAwaiter GetAwaiter()
		=> new(m_ContinueOnCapturedContext, m_CancellationToken);

	public readonly struct CancellationTokenAwaiter : ICriticalNotifyCompletion
	{
		private readonly CancellationToken m_CancellationToken;
		private readonly bool m_ContinueOnCapturedContext;

		public CancellationTokenAwaiter(bool continueOnCapturedContext, CancellationToken cancellationToken)
		{
			m_CancellationToken = cancellationToken;
			m_ContinueOnCapturedContext = continueOnCapturedContext;
		}

		public void OnCompleted(Action continuation)
			=> QueueContinuation(continuation);

		public void UnsafeOnCompleted(Action continuation)
			=> QueueContinuation(continuation);

		private void QueueContinuation(Action continuation)
		{
			ArgumentNullException.ThrowIfNull(continuation);

			m_CancellationToken.Register(continuation, m_ContinueOnCapturedContext);
		}
	}
}
