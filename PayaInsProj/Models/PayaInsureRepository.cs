using PayaInsProj.Models.PayaInsureDbData;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.InteropServices;
using PayaInsProj.Utility;
using System.Data.Entity;
namespace PayaInsProj.Models
{
    public class PayaInsureRepository
    {
        private readonly PIDbContext _context;
        public PayaInsureRepository()
        {
            _context = new PIDbContext();
        }
        public bool CreateFileUpload(FileUpoadHistory uploadHistory)
        {
            _context.FileUpoadHistories.Add(uploadHistory);
            return true;
        }
        public bool CheckInsuranceCustomerContactTableHasData()
        {
            if (_context.InsuranceCustomerContracts.Any())
            {
                return true;
            }
            return false;
        }
        public bool CheckInsuranceDetailCustomerContractTableHasData()
        {
            if (_context.InsuranceDetailCustomerContracts.Any())
            {
                return true;
            }
            return false;
        }
        public bool CreateInsurnceCutomerContracts(List<InsuranceCustomerContract> insuranceCustomerContracts)
        {
            _context.InsuranceCustomerContracts.AddRange(insuranceCustomerContracts);
            return true;
        }
        public bool CreateInsuranceDetailCustomerContracts(List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts)
        {
            _context.InsuranceDetailCustomerContracts.AddRange(insuranceDetailCustomerContracts);
            return true;
        }
        public void CreateInsuranceCusotomerContract(InsuranceCustomerContract insuranceCustomerContract)
        {
            _context.InsuranceCustomerContracts.Add(insuranceCustomerContract);
        }

        public void CreateInsuranceDetailCustomerContract(InsuranceDetailCustomerContract insuranceDetailCustomerContract)
        {
            _context.InsuranceDetailCustomerContracts.Add(insuranceDetailCustomerContract);
        }

      

        public void DeleteInsuranceCusotomerContract(InsuranceCustomerContract inuranceCustomerContract)
        {
            _context.InsuranceCustomerContracts.Remove(inuranceCustomerContract);
        }

        public void DeleteInsuranceDetailCustomerContract(InsuranceDetailCustomerContract insuranceDetailCustomerContract)
        {
            _context.InsuranceDetailCustomerContracts.Remove(insuranceDetailCustomerContract);
        }

       

        public bool ExistInsuranceCustomerContractById(int id)
        {
            return _context.InsuranceCustomerContracts.Any(x => x.ID == id);
        }

        public bool ExistInsuranceDetailCustomerContractById(int id)
        {
            return _context.InsuranceDetailCustomerContracts.Any(x => x.ID == id);
        }
        public InsuranceCustomerContract GetInsuranceCustomerContractById(int id)
        {
            return _context.InsuranceCustomerContracts.FirstOrDefault(x => x.ID == id);
        }

        public List<InsuranceCustomerContract> GetInsuranceCustomerContracts()
        {
            return _context.InsuranceCustomerContracts.ToList();
        }

        public InsuranceCustomerContract GetInsuranceCustomerContractsByNCAndChNEngN(string natinalCode, string chasisNumber, string enginNumber)
        {
            return _context.InsuranceCustomerContracts.FirstOrDefault(f => f.Nationalcode == natinalCode && f.ChassisNumber == chasisNumber && f.EngineNumber == enginNumber);
        }

        public InsuranceDetailCustomerContract GetInsuranceDetailCustomerContractById(int id)
        {
            return _context.InsuranceDetailCustomerContracts.FirstOrDefault(x => x.ID == id);
        }

        public List<InsuranceDetailCustomerContract> GetInsuranceDetailCustomerContracts()
        {
            return _context.InsuranceDetailCustomerContracts
                
                .Include(t => t.InsuranceCustomerContract)
                .ToList();
        }

