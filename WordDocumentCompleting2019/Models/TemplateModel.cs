using Antlr.Runtime.Tree;

namespace WordDocumentCompleting2019.Models
{
    public class TemplateModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
        public string FontName { get; set; }
        public int FontSize { get; set; }
        public bool Bold { get; set; }
        public bool RighToLeft { get; set; } = true;

    }
}