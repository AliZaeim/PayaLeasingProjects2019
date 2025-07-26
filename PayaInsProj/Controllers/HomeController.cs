using PayaInsProj.Models;
using PayaInsProj.Models.Dtos;
using PayaInsProj.Models.PayaInsureDbData;
using PayaInsProj.Models.ViewModels;
using PayaInsProj.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Services;

namespace PayaInsProj.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly PayaInsureRepository _payaInsureRepository;
        public HomeController()
        {
            _payaInsureRepository = new PayaInsureRepository();
        }
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(FileUploadModelViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            try
            {

                if (model.File != null)
                {


                    Dictionary<string, string> columns = new Dictionary<string, string>()
                    {
                        { "ردیف", "Rank" },
                        { "تاریخ صدور بیمه نامه", "InsuranceIssueDate" },
                        { "بیمه گذار بیمه نامه", "InsuranceInsurer" },
                        { "نوع خودرو", "CarType" },
                        { "شماره کامل بیمه نامه", "InsurancePolicyNumber" },
                        { "تاریخ شروع بیمه نامه", "InsuranceStartDate" },
                        { "تاریخ پایان بیمه نامه", "InsuranceEndDate" },
                        { "شماره پلاک", "PlateNumber" },
                        { "شماره موتور", "EngineNumber" },
                        { "شماره شاسی", "ChassisNumber" },
                        { "خالص حق بیمه + مالیات و عوارض ارزش افزوده", "NetPremiumVATDuties" },
                        { "کد ملی بیمه گذار", "InsuredNC" },
                        { "بیمه نامه سال چندم", "InsuranceAfterYear" }
                    };

                    List<string> BaseCols = columns.Keys.Cast<string>().ToList<string>();
                    //(bool valid, List<string> messages) validRes = ExcelExportHelper.ValidationUploadedExcelFile(SheetName: "پایا", SheetIndex: 0, formFile: model.File, BaseCols, maxVolum: 200);
                    //if (validRes.valid)
                    //{
                    string virtualPath = "~/App_Data/ExcelFiles";
                    string serverPath = Server.MapPath(virtualPath);
                    //string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "~/App_Data/ExcelFiles");
                    if (!Directory.Exists(serverPath))
                    {
                        Directory.CreateDirectory(serverPath);
                    }
                    
                    string fileName = DateTime.Now.ConvertDateTimeToString() + "-" + "UId1" + "-" + model.File.FileName;
                    FileUpoadHistory upload = new FileUpoadHistory()
                    {
                        Title = $"آپلود فایل اکسل در تاریخ {DateTime.Now.ToShamsi()}",
                        FileName = fileName,
                        RegDate = DateTime.Now,
                        UserId = 1,
                    };
                    _payaInsureRepository.CreateFileUpload(upload);
                    _payaInsureRepository.SaveChanges();
                    string fullPath = Path.Combine(serverPath, fileName);
                    model.File.SaveAs(fullPath);
                    //using (Stream stream = new FileStream(fullPath, FileMode.Create))
                    //{
                    //    model.File.InputStream.CopyTo(stream, bufferSize: 81920);
                    //}

                    DataTable dt = ExcelExportHelper.ReadUploadedExcelAsDataTable(model.File);
                    dt = dt.ChangeDataTableColumnsTitles(columns);
                    List<ExcelMapDto> excelMapDtos = dt.ConvertDataTable<ExcelMapDto>();


                    int loop = 0;
                    foreach (var item in excelMapDtos)
                    {
                        InsuranceCustomerContract insuranceCustomerContract = _payaInsureRepository.GetInsuranceCustomerContractsByNCAndChNEngN(natinalCode: item.InsuredNC, chasisNumber: item.ChassisNumber, enginNumber: item.EngineNumber);
                        if (insuranceCustomerContract != null)
                        {
                            InsuranceDetailCustomerContract insuranceDetailCustomerContract = insuranceCustomerContract.InsuranceDetailCustomerContracts.SingleOrDefault(x => x.InsuranceNumber == int.Parse(item.InsuranceAfterYear));
                            insuranceCustomerContract.ConfirmIssuDate = DateTime.Now.ToShamsi().ToString();
                            insuranceCustomerContract.ConfirmIssuTime = DateTime.Now.ToString("HH:mm:ss");
                            insuranceCustomerContract.ConfrimIssuUserId = (long)upload.UserId;
                            _payaInsureRepository.UpdateInsuranceCusotomerContract(insuranceCustomerContract);
                            if (insuranceDetailCustomerContract != null)
                            {

                                insuranceDetailCustomerContract.InsuranceIssueDate = item.InsuranceIssueDate;
                                insuranceDetailCustomerContract.InsuranceIssueNumber = item.InsurancePolicyNumber;
                                insuranceDetailCustomerContract.InsuranceBeginDate = item.InsuranceStartDate;
                                insuranceDetailCustomerContract.InsuranceEndDate = item.InsuranceEndDate;
                                insuranceDetailCustomerContract.ConfirmIssuDate = DateTime.Now.ToShamsi().ToString();//تاریخ شمسی قرار داده شود
                                insuranceDetailCustomerContract.ConfirmIssuTime = DateTime.Now.ToString("HH:mm:ss");
                                insuranceDetailCustomerContract.ConfrimIssuUserId = (long)upload.UserId;
                                loop++;
                            }
                            _payaInsureRepository.UpdateInsuranceDetailCustomerContract(insuranceDetailCustomerContract);
                        }
                    }

                    string Message = "فایل با موفقیت آپلود شد" + $" و تعداد {loop} رکورد اصلاح شد.";
                    _payaInsureRepository.SaveChanges();

                    return Content(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, message = Message }), "application/json");



                    //}
                    //else
                    //{
                    //    string errorMess = string.Empty;
                    //    if (validRes.messages != null)
                    //    {
                    //        foreach (var item in validRes.messages.ToList())
                    //        {
                    //            if (item == validRes.messages.FirstOrDefault())
                    //            {
                    //                errorMess = item;
                    //            }
                    //            else
                    //            { errorMess += item; }

                    //        }
                    //    }

                    //    return HttpNotFound(errorMess);
                    //}


                }
                else
                {
                    return HttpNotFound("فایلی آپلود نشده است !");
                }



            }
            catch (Exception ex)
            {
                string Mess = ex.Message;
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    Mess = ex.InnerException.Message;
                }

                return Content($"500 | Internal server error: {Mess}");
            }
        }

        public ActionResult InsuranceReport()
        {

            List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts = _payaInsureRepository.GetLastIssuedInsurances(string.Empty, string.Empty);
            InsuranceReportViewModel insuranceReportViewModel = MapDetailContractToReportViewModel(insuranceDetailCustomerContracts);
            return View(insuranceReportViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsuranceReport(InsuranceReportViewModel insuranceReportViewModel)
        {
            List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts = _payaInsureRepository.GetLastIssuedInsurances(insuranceReportViewModel.StartDate, insuranceReportViewModel.EndDate, insuranceReportViewModel.AlarmDays);
            insuranceReportViewModel = MapDetailContractToReportViewModel(insuranceDetailCustomerContracts, insuranceReportViewModel.StartDate, insuranceReportViewModel.EndDate, insuranceReportViewModel.AlarmDays);
            return View(insuranceReportViewModel);
        }
        private InsuranceReportViewModel MapDetailContractToReportViewModel(List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts, string startDate = null, string endDate = null, int? FormAlarmDays = 10)
        {
            InsuranceReportViewModel insuranceReportViewModel = new InsuranceReportViewModel()
            {
                StartDate = startDate,
                EndDate = endDate,
                InsInfoDtos = insuranceDetailCustomerContracts
                .Select(s => new InsInfoDto()
                {
                    InsertDate = s.InsertDate,
                    InsertTime = s.InsertTime,
                    InsuranceIssueDate = s.InsuranceIssueDate,
                    InsuranceIssueNumber = s.InsuranceIssueNumber,
                    InsuranceYear = (int)s.InsuranceNumber,
                    InsurerContractBeginDate = s.InsuranceBeginDate,
                    InsurerContractEndDate = s.InsuranceEndDate,
                    ChassisNumber = s.InsuranceCustomerContract?.ChassisNumber,
                    EngineNumber = s.InsuranceCustomerContract?.EngineNumber,
                    Nationalcode = s.InsuranceCustomerContract?.Nationalcode,
                    PlaqueNo = s.InsuranceCustomerContract?.PlaqueNo,
                    InsurerName = s.InsurerName ?? "---",
                    ValidDays = (!string.IsNullOrEmpty(s.InsuranceEndDate)) ? s.InsuranceEndDate.GetDifferenceFromToday() : -9999,
                }).ToList(),
                InsInfoDtosInformations = ClassAttributesHelper.GetObjectPropertyNameAndDisplayNameAndValue<InsInfoDto>(new InsInfoDto(), false),
                AlarmDays = FormAlarmDays
            };
            insuranceReportViewModel.AlarmDaysCount = insuranceReportViewModel.InsInfoDtos.Count(s => s.InsurerContractEndDate?.GetDifferenceFromToday() <= FormAlarmDays);
            return insuranceReportViewModel;
        }
        /// <summary>
        /// ایجاد فایل اکسل
        /// </summary>
        /// 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExcelReport(InsuranceReportViewModel insuranceReportViewModel)
        {
            string FName = DateTime.Now.ConvertDateTimeToString() + "-" + "InsureExcelReport.xlsx";
            Dictionary<string, string> res1 = ClassAttributesHelper.GetPropertyNamesAndDisplayNames<InsInfoDto>();
            PropertyInfo[] props = new InsInfoDto().GetType().GetProperties();
            List<InsuranceDetailCustomerContract> insuranceDetailCustomerContracts = _payaInsureRepository.GetLastIssuedInsurances(insuranceReportViewModel.StartDate, insuranceReportViewModel.EndDate, insuranceReportViewModel.AlarmDays);
            insuranceReportViewModel = MapDetailContractToReportViewModel(insuranceDetailCustomerContracts, insuranceReportViewModel.StartDate, insuranceReportViewModel.EndDate, insuranceReportViewModel.AlarmDays);
            List<string> cols = props.Where(w => !w.IsKey()).Select(f => f.Name).ToList();


            string virtualPath = "~/App_Data/ExcelReports";
            string serverPath = Server.MapPath(virtualPath);
            string root = Path.Combine(serverPath, FName);
            string Caption = "گزارش بیمه نامه های صادر شده";
            if (!string.IsNullOrEmpty(insuranceReportViewModel.StartDate))
            {
                Caption += " از تاریخ " + insuranceReportViewModel.StartDate;
            }
            if (!string.IsNullOrEmpty(insuranceReportViewModel.EndDate))
            {
                Caption += " تا تاریخ " + insuranceReportViewModel.EndDate;
            }
            if (insuranceReportViewModel.AlarmDays != null)
            {
                Caption += " با اعتبار کمتر و مساوی " + insuranceReportViewModel.AlarmDays + " روز";
            }

            ExcelGenerator.GenerateExcel<InsInfoDto>(root, insuranceReportViewModel.InsInfoDtos, selectedCols: null, sheetName: "InsSheet1", caption: Caption);
            //(string filePath, string fileName) = ExcelExportHelper.WriteExcelWithNPOI(Entity: new InsInfoDto(), data: insuranceReportViewModel.InsInfoDtos!, Root: root, SelectedCols: cols, title: "گزارش بیمه", SheetName: "MyInsSheet", FileName: FName,false);

            ExcelReportResultViewModel excelReportResultView = new ExcelReportResultViewModel()
            {
                FileName = FName,
                Root = root
            };
            //return View(excelReportResultView);
            return File(root, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FName);
            //return File(root, "xlsx");
        }
        public ActionResult FileUploads()
        {
            List<FileUpoadHistory> fileUpoadHistories = _payaInsureRepository.GetFileUpoadHistories();
            return View(fileUpoadHistories);
        }
        public ActionResult DownloadFile(string fileName)
        {

            string virtualPath = "~/App_Data/ExcelFiles";
            string serverPath = Server.MapPath(virtualPath);
            string root = Path.Combine(serverPath, fileName);
            if (!System.IO.File.Exists(root))
            {
                return Content("فایل مورد نظر موجود نمی باشد !");
            }
            return File(root, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}