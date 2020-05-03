using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace VacumUI
{
    /// <summary>
    /// Objet contenant les fonctions pour télécharger sur LelScan
    /// </summary>
    internal class LelScan
    {

        /**********   GET / SET  ***********/
        //public static HtmlAgilityPack.HtmlDocument LelScanHtmlPage { get; set; }
        //public HtmlWeb LelScanHtmlWeb { get; set; }
        public Uri LelScanUrlRoot { get; set; }
        public Uri LelScanUrl { get; set; }
        public Uri LelScanUrlMangaRoot { get; set; }
        public static Uri lelScanUrlMangaList = new Uri("https://www.lelscan-vf.com/manga-list");

        /**********   fin GET / SET  ***********/


        /**********   Constructeurs ***********/
        /// <summary>
        /// Constructeur avec Url par deffaut
        /// </summary>
        public LelScan()
        {
            LelScanUrlRoot = new Uri("https://www.lelscan-vf.com");
            LelScanUrl = new Uri(LelScanUrlRoot.AbsoluteUri);
            LelScanUrlMangaRoot = new Uri(LelScanUrlRoot + "manga/");
            //LelScanHtmlWeb = new HtmlWeb();
            //LelScanHtmlPage = LelScanHtmlWeb.Load(LelScanUrlRoot);
        }
        /// <summary>
        /// Constructeur avec Url en param
        /// </summary>
        public LelScan(Uri url)
        {
            LelScanUrlRoot = new Uri("https://www.lelscan-vf.com");
            LelScanUrl = url;
            LelScanUrlMangaRoot = new Uri(LelScanUrl + "manga");
            //LelScanHtmlWeb = new HtmlWeb();
            //LelScanHtmlPage = LelScanHtmlWeb.Load(LelScanUrl);
        }
        /**********   Fin Constructeurs ***********/


        /**********   Méthodes ***********/
        public static IEnumerable<String> getChapsLinksFromManga(Manga manga, HtmlAgilityPack.HtmlDocument mangaPage)
        {
            //On récupère toutes les divs qui ont pour class manga-name
            IEnumerable<String> links =
                from h5 in mangaPage.DocumentNode.Descendants("h5")
                from a in h5.Descendants("a")
                let href = a.GetAttributeValue("href", null)
                where href != null
                select href;

            return links;
        }

        /// <summary>
        /// Récupère tous les mangas du site LelScan
        /// </summary>
        /// <param name="mLst"></param>
        /// <returns></returns>
        public static List<Manga> getAllManga(List<Manga> mLst)
        {
            HtmlWeb web = new HtmlWeb();
            //par défault on commence à la page 1
            String url = lelScanUrlMangaList.ToString();
            bool next = true;
            //tant qu'il y a une page suivante
            while (next)
            {
                //on charge la page suivante
                HtmlAgilityPack.HtmlDocument docMangaLst = web.Load(url);
                //on récupère tous les mangas sur cette page
                var mangaNodes =
                    from h5 in docMangaLst.DocumentNode.Descendants("h5")
                    let href = h5.ChildNodes.First().GetAttributeValue("href", null)
                    where href != null
                    select href;
                
                foreach (var mangaUrl in mangaNodes)
                {
                    var title = mangaUrl.Split('/').Last();
                    Uri urlM = new Uri(mangaUrl);

                    Manga m = new Manga(title, urlM);
                    mLst.Add(m);
                }
                //on détermine la prochaine page
                var nxtPage =
                    from ul in docMangaLst.DocumentNode.Descendants("ul")
                    let classe = ul.GetAttributeValue("class", null)
                    where classe == "pagination"
                    from a in ul.Descendants("a")
                    let rel = a.GetAttributeValue("rel", null)
                    where rel == "next"
                    let href = a.GetAttributeValue("href", null)
                    where href != null
                    select href;
                String nextUrl = nxtPage.FirstOrDefault();
                //s'il n'y a pas de prochaine page on arrête
                if (nextUrl != null && nextUrl.Contains("page"))
                    url = nextUrl;
                else
                    next = false;
            }
            return mLst;
        }
        /// <summary>
        /// Complète la liste de Picture d'un chapitre contenant l'ensemble des images de celui-ci.
        /// </summary>
        public static IEnumerable<string> getChapPages(HtmlAgilityPack.HtmlDocument chapterPage)
        {
            IEnumerable<string> picsUrl = 
                from truc in chapterPage.DocumentNode.Descendants("img")
                let data = truc.GetAttributeValue("data-src", null)
                where data != null
                select data;
            return picsUrl;
        }

        //TODO à optimiser
        /// Récupère la liste de tous les manga populaires de lelscan
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<String> getAllPopularMangaLinks(HtmlAgilityPack.HtmlDocument LelScanHtmlPage)
        {
            //On récupère toutes les divs qui ont pour class manga-name
            IEnumerable<String> links =
                from div in LelScanHtmlPage.DocumentNode.Descendants("div")
                let classe = div.GetAttributeValue("class", null)
                where classe == "manga-name"
                from a in div.Descendants("a")
                let href = a.GetAttributeValue("href", null)
                where href != null
                select href;

            return links;
        }
        /**********   Fin Méthodes ***********/
    }
}