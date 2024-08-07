<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="apoio_monsanto._default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="David Branco">
    <meta name="keyword" content="">

    <title>LOGIN - Apoio Monsanto</title>

    <!-- Bootstrap core CSS -->
    <link href="assets/css/bootstrap.css" rel="stylesheet">
    <!--external css-->
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <!-- Custom styles for this template -->
    <link href="assets/css/style.css" rel="stylesheet">
    <link href="assets/css/style-responsive.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

<body>

    <!-- **********************************************************************************************************************************************************
      MAIN CONTENT
      *********************************************************************************************************************************************************** -->

    <div id="login-page">
        <div class="container">

            <form class="form-login" runat="server">
                <div class="centered">
                    <img alt="avatar" src="assets/img/logo.jpg">
                </div>
                <div class="login-wrap">
                    <input type="text" class="form-control" runat="server" id="login" placeholder="Login" autofocus>
                    <br>
                    <input type="password" class="form-control" runat="server" id="pass" placeholder="Senha">
                    <br>
                    <asp:DropDownList ID="ddSelect" runat="server" CssClass="form-control">
                        <asp:ListItem Value="1">Apoio FLC</asp:ListItem>
                        <asp:ListItem Value="2">Efetividade de Distribuição</asp:ListItem>
                    </asp:DropDownList>
                    <br>
                    <label class="checkbox">
                        <span class="pull-right">
                            <a data-toggle="modal" href="default.aspx#myModal">Esqueceu a senha?</a>
                        </span>
                    </label>
                    <button class="btn btn-theme btn-block" runat="server" onserverclick="btLogin_Click" type="submit">LOGIN</button>
                    <br />
                    <div class="centered">
                        <label class="text-center alert-danger centered" runat="server" id="error"></label>
                    </div>
                </div>
                <!-- Modal -->
                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabdashboard="-1" id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">Esqueceu a senha?</h4>
                            </div>
                            <div class="modal-body">
                                <p class="text-center">Informe seu <strong>login</strong> e enviaremos um e-mail com sua senha de acesso para seu e-mail de cadastro!</p>
                                <input type="text" id="loginForget" placeholder="Login" autocomplete="off" class="form-control placeholder-no-fix" runat="server">
                            </div>
                            <div class="modal-footer">
                                <button data-dismiss="modal" class="btn btn-default" type="button">Cancelar</button>
                                <asp:Button ID="btForget" runat="server" Text="Enviar" OnClick="btForget_click" CssClass="btn btn-info" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal -->

            </form>

        </div>
    </div>

    <!-- js placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>

    <!--BACKSTRETCH-->
    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <script type="text/javascript" src="assets/js/jquery.backstretch.min.js"></script>
    <script>
        $.backstretch("assets/img/login-bg.jpg", { speed: 300 });
    </script>


</body>
</html>
