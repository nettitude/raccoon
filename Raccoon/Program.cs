using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Raccoon
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{
				int.TryParse(args[0], out int pid);
				var hWnd = Process.GetProcessById(pid).MainWindowHandle;

				if (hWnd != IntPtr.Zero)
				{
					var lpwndpl = new Native.WINDOWPLACEMENT();
					lpwndpl.length = Marshal.SizeOf(lpwndpl);
					var status = Native.GetWindowPlacement(hWnd, ref lpwndpl);

					if (status)
					{
						if (lpwndpl.showCmd == Native.SW_SHOWMINIMIZED)
						{
							Native.ShowWindow(hWnd, Native.SW_SHOWNOACTIVATE);
						}

						var bmp = Utils.TakeScreenshot(hWnd);

						if (lpwndpl.showCmd == Native.SW_SHOWMINIMIZED)
						{
							Native.ShowWindow(hWnd, Native.SW_SHOWMINNOACTIVE);
						}

						if (bmp != null)
						{
							Console.WriteLine(Utils.ConvertToPNG(bmp));
						}
						else
						{
							Console.WriteLine("[-] An error occured.");
						}
					}
					else
					{
						Console.WriteLine("[-] Failed to retrieve the show state of the main window.");
					}
				}
				else
				{
					Console.WriteLine("[-] The specified process does not have a main window associated with it.");
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine($"[-] {exception.Message}");

				if (exception.InnerException != null)
				{
					Console.WriteLine($"[-] {exception.InnerException.Message}");
				}
			}
		}
	}
}
