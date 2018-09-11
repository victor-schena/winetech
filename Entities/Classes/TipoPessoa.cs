using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
  public class TipoPessoa
  {
    #region Vars

    public int Id { get; set; }
    public string Desc { get; set; }


    private static TipoPessoa _Fisica = new TipoPessoa() { Id = 1, Desc = "Fisica" };
    private static TipoPessoa _Juridica = new TipoPessoa() { Id = 2, Desc = "Juridica" };

    #endregion

    #region Properties

    public static TipoPessoa Fisica { get { return _Fisica; } }
    public static TipoPessoa Juridica { get { return _Juridica; } }

    #endregion

    #region All

    public static IEnumerable<TipoPessoa> All()
    {
      yield return TipoPessoa.Fisica;
      yield return TipoPessoa.Juridica;
    }

    #endregion
  }
}
