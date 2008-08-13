<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsDetails.ascx.cs" Inherits="News_NewsDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc1" %>
    <asp:DetailsView ID="NewsDetailsView" runat="server" DataSourceID="NewsDetailsDataSource" Width="100%" AutoGenerateRows="False" DataKeyNames="ID" meta:resourcekey="NewsDetailsViewResource1">
        <Fields>
            <asp:TemplateField HeaderText="Date" SortExpression="Date" meta:resourcekey="TemplateFieldResource1">
                <EditItemTemplate>
					<asp:TextBox
							runat="server"
							ID="tbEditCalendar"
							ReadOnly="true"
							Text='<%# Bind("Date") %>' />
					<cc1:CalendarExtender
							ID="CalendarExtender1"
							FirstDayOfWeek="Monday"
							runat="server"
							TargetControlID="tbEditCalendar" />
                    <%--<asp:Calendar ID="dateEditCalendar" runat="server"
                        SelectedDate='<%# Bind("Date") %>' ></asp:Calendar>--%>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="dateViewLabel" runat="server" Text='<%# Eval("Date", "{0:D}") %>' ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" meta:resourcekey="BoundFieldResource1">
                <ControlStyle Width="100%" />
            </asp:BoundField>
            
            <asp:TemplateField HeaderText="Content" SortExpression="ContentText" meta:resourcekey="TemplateFieldResource2">
                <EditItemTemplate>
                    <asp:TextBox Width="100%" ID="contentTextBox" TextMode=MultiLine runat="server" Text='<%# Bind("ContentText") %>' Height="10em" meta:resourcekey="contentTextBoxResource1"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="contentLabel" runat="server" Text='<%# Bind("ContentText") %>' meta:resourcekey="contentLabelResource1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="URL" SortExpression="Href" meta:resourcekey="TemplateFieldResource3">
                <EditItemTemplate>
                    <asp:TextBox Width="100%" ID="urlTextBox" runat="server" Text='<%# Bind("Href") %>' meta:resourcekey="urlTextBoxResource1"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="urlHyperLink" runat="server" NavigateUrl='<%# Bind("Href") %>' Text='<%# Bind("Href") %>' meta:resourcekey="urlHyperLinkResource1"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Region" SortExpression="RegionName" meta:resourcekey="TemplateFieldResource4">
                <EditItemTemplate>
                    <uc1:RegionEditControl ID="RegionEditControl1" runat="server" RegionID='<%# Bind("RegionID") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="regionLabel" runat="server" Text='<%# Bind("RegionName") %>' meta:resourcekey="regionLabelResource1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Зберегти" />&nbsp;<asp:Button ID="Button2" runat="server" CausesValidation="False"
                            CommandName="Cancel" Text="Відмінити" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Редагувати" />
                </ItemTemplate>
            </asp:TemplateField>
            
        </Fields>
    </asp:DetailsView>
    <asp:SqlDataSource ID="NewsDetailsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
        SelectCommand="dcetools_News_GetDetails" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False" OnUpdating="NewsDetailsDataSource_Updating" UpdateCommand="dcetools_News_UpdateDetails" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
            <asp:QueryStringParameter Name="newsID" QueryStringField="id" />
        </SelectParameters>
        <UpdateParameters>
            <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
            <asp:Parameter Name="ID" />
            <asp:Parameter Name="date" Type="DateTime" />
            <asp:Parameter Name="title" Type="String" />
            <asp:Parameter Name="contentText" Type="String" />
            <asp:Parameter Name="href" Type="String" />
            <asp:Parameter Name="regionID" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
