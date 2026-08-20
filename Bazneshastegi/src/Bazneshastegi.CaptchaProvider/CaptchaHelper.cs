using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Bazneshastegi.CaptchaProvider;

public static class CaptchaHelper
{
    public static MemoryStream GenerateCaptchaImage(
        int Length,
        int width,
        int height,
        int circleCount,
        int lineCount)
    {
        string captchaText = RandomStringGenerator.Generate(Length,
            RandomStringGenerator.PasswordGeneratorOptions.UseLowercaseChars
            | RandomStringGenerator.PasswordGeneratorOptions.UseUppercaseChars
            | RandomStringGenerator.PasswordGeneratorOptions.UseDigits);

        return GenerateCaptchaImage(captchaText, width, height, circleCount, lineCount);
    }

    public static MemoryStream GenerateCaptchaImage(
       string captchaText,
       int width,
       int height,
       int circleCount,
       int lineCount,
       int noiseDots = 250)
    {
        var random = new Random();

        var image = new Image<Rgba32>(width, height, Rgba32.ParseHex("FFFFFF"));

        // Dynamically determine font size
        var fontFamily = SystemFonts.Get("Arial");
        float fontSize = height * 0.6f; // start proportional to height
        var font = fontFamily.CreateFont(fontSize, FontStyle.Bold);

        // Measure text and scale down if too wide
        var textOptions = new RichTextOptions(font)
        {
            Dpi = 72,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Origin = new PointF(width / 2f, height / 2f)
        };

        // Adjust font size so text fits image width
        var textSize = TextMeasurer.MeasureSize(captchaText, textOptions);
        if (textSize.Width > width * 0.9f)
        {
            fontSize *= (width * 0.9f) / textSize.Width;
            font = fontFamily.CreateFont(fontSize, FontStyle.Bold);
            textOptions = new RichTextOptions(font)
            {
                Dpi = 72,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Origin = new PointF(width / 2f, height / 2f)
            };
        }

        // Draw text centered
        var textColor = new Rgba32(
            (byte)random.Next(180, 230),  // R
            (byte)random.Next(180, 230),  // G
            (byte)random.Next(180, 230),  // B
            255                            // opaque
        );
        image.Mutate(ctx =>
        {
            ctx.DrawText(textOptions, captchaText, textColor);
        });

        // Draw random circles
        for (int i = 0; i < circleCount; i++)
        {
            int radius = random.Next(5, 15);
            int x = random.Next(0, width - radius);
            int y = random.Next(0, height - radius);
            var color = new Rgba32(
                (byte)random.Next(100, 255),
                (byte)random.Next(100, 255),
                (byte)random.Next(100, 255),
                128
            );
            image.Mutate(ctx => ctx.Fill(color, new EllipsePolygon(x + radius / 2f, y + radius / 2f, radius)));
        }

        // Draw random lines
        for (int i = 0; i < lineCount; i++)
        {
            var color = new Rgba32(
                (byte)random.Next(100, 255),
                (byte)random.Next(100, 255),
                (byte)random.Next(100, 255)
            );
            int x1 = Random.Shared.Next(width);
            int y1 = Random.Shared.Next(height);
            int x2 = Random.Shared.Next(width);
            int y2 = Random.Shared.Next(height);

            x2 = Math.Min(Math.Abs(x2 - x1), width / 2);
            y2 = Math.Min(Math.Abs(y2 - y1), height / 2);

            image.Mutate(ctx => ctx.DrawLine(color, 2, new SixLabors.ImageSharp.PointF[] { new(x1, y1), new(x2, y2) }));
        }

        // 5️⃣ Add random noise dots
        for (int i = 0; i < noiseDots; i++)
        {
            int x = random.Next(width);
            int y = random.Next(height);
            var color = new Rgba32(
                (byte)random.Next(100, 200),
                (byte)random.Next(100, 200),
                (byte)random.Next(100, 200),
                180
            );
            image[x, y] = color;
        }

        // 6️⃣ Optional light distortion (wave-like)
        image.Mutate(ctx =>
        {
            ctx.Dither();
            ctx.GaussianBlur(0.3f);
        });

        var ms = new MemoryStream();
        image.SaveAsPng(ms);
        ms.Position = 0;
        return ms;
    }

    public static MemoryStream GenerateCaptchaImage(
       string captchaText,
       int width = 150,
       int height = 50)
    {
        int lineCount = Math.Max(1, (width + height) / 80);
        int circleCount = Math.Max(1, (width * height) / 5000);
        int noiseDots = Math.Max(50, (width * height) / 25);

        return GenerateCaptchaImage(captchaText, width, height, circleCount, lineCount, noiseDots);
    }

