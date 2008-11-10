<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Curriculum.ascx.cs" Inherits="N2.Calendar.Curriculum.UI.Views.Curriculum" %>


        <asp:Label ID="Label" runat="server" Text="------"></asp:Label>
        <asp:Repeater
            runat="server"
            ID="rpt" onitemcommand="rpt_ItemCommand">
            <ItemTemplate>
                <div>
 
                      <asp:RadioButtonList AutoPostBack = true OnSelectedIndexChanged = "rbl_SelectedIndexChanged"
                        ID="rbl"  runat="server" RepeatDirection="Horizontal" selectedValue = '<%#((CourseInfo)Container.DataItem).CourseExclude %>'>
                            <asp:ListItem value =0></asp:ListItem>
                            <asp:ListItem  value =1>o</asp:ListItem>
                            <asp:ListItem value=2>э</asp:ListItem>
                      </asp:RadioButtonList>
                     <asp:Label runat="server" ID="l1" Text='<%# Eval("CourseName") %>' />
                    <asp:HiddenField runat="server" ID="hf" Value='<%# Eval("CourseID") %>' />
                     
                      
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
<%--'<%# ((CourseInfo)Container.DataItem).Title %>'--%>