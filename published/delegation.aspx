<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="delegation.aspx.cs" Inherits="apoio_monsanto.delegation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DELEGAÇÃO DE PERMISSÕES - Apoio Monsanto</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" type="text/css" href="assets/css/datepicker.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="col-lg-12">
                <div class="form-panel col-lg-12">
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Operacional&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Delegação de Permissões</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">Perfil</label>
                                    <asp:DropDownList ID="ddPerfil" runat="server" CssClass="btn btn-default dropdown-toggle form-control">
                                        <asp:ListItem Value="1">Administrador</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <i class="fa fa-calendar-o"></i>
                                    <label class="control-label">Período - De</label>
                                    <asp:TextBox ID="txDe" runat="server" class="datepicker form-control dtrec"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <i class="fa fa-calendar-o"></i>
                                    <label class="control-label">Período - Ate</label>
                                    <asp:TextBox ID="txAte" runat="server" class="datepicker form-control dtrec"></asp:TextBox>
                                </div>
                            </div>
                            <div>&nbsp;</div>
                            <div class="form-group">
                                <div class="col-lg-8">
                                    <label class="control-label">Usuários</label>
                                    <asp:DropDownList ID="ddUsers" runat="server" DataSourceID="obAllUser" DataTextField="name" DataValueField="login" CssClass="btn btn-default dropdown-toggle form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <div>&nbsp;</div>
                            <div class="form-group">
                                <button type="submit" id="btPesq" class="btn btn-theme" runat="server">Pesquisar</button>
                                <asp:Button ID="btInc" runat="server" Text="Incluir" OnClick="btInc_Click" class="btn btn-success" />
                            </div>
                            <div class="form-group">
                                <label class="text-center alert-danger centered" runat="server" id="error"></label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:GridView ID="gvPerm" runat="server" DataSourceID="obPerm" class="table table-bordered table-striped table-condensed" AllowPaging="True" EmptyDataText="Nenhuma Permissão Encontrada" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="id_user" HeaderText="Usuário" />
                                <asp:TemplateField HeaderText="Período - De">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToDateTime(Eval("dt_perm_ini")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Período - Até">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToDateTime(Eval("dt_perm_end")).ToString("dd/MM/yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# getAction() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="obPerm" runat="server" SelectMethod="selectPermission" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="obAllUser" runat="server" SelectMethod="selectAllActiveUsers" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
                    </div>
                </div>
                <!-- /form-panel -->
            </div>
            <!-- /row -->
        </section>
    </section>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript" src="http://vitalets.github.io/bootstrap-datepicker/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $j = jQuery;

        $j(document).ready(function () {

            $j(".datepicker").datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR'
            });
        });
    </script>
</asp:Content>
