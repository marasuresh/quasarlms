<%@ Control Language="C#" CodeBehind="ACalendar_small.ascx.cs" %>
<%@ Import Namespace="N2.Resources" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        Register.JQuery(this.Page);
        Register.StyleSheet(this.Page, "~/ACalendar/UI/Js/jCal.small.css");
        Register.JavaScript(this.Page, "~/ACalendar/UI/Js/jQuery.animate.clip.js");
        Register.JavaScript(this.Page, "~/ACalendar/UI/Js/jQuery.color.js");
        Register.JavaScript(this.Page, "~/ACalendar/UI/Js/jCal.js");
        Register.JavaScript(this.Page, "~/ACalendar/UI/Js/json2.js");
        //Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.intellisense.js");

        base.OnInit(e);
    }
</script>
<n2:EditableDisplay ID="EditableDisplay2" runat="server" PropertyName="Title" />

<n2:Box ID="Box1" runat="server">

<script type ="text/javascript">


    function show_event(events_string) {

        var oevent = eval("([" + events_string + "])");
        if (oevent.length == 0) return;
        var MinMilli = 1000 * 60;       //Initialize variables.
        var HrMilli = MinMilli * 60;
        var DyMilli = HrMilli * 24;


    }
 


	</script>



	<script type ="text/javascript">
	    $(document).ready(function() {
	        $('#calOne').jCal({
	            day: new Date(),
	            days: 1,
	            showMonths: 1,
	            forceWeek: false, // force full week selection
	            dayOffset: 1, 	// start week on Monday
	            dow: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
	            ml: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
	            ms: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
	            callback: function(day, days) {

	                return false;
	            }
	        });
	    });
	</script>

<table width="100%">
		<tr>
			<td align="left" id="calOne" style="padding:10px; background:#E3E3E3;">
				loading calendar one
			</td>
		</tr>

	</table>	
 
	
	
<%--	<n2:EditableDisplay runat="server" id="t" PropertyName="Text" />
--%>	
</n2:Box>


