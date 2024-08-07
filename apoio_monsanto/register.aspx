<%@ Page Title="" Language="C#" MasterPageFile="~/mon.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="apoio_monsanto.register" %>

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
                        <span class="mb"><i class="fa fa-angle-right"></i>&nbsp;&nbsp;Cadastros/Consultas-Dados Gerais&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;Clientes</span>
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
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">RTV Responsável</label>
                                        <asp:DropDownList ID="ddRTV" runat="server" class="btn btn-default dropdown-toggle rtv col-xs-12" data-toggle="dropdown" DataSourceID="obRTV" DataTextField="name" DataValueField="name" AutoPostBack="true"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="obRTV" runat="server" SelectMethod="selectRTV" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Distrito (Baseado no RTV)</label>
                                    <asp:DropDownList ID="ddDist" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown" DataSourceID="obDis" DataTextField="distrito" DataValueField="distrito" AutoPostBack="false" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">GR Responsável</label>
                                        <asp:DropDownList ID="ddGR" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown" DataSourceID="obGR" DataTextField="name" DataValueField="name" AutoPostBack="true"></asp:DropDownList>
                                        <asp:ObjectDataSource ID="obGR" runat="server" SelectMethod="selectGR" TypeName="apoio_monsanto.commom"></asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label">Regional (Baseado no GR)</label>
                                    <asp:DropDownList ID="ddReg" runat="server" class="btn btn-default dropdown-toggle gr col-xs-12" data-toggle="dropdown" DataSourceID="obReg" DataTextField="regional" DataValueField="regional" AutoPostBack="false" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <label class="control-label"></label>
                                    <asp:CheckBox ID="userAgric" runat="server" Text="Agricultor" TextAlign="Left" Checked="False" class="switch-left switch-animate" />
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
                                        <asp:Label ID="lbAction" runat="server" Text='<%# getAction() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="obCustomers" runat="server" SelectMethod="searcCustomers" TypeName="apoio_monsanto.commom">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txNam" Name="name" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="txDc" Name="document" PropertyName="Text" Type="String" />
                                <asp:Parameter Name="id" Type="String" />
                                <asp:SessionParameter Name="gla" SessionField="typeGLA" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="obReg" runat="server" SelectMethod="retRegiByGR" TypeName="apoio_monsanto.commom">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddGR" Name="gr" PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="obDis" runat="server" SelectMethod="retDistByRTV" TypeName="apoio_monsanto.commom">
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
</asp:Content>
