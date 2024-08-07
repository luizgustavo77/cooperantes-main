<%@ Page Title="" Language="C#" MasterPageFile="~/gla.Master" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="apoio_monsanto.gla.report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RELATÓRIO - Efetividade</title>
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
                                <div class="col-lg-9">
                                    <label class="control-label">Nome</label>
                                    <asp:TextBox ID="txNome" class="form-control" name="txNome" runat="server" placeholder="Nome do Cliente"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">SAP</label>
                                    <asp:TextBox ID="txSap" class="form-control" name="txSap" runat="server" placeholder="SAP do Cliente"></asp:TextBox>
                                </div>
                            </div>
                            <div class="cform-group">
                                <div class="col-lg-4">
                                    <label class="control-label">CY</label>
                                    <asp:DropDownList ID="ddCY" runat="server" class="btn btn-default dropdown-toggle safra col-lg-12" data-toggle="dropdown">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">Cultura</label><br />
                                    <asp:DropDownList ID="ddCultura" runat="server" class="btn btn-default dropdown-toggle col-lg-12" data-toggle="dropdown" AutoPostBack="false">
                                        <asp:ListItem Value="">- Todas -</asp:ListItem>
                                        <asp:ListItem Value="MILHO E SORGO">MILHO E SORGO</asp:ListItem>
                                        <asp:ListItem Value="CROP">CROP</asp:ListItem>
                                        <asp:ListItem Value="SOJA">SOJA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">Tipo de Contrato</label><br />
                                    <asp:DropDownList ID="ddTpContrato" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="false">
                                        <asp:ListItem Value="">- Todos -</asp:ListItem>
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
                <div class="form-panel col-lg-12">
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Dados Detalhados</span>
                    </div>
                    <br />
                    <div class="form-group ">
                        <button id="btExcels" class="btn btn-success" runat="server" onserverclick="ExportToExcel">Exportar para Excel&nbsp;<i class="fa fa-file-excel-o"></i></button>
                    </div>
                    <div class="form-group" id="divDetail">
                        <asp:GridView ID="gvDetail" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="obDetail" class="table table-bordered table-striped table-condensed" EmptyDataText="Nenhum Dado Encontrado">
                        </asp:GridView>
                    </div>
                </div>

            </div>
            <asp:ObjectDataSource ID="obDetail" runat="server" SelectMethod="reportGLA" TypeName="apoio_monsanto.glamom">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txNome" Name="name" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="txSap" Name="sap" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddCY" Name="cy" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddCultura" Name="cultura" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddTpContrato" Name="tipo_contrato" PropertyName="SelectedValue" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>

        </section>
    </section>
</asp:Content>
