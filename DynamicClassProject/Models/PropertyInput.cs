namespace DynamicClassProject.Models
{
    public class PropertyInput
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }   // string because we bind from form
    }
}