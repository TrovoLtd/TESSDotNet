using System;
using System.Collections.Generic;

namespace TrovoSiteSearch
{
    public class TrovoMimeTypeConverter
    {

        private Dictionary<string, string> _mimetypes;

        public TrovoMimeTypeConverter()
        {
            _mimetypes = new Dictionary<string, string>();

            _mimetypes.Add("application/pdf", "PDF");
            _mimetypes.Add("application/msword", "MS WORD");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.wordprocessingml.document", "MS WORD");
            _mimetypes.Add("application/vnd.ms-word.document.macroenabled.12", "MS WORD");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.wordprocessingml.template", "MS WORD");
            _mimetypes.Add("application/vnd.ms-word.template.macroenabled.12", "MS WORD");
            _mimetypes.Add("application/postscript", "POSTSCRIPT");
            _mimetypes.Add("application/excel", "MS EXCEL");
            _mimetypes.Add("application/vnd.ms-excel", "MS EXCEL");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MS EXCEL");
            _mimetypes.Add("application/vnd.ms-excel.sheet.macroenabled.12", "MS EXCEL");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.spreadsheetml.template", "MS EXCEL");
            _mimetypes.Add("application/vnd.ms-excel.template.macroenabled.12", "MS EXCEL");
            _mimetypes.Add("application/mspowerpoint", "MS POWERPOINT");
            _mimetypes.Add("application/powerpoint", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.ms-powerpoint", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.presentationml.presentation", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.ms-powerpoint.presentation.macroenabled.12", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.presentationml.slideshow", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.ms-powerpoint.slideshow.macroenabled.12", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.openxmlformats-officedocument.presentationml.template", "MS POWERPOINT");
            _mimetypes.Add("application/vnd.ms-powerpoint.template.macroenabled.12", "MS POWERPOINT");
            _mimetypes.Add("application/rtf", "RICH TEXT FORMAT");
            _mimetypes.Add("application/x-rtf", "RICH TEXT FORMAT");
            _mimetypes.Add("application/vnd.oasis.opendocument.text", "OPEN OFFICE DOCUMENT");
            _mimetypes.Add("application/vnd.oasis.opendocument.presentation", "OPEN OFFICE PRESENTATION");
            _mimetypes.Add("application/vnd.oasis.opendocument.spreadsheet", "OPEN OFFICE SPREADSHEET");
            _mimetypes.Add("application/octet-stream", "OFFICE DOCUMENT"); // added for Leicester City Council
             
        }


        public string convert(string mimeType)
        {
            try
            {
                return _mimetypes[mimeType.ToLower()];
            }
            catch (KeyNotFoundException keyNotFound)
            {
                return String.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
