<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Material.aspx.cs" Inherits="Examen_3_Reciclaje.Material" %>

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
      <label>Nombre del material a reciclar:</label>
      <asp:TextBox ID="tmateria" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
  </div>

  <div class="form-group">
      <label>Tipo(papel, plastico, vidrio, entr otros):</label>
      <asp:TextBox ID="tipo" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
  </div>

  <div class="form-group">
      <label>Puntos de este material por unidad:</label>
      <asp:TextBox ID="tpuntos" runat="server" CssClass="aspNetTextBox"></asp:TextBox>
  </div>

<div>
        
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Guardar" CssClass="aspNetButton" Width="94px"  /> 
    <asp:Button ID="Button2" runat="server" Text="Modificar con el nombre del material" CssClass="aspNetButton" OnClick="Button2_Click" Width="267px" />
    <asp:Button ID="Button3" runat="server" Text="Buscar por nombre del material" CssClass="aspNetButton" OnClick="Button3_Click" Width="235px"/>
    <asp:Button ID="Button4" runat="server" Text="Eliminar por nombre del material" CssClass="aspNetButton" OnClick="Button4_Click" Width="241px" />
        
    </div>
    
    <asp:GridView ID="GridView1" runat="server" CssClass="aspNetGridView" Width="898px"></asp:GridView>
</div>
    </form>
            </body>
</html>
