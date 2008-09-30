<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master" AutoEventWireup="true" CodeFile="ACalendar.aspx.cs" Inherits="ACalendar_UI_ACalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">


	<% if (this.AEvents.Length > 0)
    {
        int i = 1;
        %>
<asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" Text="Excel" /> 
<%--<%= Server.MapPath("../../Upload")%>
--%> <table>
        <tr><th>Начало</th><th>Конец</th><th>&nbsp;&nbsp;Активность</th><th>&nbsp;&nbsp;&nbsp;&nbsp;Описание</th></tr>
	<% foreach (var _ev in this.AEvents)
    { %>
        <tr>
             <td><%= _ev.WeekStart %></td>
             <td><%= _ev.WeekEnd%></td>
             <td><%= "   " + _ev.Title%></td>
             <td> &nbsp;&nbsp;&nbsp;&nbsp; <%= _ev.Description%></td>
             </tr>
           
	<% 
        i++;
    } %>
	</table>
	<% }
    else
    { %>
    no weeks
	<% } %>

	<script>
	    $(document).ready(function() {
	        $('#calOne').jCal({
	            day: new Date(),
	            days: 7,
	            showMonths: 9,
	            forceWeek: true, // force full week selection
	            dayOffset: 1, 	// start week on Monday
	            dow: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
	            ml: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
	            ms: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
	            //	            dCheck: function(day) {
	            //	                return (day.getDate() != 3);
	            //	            },
	            callback: function(day, days) {
	                var MinMilli = 1000 * 60;       //Initialize variables.
	                var HrMilli = MinMilli * 60;
	                var DyMilli = HrMilli * 24;
	                var dateEnd = new Date(day.valueOf() + (1000 * 60 * 60 * 24 * days) - 1000);
	                $('#calOneResult').append('<div style="clear:both; font-size:7pt;">' + $('#eventtype').val() + ' ' + days / 7 + ' неделя с ' +
							day.getDate() + '/' + (day.getMonth() + 1) + '/' + day.getFullYear() + ' по ' + dateEnd.getDate() + '/' + (dateEnd.getMonth() + 1) + '/' + dateEnd.getFullYear() + '</div>');
	                return true;
	            }
	        });
	    });
	</script>



	<table width="100%">
			<tr>
				<td align=left id="Td1" valign=top style="padding:10px; background:#E3E3E3;">
			Деятельность
			<select id="eventtype" >
				<option value="полевой выход" selected >полевой выход</option><option value="экзаменационная сессия">экзаменационная сессия</option><option value="каникулы, отпуск">каникулы, отпуск</option>
				<option value="войсковая стажировка" >войсковая стажировка</option><option value="аттестационная комиссия">аттестационная комиссия</option>
			</select>				
<%--				</td>

				<td align=left id="weekSelect" valign=top style="padding:10px; background:#E3E3E3;">
--%>		

	      &nbsp;&nbsp;&nbsp;&nbsp;Кол-во недель
			<select id="weeks" onchange = "$('#calOne').data('days', $('#weeks').val()*7);">
				<option value="1" selected>1</option><option value="2">2</option><option value="3">3</option>
				<option value="4" >4</option><option value="5">5</option>
			</select>				
				</td>
			<tr>
		<tr>
			<td align=left id="calOne" valign=top style="padding:10px; background:#E3E3E3;">
				loading calendar one
			</td>
		<tr>
			<tr>
				<td align=left id="calOneResult" valign=top style="padding:10px; background:#E3E3E3;">
				<asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Записать" /> 
				</td>
			
			<tr>
	</table>

	<div id="ttt"></div>


</asp:Content>



