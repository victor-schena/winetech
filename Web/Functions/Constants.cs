using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web.Functions
{
  public class Constants
  {
    public static string UrlDominioEstaticoUploads
    {
      get { return ConfigurationManager.AppSettings["UrlDominioEstaticoUploads"].ToString(); }
    }
  }
}