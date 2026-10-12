using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTSErpSystem.BLL.Main;
using GTSErpSystem.DAL.Models;

namespace GTSErpSystem.BLL.Items;

public class DiscountCodesManager
{
	private readonly ApplicationDbContext _context;

	private Class_WaitFormFunc waitForm = new Class_WaitFormFunc();

	public DiscountCodesManager(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task AddDisCodeAsync(DiscountCodes discountCodes)
	{
		_context.DiscountCode.AddAsync(discountCodes);
		await _context.SaveChangesAsync();
	}

	public async Task DeleteDisCodeAsync(int id)
	{
		DiscountCodes existingDiscount = await Task.Run(() => _context.DiscountCode.FirstOrDefault((DiscountCodes d) => d.Id == id));
		if (existingDiscount != null)
		{
			_context.DiscountCode.Remove(existingDiscount);
			await _context.SaveChangesAsync();
			return;
		}
		throw new Exception("كود الخصم غير موجود في قاعدة البيانات");
	}

	public async Task<IEnumerable<DiscountCodes>> GetAllDisCodesAsync(string companyName, string companyVatNum)
	{
		return await Task.Run(() => _context.DiscountCode.Where((DiscountCodes d) => d.CompanyName == companyName && d.CompanyVatNum == companyVatNum).ToList());
	}

	public async Task<DiscountCodes> GetDisCodeByIdAsync(int id)
	{
		return await _context.DiscountCode.FindAsync(id);
	}

	public async Task<int> GetLastDisCodeIdAsync()
	{
		return await Task.Run(() => (from o in _context.DiscountCode
			orderby o.Id descending
			select o.Id).FirstOrDefault());
	}

	public async Task UpdateDisCodeAsync(DiscountCodes discountCodes)
	{
		DiscountCodes existingDiscount = await Task.Run(() => _context.DiscountCode.FirstOrDefault((DiscountCodes d) => d.Id == discountCodes.Id));
		if (existingDiscount != null)
		{
			_context.Entry(existingDiscount).CurrentValues.SetValues(discountCodes);
			await _context.SaveChangesAsync();
			return;
		}
		throw new Exception("كود الخصم غير موجود في قاعدة البيانات");
	}

	public async Task<bool> IsDisCodeExistsAsync(string disCode, string companyName, string companyVatNum)
	{
		return await Task.Run(() => _context.DiscountCode.Any((DiscountCodes d) => d.DisCode == disCode && d.CompanyName == companyName && d.CompanyVatNum == companyVatNum));
	}

	public async Task<(decimal DiscountValue, decimal DiscountPercentage)?> GetDisAsync(string disCode, string companyName, string companyVatNum)
	{
		return await Task.Run(delegate
		{
			waitForm.Show();
			var anon = (from d in _context.DiscountCode
				where d.DisCode == disCode && d.CompanyName == companyName && d.CompanyVatNum == companyVatNum
				select new { d.DiscountValue, d.DiscountPercentage, d.NumberUsed, d.DistExpirationDate }).FirstOrDefault();
			if (anon == null)
			{
				waitForm.Close();
				MessageBox.Show("كود الخصم غير موجود", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return ((decimal DiscountValue, decimal DiscountPercentage)?)null;
			}
			decimal? numberUsed = anon.NumberUsed;
			decimal num = 1;
			if ((numberUsed.GetValueOrDefault() < num) & numberUsed.HasValue)
			{
				waitForm.Close();
				MessageBox.Show("لا يمكن استخدام كود الخصم، عدد مرات الاستخدام انتهى", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return ((decimal DiscountValue, decimal DiscountPercentage)?)null;
			}
			if (anon.DistExpirationDate <= DateTime.Now)
			{
				waitForm.Close();
				MessageBox.Show("كود الخصم منتهي الصلاحية", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return ((decimal DiscountValue, decimal DiscountPercentage)?)null;
			}
			decimal valueOrDefault = anon.DiscountValue.GetValueOrDefault();
			decimal valueOrDefault2 = anon.DiscountPercentage.GetValueOrDefault();
			waitForm.Close();
			return (valueOrDefault, valueOrDefault2);
		});
	}

	public async Task<bool> NumberUsedAsync(string disCode)
	{
		try
		{
			DiscountCodes discount = _context.DiscountCode.FirstOrDefault((DiscountCodes d) => d.DisCode == disCode);
			int num;
			if (discount != null)
			{
				decimal? numberUsed = discount.NumberUsed;
				num = (((numberUsed.GetValueOrDefault() > 0m) & numberUsed.HasValue) ? 1 : 0);
			}
			else
			{
				num = 0;
			}
			if (num != 0)
			{
				discount.NumberUsed -= (decimal?)1;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
		catch
		{
			return false;
		}
	}
}


