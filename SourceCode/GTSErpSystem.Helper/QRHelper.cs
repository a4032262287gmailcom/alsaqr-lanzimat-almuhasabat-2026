using System.Drawing;
using QRCoder;

namespace GTSErpSystem.Helper;

public static class QRHelper
{
	public static Image GenerateQRCode(string qrText)
	{
		QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
		QRCodeData data = qRCodeGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
		QRCode qRCode = new QRCode(data);
		return qRCode.GetGraphic(20);
	}
}


