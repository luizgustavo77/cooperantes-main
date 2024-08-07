<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="apoio_monsanto.cooperantes.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PERFIL - Apoio Monsanto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <div class="col-xs-12">
                <div class="form-panel col-xs-12">
                     <div class="navbar-fixed-bottom">
                        <div class="centered text-center">
                            <label id="error" class="alert alert-danger" runat="server" visible="false"></label>
                        </div>
                    </div>
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Alterar Senha</span>
                    </div>
                    <div class="form-group">
                        <p class="alert alert-warning col-xs-12" id="requisitos" runat="server">
                            <strong>A senha deve:</strong><br />
                            - Ter pelo menos 6 caracteres
                        <br />
                            - Ter pelo menos 1 número
                        <br />
                            - Ter pelo menos 1 letra maiúscula
                        <br />
                        </p>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">Nova Senha</label>
                                    <asp:TextBox ID="txPass" class="form-control" runat="server" placeholder="Digite sua nova senha" required TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-4">
                                    <label class="control-label">Confirme a sua nova senha</label>
                                    <asp:TextBox ID="txCPass" class="form-control" runat="server" placeholder="Confirme sua nova senha" required TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <div>&nbsp;</div>
                            <div class="form-group">
                                <button type="submit" id="btGravar" class="btn btn-theme" runat="server" onserverclick="btReg_Click">Alterar Senha</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /form-panel -->
            </div>
            <!-- /row -->
        </section>
    </section>
</asp:Content>
