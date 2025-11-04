using System.Collections.Generic;

namespace DynamicClassProject.Models.ViewModels
{
    public class DynamicClassViewModel
    {
        /// <summary>
        /// نام کلاس
        /// </summary>
        public string SelectedClass { get; set; }
        public bool Marked { get; set; }
        /// <summary>
        /// فیلد ها و ویژگی هایشان
        /// </summary>
        public List<PropertyInputVM> Properties { get; set; } = new List<PropertyInputVM>();
    }
}