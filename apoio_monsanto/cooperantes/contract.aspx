<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="contract.aspx.cs" Inherits="apoio_monsanto.cooperantes.contract" EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CONTRATOS - Apoio Monsanto</title>
    <script>
        function clickTo(id) {
            document.getElementById("<%=hidden.ClientID%>").value = id;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <!--main content start-->
    <section id="container">
        <!--main content start-->
        <section id="main-content">
            <section class="wrapper">
                <!-- INLINE FORM ELELEMNTS -->
                <div class="col-lg-12">
                    <div class="form-panel">
                        <div class="navbar-fixed-bottom">
                            <div class="centered text-center">
                                <label id="error" class="alert alert-danger" runat="server" visible="false"></label>
                            </div>
                        </div>
                        <div class="header-gen">
                            <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Contratos</span>
                        </div>
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Nome do Cliente</label>
                                    <asp:TextBox ID="txNam" class="form-control" runat="server" onkeyup="copydata()" placeholder="Digite o Nome do Cliente"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">CPF / CNPJ do Cliente</label>
                                    <asp:TextBox ID="txDc" class="form-control" runat="server" onkeypress="copydata()" placeholder="Digite o CPF ou CNPJ do Cliente (Somente Números)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Contrato Mãe</label>
                                    <asp:TextBox ID="txConMae" class="form-control" runat="server" onkeypress="copydata()" placeholder="Número do Contrato Mãe"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Contrato Base</label>
                                    <asp:TextBox ID="txConBase" class="form-control" runat="server" onkeypress="copydata()" placeholder="Número do Contrato Base"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-xs-12">
                            <div>&nbsp;</div>
                            <div class="form-group">
                                <button type="submit" id="btPesq" class="btn btn-theme" runat="server" onserverclick="btPes_Click">Pesquisar</button>

                            </div>
                        </div>
                        <div class="form-group" id="divCus" runat="server">
                            <asp:GridView ID="gvCustomers" class="table table-bordered table-striped table-condensed" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="obCustomers" AutoGenerateColumns="False" EmptyDataText="Nenhum registro encontrado">
                                <Columns>
                                    <asp:TemplateField HeaderText="Nome do Cliente">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("name").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="document" HeaderText="Documento do Cliente" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# getAction() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obCustomers" runat="server" SelectMethod="searcCustomers" TypeName="apoio_monsanto.coopcom">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txNam" Name="name" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txDc" Name="document" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="hidden" Name="id" PropertyName="Text" Type="String" />
                                    <asp:SessionParameter Name="gla" SessionField="typeGLA" Type="String" />
                                    <asp:ControlParameter ControlID="txConMae" Name="ContratoMae" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txConBase" Name="ContratoBase" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                        <div class="form-group" id="divCont" runat="server" visible="false">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Contratos&nbsp;&nbsp;
                            <asp:DropDownList ID="ddSafra" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obSafra" DataTextField="safra" DataValueField="safra" AutoPostBack="True">
                            </asp:DropDownList>
                                    <asp:DropDownList ID="ddTpSafra" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="True">
                                        <asp:ListItem Value="">- Safra -</asp:ListItem>
                                        <asp:ListItem Value="V">Verão</asp:ListItem>
                                        <asp:ListItem Value="I">Inverno</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddStatus" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obFlux" DataTextField="NAME" DataValueField="ID" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="obFlux" SelectMethod="selectFlux" TypeName="apoio_monsanto.coopcom" runat="server"></asp:ObjectDataSource>
                            </div>
                            <div class="form-group" id="divGv" runat="server" visible="false">
                                <asp:GridView ID="gvContracts" runat="server" DataSourceID="obContract" EmptyDataText="Aguardando Consulta" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="num_cont_mae" HeaderText="Número do Contrato Mãe" />
                                        <asp:BoundField DataField="num_cont_base" HeaderText="Número do Contrato Base" />
                                        <asp:TemplateField HeaderText="Tipo de Contrato">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# getNameCont() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Etapa do Fluxo">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# getEtapaCont() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="safra" HeaderText="Ano" />
                                        <asp:TemplateField HeaderText="Safra">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# getSafraCont() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ações">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# getActionCont() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="obContract" runat="server" SelectMethod="selectContractByClient" TypeName="apoio_monsanto.coopcom">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="id_coop" QueryStringField="view" Type="String" />
                                        <asp:ControlParameter ControlID="ddSafra" Name="safra" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="ddTpSafra" Name="tpsafra" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="ddStatus" Name="etapa" PropertyName="SelectedValue" Type="String" />
                                        <asp:SessionParameter Name="unidade" SessionField="coopunidade" Type="String" />
                                        <asp:ControlParameter ControlID="txConMae" Name="ContratoMae" PropertyName="Text" Type="String" />
                                        <asp:ControlParameter ControlID="txConBase" Name="ContratoBase" PropertyName="Text" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>

                            </div>
                        </div>
                    </div>
                    <!-- Modal -->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabdashboard="-1" id="modTipo" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title title">TIPO DE CONTRATO - Apoio Monsanto</h4>
                                </div>
                                <div class="modal-body" id="modal_body" runat="server">
                                    <p>Selecione o Tipo de Contrato</p>
                                    <asp:DropDownList ID="ddTipoCont" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obTipos" DataTextField="name" DataValueField="id_process">
                                    </asp:DropDownList>
                                    <asp:ObjectDataSource ID="obTipos" runat="server" SelectMethod="selectTipoProcesso" TypeName="apoio_monsanto.coopcom">
                                        <SelectParameters>
                                            <asp:Parameter Name="id_process" Type="String" />
                                            <asp:Parameter DefaultValue="S" Name="ativo" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>

                                <div class="modal-footer">
                                    <button class="btn btn-theme" type="button" onserverclick="btNew_Cont_Click" runat="server">OK</button>
                                    <button data-dismiss="modal" class="btn btn-default" type="button">Cancelar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- modal -->
            </section>
            <! --/wrapper -->
            <asp:TextBox ID="hidden" runat="server" CssClass="hidden"></asp:TextBox>
        </section>
        <script src="//code.jquery.com/jquery-1.10.2.js"></script>
        <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
        <!-- /MAIN CONTENT -->
        <script type="text/javascript">
            jQuery.noConflict();
            var $j = jQuery;

            $j(document).ready(function () {

                $j('.confirmation').on('click', function () {
                    return confirm('Deseja deletar o contrato?');
                });

            });
        </script>

    </section>
    </span>
</asp:Content>
