namespace DynamicClassProj.Models.Services
{
    public interface IFormulaEngine
    {
        T Evaluate<T>(string fieldName, object scopeModel);
    }
}
