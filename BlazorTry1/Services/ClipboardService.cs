using Microsoft.JSInterop;

namespace BlazorTry1.Services;

public sealed class ClipboardService(IJSRuntime jsRuntime) {
    public ValueTask<string> ReadTextAsync() => jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
    public ValueTask WriteTextAsync(string text) => jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    public async Task CopyToClipboard(string? text) {
        if (text is null) return;
        try {
            await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        } catch {
            Console.WriteLine($"Failed to write to clipboard: {text}");
        }
    }
}