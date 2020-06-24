function verificaValor(valor) {
    return valor != "" && valor != null && valor != undefined && valor != "undefined";
    //Retorna False caso não possua valor
}

String.prototype.onlyNumber = function () { return (this.replace(/\D/g, "")); };

//**********função do dropdown***********
//*
//*   Função responsável para carregar o próximo dropdown a partir do
//*   ultimo dropdown escolhido.
//*   idAnterior = jQuery("#CampoEscolhido").val();
//*   caminho = "Controller/Metodo";
//*   idProximo = jQuery("#CampoPreencher");
//*   chamada = chamaDropDown(idAnterior, caminho, idProximo);
//*
function chamaDropDown(CampoEscolhido, caminho, CampoPreencher) {
    $.post(content + caminho, CampoEscolhido,
        function (data) {
            if (data.Status == "Ok") {
                $(CampoPreencher).find("option").remove();
                $(data.ParametrosAdicionais).each(function () {
                    $(CampoPreencher).append("<option value='" + this.Valor + "'>" + this.Texto + "</option>");
                });
                $(CampoPreencher).val("").change();
                //$(CampoPreencher).select2();
            }
            if (data.Status == "Erro") {
                mensagemErro("Ocorreu um erro inesperado, por favor, tente novamente.");
            }
        });
}


function BuscaCEP() {
    var cep = $("#cep").val();

    if (verificaValor(cep)) {
        $.post(content + 'Base/BuscarCEP', { cep: cep })
            .done(function (data) {
                if (data.Status == "Ok") {
                    $(data.ParametrosAdicionais).each(function () {
                        $("#logradouro").val(this.end);
                        $("#bairro").val(this.bairro);
                        $("#uf").val(this.uf);
                        $("#municipioid").val(this.cidade).change();
                    });
                } else {
                    mensagemErro(data.Mensagem);
                }
            });
    }
}

String.prototype.replaceAll = String.prototype.replaceAll || function (needle, replacement) {
    return this.split(needle).join(replacement);
};

function ValidarCnpj() {
    var cnpj = $("#cnpj").val().replace(/[^\d]+/g, '');

    if (!validar_cnpj(cnpj)) {

        $("#cnpj").val('');

        mensagemAviso("CNPJ inválido");
    }
}

function soNumero(campo) {
    v = campo.value;

    RegExp = /^[0-9]+$/;

    if (!RegExp.test(v)) {
        // não contem so numeros
        return false;
    }

    return true;
}

function customNumeric(e, excecoes) {
    //var whichCode = (window.Event) ? e.which : e.keyCode;
    var whichCode = (e.which) ? e.which : event.keyCode;

    //Teclas funcionais permitidas.
    switch (whichCode) {
        case (8): return true; //Backspace
        case (9): return true; //Tab
        case (16): return true; //Shift
        case (17): return true; //Ctrl
        case (35): return true; //End
        case (36): return true; //Home
        case (37): return true; //Seta esquerda
        case (44): return true; //virgula
        case (39 && String.fromCharCode(39) != "'"): return true; //Seta direita, (aspas tem o mesmo númeric, por isso o if)
        case (46 && String.fromCharCode(46) != "."): return true; //Delete

        case (67): if (e.ctrlKey) return true; //Ctrl + C
        case (88): if (e.ctrlKey) return true; //Ctrl + X
        case (118): if (e.ctrlKey) return true; //Ctrl + V

        //Tratamento para as demais.
        default:
            key = String.fromCharCode(whichCode);

            var strCheck = "0123456789";

            if (excecoes) strCheck += excecoes;

            if (strCheck.indexOf(key) == -1) {
                //e.returnValue = false;
                return false;
            }

            return true;
    }
}

