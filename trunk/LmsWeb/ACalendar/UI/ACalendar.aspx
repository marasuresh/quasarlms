<%@ Page Language="C#" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master" 
AutoEventWireup="true" CodeBehind="ACalendar.aspx.cs" Inherits="ACalendar_UI_ACalendar" %>


<%@ Register assembly="N2.Futures" namespace="N2.Web.UI.WebControls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">


<%--
<input id="event_data_in" type="text" value = "{'act': 'п', 'dateStart': 1231106400000,'dateEnd':1231711199000},{'act': 'п', 'dateStart': 1236549600000,'dateEnd':1237154399000}" />
<input id="event_data_out" type="text" />
--%>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" visible="false">   
     <script type="text/javascript" src="~/Lms/UI/Js/jQuery.intellisense.js" ></script>  
     </asp:PlaceHolder>

	<script type ="text/javascript">//(new Date().getMonth < 8)?new Date().getFullYear-1:
	    $(document).ready(function() {
	        $('#calOne').jCal({
	            day: new Date( <%= this.CurrentYear()%> , 8,1),  // текущий год ставим (надо проверять еще месяц ) 
	            days: 7,
	            showMonths: 12,
	            forceWeek: true, // force full week selection
	            dayOffset: 1, 	// start week on Monday
	            dow: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
	            ml: <%= this.MonthName(true)%> ,
	            ms: <%= this.MonthName(false)%> ,
	            //	            dCheck: function(day) {
	            //	                return (day.getDate() != 3);
	            //	            },
	            defaultBG:      'rgb(255, 255, 255)', 
	            selectedBG:     'rgb(255, 255, 255)',   
	            callback: function(day, days) 
	            {
                  if (<%=this.IsEditable.ToString().ToLower()%>)
                  {
	                var MinMilli = 1000 * 60;       //Initialize variables.
	                var HrMilli = MinMilli * 60;
	                var DyMilli = HrMilli * 24;
	                var YearMilli = DyMilli * 365;
 // comment: 604800000=1000millisec*60sec*60min*24hour*7day
 
	                //записываем данные
	                var dateEnd = new Date(day.valueOf()); // + 3600000+ one hour
	                dateEnd.setDate(dateEnd.getDate() + days);
	                var str_dateStart =day.getFullYear()  + '/' + (day.getMonth() + 1) + '/' + (day.getDate() )
	                var str_dateEnd =dateEnd.getFullYear()  + '/' + (dateEnd.getMonth() + 1) + '/' + dateEnd.getDate()
	                var _act = $('#eventtype').val().substr(0, 1);
                      var message = '{"act": "' + _act + '", "dateStart":"' + str_dateStart + '","dateEnd":"' + str_dateEnd + '"}'; 
                      var context = 'add'; 
                       //вызываем WebForm_DoCallback 
                        <%= sCallBack %>  

	                //show_event(_result);

	                //отмечаем на календаре
	                var dCursor = new Date(day.getTime());
	                for (var di = 0; di < days; di++) 
	                {
	                    var currDay = $(this._target).find('[id*=d_' + (dCursor.getMonth() + 1) + '_' + dCursor.getDate() + '_' + dCursor.getFullYear() + ']');
                        if (currDay.length) 
                        {
                        currDay.find("div").remove();
	                    if (_act != "у") currDay.append('<div class="dInfo" style="background-color: #' + colors(_act) + '"><span style="color:#ccc"></span>' + _act + '</div>');
	                    }
	                    dCursor.setDate(dCursor.getDate() + 1);
	                }
	                
	                
	                
	              }
	 	        }    
	        })
	    }).ready(function() {
	        show_event();
	    });


	</script>
	
    <style  >
        .dInfo {
            font-family:tahoma;
            font-size:7pt;
            color:#fff;
            padding-top:1px;
            padding-bottom:1px;
            background:rgb(0, 102, 153);
        }
    </style>
    
<script type ="text/javascript">

    function  colors( currentAct) {
    	        var _color = '0000FF';
	            if (currentAct == 'п') _color = '90FF90'; 
	            if (currentAct == 'э') _color = 'FFC0C0'; 
	            if (currentAct == 'в') _color = 'B0B000'; 
	            if (currentAct == 'а') _color = 'FFD080'; 
	            if (currentAct == 'к') _color = '87CEFA';  
                return _color;
    }

    function TikeInformation(infstring, context) {
        //alert('Info:\n' + infstring);
    } 


    function show_event() {

        var events_string = '<%= data_in%>';
        var oevent = eval("([" + events_string + "])");
        if (oevent.length == 0) return;

        //                    $.each(oevent, function(elem){
        //                        
        //                    });
        //
        var MinMilli = 1000 * 60;       //Initialize variables.
        var HrMilli = MinMilli * 60;
        var DyMilli = HrMilli * 24;
        for (var i = 0; i < oevent.length; i++) {
            if (oevent[i] != null) {
                var currentAct = oevent[i].act;
                var currentStart = new Date(oevent[i].dateStart);
                var currentEnd = new Date(oevent[i].dateEnd);
                //var timeSpan = currentEnd - currentStart;
                var _days = (currentEnd.valueOf() - currentStart.valueOf()) / DyMilli; // надо округлить
                var _color = colors(currentAct);

                for (var j = 0; j < _days; j++) {

                    var currDay = $('#calOne').find('[id*=d_' + (currentStart.getMonth() + 1) + '_' + currentStart.getDate() + '_' + currentStart.getFullYear() + ']');
                    if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #' + _color + '"><span style="color:#ccc"></span>' + currentAct + '</div>');
                    currentStart.setDate(currentStart.getDate() + 1);
                }
            }
        }
    }


