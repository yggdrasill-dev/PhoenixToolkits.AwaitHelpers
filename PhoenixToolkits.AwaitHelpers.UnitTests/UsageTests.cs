using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valhalla.Awaitables;
using Xunit;

namespace PhoenixToolkits.AwaitHelpers.UnitTests;

public class UsageTests
{
	[Fact(Timeout = 1000)]
	public async Task CancellationToken_Usage()
	{
		// Arrange
		using var cts = new CancellationTokenSource();
		var sut = cts.Token;

		cts.Cancel();

		// Act
		await sut;
	}

	[Fact(Timeout = 1000)]
	public async Task CancellationToken_Usage_ConfigureAwait()
	{
		// Arrange
		using var cts = new CancellationTokenSource();
		var sut = cts.Token;

		cts.Cancel();

		// Act
		await sut.ConfigureAwait(false);
	}

	[Fact]
	public async Task CancellationToken_Usage_ThrowIfCancel()
	{
		// Arrange
		using var cts = new CancellationTokenSource();
		var sut = cts.Token;

		cts.Cancel();

		// Act
		_ = await Assert.ThrowsAsync<OperationCanceledException>(async () => await sut.ThrowIfCancel());
	}

	[Fact]
	public async Task CancellationToken_Usage_ThrowIfCancel_ConfigureAwait()
	{
		// Arrange
		using var cts = new CancellationTokenSource();
		var sut = cts.Token;

		cts.Cancel();

		// Act
		_ = await Assert.ThrowsAsync<OperationCanceledException>(
			async () => await sut.ThrowIfCancel().ConfigureAwait(false));
	}

	[Fact]
	public async void NewThread_Usage()
	{
		await TaskHelper.NewThread();
	}
}
