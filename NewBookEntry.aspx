<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewBookEntry.aspx.cs" Inherits="ACCCounter.NewBookEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal clearfix" runat="server">
        <div class="panel panel panel-primary">
            <div class="panel-heading">
                <h4>New Book Entry</h4>
            </div>
            <div class="panel-body">
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtBookId" runat="server" CssClass="control-label col-md-2 no-padding-right">Book Id</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBookId" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label AssociatedControlID="txtBookName" runat="server" CssClass="control-label col-md-2 no-padding-right">Book Name</asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtBookName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label AssociatedControlID="ddlBookType" CssClass="control-label col-md-2 no-padding-right" runat="server">Book Type</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlBookType" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label AssociatedControlID="ddlClass" CssClass="control-label col-md-2 no-padding-right" runat="server">Class</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlClass" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label AssociatedControlID="ddlCompany" CssClass="control-label col-md-2 no-padding-right" runat="server">Company</asp:Label>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCompany" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-md-4 col-md-offset-2">
                        <asp:Button runat="server" CssClass="btn btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click"></asp:Button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4"></div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <asp:Label AssociatedControlID="txtSearch" runat="server" CssClass="control-label col-md-6 no-padding-right">Search</asp:Label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>                         
                            </div>
                        </div>
                    </div>
                </div>

                <div class="table-responsive  col-md-12">
                    <asp:GridView ID="gvwBookEntry" CssClass="table-responsive" runat="server" Align="Center" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvwBookEntry_PageIndexChanging" OnRowDataBound="gvwBookEntry_RowDataBound" OnSelectedIndexChanged="gvwBookEntry_SelectedIndexChanged">
                        <PagerSettings Mode="NumericFirstLast"/>
                        <Columns>
                            <asp:CommandField SelectText="Select" ShowSelectButton="true" ItemStyle-CssClass="HiddenColumn"
                                HeaderStyle-CssClass="HiddenColumn" />
                            <asp:BoundField HeaderStyle-Width="200px" DataField="BookID" HeaderText="BookID" />
                            <asp:BoundField HeaderStyle-Width="350px" DataField="BookName" HeaderText="BookName" />
                            <asp:BoundField HeaderStyle-Width="200px" DataField="Code" HeaderText="Code" />
                            <asp:BoundField HeaderStyle-Width="200px" DataField="ClassName" HeaderText="ClassName" SortExpression="DateField" />
                            <asp:BoundField HeaderStyle-Width="200px" DataField="BookType" HeaderText="BookType" />
                            <asp:BoundField HeaderStyle-Width="200px" DataField="CompanyName" HeaderText="CompanyName" />
                        </Columns>
                        <SelectedRowStyle BackColor="#393737" Font-Bold="true" ForeColor="#F7F7F7" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RequiredJS" runat="server">
    <script>
        $(document).ready(function () {
            open_menu("Book Informations");
        });
    </script>
</asp:Content>
