using System;
using System.Runtime.InteropServices;

namespace Raccoon
{
	internal class Native
	{
		internal const int SW_SHOWMINIMIZED = 2;
		internal const int SW_SHOWNOACTIVATE = 4;
		internal const int SW_SHOWMINNOACTIVE = 7;

		[StructLayout(LayoutKind.Sequential)]
		internal struct POINT
		{
			internal int x;
			internal int y;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct RECT
		{
			internal int left;
			internal int top;
			internal int right;
			internal int bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct WINDOWPLACEMENT
		{
			internal int length;
			internal int flags;
			internal int showCmd;
			internal POINT ptMinPosition;
			internal POINT ptMaxPosition;
			internal RECT rcNormalPosition;
			internal RECT rcDevice;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool PrintWindow(IntPtr hWnd, IntPtr hDcBlt, uint nFlags);

	}
}
