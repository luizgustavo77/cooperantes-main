<%@ Page Title="" Language="C#" MasterPageFile="~/coop.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="apoio_monsanto.cooperantes.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CADASTRO - Apoio Monsanto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cont_pages" runat="server">
    <!--main content start-->
    <section id="main-content">
        <section class="wrapper">
            <div class="col-md-12 mb">
                <div class="form-panel">
                    <div class="navbar-fixed-bottom">
                        <div class="centered text-center">
                            <label id="error" class="alert alert-danger" runat="server" visible="false"></label>
                        </div>
                    </div>
                    <div class="header-gen">
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cooperantes</span>
                    </div>
                    <div class="form-group">
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Nome do Cliente</label>
                                    <asp:TextBox ID="txNam" class="form-control" runat="server" placeholder="Digite o Nome do Cliente"></asp:TextBox>
                                    <input type="text" runat="server" id="txNamHid" style="display: none" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">CPF / CNPJ do Cliente</label>
                                    <asp:TextBox ID="txDc" name='cpfCnpj' class="form-control doc" runat="server" onkeypress='mascaraMutuario(this,cpfCnpj)' onblur='clearTimeout()' placeholder="Digite o CPF ou CNPJ do Cliente" MaxLength="18"></asp:TextBox>
                                    <input type="text" runat="server" id="txDocHid" style="display: none" />
                                </div>
                            </div>
                            <div class="form-group ">

                            <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">&nbsp;</label>
                                    <p class="alert alert-info">Informe o CEP sem hífen (-) e clique em consultar. As informações obtidas vem diretamente do site dos Correios.</p>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Cep</label>
                                    <asp:TextBox ID="txCep" class="form-control doc" runat="server" placeholder="Digite o Cep" MaxLength="8" TextMode="Number"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">&nbsp;</label>
                                    <button type="submit" id="btCep" class="btn btn-theme form-control" runat="server" onserverclick="btCep_Click">Consultar</button>

                                </div>
                            </div>
                            
                        </div>
                        <div class="form-group">
                                <div class="col-lg-12">
                                    <label class="control-label">&nbsp;</label>
                                    <label class="control-label">&nbsp;</label>
                                </div>
                            </div>
                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-9">
                                    <label class="control-label">Rua</label>
                                    <asp:TextBox ID="txRua" class="form-control" runat="server" placeholder="Rua do Cliente" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Número</label>
                                    <asp:TextBox ID="txNum" class="form-control" runat="server" placeholder="Número do Cliente" ReadOnly="False" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Bairro</label>
                                    <asp:TextBox ID="txBairro" class="form-control" runat="server" placeholder="Bairro do Cliente" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Cidade</label>
                                    <asp:TextBox ID="txCidade" class="form-control" runat="server" placeholder="Cidade do Cliente" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-3">
                                    <label class="control-label">Estado</label>
                                    <asp:TextBox ID="txEstado" class="form-control" runat="server" placeholder="Estado do Cliente" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-9">
                                    <label class="control-label">Complemento</label>
                                    <asp:TextBox ID="txCompl" class="form-control" runat="server" placeholder="Complemento" ReadOnly="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Telefone</label>
                                    <asp:TextBox ID="txTel" class="form-control mascara-telefone" runat="server" placeholder="Digite o Telefone do Cliente" MaxLength="14"></asp:TextBox>

                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">E-mail</label>
                                    <asp:TextBox ID="txMail" class="form-control email-vld" runat="server" placeholder="Digite o Email do Cliente"></asp:TextBox>

                                </div>
                            </div>
                        </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Técnico Responsável pelo Campo</label>
                                        <asp:DropDownList ID="ddRTV" runat="server" class="btn btn-default dropdown-toggle rtv col-xs-12" data-toggle="dropdown" DataSourceID="obRTV" DataTextField="name" DataValueField="name" AutoPostBack="true"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="obRTV" runat="server" SelectMethod="selectRTV" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Unidade Contratante</label>
                                    <asp:DropDownList ID="ddDist" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown" DataSourceID="obDis" DataTextField="distrito" DataValueField="distrito" AutoPostBack="false" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Gerente da Planta</label>
                                        <asp:DropDownList ID="ddGR" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown" DataSourceID="obGR" DataTextField="name" DataValueField="name" AutoPostBack="true"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="obGR" runat="server" SelectMethod="selectGR" TypeName="apoio_monsanto.coopcom"></asp:ObjectDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12">
                            <div>&nbsp;</div>
                            <div class="form-group">
                                <button type="submit" id="btPesq" class="btn btn-theme" runat="server" onserverclick="btPes_Click">Pesquisar</button>
                                <button type='submit' id="btAtual" class='btn btn-success' visible="false" runat='server' onserverclick='btUpd_Click'>Atualizar</button>
                                <button type='submit' id="btDel" class='btn btn-danger' visible="false" runat='server' onserverclick='btDel_Click'>Excluir</button>
                                <button type='submit' id="btCan" class='btn btn-theme' visible="false" runat='server' onserverclick='btCan_Click'>Cancelar</button>
                                <button type="submit" id="btGravar" class="btn btn-success" runat="server" onserverclick="btReg_Click">Gravar</button>

                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:GridView ID="gvCustomers" class="table table-bordered table-striped table-condensed" runat="server" AllowPaging="True" AllowSorting="True" DataSourceID="obCustomers" AutoGenerateColumns="False" EmptyDataText="Nenhum dado encontrado!">
                            <Columns>
                                <asp:BoundField DataField="name" HeaderText="Nome do Cliente" />
                                <asp:BoundField DataField="document" HeaderText="Documento do Cliente" />
                                <asp:TemplateField HeaderText="Ações">
                                    <ItemTemplate>
                                        <asp:Label ID="lbAction" runat="server" Text='<%# getActionC() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="obCustomers" runat="server" SelectMethod="searcCustomers" TypeName="apoio_monsanto.coopcom">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txNam" Name="name" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="txDc" Name="document" PropertyName="Text" Type="String" />
                                <asp:Parameter Name="id" Type="String" />
                                <asp:SessionParameter Name="gla" SessionField="typeGLA" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="obReg" runat="server" SelectMethod="retRegiByGR" TypeName="apoio_monsanto.coopcom">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddGR" Name="gr" PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="obDis" runat="server" SelectMethod="retDistByRTV" TypeName="apoio_monsanto.coopcom">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddRTV" Name="rtv" PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
                <!-- /form-panel -->
            </div>
            <!-- /row -->
        </section>
    </section>
    <script>
        function mascaraMutuario(o, f) {
            v_obj = o
            v_fun = f
            setTimeout('execmascara()', 5)
        }

        function execmascara() {
            v_obj.value = v_fun(v_obj.value)
        }

        function cpfCnpj(v) {

            //Remove tudo o que não é dígito
            v = v.replace(/\D/g, "")

            if (v.length <= 14) { //CPF

                //Coloca um ponto entre o terceiro e o quarto dígitos
                v = v.replace(/(\d{3})(\d)/, "$1.$2")

                //Coloca um ponto entre o terceiro e o quarto dígitos
                //de novo (para o segundo bloco de números)
                v = v.replace(/(\d{3})(\d)/, "$1.$2")

                //Coloca um hífen entre o terceiro e o quarto dígitos
                v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2")

            } else { //CNPJ

                //Coloca ponto entre o segundo e o terceiro dígitos
                v = v.replace(/^(\d{2})(\d)/, "$1.$2")

                //Coloca ponto entre o quinto e o sexto dígitos
                v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3")

                //Coloca uma barra entre o oitavo e o nono dígitos
                v = v.replace(/\.(\d{3})(\d)/, ".$1/$2")

                //Coloca um hífen depois do bloco de quatro dígitos
                v = v.replace(/(\d{4})(\d)/, "$1-$2")

            }

            return v

        }
    </script>

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
</asp:Content>
