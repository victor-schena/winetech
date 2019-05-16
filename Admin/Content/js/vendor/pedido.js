﻿$(document).ready(function () {
  $("#searchName").blur(function () {
    //Mostrar loading
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



});