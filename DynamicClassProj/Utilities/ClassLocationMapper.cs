using System.Collections.Generic;

namespace DynamicClassProj.Utilities
{
    public class ClassLocationMapper
    {
        private  readonly Dictionary<string, string> _classFilePaths;
        public  ClassLocationMapper(Dictionary<string, string> classFilePath)
        {
            _classFilePaths = classFilePath;
        }
        

        //// This method would ideally be populated during build or configuration
        //public  void InitializeMapping()
        //{
        //    // Example: Manually add mappings
        //    _classFilePaths.Add("MyModel", "Models.Test.");
        //    _classFilePaths.Add("ContractSettlement", "Model.Entities.");            
        //}

        public  string GetClassFilePath(string className)
        {
            if (_classFilePaths.TryGetValue(className, out string relativePath))
            {
                // You might need to combine this with the application's base directory
                // For example: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                return relativePath;
            }
            return null; // Class not found in mapping
        }
    }
}