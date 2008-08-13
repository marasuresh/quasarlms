<%@ Control Language="c#" Inherits="DCE.Common.Bottom" CodeFile="Bottom.ascx.cs" %>
<div id="CommonFooter">
	<table	id="dceFooter"
			cellspacing="0"
			cellpadding="0"
			width="100%">
		<tbody>
			<tr valign="bottom">
				<td		align="center">
					<div id="dceGlobalFooter">&copy;&nbsp;
						<asp:Literal
								runat="server"
								ID="ltrCopyRights"
								Text='<%$ Resources:Bottom, title %>' />
					</div>
				</td>
			</tr>
		</tbody>
	</table>
</div>