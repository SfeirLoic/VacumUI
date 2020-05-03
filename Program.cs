using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VacumUI
{
    static class Program
    {
        public static String _defaultRP = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Manga");
        public static String _rootPath = "";
        public static String _motifRootPath = "RP";
        public static String _motifCompress = "CP";
        public static String _separator = "#281188#";
        public static String _cfg = "cfg";
        public static String _followedManga = "fm";
        public static bool _compress = false;
        public static bool _rpChanged = false;
        public static char lelScan = 'a';

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            initFiles();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new f_main());
        }

        private static void initFiles()
        {
            try
            {
                //Création des fichiers vide s'ils n'existent pas 
                String[] files = createNecessaryFiles();
                String rp = SearchValueForRP();
                if (rp != "")
                {
                    Program._rootPath = rp;
                    Program._rpChanged = true;
                } else
                {
                    Program._rootPath = Program._defaultRP;
                    Program._rpChanged = false;
                }
                
                Program._compress = SearchValueForCP();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static List<Manga> getFavAndPopulateLB()
        {
            String[] lines = Outils.readTxtFile(Program._followedManga);
            List<Manga> mFollowedLst = new List<Manga>();
            foreach (String l in lines)
            {
                String site = l.Substring(0, 1);
                String url = Outils.searchSite(site).ToString();
                String[] s = l.Substring(1).Split(new[] { Program._separator }, StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(s[2], out int nbrHigherChap);
                url += s[0];
                Manga m = new Manga(new Uri(url));
                m.MangaPath = s[1];
                m.HigherChap = nbrHigherChap;
                mFollowedLst.Add(m);
            }
            return mFollowedLst;
        }

        public static List<Manga> updFavPath()
        {
            String[] lines = Outils.readTxtFile(Program._followedManga);
            List<Manga> mFollowedLst = new List<Manga>();
            for (int i = 0; i < lines.Length; i++)
            {
                String site = lines[i].Substring(0, 1);
                String[] s = lines[i].Substring(1).Split(new[] { Program._separator }, StringSplitOptions.RemoveEmptyEntries);
                Manga m = new Manga(new Uri(s[0]));
                String number = s[2];
                int.TryParse(number, out int nbrHigherChap);
                s[1] = m.MangaPath;
                m.HigherChap = nbrHigherChap;
                mFollowedLst.Add(m);
                Outils.reencodeSite(m.MangaUrl);
                lines[i] = m.MangaUrl.ToString() + Program._separator + m.MangaPath + Program._separator + number;
            }
            File.WriteAllLines(Program._followedManga, lines);
            return mFollowedLst;
        }


        private static String SearchValueForRP()
        {
            //Lecture du fichier de config dans le but d'extraire les lignes qui nous intérèssent.
            List<String> lines = File.ReadLines(Program._cfg).ToList();
            //on cherche le chemin root de l'appli définie par l'user
            foreach (String l in lines)
            {
                if (l.Contains(Program._motifRootPath))
                    return l.Substring(Program._motifRootPath.Length + Program._separator.Length);
            }
            return "";
        }

        private static bool SearchValueForCP()
        {
            //Lecture du fichier de config dans le but d'extraire les lignes qui nous intérèssent.
            List<String> lines = File.ReadLines(Program._cfg).ToList();
            //on cherche le chemin root de l'appli définie par l'user
            foreach (String l in lines)
            {
                if (l.Contains(Program._motifCompress))
                    return Convert.ToBoolean(l.Substring(Program._motifCompress.Length + Program._separator.Length));
            }
            return false;
        }

        private static string[] createNecessaryFiles()
        {
            String[] files = { Program._cfg, Program._followedManga };
            for (int i = 0; i < files.Length; i++)
            {
                if (!File.Exists(files[i]))
                {
                    FileStream fs = File.Create(files[i]);
                    fs.Close();
                }
            }
            return files;
        }

        public static String userDefineRootPath(object sender, FolderBrowserDialog fbd_selectRootPath)
        {
            // Show the FolderBrowserDialog.
            fbd_selectRootPath.SelectedPath = Program._rootPath;
            DialogResult result = fbd_selectRootPath.ShowDialog();
            if (result == DialogResult.OK)
            {

                Program._rpChanged = true;
                return fbd_selectRootPath.SelectedPath;
            }
            else
            {
                var a = sender.GetType().Name;
                if (sender.GetType().Name != "Button")
                {
                    Program._rpChanged = false;
                    String msg = "Par défaut le répertoire '" + Program._defaultRP + "' à été séléctionné, il sera modifiable dans les options";
                    MessageBox.Show(msg, "Répertoire par défaut", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return Program._defaultRP;
                } else
                {
                    if (Program._rpChanged)
                        return Program._rootPath;
                    else
                        return Program._defaultRP;
                }
            }
        }
    }
}
