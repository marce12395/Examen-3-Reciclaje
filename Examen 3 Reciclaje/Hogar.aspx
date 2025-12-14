<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hogar.aspx.cs" Inherits="Examen_3_Reciclaje.Hogar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/Estilo.css" rel="stylesheet"/>
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
                <div>
            <ul>
<li><a class="active" href="inicio.aspx">Inicio</a></li>
<li><a href="Hogar.aspx">Ingresar los datos del Hogar</a></li>
<li><a href="Material.aspx">Ingresar los tipos de materiales</a></li>
<li><a href="Registro.aspx">Regristrar los materiales de cada hogar</a></li>
</ul>
        </div>

       <div class="main-container">
    <h2>Ingrese los datos de su hogar</h2>

    <div class="form-group">
        <label>Nombre:</label>
        <asp:TextBox ID="tnombre" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
    </div>

    <div class="form-group">
        <label>Correo:</label>
        <asp:TextBox ID="tcorreo" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
    </div>

    <div class="form-group">
        <label>Número de casa:</label>
        <asp:TextBox ID="tubicacion" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
    </div>

    <div>
        <asp:Button ID="bnombre" runat="server" Text="Guardar" CssClass="aspNetButton" OnClick="bnombre_Click" />
        <asp:Button ID="bmodificar" runat="server" Text="Modificar con el nombre del hogar " CssClass="aspNetButton" OnClick="bmodificar_Click" />
        <asp:Button ID="beliminar" runat="server" Text="Eliminar por el número de casa" CssClass="aspNetButton" OnClick="beliminar_Click" />
        <asp:Button ID="bbuscar" runat="server" Text="Buscar por el nombre del hogar" CssClass="aspNetButton" OnClick="bbuscar_Click" />
    </div>

    <asp:GridView ID="GridView1" runat="server" CssClass="aspNetGridView" Width="878px"></asp:GridView>
</div>



         
    </form>
</body>
</html>
