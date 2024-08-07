<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="apoio_monsanto.cooperantes.report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RELATÓRIO - Apoio Monsanto</title>
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="col-lg-12">
                <div class="form-panel col-lg-12">
                    <div class="header-gen">
                        <span class="mb "><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Relatório</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Nome do Cliente</label>
                                    <asp:TextBox ID="txNam" class="form-control" runat="server" onkeyup="copydata()" placeholder="Digite o Nome do Cliente"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">CPF / CNPJ do Cliente</label>
                                    <asp:TextBox ID="txDc" class="form-control" name='cpfCnpj' runat="server" onkeypress='mascaraMutuario(this,cpfCnpj)' onblur='clearTimeout()' placeholder="Digite o CPF ou CNPJ do Cliente" MaxLength="18"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Tipo de Processo</label><br />
                                    <asp:ListBox runat="server" ID="ddTipoCont" class="btn btn-default dropdown-toggle col-lg-12" data-toggle="dropdown" DataSourceID="obTipos" DataTextField="name" DataValueField="id_process" SelectionMode="multiple"></asp:ListBox>
                                    <asp:ObjectDataSource ID="obTipos" runat="server" SelectMethod="selectTipoProcessoToReport" TypeName="apoio_monsanto.coopcom">
                                        <SelectParameters>
                                            <asp:Parameter Name="id_process" Type="String" />
                                            <asp:Parameter DefaultValue="S" Name="ativo" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Contrato Mãe</label><div></div>
                                    <asp:TextBox ID="contrato_mae" class="form-control" runat="server" placeholder="Informe o número"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Contrato Base</label><div></div>
                                    <asp:TextBox ID="contrato_base" class="form-control" runat="server" placeholder="Informe o número"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Unidade</label><div></div>
                                    <asp:DropDownList ID="ddUnidade" runat="server" class="btn btn-default dropdown-toggle col-lg-12" data-toggle="dropdown" DataSourceID="obUnidades" DataTextField="nome" DataValueField="nome"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Etapa (Status)</label><div></div>
                                    <asp:DropDownList ID="ddEtapa" runat="server" class="btn btn-default dropdown-toggle col-lg-12" data-toggle="dropdown" DataSourceID="obEtapas" DataTextField="etapa" DataValueField="id"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Mostra Cancelados?</label><div></div>
                                    <asp:DropDownList ID="ddCanc" runat="server" class="btn btn-default dropdown-toggle col-lg-12" data-toggle="dropdown">
                                        <asp:ListItem Value="false">Não</asp:ListItem>
                                        <asp:ListItem Value="true">Sim</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="fprm-group">
                                <div class="col-lg-3 ">
                                    <label class="control-label">Safra</label>
                                    <asp:DropDownList ID="ddTpSafra" runat="server" class="btn btn-default dropdown-toggle safra col-lg-12" data-toggle="dropdown">
                                        <asp:ListItem Value="">- Todos -</asp:ListItem>
                                        <asp:ListItem Value="V">Verão</asp:ListItem>
                                        <asp:ListItem Value="I">Inverno</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-3 ">
                                    <label class="control-label">Ano Safra</label>
                                    <asp:DropDownList ID="ddSafra" runat="server" class="btn btn-default dropdown-toggle safra col-lg-12" data-toggle="dropdown" DataSourceID="obSafra" DataTextField="safra" DataValueField="safra"></asp:DropDownList>
                                    <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="form-group">
                                <br />
                                <div class="col-lg-12" style="margin-top: 20px; margin-bottom: 20px;">
                                    <asp:Button ID="btPes" runat="server" Text="Pesquisar" OnClick="btPes_Click" class="btn btn-theme" />
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-panel col-md-12">
                    <ul class="nav nav-tabs">
                        <li class="active"><a data-toggle="tab" href="#data">Dados Detalhados</a></li>
                        <li><a data-toggle="tab" href="#graph">Dados Gráficos</a></li>
                    </ul>
                    <div class="tab-content">
                        <div id="data" class="tab-pane fade in active col-md-12">
                            <div class="form-group">&nbsp;</div>
                            <div class="form-group ">
                                <button runat="server" id="btExcel" class="btn btn-success" onserverclick="ExportToExcel">Exportar para Excel </button>
                            </div>
                            <div style="overflow-x: auto; width: 95%">
                                <asp:GridView ID="gvDetail" runat="server" class="table table-bordered table-striped table-condensed table-hover table-responsive" EmptyDataText="Nenhum Dado Encontrado" PageSize="5" OnRowDataBound="OnRowDataBound" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" />
                                        <asp:BoundField DataField="NroContratoMae" HeaderText="NroContratoMae" />
                                        <asp:BoundField DataField="NroContratoBase" HeaderText="NroContratoBase" />
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                        <asp:BoundField DataField="Cooperante" HeaderText="Cooperante" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="DataContato" HeaderText="DataContato" />
                                        <asp:BoundField DataField="HistoricoContato" HeaderText="HistoricoContato" />
                                        <asp:BoundField DataField="Documento" HeaderText="Documento" />
                                        <asp:BoundField DataField="DataContrato" HeaderText="DataContrato" />
                                        <asp:BoundField DataField="Unidade" HeaderText="Unidade" />
                                        <asp:BoundField DataField="Safra" HeaderText="Safra" />
                                        <asp:BoundField DataField="Ano" HeaderText="Ano" />
                                        <asp:BoundField DataField="Fazenda" HeaderText="Fazenda" />
                                        <asp:BoundField DataField="Desagio" HeaderText="Desagio" />
                                        <asp:BoundField DataField="AreaContratada" HeaderText="AreaContratada" />
                                        <asp:BoundField DataField="Teto" HeaderText="Teto" />
                                        <asp:BoundField DataField="DtIniPlantio" HeaderText="DtIniPlantio" />
                                        <asp:BoundField DataField="DtFimPlantio" HeaderText="DtFimPlantio" />
                                        <asp:BoundField DataField="urgente" HeaderText="Urgente" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div id="graph" class="tab-pane fade">
                            <div class="col-md-12 centered">
                                <div id="divGraph" class="centered"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ObjectDataSource ID="obKPI" runat="server" SelectMethod="selectKPI" TypeName="apoio_monsanto.coopcom">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddTipoCont" Name="tipo" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddUnidade" Name="unidade" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="txDc" Name="document" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddEtapa" Name="etapa" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="txNam" Name="nome" PropertyName="Text" Type="String" />
                    <asp:SessionParameter Name="unilimits" SessionField="coopunidade" Type="String" />
                    <asp:ControlParameter ControlID="ddCanc" Name="cancelados" PropertyName="SelectedValue" Type="Boolean" />
                    <asp:ControlParameter ControlID="ddTpSafra" Name="tpsafra" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddSafra" Name="safra" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="obUnidades" runat="server" SelectMethod="selectAllUnidades" TypeName="apoio_monsanto.coopcom">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="all" Type="Boolean" />
                    <asp:SessionParameter DefaultValue="" Name="unidades" SessionField="coopunidade" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="obEtapas" runat="server" SelectMethod="selectAllEtapas" TypeName="apoio_monsanto.coopcom">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="all" Type="Boolean" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </section>
    </section>
    <asp:ObjectDataSource ID="obGR" runat="server" SelectMethod="selectGRReport" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obRTV" runat="server" SelectMethod="selectRTVReport" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $j = jQuery;

        let totFolha = <%=JavaScript.Serialize(this.totFolha) %> ;
        let totAnal = <%=JavaScript.Serialize(this.totAnal) %> ;
        let totReceb = <%=JavaScript.Serialize(this.totReceb) %>  ;
        let totEnv = <%=JavaScript.Serialize(this.totEnv) %>  ;
        let totRes = <%=JavaScript.Serialize(this.totRes) %>  ;
        let totMon = <%=JavaScript.Serialize(this.totMon) %>  ;
        let totApr = <%=JavaScript.Serialize(this.totApr) %>  ;

        let percFolha = <%=JavaScript.Serialize(this.percFolha) %> ;
        let percAnal = <%=JavaScript.Serialize(this.percAnal) %> ;
        let percReceb = <%=JavaScript.Serialize(this.percReceb) %>  ;
        let percEnv = <%=JavaScript.Serialize(this.percEnv) %>  ;
        let percRes = <%=JavaScript.Serialize(this.percRes) %>  ;
        let percMon = <%=JavaScript.Serialize(this.percMon) %>  ;
        let percApr = <%=JavaScript.Serialize(this.percApr) %>  ;
        let safra = <%=JavaScript.Serialize(this.labelSafra) %>  ;
        let anoSafra = <%=JavaScript.Serialize(this.labelAnoSafra) %>  ;

        $j('#divGraph').highcharts({
            credits: {
                enabled: false
            },
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            title: {
                text: `Gráfico de Totais por Status (<b>${safra} - ${anoSafra}</b>)`
            },
            plotOptions: {
                pie: {
                    allowPointSelect: false,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name} ({point.x})</b>: {point.y} %',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    }
                }
            },
            tooltip: {
                valueSuffix: '%'
            },
            series: [{
                name: 'Status do Processo',
                colorByPoint: true,
                data: [{
                    name: 'Recebimento da Folha de Rosto',
                    x: totFolha,
                    y: percFolha,
                    color: '#fff68f'
                }, {
                    name: 'Análise e Elaboração do Contrato',
                    x: totAnal,
                    y: percAnal
                }, {
                    name: 'Enviado para Assinatura do Cooperante',
                    x: totEnv,
                    y: percEnv,
                    color: '#ff0000'
                }, {
                    name: 'Aprovado Com Ressalvas',
                    x: totRes,
                    y: percRes,
                    color: '#FFFF00'
                }, {
                    name: 'Recebimento de Via Física',
                    x: totReceb,
                    y: percReceb,
                    color: '#ff69b4'
                }, {
                    name: 'Enviado a Monsanto',
                    x: totMon,
                    y: percMon,
                    color: '#0000FF'
                }, {
                    name: 'Aprovado',
                    x: totApr,
                    y: percApr,
                    color: '#008000'
                }]
            }]
        });

    </script>

    <script>
        function mascaraMutuario(o, f) {
            v_obj = o
            v_fun = f
            setTimeout('execmascara()', 5)
        }

        function execmascara() {
            v_obj.value = v_fun(v_obj.value)
        }

        function cpfCnpj(v) {

            //Remove tudo o que não é dígito
            v = v.replace(/\D/g, "")

            if (v.length <= 14) { //CPF

                //Coloca um ponto entre o terceiro e o quarto dígitos
                v = v.replace(/(\d{3})(\d)/, "$1.$2")

                //Coloca um ponto entre o terceiro e o quarto dígitos
                //de novo (para o segundo bloco de números)
                v = v.replace(/(\d{3})(\d)/, "$1.$2")

                //Coloca um hífen entre o terceiro e o quarto dígitos
                v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2")

            } else { //CNPJ

                //Coloca ponto entre o segundo e o terceiro dígitos
                v = v.replace(/^(\d{2})(\d)/, "$1.$2")

                //Coloca ponto entre o quinto e o sexto dígitos
                v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3")

                //Coloca uma barra entre o oitavo e o nono dígitos
                v = v.replace(/\.(\d{3})(\d)/, ".$1/$2")

                //Coloca um hífen depois do bloco de quatro dígitos
                v = v.replace(/(\d{4})(\d)/, "$1-$2")

            }

            return v

        }
    </script>
    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                try {

                    var ctx = { worksheet: "report" || 'Worksheet', table: table.innerHTML }
                    var blob = new Blob([format(template, ctx)]);
                    var blobURL = window.URL.createObjectURL(blob);

                    if (ifIE()) {
                        csvData = table.innerHTML;
                        if (window.navigator.msSaveBlob) {
                            var blob = new Blob([format(template, ctx)], {
                                type: "text/html"
                            });
                            navigator.msSaveBlob(blob, '' + name + '.xls');
                        }
                    }
                    else
                        window.location.href = uri + base64(format(template, ctx))
                } catch (ex) {
                    console.log("Erro: ", ex);
                }
            }
        })()

        function ifIE() {
            var isIE11 = navigator.userAgent.indexOf(".NET CLR") > -1;
            var isIE11orLess = isIE11 || navigator.appVersion.indexOf("MSIE") != -1;
            return isIE11orLess;
        }
    </script>
</asp:Content>
