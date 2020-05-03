namespace VacumUI
{
    partial class f_opt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.l_mangaDir = new System.Windows.Forms.Label();
            this.btn_changeMangaDir = new System.Windows.Forms.Button();
            this.fbd_selectRP = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_closeOpt = new System.Windows.Forms.Button();
            this.cb_compress = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fbd_selectRootPath = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // l_mangaDir
            // 
            this.l_mangaDir.AutoSize = true;
            this.l_mangaDir.Location = new System.Drawing.Point(50, 29);
            this.l_mangaDir.Name = "l_mangaDir";
            this.l_mangaDir.Size = new System.Drawing.Size(205, 13);
            this.l_mangaDir.TabIndex = 0;
            this.l_mangaDir.Text = "Changer le répertoire de téléchargement ()";
            // 
            // btn_changeMangaDir
            // 
            this.btn_changeMangaDir.Location = new System.Drawing.Point(12, 12);
            this.btn_changeMangaDir.Name = "btn_changeMangaDir";
            this.btn_changeMangaDir.Size = new System.Drawing.Size(26, 30);
            this.btn_changeMangaDir.TabIndex = 1;
            this.btn_changeMangaDir.Text = "...";
            this.btn_changeMangaDir.UseVisualStyleBackColor = true;
            this.btn_changeMangaDir.Click += new System.EventHandler(this.btn_changeMangaDir_Click);
            // 
            // btn_closeOpt
            // 
            this.btn_closeOpt.Location = new System.Drawing.Point(391, 56);
            this.btn_closeOpt.Name = "btn_closeOpt";
            this.btn_closeOpt.Size = new System.Drawing.Size(67, 23);
            this.btn_closeOpt.TabIndex = 2;
            this.btn_closeOpt.Text = "Fermer";
            this.btn_closeOpt.UseVisualStyleBackColor = true;
            this.btn_closeOpt.Click += new System.EventHandler(this.btn_closeOpt_Click);
            // 
            // cb_compress
            // 
            this.cb_compress.AutoSize = true;
            this.cb_compress.Location = new System.Drawing.Point(12, 61);
            this.cb_compress.Name = "cb_compress";
            this.cb_compress.Size = new System.Drawing.Size(15, 14);
            this.cb_compress.TabIndex = 3;
            this.cb_compress.UseVisualStyleBackColor = true;
            this.cb_compress.CheckedChanged += new System.EventHandler(this.cb_compress_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Compresser après téléchargement";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // f_opt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 97);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_compress);
            this.Controls.Add(this.btn_closeOpt);
            this.Controls.Add(this.btn_changeMangaDir);
            this.Controls.Add(this.l_mangaDir);
            this.Name = "f_opt";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.f_opt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label l_mangaDir;
        private System.Windows.Forms.Button btn_changeMangaDir;
        private System.Windows.Forms.FolderBrowserDialog fbd_selectRP;
        private System.Windows.Forms.Button btn_closeOpt;
        private System.Windows.Forms.CheckBox cb_compress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbd_selectRootPath;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}