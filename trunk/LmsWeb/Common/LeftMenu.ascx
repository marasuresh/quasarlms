<%@ Control Language="c#" Inherits="DCE.Common.Migrated_LeftMenu" CodeFile="LeftMenu.ascx.cs" %>
<%@ Register Namespace="N2.Web.UI.WebControls" Assembly="N2" TagPrefix="n2" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Xml" %>

	<div class="Locator CommonSidebarArea">
		<div class="CommonSidebarContent">
			<nobr>
			<asp:SiteMapPath
					ID="SiteMapPath1"
					runat="server"
					ParentLevelsDisplayed="0"
					Font-Bold="False">
				<CurrentNodeTemplate>
					<%# ((SiteMapNodeItem)Container).SiteMapNode["FullDescr"] %>
				</CurrentNodeTemplate>
				<NodeStyle Font-Bold="True" Font-Size="12pt" ForeColor="#D0D0D0" />
			</asp:SiteMapPath>
			</nobr>
		</div>
	</div>

<%--<n2:Tree runat="server" ID="n2Tree" /> --%>

<asp:TreeView
		ID="TreeView1"
		runat="server"
		DataSourceID="XmlDataSource1"
		AutoGenerateDataBindings="False"
		NodeIndent="10"
		Visible="true">
	<DataBindings>
		<asp:TreeNodeBinding
			DataMember="item"
			TextField="text"
			ValueField="id"
			NavigateUrlField="NavigateUrl"
			ToolTipField="alt" />
		<asp:TreeNodeBinding
				DataMember="Items"
				Depth="0"
				SelectAction="None"
				Text="Items"
				Value="Items" />
	</DataBindings>
	<SelectedNodeStyle
			Font-Bold="True"
			BackColor="White"
			HorizontalPadding="3px"
			VerticalPadding="1px" />
	<NodeStyle
			Font-Names="Verdana"
			Font-Size="8pt"
			Font-Underline="true" />
	<ParentNodeStyle Font-Bold="False" />
	<HoverNodeStyle
			Font-Underline="false" />
</asp:TreeView>
<asp:XmlDataSource
		ID="XmlDataSource1"
		runat="server"
		EnableCaching="False"
		XPath="/Items/*">
	<Data>
<xml>
	<Items>
		<item>
			<id />
			<text/>
			<alt/>
			<control/>
		</item>
	</Items>
</xml>
	</Data>
	<Transform>
<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
	<Items>
		<xsl:apply-templates />
	</Items>
</xsl:template>

<xsl:template match="//item">
	<xsl:element name="{name()}">
		<xsl:apply-templates select="child::*"
							 mode="child2attr" />
		<xsl:call-template name="NavigateUrl" />
		<xsl:if test="not(alt)">
			<xsl:attribute name="alt">
				(empty)
			</xsl:attribute>
		</xsl:if>
		
		<xsl:if test="not(text)">
			<xsl:attribute name="text">
				(empty)
			</xsl:attribute>
		</xsl:if>
		
		<xsl:apply-templates />
	</xsl:element>
</xsl:template>

<xsl:template match="*"
			  mode="child2attr">
	<xsl:attribute name="{name()}">
		<xsl:value-of select="." />
	</xsl:attribute>
</xsl:template>

<xsl:template name="NavigateUrl">
	<xsl:attribute name="NavigateUrl">javascript:menuClick('<xsl:value-of select="page" />','<xsl:value-of select="control" />','<xsl:value-of select="action" />','<xsl:value-of select="id" />','<xsl:value-of select="trId" />','<xsl:value-of select="cId" />')</xsl:attribute>
</xsl:template>

</xsl:stylesheet></Transform>
</asp:XmlDataSource>