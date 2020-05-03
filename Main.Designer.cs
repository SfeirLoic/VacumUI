using System;
using System.Windows.Forms;

namespace VacumUI
{
    partial class f_main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_main));
            this.m_menu1 = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lb_mangaFollowed = new System.Windows.Forms.ListBox();
            this.btn_scanPopularMangas = new System.Windows.Forms.Button();
            this.lb_mangaScanned = new System.Windows.Forms.ListBox();
            this.btn_addSelectedScanToFollowedManga = new System.Windows.Forms.Button();
            this.l_mangaFollowed = new System.Windows.Forms.Label();
            this.btn_scanAllMangas = new System.Windows.Forms.Button();
            this.fbd_selectRootPath = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_downloadAllMangaScanned = new System.Windows.Forms.Button();
            this.btn_downloadFollowedManga = new System.Windows.Forms.Button();
            this.btn_getAllChapterForSelectedManga = new System.Windows.Forms.Button();
            this.lb_chap = new System.Windows.Forms.ListBox();
            this.tt_showFullTitle = new System.Windows.Forms.ToolTip(this.components);
            this.btn_downloadSelectedChapters = new System.Windows.Forms.Button();
            this.btn_deleteMangaFollowed = new System.Windows.Forms.Button();
            this.m_menu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_menu1
            // 
            this.m_menu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
            this.m_menu1.Location = new System.Drawing.Point(0, 0);
            this.m_menu1.Name = "m_menu1";
            this.m_menu1.Size = new System.Drawing.Size(784, 24);
            this.m_menu1.TabIndex = 4;
            this.m_menu1.Text = "Menu";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configToolStripMenuItem.Text = "Configuration";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // lb_mangaFollowed
            // 
            this.lb_mangaFollowed.FormattingEnabled = true;
            this.lb_mangaFollowed.Location = new System.Drawing.Point(0, 52);
            this.lb_mangaFollowed.Name = "lb_mangaFollowed";
            this.lb_mangaFollowed.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb_mangaFollowed.Size = new System.Drawing.Size(157, 420);
            this.lb_mangaFollowed.Sorted = true;
            this.lb_mangaFollowed.TabIndex = 0;
            this.lb_mangaFollowed.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb_mangaFollowed_MouseMove);
            // 
            // btn_scanPopularMangas
            // 
            this.btn_scanPopularMangas.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_scanPopularMangas.Location = new System.Drawing.Point(365, 52);
            this.btn_scanPopularMangas.Name = "btn_scanPopularMangas";
            this.btn_scanPopularMangas.Size = new System.Drawing.Size(128, 23);
            this.btn_scanPopularMangas.TabIndex = 6;
            this.btn_scanPopularMangas.Text = "Manga populaires";
            this.btn_scanPopularMangas.UseVisualStyleBackColor = true;
            this.btn_scanPopularMangas.Click += new System.EventHandler(this.btn_scanPopularManga_Click);
            // 
            // lb_mangaScanned
            // 
            this.lb_mangaScanned.FormattingEnabled = true;
            this.lb_mangaScanned.Location = new System.Drawing.Point(202, 52);
            this.lb_mangaScanned.Name = "lb_mangaScanned";
            this.lb_mangaScanned.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb_mangaScanned.Size = new System.Drawing.Size(157, 498);
            this.lb_mangaScanned.Sorted = true;
            this.lb_mangaScanned.TabIndex = 0;
            this.lb_mangaScanned.DoubleClick += new System.EventHandler(this.lb_mangaScanned_DoubleClick);
            this.lb_mangaScanned.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb_mangaScanned_MouseMove);
            // 
            // btn_addSelectedScanToFollowedManga
            // 
            this.btn_addSelectedScanToFollowedManga.Location = new System.Drawing.Point(163, 52);
            this.btn_addSelectedScanToFollowedManga.Name = "btn_addSelectedScanToFollowedManga";
            this.btn_addSelectedScanToFollowedManga.Size = new System.Drawing.Size(33, 23);
            this.btn_addSelectedScanToFollowedManga.TabIndex = 8;
            this.btn_addSelectedScanToFollowedManga.Text = "<<<";
            this.btn_addSelectedScanToFollowedManga.UseVisualStyleBackColor = true;
            this.btn_addSelectedScanToFollowedManga.Click += new System.EventHandler(this.btn_addSelectedScanToFollowedManga_Click);
            // 
            // l_mangaFollowed
            // 
            this.l_mangaFollowed.AutoSize = true;
            this.l_mangaFollowed.Location = new System.Drawing.Point(-3, 36);
            this.l_mangaFollowed.Name = "l_mangaFollowed";
            this.l_mangaFollowed.Size = new System.Drawing.Size(89, 13);
            this.l_mangaFollowed.TabIndex = 9;
            this.l_mangaFollowed.Text = "Manga(s) suivi(s):";
            // 
            // btn_scanAllMangas
            // 
            this.btn_scanAllMangas.Location = new System.Drawing.Point(365, 84);
            this.btn_scanAllMangas.Name = "btn_scanAllMangas";
            this.btn_scanAllMangas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_scanAllMangas.Size = new System.Drawing.Size(128, 23);
            this.btn_scanAllMangas.TabIndex = 10;
            this.btn_scanAllMangas.Text = "Tous les mangas";
            this.btn_scanAllMangas.UseVisualStyleBackColor = true;
            this.btn_scanAllMangas.Click += new System.EventHandler(this.btn_scanAllMangas_Click);
            // 
            // fbd_selectRootPath
            // 
            this.fbd_selectRootPath.Description = "Choisir un répertoire de téléchargement des mangas";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_downloadAllMangaScanned
            // 
            this.btn_downloadAllMangaScanned.Location = new System.Drawing.Point(365, 113);
            this.btn_downloadAllMangaScanned.Name = "btn_downloadAllMangaScanned";
            this.btn_downloadAllMangaScanned.Size = new System.Drawing.Size(128, 23);
            this.btn_downloadAllMangaScanned.TabIndex = 11;
            this.btn_downloadAllMangaScanned.Text = "Télécharger la sélection";
            this.btn_downloadAllMangaScanned.UseVisualStyleBackColor = true;
            this.btn_downloadAllMangaScanned.Click += new System.EventHandler(this.btn_downloadAllMangaScanned_Click);
            // 
            // btn_downloadFollowedManga
            // 
            this.btn_downloadFollowedManga.Location = new System.Drawing.Point(0, 516);
            this.btn_downloadFollowedManga.Name = "btn_downloadFollowedManga";
            this.btn_downloadFollowedManga.Size = new System.Drawing.Size(157, 23);
            this.btn_downloadFollowedManga.TabIndex = 13;
            this.btn_downloadFollowedManga.Text = "Télécharger les mangas suivis";
            this.btn_downloadFollowedManga.UseVisualStyleBackColor = true;
            this.btn_downloadFollowedManga.Click += new System.EventHandler(this.btn_downloadMangaFollowed_Click);
            // 
            // btn_getAllChapterForSelectedManga
            // 
            this.btn_getAllChapterForSelectedManga.Location = new System.Drawing.Point(365, 142);
            this.btn_getAllChapterForSelectedManga.Name = "btn_getAllChapterForSelectedManga";
            this.btn_getAllChapterForSelectedManga.Size = new System.Drawing.Size(128, 23);
            this.btn_getAllChapterForSelectedManga.TabIndex = 15;
            this.btn_getAllChapterForSelectedManga.Text = "Chapitres >>>";
            this.btn_getAllChapterForSelectedManga.UseVisualStyleBackColor = true;
            this.btn_getAllChapterForSelectedManga.Click += new System.EventHandler(this.btn_getAllChapterForSelectedManga_Click);
            // 
            // lb_chap
            // 
            this.lb_chap.FormattingEnabled = true;
            this.lb_chap.Location = new System.Drawing.Point(499, 52);
            this.lb_chap.Name = "lb_chap";
            this.lb_chap.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb_chap.Size = new System.Drawing.Size(157, 498);
            this.lb_chap.TabIndex = 16;
            this.lb_chap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lb_chap_MouseMove);
            // 
            // btn_downloadSelectedChapters
            // 
            this.btn_downloadSelectedChapters.Location = new System.Drawing.Point(662, 52);
            this.btn_downloadSelectedChapters.Name = "btn_downloadSelectedChapters";
            this.btn_downloadSelectedChapters.Size = new System.Drawing.Size(126, 23);
            this.btn_downloadSelectedChapters.TabIndex = 17;
            this.btn_downloadSelectedChapters.Text = "DL Chaps Selectionnés";
            this.btn_downloadSelectedChapters.UseVisualStyleBackColor = true;
            this.btn_downloadSelectedChapters.Click += new System.EventHandler(this.btn_downloadSelectedChapters_Click);
            // 
            // btn_deleteMangaFollowed
            // 
            this.btn_deleteMangaFollowed.Location = new System.Drawing.Point(0, 487);
            this.btn_deleteMangaFollowed.Name = "btn_deleteMangaFollowed";
            this.btn_deleteMangaFollowed.Size = new System.Drawing.Size(157, 23);
            this.btn_deleteMangaFollowed.TabIndex = 12;
            this.btn_deleteMangaFollowed.Text = "Retirer des mangas suivis";
            this.btn_deleteMangaFollowed.UseVisualStyleBackColor = true;
            this.btn_deleteMangaFollowed.Click += new System.EventHandler(this.btn_deleteMangaFollowed_Click);
            // 
            // f_main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btn_downloadSelectedChapters);
            this.Controls.Add(this.lb_chap);
            this.Controls.Add(this.btn_getAllChapterForSelectedManga);
            this.Controls.Add(this.btn_downloadFollowedManga);
            this.Controls.Add(this.btn_deleteMangaFollowed);
            this.Controls.Add(this.btn_downloadAllMangaScanned);
            this.Controls.Add(this.btn_scanAllMangas);
            this.Controls.Add(this.l_mangaFollowed);
            this.Controls.Add(this.btn_addSelectedScanToFollowedManga);
            this.Controls.Add(this.lb_mangaScanned);
            this.Controls.Add(this.btn_scanPopularMangas);
            this.Controls.Add(this.lb_mangaFollowed);
            this.Controls.Add(this.m_menu1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_menu1;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "f_main";
            this.Text = "VacumUI";
            this.Load += new System.EventHandler(this.f_main_Load);
            this.Shown += new System.EventHandler(this.f_main_Shown);
            this.m_menu1.ResumeLayout(false);
            this.m_menu1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip m_menu1;
        private ToolStripMenuItem configToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private Label l_mangaFollowed;
        private ListBox lb_mangaFollowed;
        private ListBox lb_mangaScanned;
        private Button btn_scanPopularMangas;
        private Button btn_addSelectedScanToFollowedManga;
        private Button btn_scanAllMangas;
        private FolderBrowserDialog fbd_selectRootPath;
        private OpenFileDialog openFileDialog1;
        private Button btn_downloadAllMangaScanned;
        private Button btn_deleteMangaFollowed;
        private Button btn_downloadFollowedManga;
        private Button btn_getAllChapterForSelectedManga;
        private ListBox lb_chap;
        private ToolTip tt_showFullTitle;
        private Button btn_downloadSelectedChapters;
    }
}

