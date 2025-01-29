﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="motivoreprova.aspx.cs" Inherits="apoio_monsanto.motivoreprova" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Motivos de Reprovação - Apoio Monsanto</title>
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
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Operacional&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Motivos de Reprovação</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">Motivos de Reprovação</label>
                                    <asp:TextBox ID="txCrite" runat="server" class="form-control" placeholder="Motivos de Reprovação"></asp:TextBox>
                                </div>
                                <div class="col-lg-3" style="display: NONE;">
                                    <label class="control-label">Exclusivo GLA</label>
                                    <asp:CheckBox ID="userGLA" runat="server" Text="" TextAlign="Left" Checked="False" class="switch-left switch-animate" />
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
                            <asp:GridView ID="gvMotivo" runat="server" class="table table-bordered table-striped table-condensed" AutoGenerateColumns="False" DataSourceID="obMotivo" EmptyDataText="Nenhum Motivo Encontrado">
                                <Columns>
                                    <asp:BoundField DataField="motivo" HeaderText="Motivo" />
                                    <asp:TemplateField HeaderText="Perfil">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# retPerfil() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ações">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# getAction() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obMotivo" runat="server" SelectMethod="selectAllMotivo" TypeName="apoio_monsanto.commom">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="" Name="type" Type="String" />
                                </SelectParameters>
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
