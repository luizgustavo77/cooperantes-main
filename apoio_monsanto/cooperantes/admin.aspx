<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="apoio_monsanto.cooperantes.admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ADMIN - Apoio Monsanto</title>
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
                        <span class="mb" id="h4Title" runat="server"></span>
                    </div>
                    <div class="col-xs-12 alert alert-warning">
                        <p>
                            - Todos os campos são pesquisáveis<br />
                            - Os campos Nome e Email são obrigatórios para inclusão de registros<br />
                        </p>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">Nome</label>
                                    <asp:TextBox ID="txNam" class="form-control" runat="server" placeholder="Nome Completo"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Login</label>
                                    <asp:TextBox ID="txLogin" class="form-control tooltips" runat="server" data-placement='top' data-original-title='Formato do login primeiro nome + (ponto) + último sobrenome'></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Contato</label>
                                    <asp:TextBox ID="txCont1" class="form-control mascara-telefone" runat="server" placeholder="Contato" MaxLength="15"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">Unidade(s)</label><div></div>
                                    <asp:TextBox ID="txUnidades" class="form-control tooltips col-lg-3" runat="server" data-placement='top' data-original-title='Escolha uma das opções ao abaixo' ReadOnly="true"></asp:TextBox>
                                    <button class="btn btn-success" type="button" runat="server" data-toggle="modal" data-target="#modUnid" id="btAddUni">Adicionar Unidades</button>
                                    <button class="btn btn-danger" type="button" runat="server" onserverclick="btDelUni_Click" id="btDelUni">Remover Unidades</button>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Contato Alternativo</label>
                                    <asp:TextBox ID="txCont2" class="form-control mascara-telefone" runat="server" placeholder="Contato Alternativo" MaxLength="15"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">E-mail</label>
                                    <asp:TextBox ID="txEmail" class="form-control email-vld" runat="server" placeholder="E-mail" onkeyup="validateEmail()"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">

                                <div class="col-lg-6">
                                    <label class="control-label">&nbsp;</label>
                                    <asp:CheckBox ID="userActive" runat="server" Text="Usuário Ativo" TextAlign="Left" Checked="True" class="switch-left switch-animate" />
                                </div>
                                <div class="col-lg-6">
                                    <label class="control-label">&nbsp;</label>
                                    <asp:CheckBox ID="userVisual" runat="server" Text="Somente Visualização" TextAlign="Left" Checked="false" class="switch-left switch-animate" />
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <div>&nbsp;</div>
                            <div class="form-group">
                                <asp:Button ID="btpesq" runat="server" Text="Pesquisar" class="btn btn-theme" />
                                <button class="btn btn-success" type="button" runat="server" onserverclick="btGrava_Click" id="btgrav">Gravar</button>
                                <button class="btn btn-success" type="button" runat="server" onserverclick="btEdit_Click" id="btedit" visible="false">Atualizar</button>
                                <button class="btn btn-danger" type="button" runat="server" onserverclick="btDel_Click" id="btdel" visible="false">Excluir</button>
                                <button class="btn btn-theme" type="button" runat="server" onserverclick="btCan_Click" id="btcan" visible="false">Cancelar</button>
                            </div>
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:GridView ID="gvUsers" runat="server" DataSourceID="obUser" class="table table-bordered table-striped table-condensed" EmptyDataText="Nenhum usuário encontrado" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="login" HeaderText="Login" />
                                <asp:BoundField DataField="name" HeaderText="Nome" />
                                <asp:BoundField DataField="email" HeaderText="E-mail" />
                                <asp:BoundField DataField="distrito" HeaderText="Unidades" />
                                <asp:BoundField DataField="status" HeaderText="Status" />
                                <asp:BoundField DataField="visualiza" HeaderText="Somente Visualiza?" />
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# getAction() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="obUser" runat="server" SelectMethod="selectUsers" TypeName="apoio_monsanto.coopcom">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txNam" Name="name" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="txEmail" Name="email" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="txLogin" Name="login" PropertyName="Text" Type="String" />
                                <asp:QueryStringParameter Name="type" QueryStringField="type" Type="String" />
                                <asp:ControlParameter ControlID="ddUnidade" Name="distrito" PropertyName="SelectedValue" Type="String" />
                                <asp:ControlParameter ControlID="txCont1" Name="cont1" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="obRegion" runat="server" SelectMethod="selectAllRegion" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
                    </div>
                </div>
                <!-- /form-panel -->
            </div>
            <!-- Modal -->
            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabdashboard="-1" id="modUnid" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title title">Unidades - Apoio Monsanto</h4>
                        </div>
                        <div class="modal-body" id="modal_body" runat="server">
                            <p>Selecione a unidade</p>
                            <asp:DropDownList ID="ddUnidade" runat="server" class="btn btn-default dropdown-toggle" data-toggle="dropdown" DataSourceID="obUnidades" DataTextField="nome" DataValueField="nome"></asp:DropDownList>
                        </div>

                        <div class="modal-footer">
                            <button class="btn btn-theme" type="button" onserverclick="btAddUni_Click" runat="server">OK</button>
                            <button data-dismiss="modal" class="btn btn-default" type="button">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- modal -->
            <asp:ObjectDataSource ID="obUnidades" runat="server" SelectMethod="selectAllUnidades" TypeName="apoio_monsanto.coopcom">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="all" Type="Boolean" />
                    <asp:Parameter DefaultValue="" Name="unidades" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <!-- /row -->
        </section>
        <script>
            function mascara(o, f) {
                v_obj = o
                v_fun = f
                setTimeout("execmascara()", 1)
            }
            function execmascara() {
                v_obj.value = v_fun(v_obj.value)
            }
            function mtel(v) {
                v = v.replace(/\D/g, ""); //Remove tudo o que não é dígito
                v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
                v = v.replace(/(\d)(\d{4})$/, "$1-$2"); //Coloca hífen entre o quarto e o quinto dígitos
                return v;
            }

            function getByclass(el) {
                return document.getElementsByClassName(el);
            }
            function validateEmail() {
                var re = /\S+@\S+\.\S+/;
                if (!re.test($('.email-vld').val())) {
                    $('.email-vld').addClass('alert alert-danger');
                }
                else {
                    $('.email-vld').removeClass('alert alert-danger').addClass('alert alert-success');
                }
            }
            window.onload = function () {
                var elems = document.getElementsByClassName('mascara-telefone')

                for (var i = 0; i < elems.length; i++) {
                    elems[i].onkeypress = function () {
                        mascara(this, mtel);
                    }
                }
            }
        </script>
    </section>
</asp:Content>
