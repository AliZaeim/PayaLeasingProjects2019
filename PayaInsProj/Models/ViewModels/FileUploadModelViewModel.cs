using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PayaInsProj.Models.ViewModels
{
    public class FileUploadModelViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Display(Name = "فایل اکسل")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
       
    }
}