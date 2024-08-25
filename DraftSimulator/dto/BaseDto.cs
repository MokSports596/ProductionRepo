using System;
using Mok.Web.Data.Dao;


namespace Mok.Web.Data.Dto
{
	public abstract class BaseDto
	{
		public BaseDao Dao { get; set; }
		public bool IsBound { get; set; }

		public bool Read()
		{
			IsBound = Dao.Read(this);
			return IsBound;
		}

		public void Save()
		{
			if (IsBound)
			{
				Dao.Update(this);
			}
			else
			{
				Dao.Insert(this);
				IsBound = true;
			}
		}

		public void Delete()
		{
			Dao.Delete(this);
			IsBound = false;
		}
	}
}