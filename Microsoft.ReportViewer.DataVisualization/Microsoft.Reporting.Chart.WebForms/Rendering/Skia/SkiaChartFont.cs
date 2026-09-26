using System.Drawing;
using SkiaSharp;
using Microsoft.Reporting.Rendering;

namespace Microsoft.Reporting.Chart.WebForms.Rendering.Skia
{
	/// <summary>
	/// Spike adapter — wraps an <see cref="SKFont"/>/<see cref="SKTypeface"/> pair behind
	/// <see cref="IChartFont"/>. GDI+ measures font size in points at 96 DPI by convention
	/// (<see cref="GraphicsUnit.Point"/>); Skia's <see cref="SKFont.Size"/> is in raw pixels,
	/// so construction starts at 96 DPI. SkiaChartGraphics updates the native size to
	/// the bound surface's DPI before measuring or drawing; SizeInPoints stays logical.
	/// </summary>
	internal sealed class SkiaChartFont : IChartFont
	{
		private const float PointsToPixels = 96f / 72f;

		internal SKFont NativeFont { get; }

		public string FontFamilyName { get; }

		public float SizeInPoints { get; }

		public FontStyle Style { get; }

		public GraphicsUnit Unit => GraphicsUnit.Point;

		internal SkiaChartFont(string familyName, float sizeInPoints, FontStyle style = FontStyle.Regular)
		{
			FontFamilyName = familyName;
			SizeInPoints = sizeInPoints;
			Style = style;

			var weight = (style & FontStyle.Bold) != 0 ? SKFontStyleWeight.Bold : SKFontStyleWeight.Normal;
			var slant = (style & FontStyle.Italic) != 0 ? SKFontStyleSlant.Italic : SKFontStyleSlant.Upright;
			var typeface = SKTypeface.FromFamilyName(familyName, weight, SKFontStyleWidth.Normal, slant)
				?? SKTypeface.Default;

			NativeFont = new SKFont(typeface, sizeInPoints * PointsToPixels);
		}

		public void Dispose() => NativeFont.Dispose();
	}
}
