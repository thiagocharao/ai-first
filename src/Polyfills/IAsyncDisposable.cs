// Polyfill for IAsyncDisposable in netstandard2.0
#if NETSTANDARD2_0
namespace System;

public interface IAsyncDisposable
{
    System.Threading.Tasks.ValueTask DisposeAsync();
}
#endif