    public static MemoryStream GenerateCaptchaImageUsingLittleDots(
        string captchaText,
        int width,
        int height,
        int circleRatio = 2500,
        int lineRatio = 40,
        int noiseDotsRatio = 50)
    {
        var circleCount = circleRatio == 0 ? 0 : (width * height) / circleRatio;
        var lineCount = lineRatio == 0 ? 0 : (width + height) / lineRatio;
        var noiseDots = noiseDotsRatio == 0 ? 0 : (width * height) / noiseDotsRatio;

        using var image = new Image<Rgba32>(width, height, Rgba32.ParseHex("FFFFFF"));

        var font = CreateAdjustedFont(captchaText, width, height);
        using var textMask = DrawMessyTextMask(captchaText, width, height, font);

        ScatterTextDots(image, textMask, height);
        if (circleCount > 0)
            DrawRandomCircles(image, width, height, circleCount);
        if (lineCount > 0)
            DrawRandomLines(image, width, height, lineCount);
        if (noiseDots > 0)
            AddNoiseDots(image, width, height, noiseDots);

        ApplyPostProcessing(image);

        var ms = new MemoryStream();
        image.SaveAsPng(ms);
        ms.Position = 0;
        return ms;
    }


    private static Font CreateAdjustedFont(string text, int width, int height)
    {
        var fontFamily = SystemFonts.Get("Arial");
        float fontSize = height * 0.6f;
        var font = fontFamily.CreateFont(fontSize, FontStyle.Bold);

        var textOptions = new RichTextOptions(font)
        {
            Dpi = 72,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Origin = new PointF(width / 2f, height / 2f)
        };

        var textSize = TextMeasurer.MeasureSize(text, textOptions);
        if (textSize.Width > width * 0.9f)
        {
            fontSize *= (width * 0.9f) / textSize.Width;
            font = fontFamily.CreateFont(fontSize, FontStyle.Regular);
        }

        return font;
    }
    private static Image<Rgba32> DrawMessyTextMask(string captchaText, int width, int height, Font font)
    {
        // Create mask with white background
        var textMask = new Image<Rgba32>(width, height, Rgba32.ParseHex("FFFFFF"));
        var maskColor = Color.Black;

        // Measure each glyph
        var glyphSizes = new List<(char ch, FontRectangle size)>();
        float totalWidth = 0f;
        foreach (var ch in captchaText)
        {
            var size = TextMeasurer.MeasureSize(ch.ToString(), new TextOptions(font));
            glyphSizes.Add((ch, size));
            totalWidth += size.Width;
        }

        // Starting X to center the whole string
        float startX = MathF.Max(0, (width - totalWidth) / 2f);
        float currentX = startX;
        float centerY = height / 2f;

        // For each glyph: create small image, draw glyph, rotate/scale, then composite
        foreach (var (ch, measured) in glyphSizes)
        {
            // Random transform parameters
            float angle = (float)(Random.Shared.NextDouble() - 0.5) * 50f; // ±25°
            float offsetY = (float)(Random.Shared.NextDouble() - 0.5) * height * 0.25f; // ±12.5% height
            float scale = 1f + (float)(Random.Shared.NextDouble() - 0.5) * 0.2f; // ±10%

            // Compute glyph canvas size (add padding for rotation)
            int glyphW = Math.Max(1, (int)Math.Ceiling(measured.Width * scale));
            int glyphH = Math.Max(1, (int)Math.Ceiling(measured.Height * scale));
            int padding = Math.Max(glyphW, glyphH); // enough to rotate without clipping
            int canvasW = glyphW + padding * 2;
            int canvasH = glyphH + padding * 2;

            // Create small image transparent background
            using var glyphImg = new Image<Rgba32>(canvasW, canvasH, new Rgba32(0, 0, 0, 0));

            // Draw the character centered in glyphImg
            var textOptions = new RichTextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Origin = new PointF(canvasW / 2f, canvasH / 2f),
                Dpi = 72
            };

            glyphImg.Mutate(g =>
            {
                // Draw black char on transparent canvas
                g.DrawText(textOptions, ch.ToString(), maskColor);
            });

            // Scale the glyph image if scale != 1
            if (Math.Abs(scale - 1f) > 0.001f)
            {
                int scaledW = (int)MathF.Max(1, canvasW * scale);
                int scaledH = (int)MathF.Max(1, canvasH * scale);
                glyphImg.Mutate(g => g.Resize(scaledW, scaledH));
            }

            // Rotate glyph image
            glyphImg.Mutate(g => g.Rotate(angle));

            // Calculate composite position on the main mask.
            // Use currentX + half glyph width as X center, centerY + offsetY as Y center.
            // Since glyphImg size changed after rotate/scale, compute top-left from center target.
            float targetCenterX = currentX + measured.Width / 2f; // measured.Width before scaling gives spacing
            float targetCenterY = centerY + offsetY;

            int drawX = (int)MathF.Round(targetCenterX - glyphImg.Width / 2f);
            int drawY = (int)MathF.Round(targetCenterY - glyphImg.Height / 2f);

            // Composite glyph image onto textMask. Use full opacity (1f).
            textMask.Mutate(ctx => ctx.DrawImage(glyphImg, new Point(drawX, drawY), 1f));

            // Advance currentX by original measured width scaled by scale (so spacing adapts)
            float spacing = measured.Width * 0.2f; // 20% spacing
            currentX += measured.Width * scale + spacing;
        }

