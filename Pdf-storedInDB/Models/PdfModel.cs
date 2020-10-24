using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pdf_storedInDB.Models
{
    public class PdfModel
    {
        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}