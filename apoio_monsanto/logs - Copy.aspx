<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="logs.aspx.cs" Inherits="apoio_monsanto.logs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CONSULTA LOG - Apoio Monsanto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="col-lg-12">
                    <div class="form-panel col-lg-12">
                        <h4 class="mb"><i class="fa fa-angle-right"></i>Operacional <i class="fa fa-angle-right"></i>Consulta LOGs - Contratos</h4>
                        <div class="form-group">
                            <div class="form-group">
                                <div class="form-group">
                                    <div class="col-lg-4">
                                        <label class="control-label">Usuário</label>
                                        <asp:TextBox ID="txNam" class="form-control" runat="server" placeholder="Usuário que realizou alteração"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-lg-4">
                                        <label class="control-label">Contrato</label>
                                        <asp:TextBox ID="txNum" class="form-control" runat="server" placeholder="Contrato"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <div class="col-lg-4">
                                        <label class="control-label">CPF / CNPJ do Cliente</label>
                                        <asp:TextBox ID="txDc" class="form-control" runat="server" placeholder="CPF/CNPJ (Somente Números)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div>&nbsp;</div>
                                <div class="form-group">
                                    <button type="submit" id="btPesq" class="btn btn-theme" runat="server" >Pesquisar</button>
                                   
                                </div>
                                <div class="form-group">
                                    <label class="text-center alert-danger centered" runat="server" id="error"></label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            
                            <asp:GridView ID="gvLogs" runat="server" DataSourceID="obLogs" class="table table-bordered table-striped table-condensed" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="Nenhum Log Encontrado">
                                <Columns>
                                    <asp:BoundField DataField="PK" HeaderText="Número do Contrato" />
                                    <asp:BoundField DataField="FieldName" HeaderText="Nome do Campo Alterado" />
                                    <asp:BoundField DataField="OldValue" HeaderText="Valor Antigo" />
                                    <asp:BoundField DataField="NewValue" HeaderText="Valor Novo" />
                                    <asp:BoundField DataField="UpdateDate" HeaderText="Data da Atualização" />
                                    <asp:BoundField DataField="UserName" HeaderText="Usuário" />
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="obLogs" runat="server" SelectMethod="selectLog" TypeName="apoio_monsanto.commom">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txNum" Name="reference" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txDc" Name="doccli" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txNam" Name="user" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            
                        </div>
                    </div>
                    <!-- /form-panel -->
                </div>
            <!-- /row -->
        </section>
    </section>
</asp:Content>
