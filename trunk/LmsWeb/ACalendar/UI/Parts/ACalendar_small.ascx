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
//	    $(document).ready(function() {
//	    $('#calOne').jCal
//	        ({
//	            day: new Date(),
//	            days: 1,
//	            showMonths: 1,
//	            forceWeek: false, // force full week selection
//	            dayOffset: 1, 	// start week on Monday
//	            dow: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
//	            ml: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
//	            ms: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек']
//             })
//})

//	            ,
//	            callback: function(day, days) {
//	                var MinMilli = 1000 * 60;       //Initialize variables.
//	                var HrMilli = MinMilli * 60;
//	                var DyMilli = HrMilli * 24;
//	                var YaerMilli = DyMilli * 365;

//	                //отмечаем на календаре
//	                var dCursor = new Date(day.getTime());
//	                for (var di = 0; di < days; di++) {
//	                    var currDay = $(this._target).find('[id*=d_' + (dCursor.getMonth() + 1) + '_' + dCursor.getDate() + '_' + dCursor.getFullYear() + ']');

//	                        if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #' + colors("п") + '"><span style="color:#ccc"></span>' + 'п' + '</div>');
//	                    dCursor.setDate(dCursor.getDate() + 1);
//	                }


//	            }
//	        })
////	    }).ready(function() {

//	        //show_event(all_data);


//	    });



	</script>

<script type ="text/javascript">

    function  colors( currentAct) {
    	        var _color = '0000FF';
	            if (currentAct == 'п') _color = '55FF00'; //салатовый
	            if (currentAct == 'э') _color = 'FFFF00'; //красный
	            if (currentAct == 'в') _color = '0000FF'; //синий
	            if (currentAct == 'а') _color = 'ВС143С'; //светло-коричневый
	            if (currentAct == 'к') _color = '87CEFA';  //голубой
                return _color;
    }

    function show_event(events_string) {

        var oevent = eval("([" + events_string + "])");

        var MinMilli = 1000 * 60;       //Initialize variables.
        var HrMilli = MinMilli * 60;
        var DyMilli = HrMilli * 24;

        for (var i = 0; i < oevent.length; i++) {

            var currentAct = oevent[i].act;
            var currentStart = new Date(oevent[i].dateStart);
            var currentEnd = new Date(oevent[i].dateEnd);
            var _days = (currentEnd.valueOf() - currentStart.valueOf()) / DyMilli; // надо округлить
            var _color = colors(currentAct);

            for (var j = 0; j < _days; j++) {

                var currDay = $('#calOne').find('[id*=d_' + (currentStart.getMonth() + 1) + '_' + currentStart.getDate() + '_' + currentStart.getFullYear() + ']');
                if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #' + _color + '"><span style="color:#ccc"></span>' + currentAct + '</div>');
                currentStart.setDate(currentStart.getDate() + 1);
            }
        }
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
	                var dCursor = new Date(day.getTime());
	                for (var di = 0; di < days; di++) {
	                    var currDay = $(this._target).find('[id*=d_' + (dCursor.getMonth() + 1) + '_' + dCursor.getDate() + '_' + dCursor.getFullYear() + ']');
	                    if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #' + colors("э") + '"><span style="color:#ccc"></span>' + 'e' + '</div>');
	                    dCursor.setDate(dCursor.getDate() + 1);
	                }
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


<%--
<%@ Import Namespace="N2.Messaging"%>
<%@ Import Namespace="N2.Web"%>
<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="NewMessageList.ascx.cs" 
    Inherits="N2.Messaging.Messaging.UI.Parts.NewMessageList" %>

<n2:EditableDisplay ID="EditableDisplay1" runat="server" PropertyName="Title" />
<n2:Box ID="Box2" runat="server">
    <div class="part">
    <n2:ItemDataSource id="idsNews" runat="server" />
    <n2:Repeater ID="Repeater1" runat="server" DataSourceID="idsNews">
        <HeaderTemplate><div class="sidelist"></HeaderTemplate>
        <ItemTemplate>
            <div>
                <table style="width:100%" >
                    <tr>
                        <td style="width:10%; vertical-align:middle" align="left">
                            <asp:Image ID="msgTypeImage" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
                        </td>
                        <td style="width:1%"></td>
                        <td style="vertical-align:middle; text-align:center; width:auto">
                            <a href='<%# Eval("Url") %>' title='<%#Eval("From") + "   " + Eval("Published") %>'><label class="<%# Eval("TypeOfMessage") %>"><%# Eval("Subject") %></label></a>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
        <FooterTemplate></div></FooterTemplate>
        <EmptyTemplate>
            <div style="text-align:center">Новых сообщений нет</div>
        </EmptyTemplate>
    </n2:Repeater>
    <br />
    <div style="text-align:center">
        <asp:HyperLink ID="hlMailBox" runat="server">Все сообщения</asp:HyperLink>    
    </div>
    </div>
</n2:Box>--%>