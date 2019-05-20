"use strict";


$(function () {
  /*
    -------------------------
    FEEDBACK INPUT FILE
    -------------------------
  */
  $(":file").on('change', function () {
    var valorFile = $(this).val().replace("C:\\fakepath\\", "");
    if (valorFile) {
      $(".valorFile").html(valorFile);
      $(this).prev().text(valorFile);
      $(this).prev().css({ "text-transform": "uppercase" });
    }
  });


  /*
    -------------------------
    REMOVE DIACRITICS
    -------------------------
  */
  $('.add-url').on('blur', function () {
    $('.is-url').val(removeDiacritics($(this).val()).replace(/\s+/g, "-").replace(/[^a-zA-Z0-9_-]/ig, '').toLowerCase());
  });


  /*
    -------------------------
    SOMENTE NÚMEROS
    -------------------------
  */
  $('.numberonly').bind('keyup blur', function () {
    $(this).val($(this).val().replace(/[^0-9]+/, ''));
  });

  /*
    -------------------------
    Letras Maiusculas
    -------------------------
  */
  //$('[type=text]').keyup(function () {
  //  if ($(this).val() != '') {
  //    $(this).val($(this).val().str.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();}));
  //  }
  //});
  
  /*
    -------------------------
    CONFIRM DELETAR
    -------------------------
  */
  $('.btn-delete').on('click', function () {
    $('#form-delete').attr('action', $(this).attr('data-action'));
  });
  /*
    -------------------------
    CONFIRM ADICIONAR AO CARRINHO
    -------------------------
  */
  $('.btn-enviar').on('click', function () {
    $('#form-add').attr('action', $(this).attr('data-action'));
  });
  /*
    -------------------------
    CPF e RG
    -------------------------
  */
  $('.cnpj').mask('99.999.999/9999-99', { reverse: true });
  $('.cpf').mask('999.999.999-99', { reverse: true });
  $('.rg').mask('99.999.999-9', { reverse: true });
  $('.datas').mask('99/99/9999', { reverse: true });
  $('.preco').mask('000.000.000.000.000,00', { reverse: true });


  

  /*
    -------------------------
    FORMS
    -------------------------
  */
  $("form").each(function () {
    $(this).validate({
      ignore: ":hidden:not(textarea)",
      rules: {
        WysiHtmlField: "required"
      },
      errorPlacement: function (error, element) {

        $('div.validation-summary').show();
        $(element).addClass("error-placement").removeClass("unhighlight");

        if ($(element).hasClass("fileupload") || $(element).hasClass("datepicker") || $(element).hasClass("timepicker") || $(element).hasClass("input-video")) {
          $(element).parent().parent().children("label").addClass("error-placement-lbl").removeClass("unhighlight-lbl");
        }
        else if ($(element).hasClass("wysihtml5")) {
          $(element).parent().children("label").addClass("error-placement-lbl").removeClass("unhighlight-lbl");
          $(element).parent().children("iframe").addClass("error-placement-brdr").removeClass("unhighlight-brdr");
        }
        else if ($(element).hasClass("wysiwyg-froala")) {
          $(element).parent().children("label").addClass("error-placement-lbl").removeClass("unhighlight-lbl");
          $(element).parent().children("div").addClass("error-placement-brdr error-placement-rds").removeClass("unhighlight-brdr unhighlight-rds");
        }
        else {
          $(element).parent().children("label").addClass("error-placement-lbl").removeClass("unhighlight-lbl");
        }
      },
      unhighlight: function (element, errorClass, validClass) {

        $(element).addClass("unhighlight").removeClass("error-placement");

        if ($(element).hasClass("fileupload") || $(element).hasClass("datepicker") || $(element).hasClass("timepicker") || $(element).hasClass("input-video")) {
          $(element).parent().parent().children("label").addClass("unhighlight-lbl").removeClass("error-placement-lbl");
        }
        else if ($(element).hasClass("wysihtml5")) {
          $(element).parent().children("label").addClass("unhighlight-lbl").removeClass("error-placement-lbl");
          $(element).parent().children("iframe").addClass("unhighlight-brdr").removeClass("error-placement-brdr");
        }
        else if ($(element).hasClass("wysiwyg-froala")) {
          $(element).parent().children("label").addClass("unhighlight-lbl").removeClass("error-placement-lbl");
          $(element).parent().children("div").addClass("unhighlight-brdr unhighlight-rds").removeClass("error-placement-brdr error-placement-rds");
        }
        else {
          $(element).parent().children("label").addClass("unhighlight-lbl").removeClass("error-placement-lbl");
        }
      },
      submitHandler: function (form) {

        $('input[type="search"]').val("");
        $('input[type="search"]').keyup();

        $("form").find('input[type="submit"]').data("value") == undefined ? $("form").find('input[type="submit"]').val("Salvando...") : $("form").find('input[type="submit"]').val($("form").find('input[type="submit"]').data("value"));
        $("form").find('input[type="submit"]').next().attr("disabled", "disabled")
        $("form").find('input[type="submit"]').attr("disabled", "disabled");
        form.submit();
      }
    });
  });


  /*
    -------------------------
    MASKS
    -------------------------
  */
  // Telefone
  $('.telefone').focusout(function () {
    var phone, element;
    element = $(this);
    element.unmask();
    phone = element.val().replace(/\D/g, '');
    if (phone.length > 10) {
      element.mask("(99) 99999-999?9");
    } else {
      element.mask("(99) 9999-9999?9");
    }
  }).trigger('focusout');

  // Cep
  $('.cep').mask("99999-999");
  /*
    -------------------------
    LIMIT PARA TODOS OS INPUTS TEXT
    -------------------------
  */
  $.each($(':text, textarea'), function () {
    $(this).limit($(this).attr("data-val-length-max"), $(this).parent().find(".chars-left"));
  });


  /*
    -------------------------
    SORTABLE - TABLES
    -------------------------
  */
  var adjustment;
  $('.sortable').sortable({
    containerSelector: 'table',
    itemPath: '> tbody',
    itemSelector: 'tr',
    placeholder: '<tr class="placeholder"/>',
    group: 'simple_with_animation',
    handle: 'i.icon-move',
    pullPlaceholder: true,
    onDrop: function (item, targetContainer, _super) {
      var clonedItem = $('<tr/>').css({ height: 0 })
      item.before(clonedItem)
      clonedItem.animate({ 'height': item.height() })

      item.animate(clonedItem.position(), function () {
        clonedItem.detach()
        _super(item)
      })
    },
    onDragStart: function ($item, container, _super) {
      var offset = $item.offset(),
      pointer = container.rootGroup.pointer

      adjustment = {
        left: pointer.left - offset.left,
        top: pointer.top - offset.top
      }

      _super($item, container)
    },
    onDrag: function ($item, position) {
      $item.css({
        left: position.left - adjustment.left,
        top: position.top - adjustment.top
      })
    }
  });


  /*
    -------------------------
    ORDENAÇÃO - TABLES
    -------------------------
  */
  $('.btn-ordenar').on('click', function () {
    var items = $('table.sortable').not('.sync-sortable').find('tbody tr').sortable("serialize").get();
    var order = [];

    $(items).each(function () {
      order.push($(this).data('id'));
    });

    $.ajax({
      type: "POST",
      url: $('table.sortable').not('.sync-sortable').data('action'),
      data: { order: order }
    })
    .done(function (msg) {
      if (msg == "ok") {
        if ($('div.alert-success').length > 0) {
          $('div.alert-success').remove();
        }
        $(".msg-after").prepend('<div class="alert alert-success">' + $('table.sortable').not('.sync-sortable').data('info') + ' Ordenados.</div>');
      }
      else {
        if ($('div.alert-danger').length > 0) {
          $('div.alert-danger').remove();
        }
        $(".msg-after").prepend('<div class="alert alert-danger">' + $('table.sortable').not('.sync-sortable').data('info') + ' Não Ordenados.</div>');
      }
    });
  });


  /*
    -----------------------------
    EVITA A EXECUÇÃO DO SORTABLE NOS BOTÕES DE AÇÃO
    -----------------------------
  */
  $('.media-list').on('mousedown', 'span.delete-item, span.delete-item-sec, span.edit-noticia, span.highlight-macro, span.highlight-homepages, span.sec-highlight-macro, span.sec-highlight-homepages', function (event) {
    event.stopPropagation();
  });


  /*
    -------------------------
    DATA TABLES
    -------------------------
  */
  $('table.table').each(function () {
    $(this).dataTable({
      "language": {
        "processing": "Aguarde...",
        "lengthMenu": "Mostrar _MENU_ registros por página",
        "zeroRecords": "Nenhum registro encontrado ):",
        "info": "",
        "infoEmpty": "Nenhum registro encontrado ):",
        "search": "Buscar: ",
        "url": "",
        "paginate": {
          "first": "Primeiro",
          "previous": "<",
          "next": ">",
          "last": "Último"
        }
      },
      "scrollX": true,
      "scrollY": $(this).data("scrolly") == undefined ? "" : $(this).data("scrolly"),
      "scrollCollapse": true,
      "paging": $(this).data("paging") == undefined ? true : $(this).data("paging"),
      "info": false,
      "aaSorting": [],
      'iDisplayLength': $(this).data("entries") == undefined ? 10 : $(this).data("entries"),
      "bFilter": $(this).data("filter") == undefined ? true : $(this).data("filter"),
      "columnDefs": [
          { targets: 'no-sort', orderable: false }
      ]
    });
  });


  /*
    -------------------------
    CHOSEN
    -------------------------
  */
  var isMobile = false;
  // device detection
  if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
      || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) isMobile = true;

  if (!isMobile) {
    $('.chosen-select').each(function () {
      $(this).chosen({});
    });
  }
  else {
    $('.chosen-select').each(function () {
      $(this).removeClass("chosen-select");
    });
  }
});


/*
  -------------------------
  MENU ACTIVE
  -------------------------
*/
function setactivemenu(id) {
  $.each($('.sidebar-menu li a, .sidebar-menu li, .sidebar-menu li.treeview ul li a a, .sidebar-menu li.treeview ul li'), function () {
    if ($(this).hasClass('active')) {
      $(this).removeClass('active');
    }
  });
  $(id).parent().addClass('active');
  $(id).parent().parent().parent().addClass('active');
  $(id).parent().parent().parent().children().first().addClass('active');
}
$("#test").hide()