namespace PayaInsProj.Models.PayaInsureDbData
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public  class FileUpoadHistory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [StringLength(300)]
        [Display(Name = "توضیحات")]
        public string Comment { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "نام فایل")]
        public string FileName { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime RegDate { get; set; }
        [Display(Name = "کاربر")]
        public int? UserId { get; set; }
    }
}
