function SalvarPedido() {

  //Cliente
  var cliente = ;

  //Data
  var data = $("#DataPedido").val();

  
  //Valor
  var valor = $("#Total").val();

  var token = $('input[name="__RequestVerificationToken"]').val();
  var tokenadr = $('form[action="/Pedido/Create"] input[name="__RequestVerificationToken"]').val();
  var headers = {};
  var headersadr = {};
  headers['__RequestVerificationToken'] = token;
  headersadr['__RequestVerificationToken'] = tokenadr;

  //Gravar
  var url = "/Pedido/Create";

  $.ajax({
    url: url
      , type: "POST"
      , datatype: "json"
      , headers: headersadr
      , data: { Id: 0, DataPedido: data, Cliente: $("#PessoaID").val(), Valor: valor, __RequestVerificationToken: token }
      , success: function (data) {
        if (data.Resultado > 0) {
          ListarItens(data.Resultado);
        }
      }
  });
}
$.ajax({
  url: '/Pedido/TestPost',
  type: 'POST',
  datatype: 'JSON',
  data:
    {
      id: 1 ,
      valor: 1,
    }
     , success: function (data) {
       if (data> 0) {
         alert('Teste');
       }
     }, error: function (date) {
       alert('Errou!');
     }
});