        public List<InsuranceDetailCustomerContract> GetInsuranceDetailCustomerContractsByDate(string startDate, string endDate)
        {
            string x = "0";
            //Date > startDate return 1 , Date = startDate return 0, Date< startDate return -1
            //int cmp = "1404/02/27".CompareTo(startDate);
            List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts = _context.InsuranceDetailCustomerContracts
                .Include(n => n.InsuranceCustomerContract)
                .Where(w => w.InsuranceNumber != 1 && string.IsNullOrEmpty(w.InsuranceIssueNumber)).ToList();
            if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {

                x += "1";
                insuranceDetailCustomerContracts = insuranceDetailCustomerContracts
                .Where(w => w.InsuranceBeginDate?.CompareTo(startDate) >= 0)
                .ToList();
            }
            if (string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                x += "2";
                insuranceDetailCustomerContracts = insuranceDetailCustomerContracts
                .Where(w => w.InsuranceBeginDate?.CompareTo(endDate) <= 0)
                .ToList();
            }
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                x += "3";
                insuranceDetailCustomerContracts = insuranceDetailCustomerContracts
                    .Where(w => w.InsuranceBeginDate?.CompareTo(startDate) >= 0
                    && w.InsuranceBeginDate?.CompareTo(endDate) <= 0)
                    .ToList();
            }
            return insuranceDetailCustomerContracts;
        }
        private List<InsuranceDetailCustomerContract> GetLastIssueInsuranceDetailsBasedonEndDate(string startDate, string endDate, [Optional] int? ValidDay)
        {
            List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts = new List<InsuranceDetailCustomerContract>();
            if (ValidDay != null)
            {
                insuranceDetailCustomerContracts = new List<InsuranceDetailCustomerContract>();
                insuranceDetailCustomerContracts = _context.InsuranceCustomerContracts
                        .Include(n => n.InsuranceDetailCustomerContracts).AsEnumerable()
                   .Select(w => w.InsuranceDetailCustomerContracts.OrderByDescending(o => o.InsuranceNumber)
                        .FirstOrDefault(f => !string.IsNullOrEmpty(f.InsuranceEndDate) && !string.IsNullOrEmpty(f.InsuranceIssueNumber)))
                   .Where(w => w.InsuranceEndDate?.GetDifferenceFromToday() <= ValidDay.Value)
                   .ToList();

            }
            else
            {
                insuranceDetailCustomerContracts = new List<InsuranceDetailCustomerContract>();
                insuranceDetailCustomerContracts = _context.InsuranceCustomerContracts
                        .Include(n => n.InsuranceDetailCustomerContracts).AsEnumerable()
                   .Select(w => w.InsuranceDetailCustomerContracts.OrderByDescending(o => o.InsuranceNumber)
                        .FirstOrDefault(f => !string.IsNullOrEmpty(f.InsuranceEndDate) && !string.IsNullOrEmpty(f.InsuranceIssueNumber)))
                   .ToList();
            }


            return insuranceDetailCustomerContracts;
        }
        public List<InsuranceDetailCustomerContract> GetLastIssuedInsurances(string startDate, string endDate, [Optional] int? ValidDay)
        {
            List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts = new List<InsuranceDetailCustomerContract>();

            insuranceDetailCustomerContracts = GetLastIssueInsuranceDetailsBasedonEndDate(startDate, endDate, ValidDay);

            if (!string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                insuranceDetailCustomerContracts = insuranceDetailCustomerContracts.Where(w => w.InsuranceEndDate.CompareTo(startDate) >= 0).ToList();
            }
            if (!string.IsNullOrEmpty(endDate) && string.IsNullOrEmpty(startDate))
            {
                insuranceDetailCustomerContracts = insuranceDetailCustomerContracts.Where(w => w.InsuranceEndDate.CompareTo(endDate) <= 0).ToList();
            }
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                insuranceDetailCustomerContracts = insuranceDetailCustomerContracts.
                    Where(r => r.InsuranceEndDate.CompareTo(startDate) >= 0 &&
                        r.InsuranceEndDate.CompareTo(endDate) <= 0
                    ).ToList();
            }

            return insuranceDetailCustomerContracts;
        }

        

        public void SaveChanges()
        {
            _context.SaveChanges();
        }



        public void UpdateInsuranceCusotomerContract(InsuranceCustomerContract insuranceCustomerContract)
        {
            _context.InsuranceCustomerContracts.AddOrUpdate(insuranceCustomerContract);
        }

        public void UpdateInsuranceDetailCustomerContract(InsuranceDetailCustomerContract inuranceDetailCustomerContract)
        {
            _context.InsuranceDetailCustomerContracts.AddOrUpdate(inuranceDetailCustomerContract);
        }
        public List<FileUpoadHistory> GetFileUpoadHistories()
        {
            return _context.FileUpoadHistories.ToList();
        }
        
    }

}