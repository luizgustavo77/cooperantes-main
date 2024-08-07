<%@ Page Title="" Language="C#" MasterPageFile="~/gla.Master" AutoEventWireup="true" CodeBehind="general.aspx.cs" Inherits="apoio_monsanto.records.general" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>GERAIS - Apoio Monsanto</title>
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
                                    <asp:DropDownList ID="ddTypes" runat="server" CssClass="btn btn-default dropdown-toggle">
                                        <asp:ListItem Value="cy">CY</asp:ListItem>
                                        <asp:ListItem Value="co">Contratante</asp:ListItem>
                                        <asp:ListItem Value="re">Regional</asp:ListItem>
                                        <asp:ListItem Value="un">Unidade</asp:ListItem>
                                    </asp:DropDownList>
                            <asp:GridView ID="gvGeneral" CssClass="table table-bordered table-striped table-condensed" runat="server" DataSourceID="obGeneral">
                                <Columns>
                                    <asp:BoundField DataField="gla_cad_type" HeaderText="Tipo de Cadastro" />
                                    <asp:BoundField DataField="gla_cad_value" HeaderText="Valor do Cadastro" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getAct() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            
                                </div>
                                <div class="col-lg-9">
                                    <label class="control-label">Valor para Cadastro</label>
                                    <asp:TextBox ID="txValue" runat="server" class="form-control" placeholder="Valor"></asp:TextBox>
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
                            
                        </div>
                        <asp:ObjectDataSource ID="obGeneral" runat="server" SelectMethod="selectAllGeneral" TypeName="apoio_monsanto.glamom"></asp:ObjectDataSource>
                    </div>
                </div>
            </div>
            <!-- /row -->
        </section>
    </section>
</asp:Content>
