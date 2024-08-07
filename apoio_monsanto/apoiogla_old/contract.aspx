<%@ Page Title="" Language="C#" MasterPageFile="~/gla.Master" AutoEventWireup="true" CodeBehind="contract.aspx.cs" Inherits="apoio_monsanto.gla.contract" EnableViewState="true" %>

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
                            <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros</span>
                        </div>
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">Nome do Cliente</label>
                                    <asp:TextBox ID="txNam" class="form-control" runat="server" onkeyup="copydata()" placeholder="Digite o Nome do Cliente"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">CPF / CNPJ do Cliente:</label>
                                    <asp:TextBox ID="txDc" class="form-control" runat="server" onkeypress="copydata()" placeholder="Digite o CPF ou CNPJ do Cliente (Somente Números)"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <label class="control-label">Código SAP</label>
                                    <asp:TextBox ID="txSap" class="form-control" runat="server" onkeyup="copydata()" placeholder="SAP"></asp:TextBox>
                                </div>
                                <div class="col-lg-2">
                                    <label class="control-label">Código SAP Filial</label>
                                    <asp:TextBox ID="txSapFilial" class="form-control" runat="server" onkeyup="copydata()" placeholder="SAP Filial"></asp:TextBox>
                                </div>
                                <div class="col-xs-4">
                                    <label class="control-label">Marca</label>
                                    <asp:DropDownList ID="ddMarca" runat="server" class="btn btn-default dropdown-toggle rtv col-xs-12" data-toggle="dropdown">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-4">
                                    <label class="control-label">Unidade</label>
                                    <asp:DropDownList ID="ddUnidade" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-4">
                                    <label class="control-label">Regional</label>
                                    <asp:DropDownList ID="ddRegional" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-4">
                                    <label class="control-label">Tipo de Contrato</label><br />
                                    <asp:DropDownList ID="ddTpContrato" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="false">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="1">Contrato de Distribuição Comercial</asp:ListItem>
                                        <asp:ListItem Value="2">Termo de Participação no Programa de Vendas</asp:ListItem>
                                        <asp:ListItem Value="3">Carta de Quitação</asp:ListItem>
                                        <asp:ListItem Value="4">Contrato ATS</asp:ListItem>
                                        <asp:ListItem Value="5">Contratos Qion Implantação</asp:ListItem>
                                        <asp:ListItem Value="6">Contratos Qion Licenciamento</asp:ListItem>
                                        <asp:ListItem Value="8">FCPA</asp:ListItem>
                                        <asp:ListItem Value="7">Outros (Documentos Diversos)</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-xs-4">
                                    <label class="control-label">CY</label><br />
                                    <asp:DropDownList ID="ddCY" runat="server" class="btn btn-default dropdown-toggle safra col-xs-12" data-toggle="dropdown" AutoPostBack="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-xs-4">
                                    <label class="control-label">Cultura</label><br />
                                    <asp:DropDownList ID="ddCultura" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="false">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="MILHO E SORGO">MILHO E SORGO</asp:ListItem>
                                        <asp:ListItem Value="CROP">CROP</asp:ListItem>
                                        <asp:ListItem Value="SOJA">SOJA</asp:ListItem>
                                    </asp:DropDownList>
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
                                    <asp:BoundField DataField="SAP" HeaderText="SAP" />
                                    <asp:BoundField DataField="SAP_FILIAL" HeaderText="SAP Filial" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# getActions() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obCustomers" runat="server" SelectMethod="selectCustomerAndContract" TypeName="apoio_monsanto.glamom">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txNam" Name="name" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txDc" Name="document" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txSap" Name="sap" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="ddMarca" Name="distrito" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="ddUnidade" Name="unidade" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="ddRegional" Name="regional" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="txSapFilial" Name="sap_filial" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                        <div class="form-group" id="divCont" runat="server" visible="false">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Contratos&nbsp;&nbsp;
                            </div>
                            <div class="form-group" id="divGv" runat="server" visible="false">
                                <asp:GridView ID="gvContracts" runat="server" DataSourceID="obContract" EmptyDataText="Aguardando Consulta" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tipo do Contrato">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# getTypeContract() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cultura" HeaderText="Cultura" />
                                        <asp:BoundField DataField="distrito" HeaderText="Marca" />
                                        <asp:BoundField DataField="cy" HeaderText="CY" />
                                        <asp:BoundField DataField="obs" HeaderText="Nome do Contrato" />
                                        <asp:TemplateField HeaderText="Ações">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# getActionCont() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="obContract" runat="server" SelectMethod="selectContractByClient" TypeName="apoio_monsanto.glamom">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="id_client" QueryStringField="view" Type="String" />
                                        <asp:ControlParameter ControlID="ddTpContrato" Name="tpContrato" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="ddCY" Name="CY" PropertyName="SelectedValue" Type="String" />
                                        <asp:ControlParameter ControlID="ddCultura" Name="cultura" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

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
                                    <asp:DropDownList ID="ddTipoCont" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="false">
                                        <asp:ListItem Value="0">- Selecione - </asp:ListItem>
                                        <asp:ListItem Value="1">Contrato de Distribuição Comercial</asp:ListItem>
                                        <asp:ListItem Value="2">Termo de Participação no Programa de Vendas</asp:ListItem>
                                        <asp:ListItem Value="3">Carta de Quitação</asp:ListItem>
                                        <asp:ListItem Value="4">Contrato ATS</asp:ListItem>
                                        <asp:ListItem Value="5">Contratos Qion Implantação</asp:ListItem>
                                        <asp:ListItem Value="6">Contratos Qion Licenciamento</asp:ListItem>
                                        <asp:ListItem Value="8">FCPA</asp:ListItem>
                                        <asp:ListItem Value="7">Outros (Documentos Diversos)</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="modal-footer">
                                    <button class="btn btn-theme" type="button" onserverclick="btNew_Cont_Click" runat="server">Criar</button>
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
