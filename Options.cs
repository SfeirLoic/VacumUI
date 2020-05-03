using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VacumUI
{
    public partial class f_opt : Form
    {
        public f_opt()
        {
            InitializeComponent();
            
        }
        private void btn_changeMangaDir_Click(object sender, EventArgs e)
        {
            string origin = Program._rootPath;
            Program._rootPath = Program.userDefineRootPath(sender, fbd_selectRootPath);
            if (Program._rpChanged) {
                l_mangaDir.Text = "Changer le répertoire de téléchargement (" + Program._rootPath + ")";
                Outils.updateFicLineWithMotif(Program._cfg, Program._rootPath, Program._motifRootPath);
                Outils.updateFicLineWithMotif(Program._cfg, Program._rpChanged, Program._motifRootPath);
                Program.updFavPath();
                string end = Program._rootPath;
                if (!String.IsNullOrEmpty(origin) && !String.IsNullOrEmpty(end) && !end.Equals(origin))
                    Outils.moveAllDir(origin , end);
            }
        }
        private void btn_closeOpt_Click(object sender, EventArgs e)
        {
            f_opt.ActiveForm.Close();
        }
        private void f_opt_Load(object sender, EventArgs e)
        {
            l_mangaDir.Text = "Changer le répertoire de téléchargement (" + Program._rootPath + ")";
            cb_compress.Checked = Program._compress;
        }
        private void cb_compress_CheckedChanged(object sender, EventArgs e)
        {
            Program._compress = cb_compress.Checked;
            Outils.updateFicLineWithMotif(Program._cfg, cb_compress.Checked, Program._motifCompress);
        }
    }
}
