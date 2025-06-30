using System.Collections.Generic;
using System.Linq;

namespace WordDocumentCompleting2019.Helpers
{
    public static class PersianNumberHelper
    {
        // Map English digits to Persian digits
        private static readonly Dictionary<char, char> NumberMap = new Dictionary<char, char>
        {
            {'0', '۰'}, {'1', '۱'}, {'2', '۲'}, {'3', '۳'}, {'4', '۴'},
            {'5', '۵'}, {'6', '۶'}, {'7', '۷'}, {'8', '۸'}, {'9', '۹'}
        };

        public static string ConvertToPersianNumbers(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            // Convert numbers and preserve RTL marks
            return string.Concat(input.Select(c =>
                NumberMap.ContainsKey(c) ? NumberMap[c] : c
            ));
        }

        public static string ConvertToPersianNumbers(object value)
        {
            return value == null ? string.Empty : ConvertToPersianNumbers(value.ToString());
        }
    }
}