using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Admin.Functions
{
  public class Constants
  {
    public static string UrlDominioEstaticoUploads
    {
      get { return ConfigurationManager.AppSettings["UrlDominioEstaticoUploads"].ToString(); }
    }

    public static int ImageFileSize
    {
      get { return Convert.ToInt32(ConfigurationManager.AppSettings["ImageFileSize"]); }
    }

    //public static int DocumentFileSize
    //{
    //  get { return Convert.ToInt32(ConfigurationManager.AppSettings["DocumentFileSize"]); }
    //}
  }
}