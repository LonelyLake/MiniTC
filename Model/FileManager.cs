using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MiniTC.Model
{
    class FileManager
    {
        public string[] GetDrives() => DriveInfo
            .GetDrives()
            .Where(d => d.IsReady)
            .Select(d => d.RootDirectory.FullName)
            .ToArray();

        public string[] GetDirectories(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return new string[0];
            }

            try
            {
                return Directory.GetDirectories(path);
            }
            catch
            {
                return new string[0];
            }

        }

        public string[] GetFiles(string path)
        {
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                return new string[0];
            }
            try
            {
                return Directory.GetFiles(path);
            }
            catch
            {
                return new string[0];
            }
        }

        public string GetParentDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            try
            {
                var parent = Directory.GetParent(path);
                return parent?.FullName;
            }
            catch
            {
                return null;
            }
        }

        public void CopyFile(string sourceFullPath, string destFullPath)
        {
            if (string.IsNullOrEmpty(sourceFullPath) || string.IsNullOrEmpty(destFullPath))
            {
                throw new ArgumentException("Source or destination path is null or empty.");
            }
            try
            {
                File.Copy(sourceFullPath, destFullPath, true);
            }
            catch (IOException ex)
            {
                throw new IOException("Error copying file.", ex);
            }
        }

        public void CopyDirectory(string sourceDir, string destDir)
        {
            if (string.IsNullOrEmpty(sourceDir) || string.IsNullOrEmpty(destDir))
            {
                throw new ArgumentException("Source or destination directory is null or empty.");
            }
            if (!Directory.Exists(sourceDir))
            {
                throw new DirectoryNotFoundException($"Source directory '{sourceDir}' does not exist.");
            }

            try
            {
                Directory.CreateDirectory(destDir);
                foreach (var file in Directory.GetFiles(sourceDir))
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(destDir, fileName);
                    File.Copy(file, destFile, true);
                }
                foreach (var dir in Directory.GetDirectories(sourceDir))
                {
                    var dirName = Path.GetFileName(dir);
                    var destSubDir = Path.Combine(destDir, dirName);
                    CopyDirectory(dir, destSubDir);
                }
            }
            catch (IOException ex)
            {
                throw new IOException("Error copying directory.", ex);
            }
        }
    }
}
