using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
  public class ProductsListViewModel
  {
    public IEnumerable<Produto> Products { get; set; }
    public PagingInfo PagingInfo { get; set; }
    public string CurrentCategory { get; set; }
  }
}