<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="apoio_monsanto._dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DASHBOARD - Apoio Monsanto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <div class="col-md-12 mb">
                <!-- WHITE PANEL - TOP USER -->
                <div class="white-panel pn">
                    <div class="white-header">
                        <h5>Avisos e Informações</h5>
                    </div>
                    <div class="row" id="rowWarning" runat="server">
                        Nenhuma Tarefa Encontrada
                    </div>
                </div>

            </div>
        </section>
    </section>
</asp:Content>
