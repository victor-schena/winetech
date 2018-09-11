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

    //public static List<string> UploadSingleFileAndGenerateAllSizes(HttpPostedFileBase file, string storageRoot)
    //{
    //  var imgs = new List<string>();

    //  if (!Directory.Exists(storageRoot))
    //    Directory.CreateDirectory(storageRoot);

    //  string fileName = file.FileName;
    //  string[] fileNameSplit = fileName.Split(new char[] { '.' });
    //  string ext = "." + fileNameSplit[fileNameSplit.Count() - 1];
    //  StringBuilder onlyfileName = new StringBuilder();
    //  for (int i = 0; i < fileNameSplit.Count() - 1; i++)
    //  {
    //    if (i == fileNameSplit.Count() - 2)
    //      onlyfileName.Append(fileNameSplit[i]);
    //    else
    //      onlyfileName.Append(fileNameSplit[i] + ".");
    //  }

    //  fileName = string.Format("{0}-{1}{2}", Text.SetFriendlyName(onlyfileName.ToString()), Guid.NewGuid().ToString().GetHashCode().ToString("x"), ext);
    //  var fullPath = Path.Combine(storageRoot, Path.GetFileName(fileName));

    //  file.SaveAs(fullPath);

    //  /* Thumbs */
    //  var image = Image.FromFile(fullPath);
    //  foreach (var item in ImageSize.All())
    //  {
    //    var fileNameThumb = string.Format("{0}-{1}-{2}{3}", Text.SetFriendlyName(onlyfileName.ToString()), item.Desc, Guid.NewGuid().ToString().GetHashCode().ToString("x"), ext);
    //    var fullPathThumb = Path.Combine(storageRoot, Path.GetFileName(fileNameThumb));

    //    CreateThumb(fullPathThumb, image, item.Width, item.Height, item.Round);
    //    imgs.Add(fileNameThumb.ToLower());
    //  }

    //  return imgs;
    //}

    //public static List<string> UploadSingleFileAndGenerateAllSizes(Image file, string fileName, string storageRoot)
    //{
    //  var imgs = new List<string>();

    //  if (!Directory.Exists(storageRoot))
    //    Directory.CreateDirectory(storageRoot);

    //  string[] fileNameSplit = fileName.Split(new char[] { '.' });
    //  string ext = "." + fileNameSplit[fileNameSplit.Count() - 1];
    //  StringBuilder onlyfileName = new StringBuilder();
    //  for (int i = 0; i < fileNameSplit.Count() - 1; i++)
    //  {
    //    if (i == fileNameSplit.Count() - 2)
    //      onlyfileName.Append(fileNameSplit[i]);
    //    else
    //      onlyfileName.Append(fileNameSplit[i] + ".");
    //  }

    //  fileName = string.Format("{0}-{1}{2}", Text.SetFriendlyName(onlyfileName.ToString()), Guid.NewGuid().ToString().GetHashCode().ToString("x"), ext);
    //  var fullPath = Path.Combine(storageRoot, Path.GetFileName(fileName));

    //  file.Save(fullPath);

    //  /* Thumbs */
    //  var image = Image.FromFile(fullPath);
    //  foreach (var item in ImageSize.All())
    //  {
    //    var fileNameThumb = string.Format("{0}-{1}-{2}{3}", Text.SetFriendlyName(onlyfileName.ToString()), item.Desc, Guid.NewGuid().ToString().GetHashCode().ToString("x"), ext);
    //    var fullPathThumb = Path.Combine(storageRoot, Path.GetFileName(fileNameThumb));

    //    CreateThumb(fullPathThumb, image, item.Width, item.Height, item.Round);
    //    imgs.Add(fileNameThumb);
    //  }

    //  //Set lower case
    //  imgs.ForEach(i => i = i.ToLower());

    //  return imgs;
    //}

    public static void Delete(string fileName, string storageRoot)
    {
      var filePath = Path.Combine(storageRoot, fileName);
      if (System.IO.File.Exists(filePath))
      {
        System.IO.File.Delete(filePath);
      }
    }

    //public static void Download(string fileName, string storageRoot)
    //{
    //  var filePath = Path.Combine(storageRoot, fileName);
    //  if (System.IO.File.Exists(filePath))
    //  {
    //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
    //    HttpContext.Current.Response.ContentType = "application/octet-stream";
    //    HttpContext.Current.Response.ClearContent();
    //    HttpContext.Current.Response.WriteFile(filePath);
    //  }
    //  else
    //    HttpContext.Current.Response.StatusCode = 404;
    //}

    //private static void CreateThumb(string storageRootThumb, Image image, double width, double height, string round)
    //{
    //  double imgWidth = image.Width;
    //  double imgHeight = image.Height;

    //  //get proportion
    //  double proporcao = 1;
    //  if (imgHeight > imgWidth)
    //    proporcao = (imgHeight / imgWidth);
    //  else
    //    proporcao = (imgWidth / imgHeight);

    //  //get size
    //  if (imgWidth > imgHeight)
    //  {
    //    //round
    //    if (round.Equals("up"))
    //      width = Math.Ceiling(height * proporcao);
    //    else //down
    //      width = Math.Floor(height * proporcao);
    //  }
    //  else
    //    height = width * proporcao;

    //  var bitmap = new Bitmap((int)width, (int)height);
    //  var gr = Graphics.FromImage(bitmap);

    //  gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
    //  gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
    //  gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

    //  var rectDestination = new Rectangle(0, 0, (int)width, (int)height);

    //  //set image codec of JPEG type, the index of JPEG codec is “1″
    //  System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()[1];

    //  //set the parameters for defining the quality of the thumbnail… here it is set to 100%
    //  System.Drawing.Imaging.EncoderParameters eParams = new System.Drawing.Imaging.EncoderParameters(2);
    //  eParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 70L);
    //  eParams.Param[1] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 0L);

    //  gr.DrawImage(image, rectDestination, 0, 0, (float)imgWidth, (float)imgHeight, System.Drawing.GraphicsUnit.Pixel);
    //  bitmap.Save(storageRootThumb, codec, eParams);

    //  bitmap.Dispose();
    //}

    //public static Image Base64ToImage(string base64String)
    //{
    //  if (base64String.Contains("data:image/"))
    //  {
    //    base64String = base64String.Split(',')[1];
    //  }

    //  // Convert base 64 string to byte[]
    //  byte[] imageBytes = Convert.FromBase64String(base64String);
    //  // Convert byte[] to Image
    //  using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
    //  {
    //    Image image = Image.FromStream(ms, true);
    //    return image;
    //  }
    //}
  }
}
