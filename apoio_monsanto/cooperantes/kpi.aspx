<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="kpi.aspx.cs" Inherits="apoio_monsanto.cooperantes.kpi" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RELATÓRIO KPI - Apoio Monsanto</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="col-xs-12">
                <div class="form-panel col-xs-12">
                    <div class="header-gen">
                        <span class="mb "><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Relatório KPI</span>
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
                                    <label class="control-label">Tipo de Documento</label><div></div>
                                    <asp:DropDownList ID="ddTipoCont" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obTipos" DataTextField="name" DataValueField="id_process">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="obTipos" runat="server" SelectMethod="selectTipoProcesso" TypeName="apoio_monsanto.coopcom">
                                        <SelectParameters>
                                            <asp:Parameter Name="id_process" Type="String" />
                                            <asp:Parameter DefaultValue="S" Name="ativo" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Unidade</label><div></div>
                                    <asp:DropDownList ID="ddUnidade" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obUnidades" DataTextField="nome" DataValueField="nome"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Etapa (Status)</label><div></div>
                                    <asp:DropDownList ID="ddEtapa" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obEtapas" DataTextField="etapa" DataValueField="id"></asp:DropDownList>
                                </div>
                            </div>
                             <div class="fprm-group">
                                <div class="col-xs-3 ">
                                    <label class="control-label">Safra</label>
                                    <asp:DropDownList ID="ddTpSafra" runat="server" class="btn btn-default dropdown-toggle safra col-xs-12" data-toggle="dropdown">
                                        <asp:ListItem Value="">- Todos -</asp:ListItem>
                                        <asp:ListItem Value="V">Verão</asp:ListItem>
                                        <asp:ListItem Value="I">Inverno</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-6 ">
                                    <label class="control-label">Ano Safra</label>
                                    <asp:DropDownList ID="ddSafra" runat="server" class="btn btn-default dropdown-toggle safra col-xs-12" data-toggle="dropdown" DataSourceID="obSafra" DataTextField="safra" DataValueField="safra"></asp:DropDownList>
                                    <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                </div>
                                 <div class="col-lg-6">
                                    <label class="control-label">Mostra Cancelados?</label><div></div>
                                    <asp:DropDownList ID="ddCanc" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                        <asp:ListItem Value="false">Não</asp:ListItem>
                                        <asp:ListItem Value="true">Sim</asp:ListItem>
                                    </asp:DropDownList>
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
                <div id="tabs" class="form-panel col-md-12">
                    <ul>
                        <li><a href="#kpi">Relatório KPI</a></li>
                        <li><a href="#graphkpi">Dados Gráficos KPI</a></li>
                    </ul>
                    <div id="kpi" class="form-group col-md-12">
                        <div class="form-group ">
                            <button id="btExcels2" class="btn btn-success" runat="server" onserverclick="ExportToExcel2">Exportar para Excel&nbsp;<i class="fa fa-file-excel-o"></i></button>
                        </div>
                        <div style="overflow-x: auto; width: 95%">
                            <asp:GridView ID="gvKPI" runat="server" AllowPaging="True" AllowSorting="True" class="table table-bordered table-striped table-condensed table-hover table-responsive" EmptyDataText="Nenhum Dado Encontrado" PageSize="5" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="CONTRACT" HeaderText="Processo" />
                                    <asp:BoundField DataField="name" HeaderText="Cooperante" />
                                    <asp:BoundField DataField="TIPO_CONTRATO" HeaderText="Tipo de Contrato" />
                                    <asp:BoundField DataField="CONTRATO_MAE" HeaderText="Contrato Mãe" />
                                    <asp:BoundField DataField="CONTRATO_BASE" HeaderText="Contrato Base" />
                                    <asp:BoundField DataField="SAFRA" HeaderText="Safra" />
                                    <asp:BoundField DataField="ANO" HeaderText="Ano" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                    <asp:BoundField DataField="1" HeaderText="Análise da Documentação do Cooperante" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getTime(1) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="2" HeaderText="Elaboração do Contrato" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# getTime(2) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="3" HeaderText="Enviado para Assinatura do Cooperante" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# getTime(3) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="4" HeaderText="Aprovado Com Ressalvas" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# getTime(4) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="5" HeaderText="Recebimento de Via Física" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# getTime(5) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="6" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Enviado a Monsanto" />
                                    <asp:TemplateField HeaderText="Tempo">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# getTime(6) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="7" HeaderText="Aprovado" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:TemplateField HeaderText="Tempo Total">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# getTime(7) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="graphkpi" class="form-group">
                        <div class="col-md-12 centered">
                            <div id="divKpiGraph" class="centered"></div>
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
                    <asp:ControlParameter ControlID="ddCanc" DefaultValue="" Name="cancelados" PropertyName="SelectedValue" Type="Boolean" />
                    <asp:ControlParameter ControlID="ddTpSafra" Name="tpsafra" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddSafra" Name="safra" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="txNam" Name="cooperante" PropertyName="Text" Type="String" />
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
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>

    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $j = jQuery;

        $j(document).ready(function () {

            $j( function() {
                $j( "#tabs" ).tabs();
            } );

            var kpi3 = <%=JavaScript.Serialize(this.kpi3) %>
            var kpi4 = <%=JavaScript.Serialize(this.kpi4) %>
            var kpi14 = <%=JavaScript.Serialize(this.kpi14) %>
            var kpi22 = <%=JavaScript.Serialize(this.kpi22) %>
            var kpi30 = <%=JavaScript.Serialize(this.kpi30) %>
            var kpi31 = <%=JavaScript.Serialize(this.kpi31) %>

             
            $j('#divKpiGraph').highcharts({
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
                    text: 'Gráfico KPI'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.y} ',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series: [{
                    name: 'Performance',
                    colorByPoint: true,
                    data: [{
                        name: '1 - 3 Dias',
                        y: kpi3
                    }, {
                        name: '4 - 7 Dias',
                        y: kpi4,
                    }, {
                        name: '8 - 14 Dias',
                        y: kpi14,
                        color: '#FF0000'
                    }, {
                        name: '15 - 22 Dias',
                        y: kpi22,
                        color: '#FFFF00'
                    }, {
                        name: '23 - 30 Dias',
                        y: kpi30,
                        color: '#0000FF'
                    }, {
                        name: 'Mais de 31 Dias',
                        y: kpi31,
                        color: '#008000'
                    }]
                }]
            });
        })

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
</asp:Content>
