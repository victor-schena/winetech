using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
  public class PapelPessoa
  {
    #region Vars

    public virtual int Id { get; set; }
    public virtual string Desc { get; set; }

    private static PapelPessoa _Cliente = new PapelPessoa() { Id = 1, Desc = "Cliente" };
    private static PapelPessoa _Fornecedor = new PapelPessoa() { Id = 2, Desc = "Fornecedor" };

    #endregion

    #region Properties

    public static PapelPessoa Cliente { get { return _Cliente; } }
    public static PapelPessoa Fornecedor { get { return _Fornecedor; } }

    #endregion

    #region All

    public static IEnumerable<PapelPessoa> All()
    {
      yield return PapelPessoa.Cliente;
      yield return PapelPessoa.Fornecedor;
    }

    #endregion
  }
}