//********************************************************
//* Função para validar cpf
//*
//* Autor: Renan Siravegna
//* E-mail: rsmoreira@fazenda.ms.gov.br
//
function validar_cpf(cpf) {
    var numeros, digitos, soma, i, resultado, digitos_iguais;
    digitos_iguais = 1;
    if (cpf.length < 11)
        return false;
    for (i = 0; i < cpf.length - 1; i++)
        if (cpf.charAt(i) != cpf.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    if (!digitos_iguais) {
        numeros = cpf.substring(0, 9);
        digitos = cpf.substring(9);
        soma = 0;
        for (i = 10; i > 1; i--)
            soma += numeros.charAt(10 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        numeros = cpf.substring(0, 10);
        soma = 0;
        for (i = 11; i > 1; i--)
            soma += numeros.charAt(11 - i) * i;
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
}

//********************************************************
//* Função para validar cnpj
//*
//* Autor: Renan Siravegna
//
function validar_cnpj(cnpj) {
    var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
    digitos_iguais = 1;
    if (cnpj.length < 14 && cnpj.length < 15)
        return false;
    for (i = 0; i < cnpj.length - 1; i++)
        if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    if (!digitos_iguais) {
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;
        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;
        return true;
    }
    else
        return false;
}

function validaEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function mensagemConfirmacao(texto, callbackSim, callbackNao) {
    swal.fire({
        title: texto,
        type: "warning",
        confirmButtonText: "Confirmar",
        confirmButtonColor: "#26a69a",
        cancelButtonText: "Cancelar",
        showCancelButton: true,
    }).then(function (result) {
        if (result.value) {
            if (verificaValor(callbackSim)) {
                eval(callbackSim);
            }
        }
        else if (result.dismiss) {
            if (verificaValor(callbackNao)) {
                eval(callbackNao);
            }
        }
    });
}

function mensagem(texto, callback) {
    if (callback) {
        swal.fire({
            title: texto,
            confirmButtonText: "OK",
            confirmButtonColor: "#26a69a",
            showCancelButton: false,
            onClose: function () {
                if (verificaValor(callback)) {
                    eval(callback);
                }
            }
        }).then(function (result) {
            if (result.value) {
                eval(callback);
            }
        });
    }
    else {
        swal.fire({
            title: texto,
            confirmButtonColor: "#26a69a",
            showConfirmButton: true
        });
    }
}

function mensagemSucesso(texto, callback) {
    if (callback) {
        swal.fire({
            title: "Sucesso",
            text: texto,
            type: "success",
            confirmButtonColor: "#26a69a",
            confirmButtonText: "OK",
            showCancelButton: false,
            onClose: function () {
                if (verificaValor(callback)) {
                    eval(callback);
                }
            }
        }).then(function (result) {
            if (result.value) {
                eval(callback);
            }
        });
    }
    else {
        swal.fire({
            title: "Sucesso",
            text: texto,
            confirmButtonColor: "#26a69a",
            type: "success",
            showConfirmButton: true
        });
    }
}

function mensagemErro(texto, callback) {
    if (callback) {
        swal.fire({
            title: "Ocorreu um erro",
            html: texto,
            type: "error",
            confirmButtonColor: "#26a69a",
            confirmButtonText: "OK",
            showCancelButton: false,
            onClose: function () {
                if (verificaValor(callback)) {
                    eval(callback);
                }
            }
        }).then(function (result) {
            if (result.value) {
                eval(callback);
            }
        });
    }
    else {
        swal.fire({
            title: "Ocorreu um erro",
            html: texto,
            type: "error",
            confirmButtonColor: "#26a69a",
            showConfirmButton: true
        });
    }
}

function mensagensErro(arrMsg) {
    swal.fire({
        title: "Ocorreu um erro",
        html: arrMsg.join("<br>"),
        type: "error",
        confirmButtonColor: "#26a69a",
        showConfirmButton: true
    });
}

function mensagemAviso(texto, callback) {
    if (callback) {
        swal.fire({
            title: "Atenção",
            html: texto,
            type: "warning",
            showCancelButton: false,
            confirmButtonText: "OK",
            confirmButtonColor: "#26a69a",
            closeOnConfirm: true,
            onClose: function () {
                if (verificaValor(callback)) {
                    eval(callback);
                }
            }
        }).then(function (result) {
            if (result.value) {
                eval(callback);
            }
        });
    }
    else {
        swal.fire({
            title: "Atenção",
            html: texto,
            confirmButtonColor: "#26a69a",
            type: "warning",
            showConfirmButton: true
        });
    }
}

function mensagensAviso(arrMsg) {
    swal.fire({
        title: "Atenção",
        html: arrMsg.join("<br>"),
        type: "warning",
        confirmButtonColor: "#26a69a",
        showConfirmButton: true
    });
}

function desabilitarCampos() {
    $("input[type=radio],input[type=number], input[type=text], input[type=password], input[type=checkbox], input[type=date], input[type=time], select, textarea").each(function () {
        $(this).attr("disabled", true);
    });
}

function MesExtenso(mes) {
    var mesInteiro = parseInt(mes);
    switch (mesInteiro) {
        case 1:
            return "Janeiro";
        case 2:
            return "Fevereiro";
        case 3:
            return "Março";
        case 4:
            return "Abril";
        case 5:
            return "Maio";
        case 6:
            return "Junho";
        case 7:
            return "Julho";
        case 8:
            return "Agosto";
        case 9:
            return "Setembro";
        case 10:
            return "Outubro";
        case 11:
            return "Novembro";
        case 12:
            return "Dezembro";
        default:
            return "";
    }
}

function exportToExcel(tableid, nomearquivo) {
    var htmls = "";
    var uri = 'data:application/vnd.ms-excel;base64,';
    var template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head> <meta charset="UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>';
    var base64 = function (s) {
        return window.btoa(unescape(encodeURIComponent(s)))
    };

    var format = function (s, c) {
        return s.replace(/{(\w+)}/g, function (m, p) {
            return c[p];
        })
    };

    htmls = document.getElementById(tableid).innerHTML;

    var ctx = {
        worksheet: 'Worksheet',
        table: htmls
    }


    var link = document.createElement("a");
    link.download = nomearquivo + ".xls";
    link.href = uri + base64(format(template, ctx));
    link.click();
}

function getFormDataPadrao($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}

//reverse = 0 ordena crescente
//reverse = 1 ordena decrescente
function sortTable(idtabela, col, reverse) {
    var table = document.getElementById(idtabela);
    var tb = table.tBodies[0], // use `<tbody>` to ignore `<thead>` and `<tfoot>` rows
        tr = Array.prototype.slice.call(tb.rows, 0), // put rows into array
        i;
    reverse = -((+reverse) || -1);
    tr = tr.sort(function (a, b) { // sort rows
        return reverse // `-1 *` if want opposite order
            * (a.cells[col].textContent.trim() // using `.textContent.trim()` for test
                .localeCompare(b.cells[col].textContent.trim())
            );
    });
    for (i = 0; i < tr.length; ++i) tb.appendChild(tr[i]); // append each row in order
}