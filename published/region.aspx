<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="region.aspx.cs" Inherits="apoio_monsanto.region" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>REGIONAL - Apoio Monsanto</title>
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
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Operacional&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Regionais</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">Regionais</label>
                                    <asp:TextBox ID="txCrite" runat="server" class="form-control" placeholder="Regionais"></asp:TextBox>
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
                            <asp:GridView ID="gvregion" runat="server" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False" DataSourceID="obregion" EmptyDataText="Nenhum Critério Encontrado">
                                <Columns>
                                    <asp:BoundField DataField="region" HeaderText="Regional" />
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getAct() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obregion" runat="server" SelectMethod="selectAllRegion" TypeName="apoio_monsanto.commom">
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
