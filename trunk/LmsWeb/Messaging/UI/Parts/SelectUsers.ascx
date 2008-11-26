<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectUsers.ascx.cs"
    Inherits="N2.Messaging.Messaging.UI.Parts.SelectUsers" %>
<script type="text/javascript" src="../Js/jQuery.intellisense.js"></script>

<script type="text/javascript">
    $(document).ready(
	function() {
        $('.btnSelectUsersInUserTree').hide();
        $('.PUpWindow #windowMin').hide();
	    $('div.ut > table :checkbox').bind('click',
	        function() {
	            var checkedUsers = Array();
	            $('div.ut > table').each(
	                function() {
	                    if ($(this).find(':checked').length)
	                        checkedUsers.push($(this).find('span').text());
	                });
	                $('.openPUpWindowControl').val(checkedUsers.join(';'));
	        });
	});
</script>

<asp:TextBox ID="windowOpen" runat="server" Width="100%"></asp:TextBox>
<n2:PopUpWindow ID="PopUpWindow1" runat="server" Title="Выбор адресата..." AssociatedControlID="windowOpen"
    AllignTo="AssociatedControl" Height="235px" Width="200px">
    <n2:UserTree ID="UserTree1" runat="server" AllowMultipleSelection="true" SelectionMode="Users"
        DisplayMode="Users" />
</n2:PopUpWindow>
