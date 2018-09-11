using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
  public class ImageSize
  {
    #region Vars

    public virtual int Id { get; set; }
    public virtual string Desc { get; private set; }
    public virtual float Width { get; set; }
    public virtual float Height { get; set; }
    public virtual string Round { get; private set; }

    private static ImageSize _Lg = new ImageSize() { Id = 0, Desc = "lg", Width = 961, Height = 531, Round = "down" };
    private static ImageSize _Md = new ImageSize() { Id = 1, Desc = "md", Width = 634, Height = 350, Round = "up" };
    private static ImageSize _Sm = new ImageSize() { Id = 2, Desc = "sm", Width = 307, Height = 170, Round = "down" };
    private static ImageSize _Tb = new ImageSize() { Id = 3, Desc = "tb", Width = 134, Height = 74, Round = "up" };

    #endregion

    #region Properties

    public static ImageSize Lg { get { return _Lg; } }
    public static ImageSize Md { get { return _Md; } }
    public static ImageSize Sm { get { return _Sm; } }
    public static ImageSize Tb { get { return _Tb; } }

    #endregion

    #region All

    public static IEnumerable<ImageSize> All()
    {
      yield return ImageSize.Lg;
      yield return ImageSize.Md;
      yield return ImageSize.Sm;
      yield return ImageSize.Tb;
    }

    #endregion
  }
}
