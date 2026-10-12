using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Main;

internal class Class_Login
{
	private GTSdbEntities db = new GTSdbEntities();

	public int LOGIN(int usrn, string pws)
	{
		string Password = Class_Encryption.EncryptString(pws, usrn.ToString());
		int result = 0;
		int num = Convert.ToInt32((from zz in db.User_Login
			where zz.ID == usrn && zz.PassWord == Password
			select zz.GroupID).FirstOrDefault());
		if (num > 0)
		{
			result = num;
		}
		return result;
	}

	public List<User_Login> GetAllUser()
	{
		return db.User_Login.ToList();
	}
}


