// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Theme;

namespace LumexUI;

public static class Colors
{
    public record Pink : Color
    {
        public Pink()
        {
            S50 = "#FFF0F7";
            S100 = "#FFDBEB";
            S200 = "#FBB7D5";
            S300 = "#F584B7";
            S400 = "#F362A1";
            S500 = "#E24081";
            S600 = "#DC2E73";
            S700 = "#CB2567";
            S800 = "#B82361";
            S900 = "#981F50";
        }
    }

    public record Rose : Color
    {
        public Rose()
        {
            S50 = "#FFF0F2";
            S100 = "#FFD6DD";
            S200 = "#FFADB8";
            S300 = "#FA8594";
            S400 = "#F75976";
            S500 = "#EB375B";
            S600 = "#E4214B";
            S700 = "#C80E40";
            S800 = "#AA1339";
            S900 = "#881330";
        }
    }

    public record Red : Color
	{
		public Red()
		{
			S50 = "#FFF0F0";
			S100 = "#FFDBDB";
			S200 = "#FCB0B0";
			S300 = "#FC7E7E";
			S400 = "#FC5F5F";
			S500 = "#ED4040";
			S600 = "#DE2B2B";
			S700 = "#BA1212";
			S800 = "#A51212";
			S900 = "#811313";
		}
	}

    public record Orange : Color
    {
        public Orange()
        {
            S50 = "#FFF7F0";
            S100 = "#FFE3CC";
            S200 = "#FAC293";
            S300 = "#F4A966";
            S400 = "#F9923E";
            S500 = "#F07828";
            S600 = "#E46511";
            S700 = "#BD460F";
            S800 = "#9D3B10";
            S900 = "#752E0F";
        }
    }

    public record Yellow : Color
    {
        public Yellow()
        {
            S50 = "#FFFCEB";
            S100 = "#FFF5C2";
            S200 = "#FDE791";
            S300 = "#FBD86F";
            S400 = "#FBD150";
            S500 = "#F4C734";
            S600 = "#DE9F21";
            S700 = "#B8770F";
            S800 = "#915412";
            S900 = "#6F3E11";
        }
    }

    public record Green : Color
	{
		public Green()
		{
			S50 = "#F0FFF4";
			S100 = "#D3FDDE";
			S200 = "#ABF7BE";
			S300 = "#81EA9E";
			S400 = "#57DB7A";
			S500 = "#2ECC58";
			S600 = "#2BB151";
			S700 = "#1C8D3F";
			S800 = "#157A3F";
			S900 = "#0D5E2E";
		}
	}

	public record Teal : Color
	{
		public Teal()
		{
			S50 = "#ECFDF4";
			S100 = "#D3FDE3";
			S200 = "#A1F2C8";
			S300 = "#6BE6B3";
			S400 = "#24D698";
			S500 = "#19C28A";
			S600 = "#0F9F6F";
			S700 = "#107E5C";
			S800 = "#07644A";
			S900 = "#014C3B";
		}
	}

    public record LightBlue : Color
    {
        public LightBlue()
        {
            S50 = "#EBFDFF";
            S100 = "#CDF8FE";
            S200 = "#A4F2FE";
            S300 = "#71E7F9";
            S400 = "#44D6F3";
            S500 = "#33CDF0";
            S600 = "#15BBE0";
            S700 = "#0FA0CC";
            S800 = "#1089B1";
            S900 = "#096C90";
        }
    }

    public record Blue : Color
	{
		public Blue()
		{
			S50 = "#ECF6FE";
			S100 = "#D2EAFE";
			S200 = "#A9D5F9";
			S300 = "#78B7F2";
			S400 = "#2B95F3";
			S500 = "#0B77EA";
			S600 = "#0E66E1";
			S700 = "#0F53B8";
			S800 = "#1342A0";
			S900 = "#193B8A";
		}
	}

    public record Indigo : Color
    {
        public Indigo()
        {
            S50 = "#F0F2FF";
            S100 = "#DCE2FE";
            S200 = "#B8C5F9";
            S300 = "#8798EE";
            S400 = "#5D66E5";
            S500 = "#484DE0";
            S600 = "#3F44D5";
            S700 = "#4138BC";
            S800 = "#382F9D";
            S900 = "#2D297A";
        }
    }

    public record Violet : Color
	{
		public Violet()
		{
			S50 = "#F4F0FF";
			S100 = "#E8E1FF";
			S200 = "#CAB9FE";
			S300 = "#A88DFC";
			S400 = "#8D6AF6";
			S500 = "#7C53F9";
			S600 = "#6A3AE4";
			S700 = "#5B22C3";
			S800 = "#471197";
			S900 = "#3B1278";
		}
	}

	public record Gray : Color
	{
		public Gray()
		{
			S50 = "#F1F4F8";
			S100 = "#EAEDF0";
			S200 = "#D0D6DD";
			S300 = "#A0AAB6";
			S400 = "#7D8996";
			S500 = "#62707E";
			S600 = "#53616F";
			S700 = "#41505D";
			S800 = "#354350";
			S900 = "#212B36";
		}
	}

	public class Contrast
	{
		public const string White = "#FFFFFF";
		public const string Black = "#000000";
	}
}
