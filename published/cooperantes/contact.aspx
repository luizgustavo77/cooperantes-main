<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="apoio_monsanto.contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CONTATOS - Apoio Monsanto</title>
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
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Contatos</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">Descrição do Contato</label>
                                    <asp:TextBox ID="txCrite" runat="server" class="form-control" placeholder="Descrição do Contato"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div>&nbsp;</div>
                                <div class="form-group">
                                    <button type="submit" id="btPesq" class="btn btn-theme" runat="server">Pesquisar</button>
                                    <asp:Button ID="btInc" runat="server" Text="Incluir" OnClick="btInc_Click" class="btn btn-success" />
                                    <asp:Button ID="btUpd" runat="server" Text="Atualizar" OnClick="btUpd_Click" class="btn btn-warning" visible="false"/>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:GridView ID="gvCriteria" runat="server" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False" DataSourceID="obCriteria" EmptyDataText="Nenhum Contato Encontrado">
                                <Columns>
                                    <asp:BoundField DataField="description" HeaderText="Descrição do Contato" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getAct() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obCriteria" runat="server" SelectMethod="selectContactPrev" TypeName="apoio_monsanto.coopcom">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="id_contact" Name="idContact" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <!-- /form-panel -->
                </div>
            </div>
            <asp:Label ID="id_contact" runat="server" Text="" Visible="true"></asp:Label>
            <!-- /row -->
        </section>
    </section>
</asp:Content>
