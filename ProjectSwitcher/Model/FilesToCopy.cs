using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSwitcher.Model
{
    [Serializable]
    public class FilesToCopy
    {
        public FilesToCopy()
        { }
        public FilesToCopy(FileInfo fi)
        {
            FileName = fi.Name;
            FilePath = fi.FullName;
        }
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private bool _checked = false;

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }


    }
}
