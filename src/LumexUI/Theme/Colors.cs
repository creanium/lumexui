// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Theme;

namespace LumexUI;

public static class Colors
{
	public record Red : Color
	{
		public Red()
		{
			S50 = "#FDE9EA";
			S100 = "#F9C1C3";
			S200 = "#F4999C";
			S300 = "#F07175";
			S400 = "#EC494F";
			S500 = "#E82128";
			S600 = "#C51C22";
			S700 = "#A2171C";
			S800 = "#801216";
			S900 = "#5D0D10";
			S950 = "#3A080A";
		}
	}

	public record Rose : Color
	{
		public Rose()
		{
			S50 = "#FDE9EE";
			S100 = "#F8C1CF";
			S200 = "#F49AB0";
			S300 = "#F07292";
			S400 = "#EB4B73";
			S500 = "#E72354";
			S600 = "#C41E47";
			S700 = "#A2193B";
			S800 = "#7F132E";
			S900 = "#5C0E22";
			S950 = "#3A0915";
		}
	}

	public record Pink : Color
	{
		public Pink()
		{
			S50 = "#FDE9F3";
			S100 = "#F8C1DF";
			S200 = "#F49ACA";
			S300 = "#F072B5";
			S400 = "#EB4BA0";
			S500 = "#E7238B";
			S600 = "#C41E76";
			S700 = "#A21961";
			S800 = "#7F134C";
			S900 = "#5C0E38";
			S950 = "#3A0923";
		}
	}

	public record Green : Color
	{
		public Green()
		{
			S50 = "#E8FAEC";
			S100 = "#BDF0CA";
			S200 = "#93E7A9";
			S300 = "#69DE87";
			S400 = "#3FD465";
			S500 = "#15CB43";
			S600 = "#12AD39";
			S700 = "#0F8E2F";
			S800 = "#0C7025";
			S900 = "#08511B";
			S950 = "#053311";
		}
	}

	public record Lime : Color
	{
		public Lime()
		{
			S50 = "#F4FDEA";
			S100 = "#E0FAC5";
			S200 = "#CBF7A0";
			S300 = "#B7F37B";
			S400 = "#A3F056";
			S500 = "#8FED31";
			S600 = "#72C92A";
			S700 = "#5DA622";
			S800 = "#49821B";
			S900 = "#365F14";
			S950 = "#223B0C";
		}
	}

	public record Emerald : Color
	{
		public Emerald()
		{
			S50 = "#E7F7F2";
			S100 = "#BCE9DC";
			S200 = "#92DBC5";
			S300 = "#67CCAE";
			S400 = "#3CBE98";
			S500 = "#11B081";
			S600 = "#0E966E";
			S700 = "#0C7B5A";
			S800 = "#096147";
			S900 = "#074634";
			S950 = "#042C20";
		}
	}

	public record Teal : Color
	{
		public Teal()
		{
			S50 = "#E7F8F7";
			S100 = "#BDECE8";
			S200 = "#92DFD9";
			S300 = "#67D3CA";
			S400 = "#3DC6BB";
			S500 = "#12BAAC";
			S600 = "#0F9E92";
			S700 = "#0D8278";
			S800 = "#0A665F";
			S900 = "#074A45";
			S950 = "#052F2B";
		}
	}

	public record Blue : Color
	{
		public Blue()
		{
			S50 = "#E7F1FF";
			S100 = "#BAD7FF";
			S200 = "#8EBDFF";
			S300 = "#62A3FF";
			S400 = "#368AFF";
			S500 = "#0A70FF";
			S600 = "#095FD9";
			S700 = "#074EB3";
			S800 = "#063E8C";
			S900 = "#042D66";
			S950 = "#031C40";
		}
	}

	public record Sky : Color
	{
		public Sky()
		{
			S50 = "#E7F9FE";
			S100 = "#BCEDFD";
			S200 = "#91E2FC";
			S300 = "#66D6FB";
			S400 = "#3BCBFA";
			S500 = "#10BFF9";
			S600 = "#0EA2D4";
			S700 = "#0B86AE";
			S800 = "#096989";
			S900 = "#064C64";
			S950 = "#04303E";
		}
	}

	public record Cyan : Color
	{
		public Cyan()
		{
			S50 = "#E7FDFE";
			S100 = "#BCF8FD";
			S200 = "#91F4FC";
			S300 = "#66EFFB";
			S400 = "#3BEBFA";
			S500 = "#10E6F9";
			S600 = "#0EC4D4";
			S700 = "#0BA1AE";
			S800 = "#097F89";
			S900 = "#065C64";
			S950 = "#043A3E";
		}
	}

