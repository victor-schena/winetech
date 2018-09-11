using Admin.Controllers;
using Admin.DataContexts;
using Admin.Tables;
using Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Functions
{
  public static class Validations
  {
    public static bool VerificaMarioridadePenal(DateTime DataNascimento)
    {
      int AnoBase = DateTime.Today.Year - 18;
      if (DataNascimento.Year < AnoBase)
      {
        return true;
      }
      if (AnoBase == DataNascimento.Year)
      {
        if (DataNascimento.Month < DateTime.Now.Month)
        {
          if (DataNascimento.Day <= DateTime.Now.Day)
          {
            return true;
          }
        }
      }
      return false;
    }
    public static bool IsCnpj(string cnpj)
    {
      int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int soma;
      int resto;
      string digito;
      string tempCnpj;
      cnpj = cnpj.Trim();
      cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
      if (cnpj.Length != 14)
        return false;
      tempCnpj = cnpj.Substring(0, 12);
      soma = 0;
      for (int i = 0; i < 12; i++)
        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
      resto = (soma % 11);
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;
      digito = resto.ToString();
      tempCnpj = tempCnpj + digito;
      soma = 0;
      for (int i = 0; i < 13; i++)
        soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
      resto = (soma % 11);
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;
      digito = digito + resto.ToString();
      return cnpj.EndsWith(digito);
    }
    public static bool IsCpf(string cpf)
    {
      int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      string tempCpf;
      string digito;
      int soma;
      int resto;
      cpf = cpf.Trim();
      cpf = cpf.Replace(".", "").Replace("-", "");
      if (cpf.Length != 11)
        return false;
      switch (cpf)
      {
        case "11111111111":
          return false;
        case "00000000000":
          return false;
        case "2222222222":
          return false;
        case "33333333333":
          return false;
        case "44444444444":
          return false;
        case "55555555555":
          return false;
        case "66666666666":
          return false;
        case "77777777777":
          return false;
        case "88888888888":
          return false;
        case "99999999999":
          return false;
      }
      tempCpf = cpf.Substring(0, 9);
      soma = 0;

      for (int i = 0; i < 9; i++)
        soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
      resto = soma % 11;
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;
      digito = resto.ToString();
      tempCpf = tempCpf + digito;
      soma = 0;
      for (int i = 0; i < 10; i++)
        soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
      resto = soma % 11;
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;
      digito = digito + resto.ToString();
      return cpf.EndsWith(digito);
    }
    public static bool HasCredentials(string user, string action, string controller, string param = null)
    {
      if (HttpContext.Current.User.Identity.IsAuthenticated)
      {
        IdentityDb _db = new IdentityDb();

        // Primeiramente, selecionamos os papeis do usuário logado
        List<string> usrRoles = _db.Users
          .Where(u => u.UserName == user)
          .FirstOrDefault().Roles.Select(r => r.RoleId).ToList();

        // Em seguida, selecionamos todas as credenciais deste usuário
        List<Credential> usrCredentials = _db.ApplicationRoles.Join(usrRoles,
          a => a.Id,
          r => r,
          (a, r) => a).SelectMany(c => c.Credentials).ToList();

        return (usrCredentials.Where(c => c.Action.Contains(action) && c.Controller == controller && c.Param == param).Count() > 0);
      }
      else
        return false;
    }

  }
  public class Credenciais
  {

  }
}