        return textMask;
    }
    private static void ScatterTextDots(Image<Rgba32> image, Image<Rgba32> mask, int height)
    {
        int dotDensity = 3;
        int dotRadius = Math.Max(1, height / 100);

        for (int y = 0; y < mask.Height; y++)
        {
            for (int x = 0; x < mask.Width; x++)
            {
                var pixel = mask[x, y];
                if (pixel.R < 250 || pixel.G < 250 || pixel.B < 250)
                {
                    for (int i = 0; i < dotDensity; i++)
                    {
                        var dotColor = new Rgba32(
                            (byte)Random.Shared.Next(150, 230),
                            (byte)Random.Shared.Next(150, 230),
                            (byte)Random.Shared.Next(150, 230),
                            255);

                        float dx = x + (float)(Random.Shared.NextDouble() - 0.5) * 2f;
                        float dy = y + (float)(Random.Shared.NextDouble() - 0.5) * 5f;
                        float angle = (float)(Random.Shared.NextDouble() - 0.5) * 90f;

                        image.Mutate(ctx =>
                        {
                            ctx.Fill(dotColor, new EllipsePolygon(dx, dy, dotRadius).RotateDegree(angle));
                        });
                    }
                }
            }
        }
    }
    private static void DrawRandomCircles(Image<Rgba32> image, int width, int height, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int radius = Random.Shared.Next(5, 15);
            int x = Random.Shared.Next(radius, width - radius);
            int y = Random.Shared.Next(radius, height - radius);
            var color = new Rgba32(
                RandomColorPart(), RandomColorPart(), RandomColorPart(), 128);
            image.Mutate(ctx => ctx.Fill(color, new EllipsePolygon(x, y, radius)));
        }
    }
    private static void DrawRandomLines(Image<Rgba32> image, int width, int height, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var color = new Rgba32(RandomColorPart(), RandomColorPart(), RandomColorPart());
            int x1 = Random.Shared.Next(width);
            int y1 = Random.Shared.Next(height);
            int x2 = Random.Shared.Next(width);
            int y2 = Random.Shared.Next(height);

            x2 = Math.Min(Math.Abs(x2 - x1), width / 2);
            y2 = Math.Min(Math.Abs(y2 - y1), height / 2);

            image.Mutate(ctx => ctx.DrawLine(color, 1, new PointF[] { new(x1, y1), new(x2, y2) }));
        }
    }
    private static void AddNoiseDots(Image<Rgba32> image, int width, int height, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int x = Random.Shared.Next(width);
            int y = Random.Shared.Next(height);
            var color = new Rgba32(
                (byte)Random.Shared.Next(80, 230),
                (byte)Random.Shared.Next(80, 230),
                (byte)Random.Shared.Next(80, 230),
                180);
            image[x, y] = color;
        }
    }
    private static void ApplyPostProcessing(Image<Rgba32> image)
    {
        image.Mutate(ctx =>
        {
            ctx.GaussianBlur(0.2f);
            ctx.Dither();
        });
    }
    private static byte RandomColorPart() => (byte)Random.Shared.Next(80, 160);
    private static string ToPersianDigits(string input)
    {
        return input
            .Replace('0', '۰')
            .Replace('1', '۱')
            .Replace('2', '۲')
            .Replace('3', '۳')
            .Replace('4', '۴')
            .Replace('5', '۵')
            .Replace('6', '۶')
            .Replace('7', '۷')
            .Replace('8', '۸')
            .Replace('9', '۹');
    }
}
