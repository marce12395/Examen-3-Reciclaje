<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Examen_3_Reciclaje.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/Estilo.css" rel="stylesheet"/>
    <title>Registro de Reciclaje</title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Menú de navegación -->
        <div>
            <ul>
                <li><a href="inicio.aspx">Inicio</a></li>
                <li><a href="Hogar.aspx">Ingresar los datos del Hogar</a></li>
                <li><a href="Material.aspx">Ingresar los tipos de materiales</a></li>
                <li><a class="active" href="Registro.aspx">Registrar los materiales de cada hogar</a></li>
            </ul>
        </div>

        <!-- Contenedor principal -->
        <div class="main-container">
            <h2>Registrar materiales reciclados</h2>

            
           <div class="form-group">
    <label>Usuario:</label>
    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="aspNetTextBox" ></asp:DropDownList>
</div>

<div class="form-group">
    <label>Material:</label>
    <asp:DropDownList ID="ddlMaterial" runat="server" CssClass="aspNetTextBox" ></asp:DropDownList>
</div>

            <div class="form-group">
                <label>Cantidad:</label>
                <asp:TextBox ID="tcantidad" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
            </div>

<div class="form-group">
    <label>ID del Registro a modificar:</label>
    <asp:TextBox ID="tidregistro" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
</div>
             <div>
                
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Guardar"  CssClass="aspNetButton" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Modificar" CssClass="aspNetButton"  />
                <asp:Button ID="Button3" runat="server" Text="Buscar"  CssClass="aspNetButton" OnClick="Button3_Click"/>
                <asp:Button ID="Button4" runat="server" Text="Eliminar"  CssClass="aspNetButton" OnClick="Button4_Click"  />
            </div>
            
            <asp:GridView ID="GridView1" runat="server" CssClass="aspNetGridView" Width="904px"></asp:GridView>

           
                <br />
            </div>
        </div>
    </form>
</body>
</html>
