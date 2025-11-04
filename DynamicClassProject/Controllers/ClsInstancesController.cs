using DynamicClassProject.Attributes;
using DynamicClassProject.Models;
using DynamicClassProject.Models.Data;
using DynamicClassProject.Models.Entities;
using DynamicClassProject.Models.ViewModels;
using DynamicClassProject.Utilities;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;

namespace DynamicClassProject.Controllers
{
    public class ClsInstancesController : Controller
    {
        private static readonly List<string> AvailableClasses = ClassLister.GetClassNamesInNamespace();
        private readonly AppDbContext db = new AppDbContext();
        // GET: ClsInstances

        public ActionResult Index()
        {
            ViewBag.Classes = new SelectList(AvailableClasses);
            return View(new DynamicClassViewModel());
        }
        [HttpPost]
        public ActionResult Index(string selectedClass)
        {
            //var assembly = typeof(ClsInstancesController).Assembly;
            //var type = assembly.GetTypes().FirstOrDefault(t => t.Name == selectedClass);
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .FirstOrDefault(t => t.Name == selectedClass);

            if (type == null)
            {
                ViewBag.Classes = new SelectList(AvailableClasses);
                return View(new DynamicClassViewModel());
            }


            DynamicClassViewModel dynamicClassViewModel = new DynamicClassViewModel
            {
                SelectedClass = selectedClass,
                Properties = type.GetProperties().Where(w => !Attribute.IsDefined(w, typeof(FormulaAttribute)) && !Attribute.IsDefined(w, typeof(KeyAttribute)))
                                 .Select(p => new PropertyInputVM
                                 {
                                     Name = p.Name,
                                     DisplayName = p.GetCustomAttribute<DisplayAttribute>() != null ? p.GetCustomAttribute<DisplayAttribute>().Name : p.Name,
                                     Type = (p.PropertyType.Name != typeof(Nullable<>).Name) ? p.PropertyType.Name : Nullable.GetUnderlyingType(p.PropertyType).Name,
                                     PropFormula = db.Formulas.FirstOrDefault(f => f.FieldName == p.Name && f.IsActive && f.ClassName == selectedClass)?.Expression ?? "نامشخص", // get formula from db
                                     IsFormula = p.IsDefined(typeof(FormulaAttribute)),
                                     IsNumeric = AppUtilities.IsNumericType(p.PropertyType),
                                     Value = ""  // empty for user input
                                 }).OrderByDescending(x => x.IsFormula).ToList()
            };

            ViewBag.Classes = new SelectList(AvailableClasses, selectedClass);
            //return View("InputForm", dynamicClassViewModel);
            return RedirectToAction("CreateInstance", new { className = selectedClass });
        }
        public ActionResult CreateInstance(string className)
        {
            Type type = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .FirstOrDefault(t => t.Name == className);
            if (type == null)
            {
                return Content("type is null");
            }
            DynamicClassViewModel dynamicClassViewModel = new DynamicClassViewModel
            {
                Marked = false,
                SelectedClass = className,
                Properties = type.GetProperties().Where(w => /*!Attribute.IsDefined(w, typeof(FormulaAttribute)) &&*/ !Attribute.IsDefined(w, typeof(KeyAttribute)))
                                .Select(p => new PropertyInputVM
                                {
                                    Name = p.Name,
                                    DisplayName = p.GetCustomAttribute<DisplayAttribute>() != null ? p.GetCustomAttribute<DisplayAttribute>().Name : p.Name,
                                    Type = (p.PropertyType.Name != typeof(Nullable<>).Name) ? p.PropertyType.Name : Nullable.GetUnderlyingType(p.PropertyType).Name,
                                    PropFormula = db.Formulas.FirstOrDefault(f => f.FieldName == p.Name && f.IsActive && f.ClassName == className)?.Expression ?? "نامشخص", // get formula from db
                                    IsFormula = p.IsDefined(typeof(FormulaAttribute)),
                                    IsRequired = p.GetCustomAttributes(typeof(RequiredAttribute), false).Any(),
                                    IsNumeric = AppUtilities.IsNumericType(p.PropertyType),
                                    Validations = p.GetCustomAttributes(typeof(ValidationAttribute), false).Cast<ValidationAttribute>(),
                                    Order = p.GetCustomAttribute<DisplayAttribute>()?.GetOrder() ?? p.MetadataToken,
                                    Value = ""  // empty for user input
                                }).OrderBy(x => x.Order).ToList()
            };

            return View(dynamicClassViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInstance(DynamicClassViewModel dynamicClassViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Any())
                                   .ToDictionary(
                                        k => k.Key,
                                        v => v.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                    );
                    return Json(new { success = false, errors });
                }

                bool marked = true;
                Type type = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .FirstOrDefault(t => t.Name == dynamicClassViewModel.SelectedClass);
                if (type == null)
                {
                    return Json(new { success = false, message = "type is null !" });
                }


                object instance = AppUtilities.MapToInstance(dynamicClassViewModel.Properties, type); //Activator.CreateInstance(type);


                List<Formula> formulaList = db.Formulas.Where(w => w.ClassName == dynamicClassViewModel.SelectedClass).ToList();
                Dictionary<string, string> SelectedClassKeys = formulaList.Select(x => new { x.FieldName, x.Expression }).ToDictionary(g => g.FieldName, g => g.Expression);
                instance = GenericFormulaEvaluator.Evaluate(dynamicClassViewModel.SelectedClass, instance, SelectedClassKeys);
                dynamicClassViewModel = AppUtilities.MapInsatnceToViewModel(instance, new DynamicClassViewModel());
                dynamicClassViewModel.Marked = true;


                return Json(new { success = true, message = "valid data", frmdata = dynamicClassViewModel });
            }
            catch (Exception ex)
            {
                // return exception details as JSON to see in AJAX
                return Json(new { success = false, message = ex.Message, stack = ex.StackTrace });
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveInstanceData(DynamicClassViewModel dynamicClassViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "اعتبارسنجی داده ها مشکل دارد !" });
                }
                else
                {
                    if (!string.IsNullOrEmpty(dynamicClassViewModel.SelectedClass))
                    {
                        Type type = AppDomain.CurrentDomain.GetAssemblies()
                                    .SelectMany(a => a.GetTypes())
                                    .FirstOrDefault(t => t.Name == dynamicClassViewModel.SelectedClass);
                        if (type == null)
                        {
                            return Json(new { success = false, message = "type is null !" });
                        }
                        

                        if (dynamicClassViewModel.Properties.Any())
                        {

                            ClassInfo classInfo = new ClassInfo()
                            {
                                ClassName = dynamicClassViewModel.SelectedClass,
                                ClassDatas = dynamicClassViewModel.Properties.Select(s => new ClassData()
                                {
                                    FieldName = s.Name,
                                    FieldValue = s.Value.ToString(),
                                    FieldFormula = db.Formulas.FirstOrDefault(f => f.ClassName == dynamicClassViewModel.SelectedClass && f.FieldName == s.Name)?.Expression,
                                    FieldType = s.Type,
                                }).ToList()
                            };
                            
                            db.ClassInfos.Add(classInfo);
                            try
                            {
                                db.SaveChanges();
                                return Json(new { success = true, message = "داده ها ذخیره شدند !" });
                            }
                            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                            {
                                string er1 = "خطا رخ داده است!";
                                foreach (var eve in ex.EntityValidationErrors)
                                {
                                    er1 = $"Entity of type {eve.Entry.Entity.GetType().Name} in state {eve.Entry.State}";

                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        er1 += $"- Property: {ve.PropertyName} Error: {ve.ErrorMessage}";
                                    }
                                }
                                return Json(new { success = true, message = er1 });
                            }
                            
                        }
                        else
                        {
                            return Json(new { success = false, message = "هیچکدام از ویژگی های کلاس مقداردهی نشده اند !" });
                        }


                    }
                    else
                    {
                        return Json(new { success = false, message = "نام کلاس نامشخص است !" });
                    }
                }
                
                    
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message, stack = ex.StackTrace });
            }
            
        }
        [HttpPost]
        public JsonResult EvaluateFormula(FormulaRequest request)
        {
            try
            {
                var expr = new NCalc.Expression(request.Formula);

                if (request.Parameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in request.Parameters)
                    {
                        
                        double val;
                        if (double.TryParse(kvp.Value.ToString(), out val))
                        {
                            expr.Parameters[kvp.Key] = val;
                        }
                        else
                        {
                            expr.Parameters[kvp.Key] = kvp.Value;
                        }
                    }
                }

                var result = expr.Evaluate();
                return Json(new { success = true, res = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Calculate(string className,DynamicClassViewModel dynamicClassViewModel)
        {
            Dictionary<string, string> formulas = db.Formulas
                        .ToDictionary(f => f.FieldName, f => f.Expression);
            if (ModelState.IsValid)
            {
                var results = GenericFormulaEvaluator.FinalEvaluate(className,formulas);

                return Json(new
                {
                    success = true,
                    data = results
                });
            }

            return Json(new { success = false });
        }

        public ActionResult ListData()
        {
            List<ClassInfo> list = db.ClassInfos.Include(x => x.ClassDatas).ToList();
            return View(list);
        }
    }
}