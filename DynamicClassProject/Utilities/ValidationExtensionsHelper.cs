using DynamicClassProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicClassProject.Utilities
{
    public static class ValidationExtensionsHelper
    {
        public static IDictionary<string, object> GetValidationAttributes(this HtmlHelper html, PropertyInputVM prop)
        {
            var dict = new Dictionary<string, object>();

            if (prop.Validations != null && prop.Validations.Any())
                dict["data-val"] = "true"; // enable client-side validation

            foreach (var attr in prop.Validations)
            {
                switch (attr)
                {
                    case RequiredAttribute req:
                        dict["data-val-required"] = req.ErrorMessage ?? $"لطفا {prop.DisplayName} را وارد کنید!";
                        break;

                    case RangeAttribute range:
                        dict["data-val-range"] = range.ErrorMessage ?? $"{prop.DisplayName} باید بین {range.Minimum} و {range.Maximum} باشد.";
                        dict["data-val-range-min"] = range.Minimum;
                        dict["data-val-range-max"] = range.Maximum;
                        break;

                    case StringLengthAttribute str:
                        dict["data-val-length"] = str.ErrorMessage ?? $"تعداد کاراکترهای {prop.DisplayName} غیر مجاز می باشد!.";
                        dict["data-val-length-min"] = str.MinimumLength;
                        dict["data-val-length-max"] = str.MaximumLength;
                        break;

                    case RegularExpressionAttribute regex:
                        dict["data-val-regex"] = regex.ErrorMessage ?? $"فرمت {prop.DisplayName} غیر مجاز می باشد!.";
                        dict["data-val-regex-pattern"] = regex.Pattern;
                        break;

                    default:
                        // support custom ValidationAttributes
                        dict[$"data-val-{attr.GetType().Name.ToLower()}"] = attr.ErrorMessage ?? $"{prop.DisplayName} invalid.";
                        break;
                }
            }

            return dict;
        }
    }
}