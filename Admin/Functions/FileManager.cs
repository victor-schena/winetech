using Entities.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Admin.Functions
{
  public class FileManager
  {
    public static string UploadSingleFile(HttpPostedFileBase file, string storageRoot, string delete = null)
    {
      if (!Directory.Exists(storageRoot))
        Directory.CreateDirectory(storageRoot);

      var fileName = file.FileName;
      var fileNameSplit = fileName.Split(new char[] { '.' });
      var ext = "." + fileNameSplit[fileNameSplit.Count() - 1];
      var onlyfileName = new StringBuilder();

      for (int i = 0; i < fileNameSplit.Count() - 1; i++)
      {
        if (i == fileNameSplit.Count() - 2)
          onlyfileName.Append(fileNameSplit[i]);
        else
          onlyfileName.Append(fileNameSplit[i] + ".");
      }

      fileName = string.Format("{0}-{1}{2}", Text.SetFriendlyName(onlyfileName.ToString()), Guid.NewGuid().ToString().GetHashCode().ToString("x"), ext);
      var fullPath = Path.Combine(storageRoot, Path.GetFileName(fileName));

      if (!string.IsNullOrEmpty(delete))
        Delete(delete, storageRoot);

      file.SaveAs(fullPath);

      return fileName.ToLower();
    }

    public static void Delete(string fileName, string storageRoot)
    {
      var filePath = Path.Combine(storageRoot, fileName);
      if (System.IO.File.Exists(filePath))
      {
        System.IO.File.Delete(filePath);
      }
    }
  }
}
