<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master" AutoEventWireup="true" CodeFile="ACalendar.aspx.cs" Inherits="ACalendar_UI_ACalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">


<%--
<input id="event_data_in" type="text" value = "{'act': 'п', 'dateStart': 1231106400000,'dateEnd':1231711199000},{'act': 'п', 'dateStart': 1236549600000,'dateEnd':1237154399000}" />
<input id="event_data_out" type="text" />
--%>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" visible="false">   
     <script type="text/javascript" src="~/Lms/UI/Js/jQuery.intellisense.js" ></script>  
     </asp:PlaceHolder>

	<script type ="text/javascript">
	    $(document).ready(function() {
	        $('#calOne').jCal({
	            day: new Date(2008, 8,1 ),
	            days: 7,
	            showMonths: 12,
	            forceWeek: true, // force full week selection
	            dayOffset: 1, 	// start week on Monday
	            dow: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
	            ml: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
	            ms: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
	            //	            dCheck: function(day) {
	            //	                return (day.getDate() != 3);
	            //	            },
	            defaultBG:      'rgb(255, 255, 255)', 
	            selectedBG:     'rgb(255, 255, 255)',   
	            callback: function(day, days) 
	            {
	                var MinMilli = 1000 * 60;       //Initialize variables.
	                var HrMilli = MinMilli * 60;
	                var DyMilli = HrMilli * 24;
	                var YaerMilli = DyMilli * 365;

	                //записываем данные вниз таблицы
	                var dateEnd = new Date(day.valueOf()); // + (1000 * 60 * 60 * 24 * days) );
	                dateEnd.setDate(dateEnd.getDate() + days);
	                var str_dateStart = day.getFullYear() + '/' + (day.getMonth() + 1) + '/' + day.getDate()
	                var str_dateEnd = dateEnd.getFullYear() + '/' + (dateEnd.getMonth() + 1) + '/' + dateEnd.getDate()
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

	                    if (_act == "у") {
	                        if (currDay.length) currDay.find("div").remove();
	                    }
	                    else {
	                        if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #' + colors(_act) + '"><span style="color:#ccc"></span>' + _act + '</div>');
	                    }
	                    dCursor.setDate(dCursor.getDate() + 1);
	                }
	 	        }    
	        })
	    }).ready(function() {

	        show_event();
	    })


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
	            if (currentAct == 'п') _color = '55FF00'; //салатовый
	            if (currentAct == 'э') _color = 'FFFF00'; //красный
	            if (currentAct == 'в') _color = '0000FF'; //синий
	            if (currentAct == 'а') _color = 'ВС143С'; //светло-коричневый
	            if (currentAct == 'к') _color = '87CEFA';  //голубой
                return _color;
    }

    function TikeInformation(infstring, context) {
        //alert('Info:\n' + infstring);
    } 


    function show_event() {

        var events_string = '<%= data_in%>';
        var oevent = eval("([" + events_string + "])");

        //                    $.each(oevent, function(elem){
        //                        
        //                    });
        //
        var MinMilli = 1000 * 60;       //Initialize variables.
        var HrMilli = MinMilli * 60;
        var DyMilli = HrMilli * 24;

        for (var i = 0; i < oevent.length; i++) {

            var currentAct = oevent[i].act;
            var currentStart = new Date(oevent[i].dateStart);
            var currentEnd = new Date(oevent[i].dateEnd);
            //var timeSpan = currentEnd - currentStart;
            var _days = (currentEnd.valueOf() - currentStart.valueOf()) / DyMilli; // надо округлить

            //	            var _color = '0000FF';
            //	            if (currentAct == 'п') _color = '55FF00'; //салатовый
            //	            if (currentAct == 'э') _color = 'FFFF00'; //красный
            //	            if (currentAct == 'в') _color = '0000FF'; //синий
            //	            if (currentAct == 'а') _color = 'ВС143С'; //светло-коричневый
            //	            if (currentAct == 'к') _color = '87CEFA';  //голубой


            var _color = colors(currentAct);

            for (var j = 0; j < _days; j++) {

                var currDay = $('#calOne').find('[id*=d_' + (currentStart.getMonth() + 1) + '_' + currentStart.getDate() + '_' + currentStart.getFullYear() + ']');
                if (currDay.length) currDay.append('<div class="dInfo" style="background-color: #' + _color + '"><span style="color:#ccc"></span>' + currentAct + '</div>');
                currentStart.setDate(currentStart.getDate() + 1);
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


	<table width="100%">
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
			<tr>
		<tr>
			<td colspan=2 align=left id="calOne" valign=top style="padding:10px; background:#E3E3E3;">
				loading calendar one
			</td>
		</tr>
        <tr>
				<td align=left id="calOneResult" valign=top style="padding:10px; background:#E3E3E3;">

<% if (this.AEvents.Length > 0)
   {
       int i = 1;
        %>
    <% foreach (var _ev in this.AEvents)
              { %>
       <div>
          <%= _ev.Title%> &nbsp;&nbsp;/ <%= (_ev.DateStart)%> - <%= _ev.DateEnd%>
       </div>
	<% 
    i++;
              }
   } %>

				</td>
		        <td align=left id="Td2" valign=top style="padding:10px; background:#E3E3E3;">
<%--			<asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" Text="Записать" /> 
				<button ID="btnShow" >Показать</button> 
--%>                
                <asp:Button ID="btnExcel" runat="server" onclick="btnExcel_Click" Text="Excel" /> 


				</td>				
			
			</tr>
	</table>




</asp:Content>



