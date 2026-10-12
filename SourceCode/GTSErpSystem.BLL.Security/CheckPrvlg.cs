using System.Linq;

namespace GTSErpSystem.BLL.Security;

public class CheckPrvlg
{
	private GTSdbEntities db = new GTSdbEntities();

	public bool CheckUserPage(int GroupID, int ScreenID)
	{
		bool flag = false;
		return flag = (from x in db.User_Permission
			where x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID
			select x.Allow_Enter == (bool?)true).FirstOrDefault();
	}

	public bool CheckUserPageEventSave(int GroupID, int ScreenID)
	{
		bool flag = false;
		return flag = (from x in db.User_Permission
			where x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID
			select x.Allow_Save == (bool?)true).FirstOrDefault();
	}

	public bool CheckUserPageEventEdit(int GroupID, int ScreenID)
	{
		bool flag = false;
		return flag = (from x in db.User_Permission
			where x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID
			select x.Allow_Edit == (bool?)true).FirstOrDefault();
	}

	public bool CheckUserPageEventDelete(int GroupID, int ScreenID)
	{
		bool flag = false;
		return flag = (from x in db.User_Permission
			where x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID
			select x.Allow_Delete == (bool?)true).FirstOrDefault();
	}

	public bool CheckUserPageEventPrint(int GroupID, int ScreenID)
	{
		bool flag = false;
		return flag = (from x in db.User_Permission
			where x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID
			select x.Allow_Print == (bool?)true).FirstOrDefault();
	}

	public bool CheckUserPageEventExport(int GroupID, int ScreenID)
	{
		bool flag = false;
		return flag = (from x in db.User_Permission
			where x.GroupID == (int?)GroupID && x.ScreenID == (int?)ScreenID
			select x.Allow_Export == (bool?)true).FirstOrDefault();
	}
}


