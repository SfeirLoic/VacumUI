using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace VacumUI
{
    public static class Outils
    {
        /// <summary>
        /// Permet de nettoyer une chaine de caractère
        /// </summary>
        public static string cleanString(String thing)
        {
            //Attention, ne pas supprimer "." car extensions, remplacement de , pour les chapitres intermédiaires etc ....
            String thingClean = thing.Replace("-", " ")
                                     .Replace("_", " ")
                                     .Replace("*", " ")
                                     .Replace("&#039;", "'")
                                     ;
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            return myTI.ToTitleCase(thingClean);
        }

        internal static void moveAllDir(string origin, string end)
        {
            try
            {
                foreach (String dir in Directory.GetDirectories(origin))
                {
                    String endDir = Path.Combine(end, Path.GetFileName(dir));
                    Directory.Move(dir, endDir);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal static string cutStringShow(string title)
        {
            if (title.Length > 20)
            {
                var a = "b";
                String[] b = title.Split(' ');
                var c = a;
                title = b.First() + " [...] " + b.Last();
                /*if (title.Length > 20)
                {

                }*/
                return title;
            }
            return title;
        }

        /// <summary>
        /// Télécharge un fichier depuis l'url à l'adresse local en paramètre
        /// </summary>
        public static void downloadFile(String url, String path)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(url, path);
            }
            catch (Exception e) { 
                Console.WriteLine("Error {0]", e.Message); 
            }
        }



        internal static void zipAndDel(Manga m1)
        {
            if (Program._compress) { 
                foreach (Chapitre chap in m1.MangaChapToDlLst)
                {
                    string startPath = chap.ChapPath;
                    string zipPath = chap.ChapPath + @".zip";
                    ZipFile.CreateFromDirectory(startPath, zipPath);
                    Directory.Delete(chap.ChapPath, true);
                }
            }
        }
        internal static void zipAndDel(Chapitre c)
        {
            if (Program._compress)
            {
                string startPath = c.ChapPath;
                string zipPath = c.ChapPath + @".zip";
                ZipFile.CreateFromDirectory(startPath, zipPath);
                Directory.Delete(c.ChapPath, true);
            }
        }

        internal static String reencodeSite(Uri mangaUrl)
        {
            String site = mangaUrl.Host.ToString();
            String lelScan = LelScan.lelScanUrlMangaList.ToString();
            switch (site)
            {
                case "www.lelscan-vf.com":
                    return "a";
                default:
                    throw new Exception();
            }
        }

        internal static String searchSite(string site)
        {
            switch (site)
            {
                case "a":
                    LelScan lel = new LelScan();
                    return lel.LelScanUrlMangaRoot.ToString();
                default:
                    throw new Exception();
            }
        }

        internal static void zipAndDel(List<Chapitre> cLst)
        {
            if (Program._compress)
            {
                foreach (Chapitre chap in cLst)
                {
                    string startPath = chap.ChapPath;
                    string zipPath = chap.ChapPath + @".zip";
                    if (!File.Exists(zipPath))
                    {
                        ZipFile.CreateFromDirectory(startPath, zipPath);
                        Directory.Delete(chap.ChapPath, true);
                    }
                }
            }
        }

        internal static void zipAndDel(List<Manga> mangaLst)
        {
            if (Program._compress) { 
                foreach (Manga m in mangaLst)
                {
                    foreach (Chapitre chap in m.MangaChapToDlLst)
                    {
                        string startPath = chap.ChapPath;
                        string zipPath = chap.ChapPath + @".zip";
                        if (!File.Exists(zipPath))
                        {
                            ZipFile.CreateFromDirectory(startPath, zipPath);
                            Directory.Delete(chap.ChapPath, true);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// fonction d'update de fichier à partir d'un motif
        /// </summary>
        /// <param name="file"></param>
        /// <param name="upd"></param>
        /// <param name="motif"></param>
        public static void updateFicLineWithMotif(String file, String upd, String motif)
        {
            String[] lines = readTxtFile(file);
            bool rootPathFinded = false;
            for (int i = 0; i < lines.Length; i++)
            {
                //si la ligne existe déjà on la modifie 
                if (lines[i].Contains(Program._motifRootPath))
                {
                    rootPathFinded = true;
                    lines[i] = Program._motifRootPath + Program._separator + upd;
                }
            }
            if (!rootPathFinded)
            {
                List<String> linesLst = lines.ToList();
                linesLst.Add(Program._motifRootPath + Program._separator + upd);
                File.WriteAllLines(file, linesLst);
            }
            else
            {
                File.WriteAllLines(file, lines);
            }
        }
        /// <summary>
        /// fonction d'update de fichier à partir d'un motif
        /// </summary>
        /// <param name="file"></param>
        /// <param name="val"></param>
        /// <param name="motif"></param>
        public static void updateFicLineWithMotif(String file, bool val, String motif)
        {
            String[] lines = readTxtFile(file);
            bool compressfinded = false;
            for (int i = 0; i < lines.Length; i++)
            {
                //si la ligne existe déjà on la modifie 
                if (lines[i].Contains(Program._motifCompress))
                {
                    compressfinded = true;
                    lines[i] = Program._motifCompress + Program._separator + val;
                }
            }
            if (!compressfinded)
            {
                List<String> linesLst = lines.ToList();
                linesLst.Add(Program._motifCompress + Program._separator + val);
                File.WriteAllLines(file, linesLst);
            }
            else
            {
                File.WriteAllLines(file, lines);
            }
        }
        public static void updateFicMangaFollowed(List<Manga> mLst)
        {

            String[] lines = new String[mLst.Count()];

            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = mLst[i].MangaUrl + Program._separator + mLst[i].MangaPath + Program._separator + mLst[i].HigherChap;
            }
            File.WriteAllLines(Program._followedManga, lines);
        }

        public static String[] readTxtFile(String file)
        {
            if (!File.Exists(file)) throw new Exception();

            String[] lines = File.ReadAllLines(file);

            return lines;
        }

        /// <summary>
        /// Vérifie si le chapitre à déjà été téléchargé
        /// </summary>
        /// <param name="chap"></param>
        public static void checkFicChap(Chapitre chap)
        {
            //le chapitre à déjà été dl si:
            //- un dossier avec son nom existe
            if (Directory.Exists(chap.ChapPath))
            {
                chap.chapAlreadyDled = true;
                return;
            }
            if (Directory.Exists(chap.MangaPath)){
                var files = Directory.GetFiles(chap.MangaPath, "*.*");
                foreach (var f in files)
                {
                    //on prends le numero sans l'extension
                    int.TryParse(Path.GetFileNameWithoutExtension(f).Split(' ').Last(), out int numChap);
                    if (numChap == chap.ChapNumber)
                    {
                        chap.chapAlreadyDled = true;
                        return;
                    }
                }
            }
            else
            {
                chap.chapAlreadyDled = false;
                return;
            }
        }

        /// <summary>
        /// Télécharge un chapitre à partir d'un objet Chapitre
        /// </summary>
        /// <param name="chap"></param>
        /// <returns></returns>
        public static void dlChapWebClient(Chapitre c)
        {
            DirectoryInfo cDir = Directory.CreateDirectory(c.ChapPath);
            int lgtNeed = c.ChapPicLst.Count().ToString().Length;
            foreach (var p in c.ChapPicLst)
            {
                String path = p.PicPath;
                String picNbr = p.PicNumber.ToString();
                int diff = lgtNeed - picNbr.Length;
                if (diff > 0)
                {
                    for (int i = 0; i < diff; i++) picNbr = "0" + picNbr;
                    path = Path.Combine(p.ChapPath, picNbr + p.PicExt);
                }
                Outils.downloadFile(p.PicUrl.ToString(), path);
            }
        }

        /// <summary>
        /// Télécharge des chapitres à partir d'une list de Chapitres
        /// </summary>
        /// <param name="chap"></param>
        /// <returns></returns>
        public static void dlChapWebClient(List<Chapitre> cLst)
        {
            foreach (Chapitre c in cLst) { 
                DirectoryInfo cDir = Directory.CreateDirectory(c.ChapPath);
                int lgtNeed = c.ChapPicLst.Count().ToString().Length;
                foreach (var p in c.ChapPicLst)
                {
                    String path = p.PicPath;
                    String picNbr = p.PicNumber.ToString();
                    int diff = lgtNeed - picNbr.Length;
                    if (diff > 0)
                    {
                        for (int i = 0; i < diff; i++) picNbr = "0" + picNbr;
                        path = Path.Combine(p.ChapPath, picNbr + p.PicExt);
                    }
                    Outils.downloadFile(p.PicUrl.ToString(), path);
                }
            }
        }

        public static void dlMangaWebClient(List<Manga> mangaLst)
        {
            foreach (Manga m in mangaLst)
            {
                DirectoryInfo mangaDir = Directory.CreateDirectory(m.MangaPath);
                //foreach (var c in m.MangaChapToDlLst) Outils.dlChapWebClient(c);
                Outils.dlChapWebClient(m.MangaChapToDlLst);
            }
        }

        /// <summary>
        /// Complète l'objet Manga avec la liste des tous les chapitres à télécharger
        /// </summary>
        /// <param name="manga"></param>
        public static void getChapsToDlList(Manga manga, object o)
        {
            //si le dossier existe, on à déjà téléchargé des choses pour ce manga
            //on va analyser pour exclure ce qu'on à déjà téléchargé
            //sinon on prends toute la liste
            if (Directory.Exists(manga.MangaPath))
            {
                //pour chaque chapitre on vérifie s'il existe ou pas
                foreach (var chap in manga.MangaChapCompleteLst)
                {
                    Outils.checkFicChap(chap);
                    //si le chapitre à déjà été rappatrié, on s'arrête la, on prends le partie pris de considérer que si le dossier / fichier existe, il est complet
                    if (!chap.chapAlreadyDled)
                    {
                        //Sinon, on récupère la liste des pages du chapitre et on ajoute le chapitre dans la liste à dl
                        Chapitre.getChapPages(chap, o);
                        manga.MangaChapToDlLst.Add(chap);
                        manga.MangaNbrPagesToDl += chap.ChapNbrPage;
                    }
                }
            }
            else
            {
                //le répertoire n'existe pas il faut tout télécharger
                foreach (var chap in manga.MangaChapCompleteLst)
                {
                    Chapitre.getChapPages(chap, o);
                    manga.MangaChapToDlLst.Add(chap);
                    manga.MangaNbrPagesToDl += chap.ChapNbrPage;
                }
            }
        }
    }
}
