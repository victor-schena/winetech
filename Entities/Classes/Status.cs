using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Classes
{
  public class Status
  {
    #region Vars

    public virtual int Id { get; set; }
    public virtual string Desc { get; private set; }

    private static Status _Inativo = new Status() { Id = 0, Desc = "Inativo" };
    private static Status _Ativo = new Status() { Id = 1, Desc = "Ativo" };
    private static Status _PendenteEmail = new Status() { Id = 2, Desc = "Pendente Email" };
    private static Status _PendenteCadastro = new Status() { Id = 3, Desc = "Pendente Cadastro" };
    private static Status _Impugnado = new Status() { Id = 4, Desc = "Impugnado" };

    #endregion

    #region Properties

    public static Status Inativo { get { return _Inativo; } }
    public static Status Ativo { get { return _Ativo; } }
    public static Status PendenteEmail { get { return _PendenteEmail; } }
    public static Status PendenteCadastro { get { return _PendenteCadastro; } }
    public static Status Impugnado { get { return _Impugnado; } }

    #endregion

    #region All

    public static IEnumerable<Status> All()
    {
      yield return Status.Inativo;
      yield return Status.Ativo;
      yield return Status.PendenteEmail;
      yield return Status.PendenteCadastro;
      yield return Status.Impugnado;
    }

    #endregion
  }
}
