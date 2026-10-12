using System;
using System.Runtime.InteropServices;

namespace GTSErpSystem.Helper;

public class RawPrinterHelper
{
	[StructLayout(LayoutKind.Sequential)]
	public class DOCINFOA
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string pDocName;

		[MarshalAs(UnmanagedType.LPStr)]
		public string pOutputFile;

		[MarshalAs(UnmanagedType.LPStr)]
		public string pDataType;
	}

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool ClosePrinter(IntPtr hPrinter);

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool StartDocPrinter(IntPtr hPrinter, int Level, [In] DOCINFOA pDocInfo);

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool EndDocPrinter(IntPtr hPrinter);

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool StartPagePrinter(IntPtr hPrinter);

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool EndPagePrinter(IntPtr hPrinter);

	[DllImport("winspool.Drv", SetLastError = true)]
	public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

	public static bool SendBytesToPrinter(string printerName, byte[] bytes)
	{
		IntPtr phPrinter = IntPtr.Zero;
		DOCINFOA dOCINFOA = new DOCINFOA();
		dOCINFOA.pDocName = "CashDrawerCommand";
		dOCINFOA.pDataType = "RAW";
		if (OpenPrinter(printerName.Normalize(), out phPrinter, IntPtr.Zero))
		{
			if (StartDocPrinter(phPrinter, 1, dOCINFOA))
			{
				if (StartPagePrinter(phPrinter))
				{
					IntPtr intPtr = Marshal.AllocCoTaskMem(bytes.Length);
					Marshal.Copy(bytes, 0, intPtr, bytes.Length);
					WritePrinter(phPrinter, intPtr, bytes.Length, out var _);
					Marshal.FreeCoTaskMem(intPtr);
					EndPagePrinter(phPrinter);
				}
				EndDocPrinter(phPrinter);
			}
			ClosePrinter(phPrinter);
		}
		return true;
	}
}


