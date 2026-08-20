using FileSignatures;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Helpers;

public static class FileService
{
    public static async Task<(string Base64, string ContentType, bool HasInvalidFormat)> ConvertToBase64Async0(IFormFile file)
    {
        if (file == null || file.Length == 0) return (null, null, false);
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
        string base64 = Convert.ToBase64String(fileBytes);
        string contentType = file.ContentType; // e.g. "image/png", "application/pdf"
        return (base64, contentType, false);
    }

    public static async Task<(string? Base64, string? ContentType, bool HasInvalidFormat)> ConvertToBase64Async(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return (null, null, false);

        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        ms.Position = 0;

        var inspector = new FileFormatInspector();

        var format = inspector.DetermineFileFormat(ms);

        if (format is null)
            return (null, null, true);

        var allowedTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "application/pdf",
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp",
            "image/bmp",
            "image/svg+xml",
            "image/tiff"
        };

        if (!allowedTypes.Contains(format.MediaType))
            return (null, null, true);

        /*var isValid =
            format.MediaType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase) ||
            format.MediaType.StartsWith("image/", StringComparison.OrdinalIgnoreCase);

        if (!isValid)
            return (null, null, true);*/

        ms.Position = 0;

        var bytes = ms.ToArray();

        return (
            Convert.ToBase64String(bytes),
            format.MediaType, // نوع واقعی فایل، نه چیزی که کلاینت ارسال کرده
            false
        );
    }
}
