using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VacumUI
{
    public class Picture : Chapitre
    {
        public Uri PicUrl { get; set; }
        public String PicFullName { get; set; }
        public String PicNameNoExt { get; set; }
        public String PicExt { get; set; }
        public String PicPath { get; set; }
        public int PicNumber { get; set; }


        public Picture(Chapitre chap, Uri imgUrl)
        {
            ChapPath = chap.ChapPath;
            PicUrl = imgUrl;
            String urlPath = PicUrl.AbsoluteUri;
            PicFullName = Path.GetFileName(urlPath);
            PicNameNoExt = Path.GetFileNameWithoutExtension(urlPath);
            PicExt = Path.GetExtension(urlPath);
            Regex reg = new Regex ( @"\b*\d+(,?|.?)\d*\b" );
            String nbr = reg.Match(PicNameNoExt).ToString();
            int.TryParse(nbr, out int num);
            PicNumber = num;
        }
    }
}