	public record Purple : Color
	{
		public Purple()
		{
			S50 = "#F7EBFC";
			S100 = "#E7C8F7";
			S200 = "#D8A4F3";
			S300 = "#C981EE";
			S400 = "#BA5DE9";
			S500 = "#AB3AE4";
			S600 = "#9131C2";
			S700 = "#7829A0";
			S800 = "#5E207D";
			S900 = "#44175B";
			S950 = "#2B0F39";
		}
	}

	public record Violet : Color
	{
		public Violet()
		{
			S50 = "#F1E9FD";
			S100 = "#D9C3F8";
			S200 = "#C09CF4";
			S300 = "#A875F0";
			S400 = "#8F4EEB";
			S500 = "#7727E7";
			S600 = "#6521C4";
			S700 = "#531BA2";
			S800 = "#41157F";
			S900 = "#30105C";
			S950 = "#1E0A3A";
		}
	}

	public record Indigo : Color
	{
		public Indigo()
		{
			S50 = "#EEE8FD";
			S100 = "#D0C0FA";
			S200 = "#B297F7";
			S300 = "#946EF3";
			S400 = "#7646F0";
			S500 = "#581DED";
			S600 = "#4B19C9";
			S700 = "#3E14A6";
			S800 = "#301082";
			S900 = "#230C5F";
			S950 = "#16073B";
		}
	}

	public record Yellow : Color
	{
		public Yellow()
		{
			S50 = "#FFFDE7";
			S100 = "#FFF8BB";
			S200 = "#FFF490";
			S300 = "#FFF064";
			S400 = "#FFEB39";
			S500 = "#FFE70D";
			S600 = "#D9CB0B";
			S700 = "#B3A809";
			S800 = "#8C8307";
			S900 = "#666005";
			S950 = "#403C03";
		}
	}

	public record Amber : Color
	{
		public Amber()
		{
			S50 = "#FEF9E7";
			S100 = "#FCEFBB";
			S200 = "#FAE58F";
			S300 = "#F9DB63";
			S400 = "#F7D037";
			S500 = "#F5C60B";
			S600 = "#DCA40A";
			S700 = "#A67608";
			S800 = "#875E06";
			S900 = "#624704";
			S950 = "#3D2E03";
		}
	}

	public record Orange : Color
	{
		public Orange()
		{
			S50 = "#FEF1E8";
			S100 = "#FCD8BE";
			S200 = "#F9BE94";
			S300 = "#F7A56A";
			S400 = "#F58B40";
			S500 = "#F37216";
			S600 = "#CF6113";
			S700 = "#AA500F";
			S800 = "#863F0C";
			S900 = "#612E09";
			S950 = "#3D1D06";
		}
	}

	public record DeepOrange : Color
	{
		public DeepOrange()
		{
			S50 = "#FEEDE8";
			S100 = "#FCCDBE";
			S200 = "#F9AD94";
			S300 = "#F78D6A";
			S400 = "#F56D40";
			S500 = "#F34D16";
			S600 = "#CF4113";
			S700 = "#AA360F";
			S800 = "#862A0C";
			S900 = "#611F09";
			S950 = "#3D1306";
		}
	}

	public record Neutral : Color
	{
		public Neutral()
		{
			S50 = "#F2F2F2";
			S100 = "#DBDBDB";
			S200 = "#C5C5C5";
			S300 = "#AEAEAE";
			S400 = "#979797";
			S500 = "#808080";
			S600 = "#6D6D6D";
			S700 = "#5A5A5A";
			S800 = "#464646";
			S900 = "#333333";
			S950 = "#202020";
		}
	}

	public record Gray : Color
	{
		public Gray()
		{
			S50 = "#F1F0F2";
			S100 = "#D7D5DC";
			S200 = "#BDBAC5";
			S300 = "#A3A0AE";
			S400 = "#8A8598";
			S500 = "#706A81";
			S600 = "#5F5A6E";
			S700 = "#4E4A5A";
			S800 = "#3E3A47";
			S900 = "#2D2A34";
			S950 = "#1C1B20";
		}
	}

	public record Brown : Color
	{
		public Brown()
		{
			S50 = "#FBF7F4";
			S100 = "#F2E9E0";
			S200 = "#EADBCC";
			S300 = "#E2CCB9";
			S400 = "#DABEA5";
			S500 = "#D2B091";
			S600 = "#B3967B";
			S700 = "#937B66";
			S800 = "#746150";
			S900 = "#54463A";
			S950 = "#352C24";
		}
	}

	public class Contrast
	{
		public const string White = "#FFFFFF";
		public const string Black = "#000000";
		public const string Light = "#F7F7F7";
		public const string Dark = "#262626";
	}
}