//    	        var all_data = '{"act": "п", "dateStart": 1231106400000,"dateEnd":1231711199000},{"act": "п", "dateStart": 1236549600000,"dateEnd":1237154399000}';
//    	        //var all_data =  $("event_data_in").val() ;
//    	        all_data = '[' + all_data + ']';
//            var oevent = eval("(" + all_data + ")");
//    
//    //	        //                    $.each(oevent, function(elem){
//    //	        //                        
//    //	        //                    });
//    //	        //

//    	        for (var i = 0; i < oevent.length; i++) {

//    	            var currentAct = oevent[i].act;
//    	            var currentStart = new Date(oevent[i].dateStart);
//    	            var currentEnd = oevent[i].dateEnd;
//    	            var _days = (oevent[i].dateEnd - oevent[i].dateStart) / 86400000; // надо округлить
//    	            for (var j = 0; j < _days; j++) {
//    	                var currDay = $('#calOne').find('[id*=d_' + (currentStart.getMonth() + 1) + '_' + currentStart.getDate() + '_' + currentStart.getFullYear() + ']');
//    	                if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #0000FF"><span style="color:#ccc"></span>' + currentAct + '</div>');
//    	                currentStart.setDate(currentStart.getDate() + 1);
//    	            }
//    	        }
//    ;

    $(function() {
        $('#weeks').change(function() {
            $('#calOne').data('days', $('#weeks').val() * 7);
            
        });

        //    $('#btnShow').click(function(){
        //        show();
        //    });
    });

	</script>
<n2:ChromeBox ID="ChromeBox1" runat="server">

	<table width="100%">
        <% if (this.IsEditable)
      { %>
			<tr>
   				            <td align=left id="Td1" valign=top style="padding:10px; background:#E3E3E3;">
			            Деятельность
			            <select id="eventtype" >
				            <option value="полевой выход" selected >полевой выход</option><option value="экзаменационная сессия">экзаменационная сессия</option><option value="каникулы, отпуск">каникулы, отпуск</option>
				            <option value="войсковая стажировка" >войсковая стажировка</option><option value="аттестационная комиссия">аттестационная комиссия</option><option value="учеба">учеба</option>
			            </select>				

				            </td>

				            <td align=left id="weekSelect" valign=top style="padding:10px; background:#E3E3E3;">
            		

	                  &nbsp;Кол-во недель
			            <select id="weeks">
				            <option value="1" selected>1</option><option value="2">2</option><option value="3">3</option>
				            <option value="4" >4</option><option value="5">5</option>
			            </select>				
				            </td>             
			</tr>
			<% } %>
		<tr>
			<td colspan=2 align=left id="calOne" valign=top style="padding:10px; background:#E3E3E3;">
				loading calendar
			    </td>
		</tr>


        <% if (this.IsEditable)
      { %>
        
        <tr ><%--style="background-position: #E3E3E3; padding: 10px; background: #E3E3E3; visibility: hidden;"--%>
				<td align=left id="calOneResult" valign=top style="padding:10px; background:#E3E3E3;">
 
<%--    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Сохранить все" />
--%>
<% if (this.AEvents.Length > 0)
   {
       int i = 1;
        %>
    <% foreach (var _ev in this.AEvents)
              { %>
       <div>
          <%= _ev.Title%> &nbsp;<%= ConvertDateString( _ev.DateStart) + " - " + ConvertDateString(_ev.DateEnd)%>&nbsp;</div>
	<% 
    i++;
              }
   } %>

				</td>
				
	<% } %>			
		        <td align=left id="Td2" valign=top style="padding:10px; background:#E3E3E3;">
    <table style="width: 100%;">
        <tr>
            <td style="background: #90FF90;">
                п
            </td>
            <td>
                &nbsp;&nbsp;полевой выход
            </td>
        </tr>
        <tr>
            <td style="background: #FFC0C0;">
                э
            </td>
            <td>
                &nbsp;&nbsp;экзаменационная сессия
            </td>
        </tr>
        <tr>
            <td style="background: #87CEFA;">
                к
            </td>
            <td>
                &nbsp;&nbsp;каникулы, отпуск
            </td>
        </tr>
        <tr>
            <td style="background: #B0B000;">
                в
            </td>
            <td>
                &nbsp;&nbsp;войсковая стажировка
            </td>
        </tr>
        <tr>
            <td style="background: #FFD080;">
                а
            </td>
            <td>
                &nbsp;&nbsp;аттестационная комиссия
            </td>
        </tr>
        <tr>
            <td style="background: #FFFFFF;">
            </td>
            <td>
                &nbsp;&nbsp;учеба
            </td>
        </tr>
    </table>
    
                
<%--				    <asp:HyperLink ID="linkExcel" runat="server"></asp:HyperLink>
   --%>                  
				</td>				
			
			</tr>
						

	</table>

  </n2:ChromeBox>


    </asp:Content>



