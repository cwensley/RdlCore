using System.Drawing;
using Microsoft.Reporting.Rendering;

namespace Microsoft.Reporting.Chart.WebForms.Rendering
{
	// Font metadata carried from RDL without constructing a Windows-only Font.
	internal sealed class ChartFontSettings
	{
		internal string Family { get; }
		internal float SizeInPoints { get; }
		internal FontStyle Style { get; }

		internal ChartFontSettings(string family, float sizeInPoints, FontStyle style)
		{
			Family = family;
			SizeInPoints = sizeInPoints;
			Style = style;
		}

		internal IChartFont CreateFont(IDrawingResourceFactory factory) =>
			factory.CreateFont(Family, SizeInPoints, Style, GraphicsUnit.Point);
	}
}
