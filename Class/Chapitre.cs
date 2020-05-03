using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VacumUI
{
    public class Chapitre : Manga
    {
        private String _chapTitleFromUrl;
        private int _chapNumber;

        public String ChapTitleFromUrl
        {
            get { return _chapTitleFromUrl; }
            set
            {
                _chapTitleFromUrl = value;
                ChapTitleClean = Outils.cleanString(_chapTitleFromUrl).Replace(".",",");
                ChapTitleShow = Outils.cutStringShow(ChapTitleClean);
            }
        }
        public String ChapTitleClean { get; private set; }
        public String ChapTitleShow { get; private set; }
        public List<Picture> ChapPicLst { get; set; }
        public Uri ChapUrl { get; set; }
        public String ChapPath { get; set; }

        public int ChapNumber {
            get { return _chapNumber; }
            set
            {
                _chapNumber = value;
                String chapNbrStr = _chapNumber.ToString();
                // Pour les chaps on met 3 digits d'office, car il y a généralement entre 100 et 999 chap
                int lgtNeed = 3;
                int diff = lgtNeed - chapNbrStr.Length;
                if (diff > 0)
                {
                    for (int i = 0; i < diff; i++) chapNbrStr = "0" + chapNbrStr;
                    ChapPath = Path.Combine(MangaPath, "Chap " + chapNbrStr);
                } else
                {
                    ChapPath = Path.Combine(MangaPath, "Chap " + chapNbrStr);
                }
            }
        }
        public int ChapNbrPage { get; set; }
        public int ChapNbrPageAlreadyDled { get; set; }

        public bool chapAlreadyDled { get; set; }

        public Chapitre(Manga manga, string chapTitleUrl, string chapUrl)
        {
            MangaPath = manga.MangaPath;
            ChapUrl = new Uri(chapUrl);
            ChapPicLst = new List<Picture>();
            ChapTitleFromUrl = chapTitleUrl;
            int.TryParse(ChapTitleFromUrl.Split(' ').Last(), out int i);
            ChapNumber = i;
            ChapNbrPage = 0;
            ChapNbrPageAlreadyDled = 0;
            chapAlreadyDled = false;
        }
        public Chapitre() { 
        }

        public Chapitre(Manga manga, Uri url)
        {
            MangaPath = manga.MangaPath;
            ChapUrl = url;
            ChapPicLst = new List<Picture>();
            int idxLstSeg = url.Segments.Length - 1;
            ChapTitleFromUrl = url.Segments[idxLstSeg];
            int.TryParse(ChapTitleFromUrl.Split(' ').Last(), out int i);
            ChapNumber = i;
            ChapNbrPage = 0;
            ChapNbrPageAlreadyDled = 0;
            chapAlreadyDled = false;
        }



        public static void getChapPages(Chapitre chap, object o)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument chapterPage = web.Load(chap.ChapUrl);
            IEnumerable<string> picsUrl;
            if (o is LelScan)
                picsUrl = LelScan.getChapPages(chapterPage);
            else
                throw new Exception();
            addPicToChapPicLst(chap, picsUrl);
        }
        private static void addPicToChapPicLst(Chapitre c, IEnumerable<string> picsUrl)
        {
            foreach (var picUrl in picsUrl)
            {
                Picture p = new Picture(c, new Uri(picUrl));
                p.PicPath = Path.Combine(c.ChapPath, p.PicFullName);
                c.ChapPicLst.Add(p);
            }
            c.ChapNbrPage = c.ChapPicLst.Count;
        }

        /// <summary>
        /// Complète l'objet Manga avec la liste des tous les chapitres trouvés
        /// </summary>
        /// <param name="manga"></param>
        /// <returns></returns>
        public static void getChapsFromManga(Manga m, object o)
        {
            HtmlWeb web = new HtmlWeb();
            IEnumerable<String> chapsLinks;
            HtmlAgilityPack.HtmlDocument mPag = web.Load(m.MangaUrl);
            if (o is LelScan)
            {
                chapsLinks = LelScan.getChapsLinksFromManga(m, mPag);
            }
            else
                throw new Exception();
            m.MangaChapCompleteLst = Chapitre.addChapToChapLst(m, chapsLinks);
        }

        private static List<Chapitre> addChapToChapLst(Manga m, IEnumerable<String> chapsLinks)
        {
            // c'est le plus haut chapitre récupéré la dernière fois
            int higherChap = m.HigherChap;
            List<Chapitre> cLst = new List<Chapitre>();
            foreach (String url in chapsLinks)
            {
                Uri uri = new Uri(url);
                int idxLstSeg = uri.Segments.Length - 1;
                int.TryParse(uri.Segments[idxLstSeg].Split(' ').Last(), out int chapNbr);
                // si on avait déjà téléchargé un chapitre plus haut, on ne redl pas celui la
                // mais on parcours quand même toute la liste car on ne sait pas dans quel ordre on récup les chapitres
                //if (chapNbr > m.HigherChap)
                //{
                    Chapitre c = new Chapitre(m, uri);
                    cLst.Add(c);
                    if (chapNbr > higherChap)
                        higherChap = chapNbr;
                //}
            }
            m.HigherChap = higherChap;
            return cLst;
        }

        internal static void getInfoChapitres(List<Chapitre> cDlLst, object o)
        {
            foreach(Chapitre c in cDlLst)
            {
                Outils.checkFicChap(c);
                //si le chapitre à déjà été rappatrié, on s'arrête la, on prends le partie pris de considérer que si le dossier / fichier existe, il est complet
                if (!c.chapAlreadyDled)
                {
                    //Sinon, on récupère la liste des pages du chapitre et on ajoute le chapitre dans la liste à dl
                    Chapitre.getChapPages(c, o);
                }
            }
        }
        internal static void getInfoChapitres(Chapitre c, object o)
        {
            Outils.checkFicChap(c);
            //si le chapitre à déjà été rappatrié, on s'arrête la, on prends le partie pris de considérer que si le dossier / fichier existe, il est complet
            if (!c.chapAlreadyDled)
            {
                //Sinon, on récupère la liste des pages du chapitre et on ajoute le chapitre dans la liste à dl
                Chapitre.getChapPages(c, o);
            }
        }
    }
}