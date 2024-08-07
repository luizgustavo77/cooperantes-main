<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="type_process.aspx.cs" Inherits="apoio_monsanto.cooperantes.type_process" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TIPO DE PROCESSO - Apoio Monsanto</title>
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
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Tipos de Processo</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-9">
                                    <label class="control-label">Nome do Processo</label>
                                    <asp:TextBox ID="txTipo" runat="server" class="form-control" placeholder="Nome do Processo"></asp:TextBox>
                                </div>
                                <div class="col-lg-3">
                                    <label class="control-label">Ativo?</label><br />
                                    <asp:DropDownList ID="ddActive" runat="server" CssClass="btn btn-default dropdown-toggle">
                                        <asp:ListItem Value="">-- Selecione --</asp:ListItem>
                                        <asp:ListItem Value="S">Sim</asp:ListItem>
                                        <asp:ListItem Value="N">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div>&nbsp;</div>
                                <div class="form-group">
                                    <button type="submit" id="btCanc" class="btn btn-theme" runat="server" onserverclick="btCanc_Click">Cancelar</button>
                                    <asp:Button ID="btInc" runat="server" Text="Incluir" OnClick="btInc_Click" class="btn btn-success" />
                                    <asp:Button ID="btUpd" runat="server" Text="Update" OnClick="btUpd_Click" class="btn btn-warning" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:GridView ID="gvCriteria" runat="server" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False" DataSourceID="obCriteria" EmptyDataText="Nenhum Tipo de Processo Encontrado">
                                <Columns>
                                    <asp:BoundField DataField="name" HeaderText="Tipos de Processo" />
                                    <asp:BoundField DataField="active" HeaderText="Ativo?" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getActs() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obCriteria" runat="server" SelectMethod="selectTipoProcesso" TypeName="apoio_monsanto.coopcom">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="id_process" Name="id_process" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="ddActive" Name="ativo" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <!-- /form-panel -->
                </div>
            </div>
            <asp:Label ID="id_process" runat="server" Text="" Visible="false"></asp:Label>
            <!-- /row -->
        </section>
    </section>
</asp:Content>

