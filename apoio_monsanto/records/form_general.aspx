<%@ Page Title="" Language="C#" MasterPageFile="~/gla.Master" AutoEventWireup="true" CodeBehind="form_general.aspx.cs" Inherits="apoio_monsanto.records.form_general" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CADASTROS - Apoio Monsanto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="col-lg-12">
                <div class="form-panel col-lg-12">
                    <div class="navbar-fixed-bottom">
                        <div class="centered text-center">
                            <label id="error" class="alert alert-danger" runat="server" visible="false"></label>
                        </div>
                    </div>
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros Gerais</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Tipo de Cadastro</label><br />
                                    <asp:DropDownList ID="ddType" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">-- Selecione --</asp:ListItem>
                                        <asp:ListItem Value="cy">CY</asp:ListItem>
                                        <asp:ListItem Value="ma">Marca</asp:ListItem>
                                        <asp:ListItem Value="re">Regional</asp:ListItem>
                                        <asp:ListItem Value="un">Unidade</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-9">
                                    <label class="control-label">Valor do Cadastro</label><br />
                                    <asp:TextBox ID="txValue" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div>&nbsp;</div>
                                <div class="form-group">
                                    <button type="submit" id="btPesq" class="btn btn-theme" runat="server">Pesquisar</button>
                                    <asp:Button ID="btInc" runat="server" Text="Incluir" OnClick="btInc_Click" class="btn btn-success" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:GridView ID="gvCadastro" runat="server" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False" DataSourceID="obGen" EmptyDataText="Nenhum Cadastro Encontrado">
                                <Columns>
                                    <asp:BoundField DataField="gla_cad_type" HeaderText="Tipo de Cadastro" />
                                    <asp:BoundField DataField="gla_cad_value" HeaderText="Valor do Cadastro" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getAction() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obGen" runat="server" SelectMethod="selectAllGeneral" TypeName="apoio_monsanto.glamom">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddType" Name="gla_cad_type" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="txValue" Name="gla_cad_value" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="obsafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.commom">
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <!-- /form-panel -->
                </div>
            </div>
            <!-- /row -->
        </section>
    </section>
</asp:Content>
