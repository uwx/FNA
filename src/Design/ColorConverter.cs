#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2024 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System;
using System.Collections;
using System.Collections.Frozen;
using System.ComponentModel;
using System.Globalization;
#endregion

namespace Microsoft.Xna.Framework.Design
{
	public class ColorConverter : MathTypeConverter
	{
		#region Public Constructor

		public ColorConverter() : base()
		{
			// FIXME: Initialize propertyDescriptions... how? -flibit
		}

		#endregion

        // Named colors as defined in CSS Color Module Level 4.
        // https://www.w3.org/TR/css-color-4/#named-colors
        private static readonly FrozenDictionary<string, uint> NamedColors = new Dictionary<string, uint>(StringComparer.OrdinalIgnoreCase)
        {
            { "transparent", 0u },
            { "antiquewhite", 0xFAEBD7FFu },
            { "aqua", 0x00FFFFFFu },
            { "aquamarine", 0x7FFFD4FFu },
            { "azure", 0xF0FFFFFFu },
            { "beige", 0xF5F5DCFFu },
            { "bisque", 0xFFE4C4FFu },
            { "black", 0x000000FFu },
            { "blanchedalmond", 0xFFEBCDFFu },
            { "blue", 0x0000FFFFu },
            { "blueviolet", 0x8A2BE2FFu },
            { "brown", 0xA52A2AFFu },
            { "burlywood", 0xDEB887FFu },
            { "cadetblue", 0x5F9EA0FFu },
            { "chartreuse", 0x7FFF00FFu },
            { "chocolate", 0xD2691EFFu },
            { "coral", 0xFF7F50FFu },
            { "cornflowerblue", 0x6495EDFFu },
            { "cornsilk", 0xFFF8DCFFu },
            { "crimson", 0xDC143CFFu },
            { "cyan", 0x00FFFFFFu },
            { "darkblue", 0x00008BFFu },
            { "darkcyan", 0x008B8BFFu },
            { "darkgoldenrod", 0xB8860BFFu },
            { "darkgray", 0xA9A9A9FFu },
            { "darkgreen", 0x006400FFu },
            { "darkgrey", 0xA9A9A9FFu },
            { "darkkhaki", 0xBDB76BFFu },
            { "darkmagenta", 0x8B008BFFu },
            { "darkolivegreen", 0x556B2FFFu },
            { "darkorange", 0xFF8C00FFu },
            { "darkorchid", 0x9932CCFFu },
            { "darkred", 0x8B0000FFu },
            { "darksalmon", 0xE9967AFFu },
            { "darkseagreen", 0x8FBC8FFFu },
            { "darkslateblue", 0x483D8BFFu },
            { "darkslategray", 0x2F4F4FFFu },
            { "darkslategrey", 0x2F4F4FFFu },
            { "darkturquoise", 0x00CED1FFu },
            { "darkviolet", 0x9400D3FFu },
            { "deeppink", 0xFF1493FFu },
            { "deepskyblue", 0x00BFFFFFu },
            { "dimgray", 0x696969FFu },
            { "dimgrey", 0x696969FFu },
            { "dodgerblue", 0x1E90FFFFu },
            { "firebrick", 0xB22222FFu },
            { "floralwhite", 0xFFFAF0FFu },
            { "forestgreen", 0x228B22FFu },
            { "fuchsia", 0xFF00FFFFu },
            { "gainsboro", 0xDCDCDCFFu },
            { "ghostwhite", 0xF8F8FFFFu },
            { "gold", 0xFFD700FFu },
            { "goldenrod", 0xDAA520FFu },
            { "gray", 0x808080FFu },
            { "green", 0x008000FFu },
            { "greenyellow", 0xADFF2FFFu },
            { "grey", 0x808080FFu },
            { "honeydew", 0xF0FFF0FFu },
            { "hotpink", 0xFF69B4FFu },
            { "indianred", 0xCD5C5CFFu },
            { "indigo", 0x4B0082FFu },
            { "ivory", 0xFFFFF0FFu },
            { "khaki", 0xF0E68CFFu },
            { "lavender", 0xE6E6FAFFu },
            { "lavenderblush", 0xFFF0F5FFu },
            { "lawngreen", 0x7CFC00FFu },
            { "lemonchiffon", 0xFFFACDFFu },
            { "lightblue", 0xADD8E6FFu },
            { "lightcoral", 0xF08080FFu },
            { "lightcyan", 0xE0FFFFFFu },
            { "lightgoldenrodyellow", 0xFAFAD2FFu },
            { "lightgray", 0xD3D3D3FFu },
            { "lightgreen", 0x90EE90FFu },
            { "lightgrey", 0xD3D3D3FFu },
            { "lightpink", 0xFFB6C1FFu },
            { "lightsalmon", 0xFFA07AFFu },
            { "lightseagreen", 0x20B2AAFFu },
            { "lightskyblue", 0x87CEFAFFu },
            { "lightslategray", 0x778899FFu },
            { "lightslategrey", 0x778899FFu },
            { "lightsteelblue", 0xB0C4DEFFu },
            { "lightyellow", 0xFFFFE0FFu },
            { "lime", 0x00FF00FFu },
            { "limegreen", 0x32CD32FFu },
            { "linen", 0xFAF0E6FFu },
            { "magenta", 0xFF00FFFFu },
            { "maroon", 0x800000FFu },
            { "mediumaquamarine", 0x66CDAAFFu },
            { "mediumblue", 0x0000CDFFu },
            { "mediumorchid", 0xBA55D3FFu },
            { "mediumpurple", 0x9370DBFFu },
            { "mediumseagreen", 0x3CB371FFu },
            { "mediumslateblue", 0x7B68EEFFu },
            { "mediumspringgreen", 0x00FA9AFFu },
            { "mediumturquoise", 0x48D1CCFFu },
            { "mediumvioletred", 0xC71585FFu },
            { "midnightblue", 0x191970FFu },
            { "mintcream", 0xF5FFFAFFu },
            { "mistyrose", 0xFFE4E1FFu },
            { "moccasin", 0xFFE4B5FFu },
            { "navajowhite", 0xFFDEADFFu },
            { "navy", 0x000080FFu },
            { "oldlace", 0xFDF5E6FFu },
            { "olive", 0x808000FFu },
            { "olivedrab", 0x6B8E23FFu },
            { "orange", 0xFFA500FFu },
            { "orangered", 0xFF4500FFu },
            { "orchid", 0xDA70D6FFu },
            { "palegoldenrod", 0xEEE8AAFFu },
            { "palegreen", 0x98FB98FFu },
            { "paleturquoise", 0xAFEEEEFFu },
            { "palevioletred", 0xDB7093FFu },
            { "papayawhip", 0xFFEFD5FFu },
            { "peachpuff", 0xFFDAB9FFu },
            { "peru", 0xCD853FFFu },
            { "pink", 0xFFC0CBFFu },
            { "plum", 0xDDA0DDFFu },
            { "powderblue", 0xB0E0E6FFu },
            { "purple", 0x800080FFu },
            { "rebeccapurple", 0x663399FFu },
            { "red", 0xFF0000FFu },
            { "rosybrown", 0xBC8F8FFFu },
            { "royalblue", 0x4169E1FFu },
            { "saddlebrown", 0x8B4513FFu },
            { "salmon", 0xFA8072FFu },
            { "sandybrown", 0xF4A460FFu },
            { "seagreen", 0x2E8B57FFu },
            { "seashell", 0xFFF5EEFFu },
            { "sienna", 0xA0522DFFu },
            { "silver", 0xC0C0C0FFu },
            { "skyblue", 0x87CEEBFFu },
            { "slateblue", 0x6A5ACDFFu },
            { "slategray", 0x708090FFu },
            { "slategrey", 0x708090FFu },
            { "snow", 0xFFFAFAFFu },
            { "springgreen", 0x00FF7FFFu },
            { "steelblue", 0x4682B4FFu },
            { "tan", 0xD2B48CFFu },
            { "teal", 0x008080FFu },
            { "thistle", 0xD8BFD8FFu },
            { "tomato", 0xFF6347FFu },
            { "turquoise", 0x40E0D0FFu },
            { "violet", 0xEE82EEFFu },
            { "wheat", 0xF5DEB3FFu },
            { "white", 0xFFFFFFFFu },
            { "whitesmoke", 0xF5F5F5FFu },
            { "yellow", 0xFFFF00FFu },
            { "yellowgreen", 0x9ACD32FFu }
        }.ToFrozenDictionary();

