using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordDocumentCompleting2019.Helpers
{

    public class PersianTextExtractionStrategy : LocationTextExtractionStrategy
    {
        //public override void RenderText(TextRenderInfo renderInfo)
        //{
        //    base.RenderText(renderInfo);

        //    // Force RTL direction for the entire text
        //    this.AppendTextChunk("\u200F");  // Unicode RTL mark
        //}

        //protected override void AppendTextChunk(string newText)
        //{
        //    // Reverse chunk order for RTL
        //    base.AppendTextChunk(ReverseString(newText));
        //}

        private string ReverseString(string input)
        {
            // Only reverse non-English/non-numeric chunks
            if (string.IsNullOrWhiteSpace(input) || IsLatinString(input))
                return input;

            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        private bool IsLatinString(string input)
        {
            // Check if string contains only Latin characters
            return input.All(c => c <= 0x00FF);
        }
    }

}