<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="apoio_monsanto.report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RELATÓRIO - Apoio Monsanto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="col-sm-12">
                <div class="form-panel col-sm-12">
                    <div class="header-gen">
                        <span class="mb "><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Relatório</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">RTV Responsável</label><div></div>
                                    <asp:DropDownList ID="ddRTV" runat="server" class="btn btn-default dropdown-toggle rtv col-lg-10" data-toggle="dropdown" DataSourceID="obRTV" DataTextField="name" DataValueField="name"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">GR Responsável</label><div></div>
                                    <asp:DropDownList ID="ddGR" runat="server" class="btn btn-default dropdown-toggle gr col-lg-10" data-toggle="dropdown" DataSourceID="obGR" DataTextField="name" DataValueField="name"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">CPF / CNPJ do Cliente</label>
                                    <asp:TextBox ID="txDc" class="form-control" name='cpfCnpj' runat="server" onkeypress='mascaraMutuario(this,cpfCnpj)' onblur='clearTimeout()' placeholder="Digite o CPF ou CNPJ do Cliente" MaxLength="18"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3 ">
                                    <label class="control-label">Regional</label><div></div>
                                    <asp:DropDownList ID="ddRegional" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                        <asp:ListItem Value="">Todos</asp:ListItem>
                                        <asp:ListItem Value="BCERL">BCERL</asp:ListItem>
                                        <asp:ListItem Value="BCERO">BCERO</asp:ListItem>
                                        <asp:ListItem Value="BPRNA">BPRNA</asp:ListItem>
                                        <asp:ListItem Value="BRSSC">BRSSC</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Tipo de Contrato</label><div></div>
                                    <asp:DropDownList ID="ddTipoCont" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="false" onclick="getDisabled();">
                                        <asp:ListItem Value="">Todos</asp:ListItem>
                                        <asp:ListItem Value="1">Distribuidor</asp:ListItem>
                                        <asp:ListItem Value="2">POD</asp:ListItem>
                                        <asp:ListItem Value="3">Produtor de Sementes</asp:ListItem>
                                        <asp:ListItem Value="4">Monsoy</asp:ListItem>
                                        <asp:ListItem Value="5">Comissões</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Tipo de Documento</label>
                                    <asp:DropDownList ID="ddTpDoc" runat="server" class="btn btn-default dropdown-toggle form-control" data-toggle="dropdown" AutoPostBack="false">
                                        <asp:ListItem Value="1">Contrato de Licenciamento de Tecnologia Intacta RR2 PRO</asp:ListItem>
                                        <asp:ListItem Value="2">Contrato de Prestação de Serviços e outras avenças</asp:ListItem>
                                        <asp:ListItem Value="3">Termo de Licenciamento</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Safra</label><div></div>
                                    <asp:DropDownList ID="ddSafra" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obSafra" DataTextField="safra" DataValueField="safra"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Tipo de Termo</label><div></div>
                                    <asp:DropDownList ID="ddTpTermo" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                                        <asp:ListItem Value="">Todos</asp:ListItem>
                                        <asp:ListItem Value="ta">Termo Aditivo</asp:ListItem>
                                        <asp:ListItem Value="at">Aditivo ao Termo</asp:ListItem>
                                        <asp:ListItem Value="tx">Termo Aditivo - Taxa de Serviço</asp:ListItem>
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


                <div class="form-panel col-sm-12">
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Gráfico</span>
                    </div>
                    <div class="form-group col-sm-12">
                        <div id="divGraph"></div>
                    </div>
                </div>
                <div class="form-panel col-sm-12">
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Dados Detalhados</span>
                    </div>
                    <br />
                    <div class="form-group ">
                        <button id="btExcels" class="btn btn-success" runat="server" onserverclick="ExportToExcel">Exportar para Excel&nbsp;<i class="fa fa-file-excel-o"></i></button>
                    </div>
                    <div class="form-group" id="divDetail" style="overflow-x: auto; width: 98%">
                        <asp:GridView ID="gvDetail" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="obDetail" class="table table-bordered table-striped table-condensed" EmptyDataText="Nenhum Dado Encontrado">
                        </asp:GridView>
                    </div>
                </div>

            </div>
            <asp:ObjectDataSource ID="obDetail" runat="server" SelectMethod="selectDetailAll" TypeName="apoio_monsanto.commom">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddGR" Name="gr" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddRTV" Name="rtv" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddSafra" Name="safra" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddRegional" Name="regional" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="txDc" Name="document" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddTipoCont" Name="tpCont" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddTpDoc" Name="tpDoc" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddTpTermo" Name="tpTermo" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>

        </section>
    </section>
    <asp:ObjectDataSource ID="obGR" runat="server" SelectMethod="selectGRReport" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="obRTV" runat="server" SelectMethod="selectRTVReport" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="http://code.highcharts.com/highcharts.js"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $j = jQuery;

        function getDisabled() {
            if ($j('#<%= ddTipoCont.ClientID %>').val() != 3)
                $j('#<%= ddTpDoc.ClientID %>').prop('disabled', true);
            else
                $j('#<%= ddTpDoc.ClientID %>').prop('disabled', false);
        }

        $j(document).ready(function () {
            getDisabled();

            var categorie = <%=JavaScript.Serialize(this.acategorie) %>
            var aproved = <%=JavaScript.Serialize(this.aaproved) %>
            var reproved = <%=JavaScript.Serialize(this.areproved) %>
            var sent = <%=JavaScript.Serialize(this.asent) %>

            $j('#divGraph').highcharts({
                credits: {
                    enabled: false
                },
                title: {
                    text: 'Relatório de Contratos por Safra'
                },
                xAxis: {
                    categories: categorie
                },
                series: [{
                    type: 'column',
                    name: 'Contratos Aprovados',
                    data: aproved,
                    color: '#33CC33'
                }, {
                    type: 'column',
                    name: 'Contratos Reprovados',
                    data: reproved,
                    color: '#FF0000'
                }, {
                    type: 'spline',
                    name: 'Contratos Enviados',
                    data: sent,
                    color: '#0099FF'
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
