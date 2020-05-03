using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace VacumUI
{
    public partial class f_main : Form
    {
        int hoveredChapIndex = -1;
        int hoveredMSIndex = -1;
        int hoverMangaFollowedIdx = -1;

        /*bool termine = true;
        //On crée notre delagate.
        public delegate void MontrerProgres(int valeur);*/

        public f_main()
        {
            InitializeComponent();
        }


        /***************    BUTTONS    ***************/
        private void btn_scanPopularManga_Click(object sender, EventArgs e)
        {
            LelScan lel = new LelScan();
            //récupère la liste des mangas populaires
            List<Manga> mangaLst = Manga.getAllPopularMangaList(lel);
            addMangaToScannedList(mangaLst);
        }
        private void btn_scanAllMangas_Click(object sender, EventArgs e)
        {
            LelScan lel = new LelScan();
            List<Manga> mangaLst = Manga.getAllManga(lel);
            addMangaToScannedList(mangaLst);
        }
        private void btn_addSelectedScanToFollowedManga_Click(object sender, EventArgs e)
        {
            addItemsToFollowedList();
        }
        private void btn_downloadAllMangaScanned_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch dlTime = new Stopwatch();
                dlTime.Start();
                if (lb_mangaScanned.SelectedItems.Count > 0)
                {
                    allButtonDisable();
                    List<Manga> mangaLst = new List<Manga>();
                    foreach (Manga m in lb_mangaScanned.SelectedItems)
                    {
                        mangaLst.Add(m);
                    }
                    Manga.infDlZip(mangaLst);
                    //une fois terminé, on vide la liste des chapitres à dl;
                    foreach (Manga m in lb_mangaScanned.SelectedItems)
                    {
                        m.MangaChapToDlLst.Clear();
                        m.MangaChapCompleteLst.Clear();
                    }
                    allButtonEnable();
                }
                dlTime.Stop();
                String time = dlTime.Elapsed.Seconds.ToString();
                String msg = "Téléchargement terminé en " + time + " secondes...";
                MessageBox.Show(msg, "Cool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_downloadMangaFollowed_Click(object sender, EventArgs e)
        {
            allButtonDisable();
            List<Manga> mLst = (List<Manga>)lb_mangaFollowed.DataSource;
            Manga.infDlZip(mLst);
            //on met à jour la liste des mangas suivis avec le chapitre le plus haut DL
            Outils.updateFicMangaFollowed(mLst);
            MessageBox.Show("Les téléchargements des mangas suivis sont terminés","Cool",MessageBoxButtons.OK, MessageBoxIcon.Information);
            allButtonEnable();
        }
        private void allButtonEnable()
        {
            btn_addSelectedScanToFollowedManga.Enabled  = true;
            btn_deleteMangaFollowed.Enabled             = true;
            btn_downloadAllMangaScanned.Enabled         = true;
            btn_downloadFollowedManga.Enabled           = true;
            btn_downloadSelectedChapters.Enabled        = true;
            btn_getAllChapterForSelectedManga.Enabled   = true;
            btn_scanAllMangas.Enabled                   = true;
            btn_scanPopularMangas.Enabled               = true;
            Cursor.Current = Cursors.Default;
        }
        private void allButtonDisable()
        {
            btn_addSelectedScanToFollowedManga.Enabled  = false;
            btn_deleteMangaFollowed.Enabled             = false;
            btn_downloadAllMangaScanned.Enabled         = false;
            btn_downloadFollowedManga.Enabled           = false;
            btn_downloadSelectedChapters.Enabled        = false;
            btn_getAllChapterForSelectedManga.Enabled   = false;
            btn_scanAllMangas.Enabled                   = false;
            btn_scanPopularMangas.Enabled               = false;
            Cursor.Current = Cursors.WaitCursor;
        }
        private void btn_deleteMangaFollowed_Click(object sender, EventArgs e)
        {
            List<Manga> mangaFollowedList = new List<Manga>();
            mangaFollowedList = (List<Manga>)lb_mangaFollowed.DataSource;

            //Apparement nécéssaire pour le refresh du Datasource

            foreach (Manga item in lb_mangaFollowed.SelectedItems)
                mangaFollowedList.Remove(item);

            lb_mangaFollowed.DataSource = null;
            lb_mangaFollowed.DataSource = mangaFollowedList;
            lb_mangaFollowed.DisplayMember = "MangaTitleShow";
            updateMangaFollowedListFic(mangaFollowedList);
        }
        private void btn_downloadSelectedChapters_Click(object sender, EventArgs e)
        {
            allButtonDisable();
            LelScan lel = new LelScan();
            List<Chapitre> cDlLst = new List<Chapitre>();
            foreach (Chapitre c in lb_chap.SelectedItems)
                cDlLst.Add(c);
            Chapitre.getInfoChapitres(cDlLst, lel);
            Outils.dlChapWebClient(cDlLst);
            Outils.zipAndDel(cDlLst);
            MessageBox.Show("Les téléchargements des chapitres selectionnés sont terminés", "Cool", MessageBoxButtons.OK, MessageBoxIcon.Information);
            allButtonEnable();
        }
        private void btn_getAllChapterForSelectedManga_Click(object sender, EventArgs e)
        {
            lb_chap.DataSource = null;
            if (lb_mangaScanned.SelectedItems.Count > 0)
            {
                LelScan lel = new LelScan();
                Manga m = (Manga)lb_mangaScanned.SelectedItem;
                Chapitre.getChapsFromManga(m, lel);
                lb_chap.DataSource = m.MangaChapCompleteLst;
                lb_chap.DisplayMember = "ChapTitleShow";
            }
        }

        /***************    END BUTTONS    ***************/
        /***************    LISTBOX    ***************/
        private void lb_mangaScanned_DoubleClick(object sender, EventArgs e)
        {
            addItemsToFollowedList();
        }
        // Class variable to keep track of which row is currently selected:

        private void lb_mangaScanned_MouseMove(object sender, MouseEventArgs e)
        {
            // See which row is currently under the mouse:
            int newHoveredIndex = lb_mangaScanned.IndexFromPoint(e.Location);

            // If the row has changed since last moving the mouse:
            if (hoveredMSIndex != newHoveredIndex)
            {
                // Change the variable for the next time we move the mouse:
                hoveredMSIndex = newHoveredIndex;

                // If over a row showing data (rather than blank space):
                if (hoveredMSIndex > -1)
                {
                    String title = ((Manga)lb_mangaScanned.Items[hoveredMSIndex]).MangaTitleClean;
                    if (title.Length > 20)
                        showTT(title, lb_mangaScanned);
                }
            }
        }

        // Class variable to keep track of which row is currently selected:

        private void lb_mangaFollowed_MouseMove(object sender, MouseEventArgs e)
        {
            // See which row is currently under the mouse:
            int newHoveredIndex = lb_mangaFollowed.IndexFromPoint(e.Location);

            // If the row has changed since last moving the mouse:
            if (hoverMangaFollowedIdx != newHoveredIndex)
            {
                // Change the variable for the next time we move the mouse:
                hoverMangaFollowedIdx = newHoveredIndex;

                if (hoverMangaFollowedIdx > -1)
                {
                    String title = ((Manga)lb_mangaFollowed.Items[hoverMangaFollowedIdx]).MangaTitleClean;
                    if (title.Length > 20)
                        showTT(title, lb_mangaFollowed);
                }
            }

        }

        // Class variable to keep track of which row is currently selected:


        private void lb_chap_MouseMove(object sender, MouseEventArgs e)
        {
            // See which row is currently under the mouse:
            int newHoveredIndex = lb_chap.IndexFromPoint(e.Location);

            // If the row has changed since last moving the mouse:
            if (hoveredChapIndex != newHoveredIndex)
            {
                // Change the variable for the next time we move the mouse:
                hoveredChapIndex = newHoveredIndex;

                // If over a row showing data (rather than blank space):
                if (hoveredChapIndex > -1)
                {
                    String title = ((Chapitre)lb_chap.Items[hoveredChapIndex]).ChapTitleClean;
                    if (title.Length > 20)
                        showTT(title, lb_chap);
                }
            }
        }
        private void showTT(String title, ListBox lb)
        {
            //Set tooltip text for the row now under the mouse:
            tt_showFullTitle.Active = false;
            tt_showFullTitle.SetToolTip(lb, title);
            tt_showFullTitle.Active = true;
        }
        /***************    END LISTBOX    ***************/


        /***************    FUNCTIONS    ***************/

        private void addMangaToScannedList(List<Manga> mangaLst)
        {
            lb_mangaScanned.DataSource = mangaLst;
            lb_mangaScanned.DisplayMember = "MangaTitleShow";
        }
        private void addItemsToFollowedList()
        {
            //on charge la liste des mangas suivis avec ce que contient la LB
            List<Manga> mangaFollowedList = new List<Manga>();
            foreach (Manga m in lb_mangaFollowed.Items) mangaFollowedList.Add((m));
            try
            {
                //on parcours les manga selectionnés à ajouter
                foreach (Manga mScan in lb_mangaScanned.SelectedItems)
                {
                    bool finded = false;
                    //et on vérifie qu'il ne se trouve pas déjà dans les mangas suivies
                    foreach (Manga m in mangaFollowedList)
                    {
                        if (m.MangaUrl == mScan.MangaUrl || m.MangaTitleClean == mScan.MangaTitleClean)
                        {
                            finded = true;
                            break;
                        }
                    }
                    // s'il n'y est pas on l'ajoute
                    if (!finded)
                        mangaFollowedList.Add(mScan);
                }
                lb_mangaFollowed.DataSource = mangaFollowedList;
                lb_mangaFollowed.DisplayMember = "MangaTitleShow";
                updateMangaFollowedListFic(mangaFollowedList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        private void updateMangaFollowedListFic(List<Manga> mangaFollowedList)
        {
            if (!File.Exists(Program._followedManga)) throw new Exception();
            string[] lines = new string[mangaFollowedList.Count];
            int i = 0;
            foreach (Manga m in mangaFollowedList)
            {
                String site = Outils.reencodeSite(m.MangaUrl);
                lines[i] = site + m.MangaTitleFromUrl + Program._separator + m.MangaPath + Program._separator + m.HigherChap;
                i++;
            }
            try
            {
                File.WriteAllLines(Program._followedManga, lines);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        /***************    END FUNCTIONS    ***************/

        /***************    MENU    ***************/
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            f_opt form = new f_opt();
            form.Show();
        }

        private void f_main_Load(object sender, EventArgs e)
        {
            List<Manga> mFollowedLst = Program.getFavAndPopulateLB();
            lb_mangaFollowed.DataSource = mFollowedLst;
            lb_mangaFollowed.DisplayMember = "MangaTitleShow";

        }

        private void f_main_Shown(object sender, EventArgs e)
        {
            // si on a pas trouvé le RP dans le fichier de config, on demande au user de le définir
            if (!Program._rpChanged)
            {
                Program._rootPath = Program.userDefineRootPath(sender, fbd_selectRootPath);
                Outils.updateFicLineWithMotif(Program._cfg, Program._rootPath, Program._motifRootPath);
                Outils.updateFicLineWithMotif(Program._cfg, Program._rpChanged, Program._motifRootPath);
            }
        }
        /***************    END MENU    ***************/
    }
}

