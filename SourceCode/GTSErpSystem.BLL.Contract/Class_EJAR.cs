using System;
using System.Collections.Generic;
using System.Linq;

namespace GTSErpSystem.BLL.Contract;

internal class Class_EJAR
{
	private GTSdbEntities db = new GTSdbEntities();

	public List<SELECT_Contract_EJAR_Search_Result> GetSelectAll()
	{
		return db.SELECT_Contract_EJAR_Search().ToList();
	}

	public List<SELECT_Contract_EJAR_Result> GetSelectAllEJAR(int ID)
	{
		return db.SELECT_Contract_EJAR(ID).ToList();
	}

	public int InsertUpdate(int ID, string ContractRegistryNumber, DateTime TheDateOfTheContract, DateTime TheStartDateOftheLeaseTerm, string conditionallySuspended, string MethodOfPaymentOfContractFees, string TypeOfContract, string ThePlaceOfConclusionOfTheContract, DateTime TheEndDateOfTheLeaseTerm, string LessorsName, string ThelessorsNationality, string LessorIDType, string ThelandlordsMobileNumber, string LandlordNationalAddress, string LessorIdentificationNumber, string LandlordsEmail, string TheLessorIsRepresentedByHimselfOrHerself, string TenantAreaType, string CommercialRegistrationNumberOfTheTenant, string TheIssuingEntityOfTheTenant, string TheNameOfTheTenantCompany, string UnifiedNumberOfTheTenant, string TheDateOfTheCommercialRegistrationOfTheTenant, string TheNameOfTheTenantsRepresentative, string TheNationalityOfTheTenantsRepresentative, string Note)
	{
		int num = 0;
		Contract_EJAR contract_EJAR = db.Contract_EJAR.Where((Contract_EJAR x) => x.ID == ID).FirstOrDefault();
		if (contract_EJAR != null)
		{
			contract_EJAR.ContractRegistryNumber = ContractRegistryNumber;
			contract_EJAR.TheDateOfTheContract = TheDateOfTheContract;
			contract_EJAR.TheStartDateOftheLeaseTerm = TheStartDateOftheLeaseTerm;
			contract_EJAR.conditionallySuspended = conditionallySuspended;
			contract_EJAR.MethodOfPaymentOfContractFees = MethodOfPaymentOfContractFees;
			contract_EJAR.TypeOfContract = TypeOfContract;
			contract_EJAR.ThePlaceOfConclusionOfTheContract = ThePlaceOfConclusionOfTheContract;
			contract_EJAR.TheEndDateOfTheLeaseTerm = TheEndDateOfTheLeaseTerm;
			contract_EJAR.LessorsName = LessorsName;
			contract_EJAR.ThelessorsNationality = ThelessorsNationality;
			contract_EJAR.LessorIDType = LessorIDType;
			contract_EJAR.ThelandlordsMobileNumber = ThelandlordsMobileNumber;
			contract_EJAR.LandlordNationalAddress = LandlordNationalAddress;
			contract_EJAR.LessorIdentificationNumber = LessorIdentificationNumber;
			contract_EJAR.LandlordsEmail = LandlordsEmail;
			contract_EJAR.TheLessorIsRepresentedByHimselfOrHerself = TheLessorIsRepresentedByHimselfOrHerself;
			contract_EJAR.TenantAreaType = TenantAreaType;
			contract_EJAR.CommercialRegistrationNumberOfTheTenant = CommercialRegistrationNumberOfTheTenant;
			contract_EJAR.TheIssuingEntityOfTheTenant = TheIssuingEntityOfTheTenant;
			contract_EJAR.TheNameOfTheTenantCompany = TheNameOfTheTenantCompany;
			contract_EJAR.UnifiedNumberOfTheTenant = UnifiedNumberOfTheTenant;
			contract_EJAR.TheDateOfTheCommercialRegistrationOfTheTenant = TheDateOfTheCommercialRegistrationOfTheTenant;
			contract_EJAR.TheNameOfTheTenantsRepresentative = TheNameOfTheTenantsRepresentative;
			contract_EJAR.TheNationalityOfTheTenantsRepresentative = TheNationalityOfTheTenantsRepresentative;
			contract_EJAR.Note = Note;
			db.SaveChanges();
			return ID;
		}
		Contract_EJAR contract_EJAR2 = new Contract_EJAR();
		contract_EJAR2.ID = Convert.ToInt32(db.Contract_EJAR.Select((Contract_EJAR x) => x.ID).DefaultIfEmpty(0).Max()) + 1;
		contract_EJAR2.ContractRegistryNumber = ContractRegistryNumber;
		contract_EJAR2.TheDateOfTheContract = TheDateOfTheContract;
		contract_EJAR2.TheStartDateOftheLeaseTerm = TheStartDateOftheLeaseTerm;
		contract_EJAR2.conditionallySuspended = conditionallySuspended;
		contract_EJAR2.MethodOfPaymentOfContractFees = MethodOfPaymentOfContractFees;
		contract_EJAR2.TypeOfContract = TypeOfContract;
		contract_EJAR2.ThePlaceOfConclusionOfTheContract = ThePlaceOfConclusionOfTheContract;
		contract_EJAR2.TheEndDateOfTheLeaseTerm = TheEndDateOfTheLeaseTerm;
		contract_EJAR2.LessorsName = LessorsName;
		contract_EJAR2.ThelessorsNationality = ThelessorsNationality;
		contract_EJAR2.LessorIDType = LessorIDType;
		contract_EJAR2.ThelandlordsMobileNumber = ThelandlordsMobileNumber;
		contract_EJAR2.LandlordNationalAddress = LandlordNationalAddress;
		contract_EJAR2.LessorIdentificationNumber = LessorIdentificationNumber;
		contract_EJAR2.LandlordsEmail = LandlordsEmail;
		contract_EJAR2.TheLessorIsRepresentedByHimselfOrHerself = TheLessorIsRepresentedByHimselfOrHerself;
		contract_EJAR2.TenantAreaType = TenantAreaType;
		contract_EJAR2.CommercialRegistrationNumberOfTheTenant = CommercialRegistrationNumberOfTheTenant;
		contract_EJAR2.TheIssuingEntityOfTheTenant = TheIssuingEntityOfTheTenant;
		contract_EJAR2.TheNameOfTheTenantCompany = TheNameOfTheTenantCompany;
		contract_EJAR2.UnifiedNumberOfTheTenant = UnifiedNumberOfTheTenant;
		contract_EJAR2.TheDateOfTheCommercialRegistrationOfTheTenant = TheDateOfTheCommercialRegistrationOfTheTenant;
		contract_EJAR2.TheNameOfTheTenantsRepresentative = TheNameOfTheTenantsRepresentative;
		contract_EJAR2.TheNationalityOfTheTenantsRepresentative = TheNationalityOfTheTenantsRepresentative;
		contract_EJAR2.Note = Note;
		db.Contract_EJAR.Add(contract_EJAR2);
		db.SaveChanges();
		return contract_EJAR2.ID;
	}

	public int Delete(int Code)
	{
		int result = 0;
		Contract_EJAR contract_EJAR = db.Contract_EJAR.Where((Contract_EJAR x) => x.ID == Code).FirstOrDefault();
		if (contract_EJAR != null)
		{
			db.Contract_EJAR.Attach(contract_EJAR);
			db.Contract_EJAR.Remove(contract_EJAR);
			result = db.SaveChanges();
		}
		return result;
	}
}


