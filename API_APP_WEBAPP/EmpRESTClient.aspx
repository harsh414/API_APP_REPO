<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpRESTClient.aspx.cs" Inherits="API_APP_WEBAPP.EmpRESTClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" Text="EmpNo"></asp:Label>
            <asp:TextBox ID="txteno" runat="server" CssClass="form-control"
               ></asp:TextBox>
            <asp:RequiredFieldValidator ID="deptnorequired" runat="server" ErrorMessage="EmpNo is Required"
                  ControlToValidate="txteno"></asp:RequiredFieldValidator>
     
        </div>
        <div class="form-group">
            <asp:Label ID="Label2" runat="server" Text="EmpName"></asp:Label>
            <asp:TextBox ID="txtename" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                 ErrorMessage="EmpName is Required"
                 ControlToValidate="txtename"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:Label ID="Label3" runat="server" Text="Designation"></asp:Label>
            <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                 ErrorMessage="Designation is Required"
                 ControlToValidate="txtdesignation"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:Label ID="Label4" runat="server" Text="Salary"></asp:Label>
            <asp:TextBox ID="txtsalary" runat="server" CssClass="form-control"></asp:TextBox>
        </div>


        <div class="form-group">
            <asp:Label runat="server">Department</asp:Label>
             <asp:DropDownList ID="ddlDepart" AutoPostBack="True" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" runat="server" CssClass="form-control">
                
             </asp:DropDownList>
        </div>

        <div class="form-group">
        <asp:Label ID="Label5" runat="server" Text="DeptNo"></asp:Label>
            <asp:TextBox ReadOnly="true" ID="txtdno" runat="server" CssClass="form-control"
               ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="DeptNo is Required"
                  ControlToValidate="txtdno"></asp:RequiredFieldValidator>
        </div>

        <div class="form-group">
            <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-warning" 
                OnClick="btnNew_Click"/>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" 
                OnClick="btnSave_Click"/>
        </div>
    </div>
    <br/>
    <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>

    <hr />
    <asp:GridView ID="gvEmp" runat="server" BackColor="black" 
    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
    GridLines="Vertical" AutoGenerateColumns="False" 
        OnRowCancelingEdit="gvEmp_RowCancelingEdit" OnRowDeleting="gvEmp_RowDeleting"
        OnRowEditing="gvEmp_RowEditing" OnRowUpdating="gvEmp_RowUpdating"
        OnSelectedIndexChanged="gvEmp_SelectedIndexChanged" CellSpacing="2" 
         PageSize="2">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:BoundField DataField="EmpNo" ReadOnly="true" HeaderText="DeptNo" />
            <asp:BoundField DataField="EmpName" HeaderText="EmpName" />
            <asp:BoundField DataField="Designation" HeaderText="Designation" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
            <asp:BoundField DataField="DeptNo" HeaderText="DeptNo" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
</asp:GridView>
</asp:Content>