<%@ Page Title="" Language="C#" MasterPageFile="~/gla.Master" AutoEventWireup="true" CodeBehind="newcontract.aspx.cs" Inherits="apoio_monsanto.records.newcontract" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>NOVO CONTRATO - Apoio Monsanto</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <link rel="stylesheet" type="text/css" href="assets/css/datepicker.css">
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
                        <a href="/records/contract" id="btCan" class="btn btn-danger tooltips" data-original-title='Sair do Contrato' runat="server">Cancelar</a>
                    </div>
                </div>
                <div class="col-lg-12" >
                    <div class="form-panel" runat="server" visible="false">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Dados do Cliente</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div id="divStat" class="centered form-group text-center" runat="server">
                                    <div class="col-xs-2 centered">
                                        <div class="alert alert-warning centered inic">
                                            <p>
                                                Iniciando
                                        <br />
                                                Processo
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-xs-2 centered">
                                        <div class="alert alert-warning centered conf">
                                            <p>
                                                Conferindo
                                        <br />
                                                Contrato
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-xs-2 centered">
                                        <div class="alert alert-warning centered dig" runat="server">
                                            <p>
                                                Digitalizando
                                        <br />
                                                Contrato
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-xs-2 centered">
                                        <div class="alert alert-warning centered arq" runat="server">
                                            <p>
                                                Arquivando
                                        <br />
                                                Keepers
                                            </p>
                                        </div>
                                    </div>
                                    <div class="col-xs-2 centered">
                                        <div class="alert alert-warning centered text-center fin" runat="server">
                                            <p>
                                                Processo
                                        <br />
                                                Finalizado
                                            </p>
                                        </div>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <label class="control-label">Nome do Cliente</label>
                                        <div>
                                            <h3><i class="fa fa-child"></i>&nbsp;<label class="text-info" id="txName" runat="server"></label></h3>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">
                                        <label class="control-label">CPF / CNPJ do Cliente</label>
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
                                    <div class="col-xs-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Data do Contrato</label>
                                        <asp:TextBox ID="txDtRec" runat="server" class="datepicker form-control dtrec"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-6 ">
                                        <div class="form-group col-xs-6">
                                            <label class="control-label">CY</label>
                                            <asp:DropDownList ID="ddCY" runat="server" class="btn btn-default dropdown-toggle safra col-xs-12" data-toggle="dropdown">

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-6 ">
                                        <asp:TextBox ID="txObsCon" class="form-control obs" runat="server" placeholder="Observações" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-xs-3">
                                        <div class="form-group col-xs-3">
                                            <label class="control-label">Cultura</label>
                                            <asp:DropDownList ID="ddCultura" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" AutoPostBack="false">
                                                <asp:ListItem Value="">- Selecione -</asp:ListItem>
                                                <asp:ListItem Value="MILHO E SORGO">MILHO E SORGO</asp:ListItem>
                                                <asp:ListItem Value="CROP">CROP</asp:ListItem>
                                                <asp:ListItem Value="SOJA">SOJA</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <label class="control-label">Conferido Por:</label>
                                        <asp:TextBox ID="txConfPor" class="form-control confpor" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-3">
                                        <label class="control-label">Marca</label>
                                        <div class="form-group col-xs-12">
                                            <asp:DropDownList ID="ddMarca" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown">

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <label class="control-label">Unidade</label>
                                        <div class="form-group col-xs-12">
                                            <asp:DropDownList ID="ddUnidade" runat="server" class="btn btn-default dropdown-toggle rtv col-xs-12" data-toggle="dropdown">

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-xs-3">
                                        <label class="control-label">Regional</label>
                                        <div class="form-group col-xs-12">
                                            <asp:DropDownList ID="ddRegional" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown">

                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-xs-3">
                                        <i class="fa fa-calendar-o"></i>
                                        <label class="control-label">Data da Digitalização</label>
                                        <asp:TextBox ID="txDtDigit" class="datepicker form-control dtdigit" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-xs-3">
                                        <label class="control-label">Número de Caixa</label>
                                        <asp:TextBox ID="txCKeepers" class="form-control keep" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-panel">
                        <div class="form-group" style="margin-left: 12px;">
                            <div class="header-gen">
                                <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Novo Contrato&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Documentos Necessários</span>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-xs-4">
                                        <span class="btn btn-clear-g" runat="server" id="divUp">
                                            <input type="file" id="txUp" name="txUp" runat="server">
                                        </span>
                                    </div>
                                    <div class="col-xs-3">
                                        <input type="submit" class="btn btn-theme02 pull-left" id="btUp" value="Upload" runat="server" name="Upload">
                                    </div>
                                </div>
                                <div class="form-group col-xs-12">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvDoc" runat="server" AutoGenerateColumns="False" class="table table-bordered table-striped table-condensed" DataSourceID="obDocs" EmptyDataText="Nenhum Documento Encontrado">
                                                        <Columns>
                                                            <asp:BoundField DataField="docname" HeaderText="Nome do Arquivo" />
                                                            <asp:TemplateField HeaderText="Ações">
                                                                 <ItemTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# getAct() %>'></asp:Label>
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
                                    <div class="col-xs-6">
                                        <asp:ObjectDataSource ID="obDocs" runat="server" SelectMethod="selectDocument" TypeName="apoio_monsanto.glamom">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="id_contract" QueryStringField="id_contract" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:ObjectDataSource ID="obCriteria" runat="server" SelectMethod="selectAllCriteria" TypeName="apoio_monsanto.commom">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="" Name="type" SessionField="typeGLA" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                <asp:ObjectDataSource ID="obSafra" runat="server" SelectMethod="selectAllSafra" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>

            </section>
        </section>
    </section>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript" src="http://vitalets.github.io/bootstrap-datepicker/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        jQuery.noConflict();
        var $j = jQuery;

        function Reproved() {
            if ($j("[id*=ddStatus]").val() == 2) {
                $j('.fin').removeClass().addClass("alert alert-danger centered text-center fin");
                $j('.conf').removeClass().addClass("alert alert-danger centered text-center fin");
                // bloqueia os campos para não serem preenchidos
                $j(".dtdigit").prop('disabled', true);
                $j(".arqui").prop('disabled', true);
                $j(".keep").prop('disabled', true);
                $j(".aprov").prop('disabled', true);
            } else {
                // libera os campos para serem preenchidos
                $j(".dtdigit").prop('disabled', false);
                $j(".arqui").prop('disabled', false);
                $j(".keep").prop('disabled', false);
                $j(".aprov").prop('disabled', false);
            }
        }

        function validateClass() {
            var set = 0;
            if ($j(".dtrec").val() != null &&
                $j(".dtrec").val() != "") {
                $j('.inic').removeClass().addClass("alert alert-success centered inic");
                set++;
            } else {
                $j('.inic').removeClass().addClass("alert alert-warning centered inic");
            }

            if ($j(".dtstatus").val() != null &&
                $j(".dtstatus").val() != "" &&
                $j(".confpor").val() != null &&
                $j(".confpor").val() != "") {
                $j('.conf').removeClass().addClass("alert alert-success centered conf");
                set++;
            } else {
                $j('.conf').removeClass().addClass("alert alert-warning centered conf");
            }

            if ($j(".dtdigit").val() != null &&
                $j(".dtdigit").val() != "" &&
                $j(".arqui").val() != null &&
                $j(".arqui").val() != "" &&
                $j(".keep").val() != null &&
                $j(".keep").val() != "") {
                $j('.dig').removeClass().addClass("alert alert-success centered dig");
                $j('.arq').removeClass().addClass("alert alert-success centered arq");
                set++;
            } else {
                $j('.dig').removeClass().addClass("alert alert-warning centered dig");
                $j('.arq').removeClass().addClass("alert alert-warning centered arq");
            }
            if (set == 3 && $j("[id*=ddStatus]").val() != 2) {
                $j('.fin').removeClass().addClass("alert alert-success centered text-center fin");
                set = 0;
            } else if ($j("[id*=ddStatus]").val() == 2) {
                $j('.fin').removeClass().addClass("alert alert-danger centered text-center fin");
                set = 0;

            } else {
                $j('.fin').removeClass().addClass("alert alert-warning centered text-center fin");
                set = 0;
            }

        };

        $j(document).ready(function () {

            validateClass();
            Reproved();

            $j(".datepicker").datepicker({
                format: 'dd/mm/yyyy',
                language: 'pt-BR'
            });

            $j('.confirmation').on('click', function () {
                return confirm('Deseja deletar o documento?');
            });

            $j(document).click(function () {
                validateClass();
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
