using System.Runtime.CompilerServices;

namespace Valhalla.Awaitables;

public readonly struct CancellationTokenAwaitable
{
	private readonly CancellationToken m_CancellationToken;
	private readonly bool m_ContinueOnCapturedContext;
	private readonly bool m_ThrowCanceledException;

	public CancellationTokenAwaitable(
		bool continueOnCapturedContext,
		bool throwCanceledException,
		CancellationToken cancellationToken)
	{
		m_CancellationToken = cancellationToken;
		m_ContinueOnCapturedContext = continueOnCapturedContext;
		m_ThrowCanceledException = throwCanceledException;
	}

	public CancellationTokenAwaitable ConfigureAwait(bool continueOnCapturedContext)
		=> new(
			continueOnCapturedContext,
			m_ThrowCanceledException,
			m_CancellationToken);

	public CancellationTokenAwaitable ThrowIfCancel()
		=> new(
			m_ContinueOnCapturedContext,
			true,
			m_CancellationToken);

	public CancellationTokenAwaiter GetAwaiter()
		=> new(
			m_ContinueOnCapturedContext,
			m_ThrowCanceledException,
			m_CancellationToken);

	public readonly struct CancellationTokenAwaiter : ICriticalNotifyCompletion
	{
		private readonly CancellationToken m_CancellationToken;
		private readonly bool m_ContinueOnCapturedContext;
		private readonly bool m_ThrowCanceledException;

		public bool IsCompleted => m_CancellationToken.IsCancellationRequested;

		public CancellationTokenAwaiter(
			bool continueOnCapturedContext,
			bool throwCanceledException,
			CancellationToken cancellationToken)
		{
			m_CancellationToken = cancellationToken;
			m_ContinueOnCapturedContext = continueOnCapturedContext;
			m_ThrowCanceledException = throwCanceledException;
		}

		public void GetResult()
		{
			if (!IsCompleted)
				throw new InvalidOperationException("The cancellation token has not yet been cancelled.");

			if (m_ThrowCanceledException)
				throw new OperationCanceledException();
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
