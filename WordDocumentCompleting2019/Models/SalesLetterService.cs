using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordDocumentCompleting2019.Models
{
    public class SalesLetterService
    {
        Dictionary<string, bool> keyValuePairs = new Dictionary<string, bool>();
        public byte[] FillTemplate(byte[] templateBytes, SalesLetterModel data)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(templateBytes, 0, templateBytes.Length);
                using (var doc = WordprocessingDocument.Open(stream, true))
                {
                    var body = doc.MainDocumentPart.Document.Body;
                    //var headers = doc.MainDocumentPart.HeaderParts.ToList();
                    List<DicModel> PlaceValueDic = new List<DicModel>
                    {
                        new DicModel() { Key = "SELLERNAME", Value = data.SellerName },
                        new DicModel() { Key = "SRELATED", Value = data.SRelated },
                        new DicModel() { Key = "STASIS", Value = data.SStartDate },
                        new DicModel() { Key = "SELLERID", Value = data.SellerId },
                        new DicModel() { Key = "SADEREH", Value = data.SSadereh },
                        new DicModel() { Key = "SNC", Value = data.SNC },
                        new DicModel() { Key = "ADDRESS", Value = data.SAddress },
                        new DicModel() { Key = "ECONO", Value = data.SEcoNo },
                        new DicModel() { Key = "PHONE", Value = data.SPhone },
                        new DicModel() { Key = "CELLPHONE", Value = data.SCellphone },
                        new DicModel() { Key = "POSTALCODE", Value = data.SPostalCode },

                        new DicModel() { Key = "BUYERNAME", Value = data.BuyerName },
                        new DicModel() { Key = "BRELATED", Value = data.BRelated },
                        new DicModel() { Key = "BTASIS", Value = data.BStartDate },
                        new DicModel() { Key = "BUYERID", Value = data.BuyerId },
                        new DicModel() { Key = "BSADEREH", Value = data.BSadereh },
                        new DicModel() { Key = "BNC", Value = data.BNC },
                        new DicModel() { Key = "BADDRESS", Value = data.BAddress },
                        new DicModel() { Key = "BECONO", Value = data.BEcoNo },
                        new DicModel() { Key = "BPHONE", Value = data.BPhone },
                        new DicModel() { Key = "BCELLPHONE", Value = data.BCellphone },
                        new DicModel() { Key = "BPOSTALCODE", Value = data.BPostalCode },

                        new DicModel() { Key = "DASTGAH", Value = data.Dastgah },
                        new DicModel() { Key = "SYSTEM", Value = data.System },
                        new DicModel() { Key = "TIP", Value = data.Tip },
                        new DicModel() { Key = "MODEL", Value = data.MModel },
                        new DicModel() { Key = "COLOR", Value = data.Color },
                        new DicModel() { Key = "ENGINENOMBER", Value = data.EngineNomber },
                        new DicModel() { Key = "CHASISENUMBER", Value = data.ChasiseNumber },
                        new DicModel() { Key = "RAHNO", Value = data.RahnamiyRanandegoNO },
                        new DicModel() { Key = "KE", Value = data.KeDaraye },
                        
                        new DicModel(){ Key = "OrderNoooooomber", Value=data.OrderNomber },
                        new DicModel(){ Key = "OrderDate", Value=data.OrderDate },
                    };

                    var Res = ReplaceWordPlaceHolder(body, PlaceValueDic);               
                    doc.Save();
                }
                return stream.ToArray();
            }
        }

        private bool ReplacePlaceholder(Body body, string placeholder, string value)
        {
            bool res = false;
            var txts = body.Descendants<Text>();
            List<Text> st = txts.ToList();


            foreach (Text text in txts)
            {
                if (text.Text.Contains(placeholder))
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        text.Text = text.Text.Replace(placeholder, value);
                        res = true;
                    }
                }
            }
            return res;

        }
        public class DicModel
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
        private Dictionary<string,string> ReplaceWordPlaceHolder(Body body, List<DicModel> data)
        {
            Dictionary<string,string> Result = new Dictionary<string,string>();
            
            List<Text> texts = body.Descendants<Text>().ToList();
            texts = texts.Where(w => !string.IsNullOrEmpty(w.Text)).ToList();
            List<Words> lines = body.Descendants<Words>().ToList();
            List<Paragraph> pars = body.Descendants<Paragraph>().ToList();
            var h = body.Descendants<Header>().ToList();
            IEnumerable<Table> tables = body.Descendants<Table>();
            pars = pars.Where(w => !string.IsNullOrEmpty(w.InnerText)).ToList();
            int c = texts.Select(x => x.InnerText.Trim()).Intersect(data.Select(s => s.Key.Trim())).Count();
            
            
            foreach(var item in data)
            {
                if (texts.Any(a => a.Text.Trim() == item.Key))
                {
                    Text txt = texts.FirstOrDefault(f => f.Text.Trim() == item.Key);
                    if ( (txt != null))
                    {
                        txt.Text = item.Value;
                        Result.Add(item.Key,item.Value);
                    }
                    
                }
            }
            return Result;
        }

    }
}