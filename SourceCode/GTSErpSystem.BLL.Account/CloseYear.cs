using DevExpress.XtraEditors;

namespace GTSErpSystem.BLL.Account;

public class CloseYear
{
	public static void HideButtons(params SimpleButton[] btns)
	{
		foreach (SimpleButton simpleButton in btns)
		{
			simpleButton.Visible = false;
		}
	}
}


