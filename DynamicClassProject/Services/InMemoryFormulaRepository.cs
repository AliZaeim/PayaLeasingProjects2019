using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicClassProject.Services
{
    public class InMemoryFormulaRepository : IFormulaRepository
    {
        private readonly Dictionary<(string ClassName, string PropertyName), string> _map =
         new Dictionary<(string, string), string>();

        public void Add(string className, string propertyName, string expression)
        {
            _map[(className, propertyName)] = expression;
        }

        public string GetExpression(string className, string propertyName)
        {
            _map.TryGetValue((className, propertyName), out var expr);
            return expr;
        }
    }
}