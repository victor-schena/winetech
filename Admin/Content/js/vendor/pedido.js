$(document).ready(function () {

 
  $("#searchName").blur(function () {
    if ($("#searchName").val() === "") return;
    $.post('/ClientePessoaFisica/Search', { Nome: $("#searchName").val() },
      function (data) {
        var mySelect = $('#selectClient').empty();
        mySelect.append($('<option></option>')).val(0).text("------------------------------");
        if (data !== [] && data.length > 0) {
          console.log(data);
          //ocultar loading
          for (var i = 0; i < data.length; i++) {
            if (data[i].PapelPessoaId === 1 && data[i].TipoPessoaId === 1) {
              var myOptions = { val: data[i].NomeCompleto };
              $.each(myOptions, function (val, text) {
                mySelect.append(
                  $('<option></option>').val(data[i].Id).html(text)
                );
              });
              console.log(data[i].NomeCompleto);
            }
            if (data[i].PapelPessoaId === 1 && data[i].TipoPessoaId === 2) {
              myOptions = { val: data[i].NomeFantasia };
              $.each(myOptions, function (val, text) {
                mySelect.append(
                  $('<option></option>').val(data[i].Id).html(text)
                );
              });
              console.log(data[i].NomeFantasia);
            }
          }
        }
        else {
          //exibir msg na tela
          mySelect.append($('<option></option>').val(0).text("Cliente não encontrado!"));
          console.log("Cliente não encontrado!");
        }
        mySelect.show();
      }
      , 'json');
  }
  );

  $("#searchProduct").blur(function () {
    if ($("#searchProduct").val() === "") return;
    try {
      $.post('/Produtos/Search', { Nome: $("#searchProduct").val() },
        function (data) {
          var mySelect = $('#selectProduct').empty();
          mySelect.append($('<option></option>')).val(0).text("------------------------------");
          if (data !== [] && data.length > 0) {
            console.log(data);
            //ocultar loading
            for (var i = 0; i < data.length; i++) {
              var myOptions = { val: data[i].Nome +" - "+ data[i].PrecoVenda };
              $.each(myOptions, function (val, text) {
                mySelect.append(
                  $('<option></option>').val(data[i].Id).html(text)
                );
              });
              console.log(data[i].Nome);
            }
          }
          else {
            //exibir msg na tela
            mySelect.append($('<option></option>').val(0).text("Produto não encontrado!"));
            console.log("Produto não encontrado!");
          }
          mySelect.show();
        }
        , 'json');
    } catch (e) {
      console.log(e)
    }
  }
  );

  $("#enviarProduto").click(
    function enviarClienteItem() {
      $.post('/Pedido/AddItem', { PedidoId: $('#PedidoId').val(), UserId: $('#User').val(), PessoaId: $('#selectClient').val(), ProdutoId: $('#selectProduct').val(), Quantidade: $('#qtdeProduto').val() },
        function (data) {
          console.log(data);
          $('#PedidoId').val(data.Pedido.Id);
          $('#User').val(data.Pedido.UserId);
          $('#selectClient').val(data.PessoaId);
          $('#qtdeProduto').val(0);
          $('.dataTables_empty').hide();
          $("#itemTable > tbody").html("");
          for (var i = 0; i < data.Produtos.length; i++) {
            var tr = $('<tr>');
            tr.append("<td class='text-center'>" + data.Produtos[i].Id + "</td>");
            tr.append("<td class='text-center'>" + data.Produtos[i].Nome + "</td>");
            tr.append("<td class='text-center'>" + data.Produtos[i].Quantidade + "</td>");
            tr.append("<td class='text-center'>" + data.Produtos[i].PrecoVenda.toLocaleString('pt-BR', { minimumFractionDigits: 2, style: 'currency', currency: 'BRL' }) + "</td>");
            tr.append("<td class='text-center'><div class='btn btn-default btn-flat' onclick = 'removerItem(" + data.Produtos[i].Id + ");' >Deletar</div></td></tr>");
            $('#itemTable').append(tr);
          }
          var mySelect = $('#selectProduct').empty();
          mySelect.append($('<option></option>')).val(0).text("------------------------------");
          $("#total").val(data.Pedido.Total.toLocaleString('pt-BR', { minimumFractionDigits: 2, style: 'currency', currency: 'BRL' }));

        }
        , 'json');
    }
  );

  $("#finalizarPedido").click(function () {
    $.post('/Pedido/FinalizarPedido', { PedidoId: $('#PedidoId').val(), UserId: $('#User').val(), PessoaId: $('#selectClient').val() },
      function (data) {
        window.location.href = data;
      }
      , 'json');
  }
  );



});
function removerItem(Id) {
  $.post('/Pedido/RemoverItem', { PedidoId: $('#PedidoId').val(), ProdutoId: Id },
    function (data) {
      console.log(data);
      $("#itemTable > tbody").html("");
      if (data.Produtos.length > 0) {
        for (var i = 0; i < data.Produtos.length; i++) {
          $("#total").val(data.Pedido.Total.toLocaleString('pt-BR', { minimumFractionDigits: 2, style: 'currency', currency: 'BRL' }));
          var tr = $('<tr>');
          tr.append("<td class='text-center'>" + data.Produtos[i].Id + "</td>");
          tr.append("<td class='text-center'>" + data.Produtos[i].Nome + "</td>");
          tr.append("<td class='text-center'>" + data.Produtos[i].Quantidade + "</td>");
          tr.append("<td class='text-center'>" + data.Produtos[i].PrecoVenda.toLocaleString('pt-BR', { minimumFractionDigits: 2, style: 'currency', currency: 'BRL' }) + "</td>");
          tr.append("<td class='text-center'><div class='btn btn-default btn-flat' onclick = 'removerItem(" + data.Produtos[i].Id + ");' >Deletar</div></td></tr>");
          $('#itemTable').append(tr);
        }
      }
      else {
        $("#total").val(0);
        $("#itemTable > tbody").html("");
        $("#itemTable > tbody").append("<tr><td valign='top' colspan='5' class='dataTables_empty'>Nenhum registro encontrado ):</td></tr>");
        $('.dataTables_empty').show();
      }
    }
    , 'json');
}
function devolverItem(Pedido_Id,Produto_Id) {
  $.post('/Pedido/DevolverItem', { PedidoId: Pedido_Id, ProdutoId: Produto_Id }, function (data) {
    console.log(data);
  } ,"json");  
}