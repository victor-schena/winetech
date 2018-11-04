$(document).ready(function () {

  function limpa_formulário_cep() {
    // Limpa valores do formulário de cep.
    $("#cep").val("");
    $("#rua").val("");
    $("#bairro").val("").prop("readonly", true);
    $("#cidade").val("");
    $("#uf").val("");
    $("#ibge").val("");
  }
  //Quando o campo cep perde o foco.
  $("#cep").blur(function () {
    //Nova variável "cep" somente com dígitos.
    var cep = $(this).val().replace(/\D/g, '');
    //Verifica se campo cep possui valor informado.
    if (cep != "") {
      //Expressão regular para validar o CEP.
      var validacep = /^[0-9]{8}$/;
      //Valida o formato do CEP.
      if (validacep.test(cep)) {
        //Preenche os campos com "..." enquanto consulta webservice.
        $("#rua").val("...");
        $("#bairro").val("...");
        $("#cidade").val("...");
        $("#uf").val("...");
        $("#ibge").val("...");
        //Consulta o webservice viacep.com.br/
        $.getJSON("//viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

          if (!("erro" in dados)) {
            //Atualiza os campos com os valores da consulta.
            $("#rua").val(dados.logradouro);
            $('#rua').prop('readonly', true);
            $("#bairro").val(dados.bairro);
            $('#bairro').prop('readonly', true);
            $("#cidade").val(dados.localidade);
            $('#cidade').prop('readonly', true);
            $("#uf").val(dados.uf);
            $('#uf').prop('readonly', true);
            $("#numero").val(' ');
            $('#complemento').val(' ');

          } //end if.
          else {
            //CEP pesquisado não foi encontrado.
            limpa_formulário_cep();
            alert("CEP não encontrado.");
          }
        });
      } //end if.
      else {
        //cep é inválido.
        limpa_formulário_cep();
        alert("Formato de CEP inválido.");
      }
    } //end if.
    else {
      //cep sem valor, limpa formulário.
      limpa_formulário_cep();
    }
  });
});

$(document).ready(function () {
  $("#cep").val("");
  $("#rua").val("");
  $("#bairro").val("");
  $("#cidade").val("");
  $("#uf").val("");
  $("#ibge").val("");
  $("#numero").val("");
  $("#complemento").val("");
});
//Adicionar endereco:
function finalizarCadastro(controllerName) {
  debugger;
  window.location.replace('/' + controllerName + '/Index');
}

$(document).ready(function () {
  $("#Pessoa_CPF").blur(function () {
    debugger;
    var cpf = $(this).val().replace(/[^\d]+/g, '');
    if (cpf == '') return false;
    // Elimina CPFs invalidos conhecidos    
    if (cpf.length != 11 ||
        cpf == "00000000000" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999")
      return false;
    // Valida 1o digito 
    add = 0;
    for (i = 0; i < 9; i++)
      add += parseInt(cpf.charAt(i)) * (10 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
      rev = 0;
    if (rev != parseInt(cpf.charAt(9)))
      return false;
    // Valida 2o digito 
    add = 0;
    for (i = 0; i < 10; i++)
      add += parseInt(cpf.charAt(i)) * (11 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
      rev = 0;
    if (rev != parseInt(cpf.charAt(10)))
      return false;
    return true;
  });
});