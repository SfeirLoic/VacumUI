using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Policy;

namespace VacumUI
{
    public class Manga
    {
        //Variables cachés
        private String _mangaTitleFromUrl;

        public String MangaTitleFromUrl
        {
            get { return _mangaTitleFromUrl; }
            set
            {
                _mangaTitleFromUrl = value;
                MangaTitleClean = Outils.cleanString(_mangaTitleFromUrl);
                MangaPath = Path.Combine(Program._rootPath, MangaTitleClean);
                MangaTitleShow = Outils.cutStringShow(MangaTitleClean);
            }
        }

        public String MangaTitleClean { get; private set; }

        public Uri MangaUrl { get; set; }
        public String MangaPath { get; set; }
        public String MangaTitleShow { get; private set; }
        public List<Chapitre> MangaChapCompleteLst { get; set; }
        public int MangaNbrPagesTot { get; set; }
        public List<Chapitre> MangaChapToDlLst { get; set; }
        public int MangaNbrPagesToDl { get; set; }

        public int HigherChap { get; set; }

        public Manga()
        {
            MangaChapCompleteLst = new List<Chapitre>();
            MangaChapToDlLst = new List<Chapitre>();
        }
        public Manga(string titleUrl)
        {
            MangaTitleFromUrl = titleUrl;
            MangaChapCompleteLst = new List<Chapitre>();
            MangaChapToDlLst = new List<Chapitre>();
        }
        public Manga(Uri url)
        {
            int idxLstSeg = url.Segments.Length - 1;
            MangaTitleFromUrl = url.Segments[idxLstSeg];
            MangaUrl = url;
            MangaChapCompleteLst = new List<Chapitre>();
            MangaChapToDlLst = new List<Chapitre>();
        }

        public Manga(string title, Uri url)
        {
            MangaTitleFromUrl = title;
            MangaUrl = url;
            MangaChapCompleteLst = new List<Chapitre>();
            MangaChapToDlLst = new List<Chapitre>();
        }

        internal static void infDlZip(List<Manga> mangaLst)
        {
            LelScan lel = new LelScan();
            Manga.getInfosMangas(mangaLst, lel);
            Outils.dlMangaWebClient(mangaLst);
            Outils.zipAndDel(mangaLst);
        }

        /// <summary>
        /// Récupère tout ce qui est nécéssaire sur les mangas à partir d'une liste
        /// </summary>
        /// <param name="mangaLst"></param>
        /// <returns></returns>
        internal static List<Manga> getInfosMangas(List<Manga> mLst, object o)
        {
            foreach (Manga m in mLst)
            {
                Chapitre.getChapsFromManga(m, o);
                Outils.getChapsToDlList(m, o);
            }
            return mLst;
        }

        /// <summary>
        /// Récupère tous les mangas dispo
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static List<Manga> getAllManga(object o)
        {
            List<Manga> mLst = new List<Manga>();
            if (o is LelScan)
                LelScan.getAllManga(mLst);
            else
                throw new Exception();
            return mLst;
        }
        
        /// <summary>
        /// Récupère la liste de tous les manga populaires de lelscan
        /// </summary>
        /// <returns></returns>
        public static List<Manga> getAllPopularMangaList(object o)
        {
            HtmlWeb web = new HtmlWeb();
            IEnumerable<String> mangaUrl;
            if (o is LelScan)
            {
                LelScan lel = (LelScan)o;
            HtmlAgilityPack.HtmlDocument LelScanHtmlPage = web.Load(lel.LelScanUrlRoot);
                mangaUrl = LelScan.getAllPopularMangaLinks(LelScanHtmlPage);
            }
            else
                throw new Exception();
            return Manga.addMangaToMangaLst(mangaUrl);
        }
        private static List<Manga> addMangaToMangaLst(IEnumerable<String> mangaUrl)
        {
            List<Manga> mPopLst = new List<Manga>();
            foreach (var url in mangaUrl)
            {
                Manga manga = new Manga(new Uri(url));
                mPopLst.Add(manga);
            }
            return mPopLst;
        }
    }
}