		public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	    {
	        if (value is string str)
	        {
	            var parts = str.Split(',');
	            if (parts.Length == 3)
	            {
	                return new Color(
	                    byte.Parse(parts[0].Trim(), CultureInfo.InvariantCulture),
	                    byte.Parse(parts[1].Trim(), CultureInfo.InvariantCulture),
	                    byte.Parse(parts[2].Trim(), CultureInfo.InvariantCulture)
	                );
	            }
	            if (parts.Length == 4)
	            {
	                return new Color(
	                    byte.Parse(parts[0].Trim(), CultureInfo.InvariantCulture),
	                    byte.Parse(parts[1].Trim(), CultureInfo.InvariantCulture),
	                    byte.Parse(parts[2].Trim(), CultureInfo.InvariantCulture),
	                    byte.Parse(parts[3].Trim(), CultureInfo.InvariantCulture)
	                );
	            }
	            // parse hex color
	            var trimmed = str.AsSpan().Trim();

	            // empty color
	            if (trimmed.Length == 0)
	                return new Color(0,0,0,0);

	            // #RRGGBB or #RGB
	            if (trimmed[0] == '#' &&
	                trimmed.Length is 9 or 7 or 5 or 4)
	            {
		            if (trimmed.Length == 9)
		            {
			            return new Color(
				            int.Parse(trimmed.Slice(1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
				            int.Parse(trimmed.Slice(3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
				            int.Parse(trimmed.Slice(5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
				            int.Parse(trimmed.Slice(7, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)
			            );
		            }
	                if (trimmed.Length == 7)
	                {
	                    return new Color(
	                        int.Parse(trimmed.Slice(1, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
	                        int.Parse(trimmed.Slice(3, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture),
	                        int.Parse(trimmed.Slice(5, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture)
	                    );
	                }
	                else if (trimmed.Length == 5)
	                {
		                var r = int.Parse(trimmed.Slice(1, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		                var g = int.Parse(trimmed.Slice(2, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		                var b = int.Parse(trimmed.Slice(3, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		                var a = int.Parse(trimmed.Slice(4, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
		                return new Color(
			                (byte)(r * 17),
			                (byte)(g * 17),
			                (byte)(b * 17),
			                (byte)(a * 17)
		                );
	                }
	                else
	                {
	                    var r = int.Parse(trimmed.Slice(1, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
	                    var g = int.Parse(trimmed.Slice(2, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
	                    var b = int.Parse(trimmed.Slice(3, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
	                    return new Color(
	                        (byte)(r * 17),
	                        (byte)(g * 17),
	                        (byte)(b * 17)
	                    );
	                }
	            }

	            // rgb(...) or rgba(...)
	            if (trimmed.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
	            {
		            var start = trimmed.IndexOf('(');
		            var end = trimmed.IndexOf(')');
		            if (start >= 0 && end > start)
		            {
			            var inner = trimmed.Slice(start + 1, end - start - 1);
			            var rgbParts = inner.ToString().Split(' ');
			            if (rgbParts.Length == 3)
			            {
				            return new Color(
					            byte.Parse(CssTrim(rgbParts[0]), CultureInfo.InvariantCulture),
					            byte.Parse(CssTrim(rgbParts[1]), CultureInfo.InvariantCulture),
					            byte.Parse(CssTrim(rgbParts[2]), CultureInfo.InvariantCulture)
				            );
			            }

			            if (rgbParts.Length == 4)
			            {
				            return new Color(
					            byte.Parse(CssTrim(rgbParts[0]), CultureInfo.InvariantCulture),
					            byte.Parse(CssTrim(rgbParts[1]), CultureInfo.InvariantCulture),
					            byte.Parse(CssTrim(rgbParts[2]), CultureInfo.InvariantCulture),
					            ParsePercent(CssTrim(rgbParts[3]))
				            );
			            }

			            if (rgbParts.Length == 5)
			            {
				            // rgb(a b c / d%)
				            return new Color(
					            byte.Parse(CssTrim(rgbParts[0]), CultureInfo.InvariantCulture),
					            byte.Parse(CssTrim(rgbParts[1]), CultureInfo.InvariantCulture),
					            byte.Parse(CssTrim(rgbParts[2]), CultureInfo.InvariantCulture),
					            ParsePercent(CssTrim(rgbParts[4])) // convert 0-100% to 0-255
				            );
			            }
		            }
	            }

	            // hsl(...) or hsla(...)
	            if (trimmed.StartsWith("hsl", StringComparison.OrdinalIgnoreCase))
	            {
		            var start = trimmed.IndexOf('(');
		            var end = trimmed.IndexOf(')');


		            if (start >= 0 && end > start)
		            {
			            var inner = trimmed.Slice(start + 1, end - start - 1);
			            var hslParts = inner.ToString().Split(' ');
			            if (hslParts.Length == 3)
			            {
				            return FromHsl(
					            ParseHue(CssTrim(hslParts[0])),
					            ParsePercentFloat(CssTrim(hslParts[1])),
					            ParsePercentFloat(CssTrim(hslParts[2]))
				            );
			            }

			            if (hslParts.Length == 4)
			            {
				            return FromHsla(
					            ParseHue(CssTrim(hslParts[0])),
					            ParsePercentFloat(CssTrim(hslParts[1])),
					            ParsePercentFloat(CssTrim(hslParts[2])),
					            ParsePercent(CssTrim(hslParts[3]))
				            );
			            }

			            if (hslParts.Length == 5)
			            {
				            // hsl(a b c / d%)
				            return FromHsla(
					            ParseHue(CssTrim(hslParts[0])),
					            ParsePercentFloat(CssTrim(hslParts[1])),
					            ParsePercentFloat(CssTrim(hslParts[2])),
					            ParsePercent(CssTrim(hslParts[4])) // convert 0-100% to 0-255
				            );
			            }
		            }

		            // from https://github.com/dmester/jdenticon-net/blob/master/Core/Jdenticon/Rendering/Color.Conversion.cs
		            /*
		            MIT License

					Copyright (c) 2018 Daniel Mester Pirttijärvi

					Permission is hereby granted, free of charge, to any person obtaining a copy
					of this software and associated documentation files (the "Software"), to deal
					in the Software without restriction, including without limitation the rights
					to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
					copies of the Software, and to permit persons to whom the Software is
					furnished to do so, subject to the following conditions:

					The above copyright notice and this permission notice shall be included in all
					copies or substantial portions of the Software.

					THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
					IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
					FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
					AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
					LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
					OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
					SOFTWARE.
		             */
		            static Color FromHsla(float hue, float saturation, float lightness, byte alpha)
		            {
			            var color = FromHsl(hue, saturation, lightness);
			            return new Color(color.R, color.G, color.B, alpha);
		            }

		            static Color FromHsl(float hue, float saturation, float lightness)
		            {
			            // h in [0,360], s and l in [0,1]
			            // Based on http://www.w3.org/TR/2011/REC-css3-color-20110607/#hsl-color
			            if (saturation == 0)
			            {
				            var value = (int)(lightness * 255);
				            return new Color(value, value, value);
			            }
			            else
			            {
				            var m2 = lightness <= 0.5f ? lightness * (saturation + 1) : lightness + saturation - lightness * saturation;
				            var m1 = lightness * 2 - m2;

				            return new Color(
					            HueToRgb(m1, m2, hue * 6 + 2),
					            HueToRgb(m1, m2, hue * 6),
					            HueToRgb(m1, m2, hue * 6 - 2));
			            }
		            }

		            static int HueToRgb(float m1, float m2, float h)
		            {
			            h = h < 0 ? h + 6 : h > 6 ? h - 6 : h;
			            return (int)(255 * (
				            h < 1 ? m1 + (m2 - m1) * h :
				            h < 3 ? m2 :
				            h < 4 ? m1 + (m2 - m1) * (4 - h) :
				            m1));
		            }

		            // Vaguely inspired by https://github.com/dmester/jdenticon-net/blob/master/Core/Jdenticon/Rendering/Color.Parser.cs
		            static float ParseHue(ReadOnlySpan<char> str)
		            {
			            if (str.EndsWith("deg", StringComparison.OrdinalIgnoreCase))
			            {
				            str = str[..^3].Trim();
				            return float.Parse(str, CultureInfo.InvariantCulture);
			            }

			            if (str.EndsWith("rad", StringComparison.OrdinalIgnoreCase))
			            {
				            str = str[..^3].Trim();
				            return (float)(float.Parse(str, CultureInfo.InvariantCulture) * (180.0 / Math.PI));
			            }

			            if (str.EndsWith("turn", StringComparison.OrdinalIgnoreCase))
			            {
				            str = str[..^4].Trim();
				            return (float)(float.Parse(str, CultureInfo.InvariantCulture) * 360.0);
			            }

			            if (str.EndsWith("grad", StringComparison.OrdinalIgnoreCase))
			            {
				            str = str[..^4].Trim();
				            return (float)(float.Parse(str, CultureInfo.InvariantCulture) * 0.9);
			            }

			            // assume degrees
			            return float.Parse(str, CultureInfo.InvariantCulture);
		            }
	            }

	            // special case. Html requires LightGrey, but .NET uses LightGray
	            if (trimmed.Equals("LightGrey", StringComparison.OrdinalIgnoreCase))
	            {
		            return Color.LightGray;
	            }

	            if (NamedColors.TryGetAlternateLookup<ReadOnlySpan<char>>(out var lookup) &&
	                lookup.TryGetValue(trimmed, out var argb))
	            {
		            return new Color(
			            (byte)((argb >> 16) & 0xFF),
			            (byte)((argb >> 8) & 0xFF),
			            (byte)(argb & 0xFF),
			            (byte)((argb >> 24) & 0xFF)
		            );
	            }

	            throw new FormatException($"Invalid color format: {str}. Expected 'R,G,B' or 'R,G,B,A', or rgb(r,g,b), or rgba(r,g,b,a), or hsl(h,s,l), or hsla(h,s,l,a), or a valid hex color, or a named CSS color.");

	            static byte ParsePercent(ReadOnlySpan<char> str)
	            {
		            // 0-100%
		            if (str.EndsWith("%"))
		            {
			            str = str[..^1].Trim();
			            return (byte)(float.Parse(str, CultureInfo.InvariantCulture) * 2.55f);
		            }

		            // 0-1
		            return (byte)(float.Parse(str, CultureInfo.InvariantCulture) * 255f);
	            }
	            static float ParsePercentFloat(ReadOnlySpan<char> str)
	            {
		            // 0-100%
		            if (str.EndsWith("%"))
		            {
			            str = str[..^1].Trim();
			            return float.Parse(str, CultureInfo.InvariantCulture) / 100f;
		            }

		            // 0-1
		            return float.Parse(str, CultureInfo.InvariantCulture);
	            }

	            static ReadOnlySpan<char> CssTrim(ReadOnlySpan<char> span)
	            {
		            span = span.Trim();
		            if (span.EndsWith(","))
		            {
			            span = span[..^1].Trim();
		            }
		            else if (span.StartsWith(","))
		            {
			            span = span[1..].Trim();
		            }
		            if (span.EndsWith(","))
		            {
			            span = span[..^1].Trim();
		            }
		            else if (span.StartsWith(","))
		            {
			            span = span[1..].Trim();
		            }

		            return span;
	            }
	        }
	        return base.ConvertFrom(context, culture, value);
	    }

		#region Public Methods

		public override object ConvertTo(
			ITypeDescriptorContext context,
			CultureInfo culture,
			object value,
			Type destinationType
		) {
			if (destinationType == typeof(string))
			{
				Color src = (Color) value;
				return string.Join(
					culture.TextInfo.ListSeparator + " ",
					new string[]
					{
						src.R.ToString(culture),
						src.G.ToString(culture),
						src.B.ToString(culture),
						src.A.ToString(culture)
					}
				);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override object CreateInstance(
			ITypeDescriptorContext context,
			IDictionary propertyValues
		) {
			return (object) new Color(
				(int) propertyValues["R"],
				(int) propertyValues["G"],
				(int) propertyValues["B"],
				(int) propertyValues["A"]
			);
		}

		#endregion
	}
}
