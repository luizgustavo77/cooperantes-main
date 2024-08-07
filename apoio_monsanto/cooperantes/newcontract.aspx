<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="newcontract.aspx.cs" Inherits="apoio_monsanto.cooperantes.newcontract" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>NOVO CONTRATO - Apoio Monsanto</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" type="text/css" href="assets/css/datepicker.css">
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" media="screen">
    <link href="assets/css/bootstrap-datetimepicker.min.css" rel="stylesheet" media="screen">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <!--main content start-->
    <section id="container">
        <!--main content start-->
        <section id="main-content">
            <section class="wrapper">
                <!-- INLINE FORM ELELEMNTS -->
                <div class="navbar-fixed-bottom">
                    <div class="centered text-center">
                        <label id="error" class="alert alert-danger" runat="server" visible="false"></label>
                    </div>
                    <div class="pull-right">
                        <button type="submit" id="btGravar" class="btn btn-success tooltips" data-original-title='Salva Alterações' runat="server" onserverclick="btGrava_Click">Salvar Processo</button>
                        <button type="submit" id="btAcomp" class="btn btn-info tooltips" data-original-title='Finaliza Acompanhamento' runat="server" onserverclick="btAcomp_Click" visible="false">Finalizar Acompanhamento</button>
                        <button type="submit" id="btCanc" class="btn btn-danger tooltips" data-original-title='Sair do Contrato' runat="server" onserverclick="btCanc_Click">Cancelar</button>
                        <asp:Button ID="btRollBacks" runat="server" Text="Voltar Etapa" OnClientClick="if ( !confirm('Deseja Voltar a Etapa?')) return false;" OnClick="btRollBack_Click" Visible="false" CssClass="btn btn-warning" />
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Dados do Cooperante</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div id="divStat" class="centered form-group text-center" runat="server">
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btAnali" runat="server" Text="Análise da Documentação do Cooperante" CssClass="btn btn-warning btn-group-justified flux" OnClick="btAnali_Click" />
                                        <div class="text-center">
                                            <asp:Label ID="lbAnaliDate" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btElab" runat="server" Text="Elaboração do Documento" CssClass="btn btn-default flux" Enabled="false" OnClick="btElab_Click" />
                                        <asp:Label ID="lbElabDate" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3 centered text-center">
                                        <div class="centered dig" runat="server">
                                            <asp:Button ID="btEnvia" runat="server" Text="Enviado para Assinatura do Cooperante" CssClass="btn btn-default flux" Enabled="false" OnClick="btEnvia_Click" />
                                            <asp:Label ID="lbEnviaDate" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btRes" runat="server" Text="Aprovado Com Ressalvas" CssClass="btn btn-default flux" Enabled="false" OnClick="btRes_Click" />
                                        <asp:Label ID="lbResDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div id="div5" class="centered form-group text-center" runat="server">
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btFis" runat="server" Text="Recebimento da Via Física" CssClass="btn btn-default flux" Enabled="false" OnClick="btRes_Click" />
                                        <asp:Label ID="lbViaFisica" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btMon" runat="server" Text="Enviado a Monsanto" CssClass="btn btn-default flux" Enabled="false" OnClick="btMon_Click" />
                                        <asp:Label ID="lbMonDate" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btApr" runat="server" Text="Aprovado" CssClass="btn btn-default flux" Enabled="false" OnClick="btApr_Click" />
                                        <asp:Label ID="lbAprDate" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3 centered text-center">
                                        <asp:Button ID="btDev" runat="server" Text="Devolvido ao Cooperante" CssClass="btn btn-default flux" Enabled="false" OnClick="btDev_Click" />
                                        <asp:Label ID="lbDevDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <label class="control-label">Nome do Cooperante</label>
                                        <div>
                                            <h3><i class="fa fa-child"></i>&nbsp;<label class="text-info" id="txName" runat="server"></label></h3>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <label class="control-label">Contatos do Cooperante</label>
                                        <div>
                                            <h3><i class="fa fa-file-o"></i>&nbsp;<label class="text-info" id="txDoc" runat="server"></label></h3>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Dados do Contrato</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label" id="lbDtContrato" runat="server">Data do Contrato</label>
                                        <asp:TextBox ID="txDtContrato" runat="server" class="datepicker form-control dtrec"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <label class="control-label">Safra</label>
                                        <asp:DropDownList ID="ddTpSafra" runat="server" class="btn btn-default dropdown-toggle safra col-sm-12" data-toggle="dropdown">
                                            <asp:ListItem Value="V">Verão</asp:ListItem>
                                            <asp:ListItem Value="I">Inverno</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 ">
                                        <label class="control-label">Ano Safra</label>
                                        <asp:DropDownList ID="ddSafra" runat="server" class="btn btn-default dropdown-toggle safra col-sm-12" data-toggle="dropdown" DataSourceID="obSafra" DataTextField="safra" DataValueField="safra"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="control-label">Unidade Contratante</label>
                                        <asp:DropDownList ID="ddUnidades" runat="server" class="btn btn-default dropdown-toggle gr col-sm-12" data-toggle="dropdown" DataSourceID="obUnidades" DataTextField="nome" DataValueField="nome"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="obUnidades" runat="server" SelectMethod="selectAllUnidades" TypeName="apoio_monsanto.coopcom">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="true" Name="all" Type="Boolean" />
                                                <asp:SessionParameter DefaultValue="" Name="unidades" SessionField="coopunidade" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>

                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Status (Fluxo)</label><br />
                                        <asp:DropDownList ID="ddStatus" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" onclick="Reproved();">
                                            <asp:ListItem>- Status - </asp:ListItem>
                                            <asp:ListItem Value="1">Etapa Em Andamento</asp:ListItem>
                                            <asp:ListItem Value="2">Etapa Aprovada</asp:ListItem>
                                            <asp:ListItem Value="3">Etapa Reprovada</asp:ListItem>
                                            <asp:ListItem Value="4">Contrato Cancelado</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-sm-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Data do Fluxo</label>
                                        <asp:TextBox ID="txDtFlux" class="datepicker form-control keep" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="control-label">Contrato Urgente?</label><br />
                                        <asp:DropDownList ID="ddUrgente" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" onclick="Reproved();">
                                            <asp:ListItem Selected="True" Value="N">Não</asp:ListItem>
                                            <asp:ListItem Value="S">Sim</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Matrícula</label>
                                        <asp:TextBox ID="txMatricula" class="form-control obs" runat="server" placeholder="Matrícula"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-9 ">
                                        <asp:TextBox ID="txObsCon" class="form-control obs" runat="server" placeholder="Comentários" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-sm-12 centered">
                                            <table border="0" class="centered">
                                                <tr>
                                                    <td width="100%">
                                                        <label>Documentos a Conferir</label>
                                                        <asp:ListBox ID="lstLeft" runat="server" SelectionMode="Multiple" CssClass="form-control col-sm-12">
                                                            <asp:ListItem>Matrícula da Fazenda (PF e PJ)</asp:ListItem>
                                                            <asp:ListItem>RG e CPF (PF)</asp:ListItem>
                                                            <asp:ListItem>Contrato Social e CNPJ (PJ)</asp:ListItem>
                                                            <asp:ListItem>Procuração Grower (PJ) (se aplicável)</asp:ListItem>
                                                            <asp:ListItem>Telefone e Email (PF e PJ)</asp:ListItem>
                                                            <asp:ListItem>Comprovante Endereço (PF e PJ)</asp:ListItem>
                                                            <asp:ListItem>Comprovante Conta Corrente (PF e PJ)</asp:ListItem>
                                                            <asp:ListItem>Inscrição Estadual (PF e PJ)</asp:ListItem>
                                                            <asp:ListItem>CCIR/INCRA (PF e PJ)</asp:ListItem>
                                                            <asp:ListItem>CTR arrendamento (se aplicável)</asp:ListItem>
                                                            <asp:ListItem>CTR parceria agrícola (se aplicável)</asp:ListItem>
                                                            <asp:ListItem>Liminar FUNRURAL (PF) (se aplicável)</asp:ListItem>

                                                        </asp:ListBox>
                                                    </td>
                                                    <td>
                                                        <input type="button" class="btn btn-info" id="right" value="\/" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td width="100%">
                                                        <label>Documentos Conferidos</label>
                                                        <asp:ListBox ID="lstRight" runat="server" SelectionMode="Multiple" CssClass="form-control col-sm-12 crit"></asp:ListBox>
                                                    </td>
                                                    <td>
                                                        <input type="button" class="btn btn-info" id="left" value="/\" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Técnico Responsável pelo Campo</label>
                                            <div class="form-group col-sm-12">
                                                <asp:DropDownList ID="ddRTV" runat="server" class="btn btn-default dropdown-toggle rtv col-sm-12" data-toggle="dropdown" DataSourceID="obRTV" DataTextField="name" DataValueField="name"></asp:DropDownList>
                                                <asp:ObjectDataSource ID="obRTV" runat="server" SelectMethod="selectRTV" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Supervisor de Campo</label>
                                            <div class="form-group col-sm-12">
                                                <asp:DropDownList ID="ddSuper" runat="server" class="btn btn-default dropdown-toggle gr col-sm-12" data-toggle="dropdown" DataSourceID="obSR" DataTextField="name" DataValueField="name"></asp:DropDownList>
                                                <asp:ObjectDataSource ID="obSr" runat="server" SelectMethod="selectSR" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="control-label">Gerente da Planta</label>
                                            <div class="form-group col-sm-12">
                                                <asp:DropDownList ID="ddGr" runat="server" class="btn btn-default dropdown-toggle gr col-sm-12" data-toggle="dropdown" DataSourceID="obGR" DataTextField="name" DataValueField="name"></asp:DropDownList>
                                                <asp:ObjectDataSource ID="obGR" runat="server" SelectMethod="selectGR" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Dados do Contrato</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="control-label">Número do Contrato Base</label>
                                        <asp:TextBox ID="txNumContBase" class="form-control dtdigit" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Número do Contrato Mãe</label>
                                        <asp:TextBox ID="txNumContMae" class="form-control arqui" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Recebimento Folha de Rosto</label>
                                        <asp:TextBox ID="txDataFolha" class="datepicker form-control keep" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <label class="control-label">Fazenda</label>
                                        <asp:TextBox ID="txFazenda" class="form-control aprov" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <label class="control-label">Dados da Fazenda</label>
                                        <asp:TextBox ID="txDFazenda" class="form-control keep" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="control-label">Area Contratada (sc/ha)</label>
                                        <asp:TextBox ID="txArea" class="form-control aprov" runat="server" Text="0,00"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Incra/Inscrição Estadual</label>
                                        <asp:TextBox ID="txIncra" class="form-control keep" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="control-label">Deságio de Praça (%)</label>
                                        <asp:TextBox ID="txDesagio" class="form-control aprov" runat="server" Text="0,00"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Teto (sc/ha)</label>
                                        <asp:TextBox ID="txTeto" class="form-control keep" runat="server" Text="0,00"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Início do Plantio</label>
                                        <asp:TextBox ID="txDtIni" class="datepicker form-control aprov" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Fim do Plantio</label>
                                        <asp:TextBox ID="txDtFim" class="datepicker form-control keep" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <label class="control-label">Garantia (sc/ha)</label>
                                        <asp:TextBox ID="txGarantiaS" class="form-control aprov" runat="server" Text="0,00"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="control-label">Garantia (R$/ha)</label>
                                        <asp:TextBox ID="txGarantiaR" class="form-control keep" runat="server" Text="0,00"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:ObjectDataSource ID="obAnda" runat="server" SelectMethod="selectContact" TypeName="apoio_monsanto.coopcom">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id_contract" QueryStringField="id_contract" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="obConta" runat="server" SelectMethod="selectContactJoined" TypeName="apoio_monsanto.coopcom">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id_process" QueryStringField="id_contract" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Contatos</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <button class="btn btn-theme tooltips" type="button" runat="server" data-toggle='modal' href='prospect#modAndamento' data-original-title='Inserir Novo Andamento'>Inserir Novo Andamento</button>
                                        <button id="btExcel" class="btn btn-success" runat="server" onserverclick="ExportToExcel">Exportar para Excel&nbsp;<i class="fa fa-file-excel-o"></i></button>

                                    </div>
                                    <div class="col-sm-12" id="div1" runat="server">

                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-12">

                                                <asp:GridView ID="gvAnda" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed" DataSourceID="obAnda" EmptyDataText="Nenhum Andamento Realizado">
                                                    <Columns>
                                                        <asp:BoundField DataField="contato" HeaderText="Andamento" />
                                                        <asp:BoundField DataField="Data do Contato" HeaderText="Data do Contato" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="lembrar" HeaderText="Lembrar Contato?" />
                                                        <asp:BoundField DataField="Data do Lembrete" HeaderText="Data do Lembrete" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:TemplateField HeaderText="Ações">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# getAndAction("anda") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <button class="btn btn-theme tooltips" type="button" runat="server" data-toggle='modal' href='prospect#contact' data-original-title='Inserir Novo Contato'>Inserir Novo Contato</button>
                                        <button id="Button1" class="btn btn-success" runat="server" onserverclick="ExportToExcel">Exportar para Excel&nbsp;<i class="fa fa-file-excel-o"></i></button>

                                    </div>
                                    <div class="col-sm-12" id="div4" runat="server">

                                        <br />
                                        <div class="form-group">
                                            <div class="col-sm-12">

                                                <asp:GridView ID="gvContactoPrev" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-condensed" DataSourceID="obConta" EmptyDataText="Nenhum Contato Realizado">
                                                    <Columns>
                                                        <asp:BoundField DataField="contato" HeaderText="Contato" />
                                                        <asp:BoundField DataField="data" HeaderText="Data do Contato" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="etapa" HeaderText="Etapa" />
                                                        <asp:TemplateField HeaderText="Ações">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# getAndAction("cont") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Upload de Documentos</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-sm-12" id="divEspec" runat="server">
                                        <p>
                                            <strong>Selecione o tipo de documento</strong>
                                            <br />
                                            <asp:RadioButtonList ID="rbTpCont" runat="server" CssClass="checkbox" DataSourceID="obTipos" DataTextField="name" DataValueField="id_process">
                                            </asp:RadioButtonList>
                                            <asp:ObjectDataSource ID="obTipos" runat="server" SelectMethod="selectTipoProcesso" TypeName="apoio_monsanto.coopcom">
                                                <SelectParameters>
                                                    <asp:Parameter Name="id_process" Type="String" />
                                                    <asp:Parameter DefaultValue="S" Name="ativo" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Data do Documento</label>
                                        <asp:TextBox ID="txDtDocUpd" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 ">
                                        <label class="control-label">Observações</label>
                                        <asp:TextBox ID="txDocObs" class="form-control obs" runat="server" placeholder="Observações" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-9">
                                        <span class="btn btn-clear-g" runat="server" id="divUp">
                                            <input type="file" id="txUp" name="txUp" runat="server">
                                        </span>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-3">
                                        <input type="submit" class="btn btn-theme02 pull-left" id="btUp" value="Enviar Documento" runat="server" name="Upload">
                                    </div>
                                </div>
                                <div class="form-group col-sm-6">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvDoc" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped table-condensed" DataSourceID="obDocs" EmptyDataText="Nenhum Documento Encontrado">
                                                        <Columns>
                                                            <asp:BoundField DataField="docname" HeaderText="Nome do Arquivo" />
                                                            <asp:BoundField DataField="date" HeaderText="Data do Arquivo" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                                            <asp:BoundField DataField="obs" HeaderText="Observações" />
                                                            <asp:TemplateField HeaderText="Ações">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text="<%# getAction() %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btUp" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="form-group" id="docs">
                                    <div class="col-sm-6">
                                        <asp:ObjectDataSource ID="obDocs" runat="server" SelectMethod="selectDocument" TypeName="apoio_monsanto.coopcom">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="id_contract" QueryStringField="id_contract" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>

                                    </div>

                                    <br />
                                    <!-- Modal -->
                                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabdashboard="-1" id="modAndamento" class="modal fade">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                    <h4 class="modal-title title">RSystem - Andamentos</h4>
                                                </div>
                                                <div class="modal-body" id="Div2" runat="server">
                                                    <p>Inclusão da Contato</p>
                                                    <label class="control-label">Andamento</label>
                                                    <asp:TextBox ID="txNewAnda" class="form-control confpor" runat="server"></asp:TextBox>
                                                    <i class="fa fa-calendar-o"></i>
                                                    <label class="control-label">Data do Contato</label>
                                                    <asp:TextBox ID="txNewDtAnda" runat="server" class="form-control dtrec datepicker clsDatePicker"></asp:TextBox>
                                                    <asp:CheckBox ID="cbLembAnd" runat="server" Text="Lembrar Contato?" />
                                                    <br />
                                                    <label class="control-label">Data do Lembrete</label>
                                                    <asp:TextBox ID="txDtNewLemb" runat="server" class="form-control dtlem datepicker clsDatePicker"></asp:TextBox>
                                                </div>

                                                <div class="modal-footer">
                                                    <button class="btn btn-theme" type="button" runat="server" onserverclick="btNewAnda_Click">Gravar</button>
                                                    <button data-dismiss="modal" class="btn btn-default" type="button">Sair</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabdashboard="-1" id="contact" class="modal fade">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                    <h4 class="modal-title title">RSystem - Contato</h4>
                                                </div>
                                                <div class="modal-body" id="Div3" runat="server">
                                                    <p>Inclusão da Contato</p>
                                                    <i class="fa fa-calendar-o"></i>
                                                    <label class="control-label">Data do Contato</label>
                                                    <asp:TextBox ID="txDtContact" class="datepicker form-control aprov" runat="server"></asp:TextBox>
                                                    <label class="control-label">Contato</label>
                                                    <asp:DropDownList ID="ddContact" runat="server" class="btn btn-default dropdown-toggle gr col-sm-12" data-toggle="dropdown" DataSourceID="obContact" DataTextField="description" DataValueField="id_coop_contact"></asp:DropDownList>
                                                    <asp:ObjectDataSource ID="obContact" runat="server" SelectMethod="selectContactPrev" TypeName="apoio_monsanto.coopcom">
                                                        <SelectParameters>
                                                            <asp:Parameter Name="idContact" Type="String" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </div>

                                                <div class="modal-footer">
                                                    <br />
                                                    <button class="btn btn-theme" type="button" runat="server" onserverclick="btNewCont_Click">Gravar</button>
                                                    <button data-dismiss="modal" class="btn btn-default" type="button">Cancelar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
            </section>
        </section>
    </section>

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
    <script src="assets/js/jquery-1.8.3.min.js"></script>

    <script type="text/javascript" src="assets/js/bootstrap-datetimepicker.js" charset="UTF-8"></script>
    <script type="text/javascript" src="assets/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $q = jQuery;
        $q('.form_datetime').datetimepicker({
            format: 'dd/mm/yyyy hh:ii',
            todayBtn: 1,
            autoclose: 1,
        });
    </script>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript" src="https://vitalets.github.io/bootstrap-datepicker/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $j = jQuery;

        $j(document).ready(function () {


            $j(".datepicker").datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR'
            });

            $j('.confirmation').on('click', function () {
                return confirm('Confirma exclusão?');
            });

            $j('.flux').on('click', function () {
                return confirm('Deseja Atualizar Etapa?');
            });

            $j('.rollback_flux').on('click', function () {
                return confirm('Deseja Voltar a Etapa Anterior?');
            });

        });

        $j(function () {
            $j("#left").bind("click", function () {
                var options = $j("[id*=lstRight] option:selected");
                for (var i = 0; i < options.length; i++) {
                    var opt = $j(options[i]).clone();
                    $j(options[i]).remove();
                    $j("[id*=lstLeft]").append(opt);
                }
            });
            $j("#right").bind("click", function () {
                var options = $j("[id*=lstLeft] option:selected");
                for (var i = 0; i < options.length; i++) {
                    var opt = $j(options[i]).clone();
                    $j(options[i]).remove();
                    $j("[id*=lstRight]").append(opt);
                }
            });
        });

        $j(function () {
            $j("[id*=btGravar]").bind("click", function () {
                $j("[id*=lstRight] option").attr("selected", "selected");
            });
        });

        function scrollDude() {
            $j.ajax({
                url: "",
                context: document.body,
                success: function (s, x) {
                    $(this).html(s);
                }
            });
        }

    </script>



</asp:Content>
