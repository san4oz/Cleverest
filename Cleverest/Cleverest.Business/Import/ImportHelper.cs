using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Import
{
    public static class ImportHelper
    {
        public const string SuccessFolderName = "Imported";
        public const string FailedFolderName = "Failed";

        public static void MoveToSuccessFolder(string file, string importFolderPath = null)
        {
            MoveToSuccessFolder(new List<string> { file }, importFolderPath);
        }

        public static void MoveToSuccessFolder(List<string> files, string importFolderPath = null)
        {
            foreach (var file in files)
            {
                var direcory = string.IsNullOrEmpty(importFolderPath)
                                   ? Path.Combine(Path.GetDirectoryName(file), SuccessFolderName)
                                   : Path.Combine(importFolderPath, SuccessFolderName);
                MoveFileToDirectory(file, direcory);
            }
        }

        public static void MoveToFailedFolder(string file, string importFolderPath = null)
        {
            MoveToFailedFolder(new List<string> { file });
        }

        private static void MoveFileToDirectory(string file, string directory)
        {
            if (string.IsNullOrEmpty(file)) return;
            var fileName = Path.GetFileName(file);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            File.Move(file, string.Format("{0}\\{1}_{2}", directory, DateTime.Now.ToString("yyyy-MM-dd.HH-mm-ss"), fileName));
        }

        public static void MoveToFailedFolder(List<string> files, string importFolderPath = null)
        {
            foreach (var file in files)
            {
                var direcory = string.IsNullOrEmpty(importFolderPath)
                                   ? Path.Combine(Path.GetDirectoryName(file), FailedFolderName)
                                   : Path.Combine(importFolderPath, FailedFolderName);
                MoveFileToDirectory(file, direcory);
            }
        }
    }
}
