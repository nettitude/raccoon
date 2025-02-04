using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Raccoon
{
	internal class Utils
	{
		internal static string ConvertToPNG(Bitmap bmp)
		{

			using (MemoryStream ms = new MemoryStream())
			{
				bmp.Save(ms, ImageFormat.Png);
				return Convert.ToBase64String(ms.ToArray());
			}
		}

		internal static Bitmap TakeScreenshot(IntPtr hWnd)
		{
			var status = Native.GetWindowRect(hWnd, out Native.RECT lpRect);

			if (status)
			{
				var bmp = new Bitmap(lpRect.right - lpRect.left, lpRect.bottom - lpRect.top);
				var gdi = Graphics.FromImage(bmp);
				var hDcBlt = gdi.GetHdc();
				status = Native.PrintWindow(hWnd, hDcBlt, 0);
				gdi.ReleaseHdc(hDcBlt);

				if (status)
				{
					return bmp;
				}
			}

			return null;
		}
	}
}
