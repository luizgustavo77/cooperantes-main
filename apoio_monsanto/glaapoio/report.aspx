<%@ Page Title="" Language="C#" MasterPageFile="~/newgla.Master" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="apoio_monsanto.glaapoio.report" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RELATÓRIO - GLA</title>
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
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label class="control-label">Nome</label>
                                        <asp:TextBox ID="nome" class="form-control" name="txNome" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="control-label">CNPJ/CPF</label>
                                        <asp:TextBox ID="documento" class="form-control" name="txNome" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Unidade</label>
                                        <asp:DropDownList ID="ddUnidade" runat="server" class="btn btn-default dropdown-toggle gr col-sm-12" data-toggle="dropdown">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Regional</label>
                                        <asp:DropDownList ID="ddRegional" runat="server" class="btn btn-default dropdown-toggle gr col-sm-12" data-toggle="dropdown">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label class="control-label">Data do Recebimento</label>
                                        <asp:TextBox ID="data_recebimento" class="form-control dtdigit" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="control-label">Data do Contrato</label>
                                        <asp:TextBox ID="data_contrato" class="form-control dtdigit" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <label class="control-label">Número de Caixa</label>
                                        <asp:TextBox ID="caixa" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Status</label><br />
                                        <asp:DropDownList ID="ddStatus" runat="server" class="btn btn-default dropdown-toggle col-sm-12" data-toggle="dropdown" AutoPostBack="false">
                                            <asp:ListItem Value="">- Selecione -</asp:ListItem>
                                            <asp:ListItem Value="A">Aprovado</asp:ListItem>
                                            <asp:ListItem Value="R">Reprovado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12" style="margin-top: 20px; margin-bottom: 20px;">
                                        <asp:Button ID="btPes" runat="server" Text="Pesquisar" OnClick="btPes_Click" class="btn btn-theme" />
                                    </div>
                                </div>
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
            <asp:ObjectDataSource ID="obDetail" runat="server" SelectMethod="reportNEWGLA" TypeName="apoio_monsanto.newmom">
                <SelectParameters>
                    <asp:ControlParameter ControlID="nome" Name="name" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="documento" Name="document" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddUnidade" Name="unidade" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="ddUnidade" Name="regional" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="data_recebimento" Name="data_recebimento" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="data_contrato" Name="data_contrato" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddStatus" Name="status" PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="caixa" Name="caixa" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>

        </section>
    </section>
</asp:Content>
