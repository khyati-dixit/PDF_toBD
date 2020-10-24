using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Pdf_storedInDB.EntityFramework;
using Pdf_storedInDB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Pdf_storedInDB.Controllers
{
    public class HomeController : Controller
    {
        private khyatiDBEntities db = new khyatiDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PdfModel pdf)
        {
            string FileName = System.IO.Path.GetFileNameWithoutExtension(pdf.ImageFile.FileName);                                         //name pike kiya
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();                                             //yahn uska path dala
            pdf.ImagePath = UploadPath + FileName;                                                                                        // both combine kiye model ki property ImagePath me store kiya
            string FilePath = pdf.ImagePath;                                                                                               //string me convert kiya path ko
            pdf.ImageFile.SaveAs(pdf.ImagePath);
            StringBuilder sb = new StringBuilder();
            Pdf_Data pd = new Pdf_Data();

            using (PdfReader reader = new PdfReader(FilePath))
            {
                ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string ExtractedData = string.Empty;
                    ExtractedData = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                    string[] lines = ExtractedData.Split('\n');

                    foreach (string line in lines)
                    {
                        pd.Data = line;
                        db.Pdf_Data.Add(pd);
                        db.SaveChanges();
                    }
                }

            }

            return View(sb.ToString());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}