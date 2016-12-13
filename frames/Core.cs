using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace frames
{
    class MyEventArgs : EventArgs
    {
        public string Message { get; private set; }
        public MyEventArgs(string message = null)
        {
            Message = message;
        }
    }
    class Core
    {
        public event EventHandler<MyEventArgs> CallBack;
        public Core()
        {

        }
        public void Transform(string file, string subfolder)
        {
            CheckInput(file);
            if (Directory.Exists(file)) // do loop if this is a folder 
            {
                var files = Directory.GetFiles(file);
                Transform(files, subfolder);
            }

            if (CallBack != null)
                CallBack(this, new MyEventArgs(file));
            
            string path = Path.GetDirectoryName(file);
            if (subfolder != null)
                path = CheckFolder(Path.Combine(path, subfolder));

            var bmpDec = BitmapDecoder.Create(new Uri(file), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            var bmpEnc = new JpegBitmapEncoder();
            bmpEnc.QualityLevel = 100;
            bmpEnc.Frames.Add(bmpDec.Frames[0]);
            var oldfn = Path.GetFileName(file);
            var newfn = Path.ChangeExtension(oldfn, "JPG");
            using (var ms = File.Create(Path.Combine(path, newfn), 10000000))
            {
                bmpEnc.Save(ms);
                if (CallBack != null)
                    CallBack(this, new MyEventArgs(path + "\\" + newfn));
            }
            
        }

        private string CheckFolder(string path)
        {
            // todo: add validation!!!!!
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        void CheckInput(string i)
        {
            if (!File.Exists(i) && !Directory.Exists(i) || File.Exists(i) && Path.GetExtension(i) != ".CR2")
                throw new Exception("wrong format");
        }
        public void Transform(string[] files, string subfolder)
        {
            foreach (var file in files)
            {
                Transform(file, subfolder);
            }

        }

    }
}
