	//日期选择控件
	
	//公共变量
	var Today = new Date();
	var TheYear = Today.getYear();
	if (TheYear < 1900) TheYear+=1900;
	var TheMonth = Today.getMonth()+1;
	var TheDate = Today.getDate();
	var TheDay = Today.getDay();
	var xYear = TheYear;
	var xMonth = TheMonth;
	var xDate = TheDate;
	
	//是否闰年
	function RunNian(The_Year){
 		if ((The_Year%400==0) || ((The_Year%4==0) && (The_Year%100!=0)))
  			return true;
 		else
  			return false;
	}
	
	//获取月份天数
	function GetMonthDays(xYear,xMonth) {
		var monDays = 0;
		switch (xMonth){
			case 1 : monDays = 31; break;
	 		case 2 : 
	  			if (RunNian(xYear)) monDays = 29;
				else monDays = 28;
	  			break;
		    case 3 : monDays = 31; break;
		    case 4 : monDays = 30; break;
		    case 5 : monDays = 31; break;
		    case 6 : monDays = 30; break;
		    case 7 : monDays = 31; break;
		    case 8 : monDays = 31; break;
		    case 9 : monDays = 30; break;
		    case 10 : monDays = 31; break;
		    case 11 : monDays = 30; break;
		    case 12 :  monDays = 31; break;
		}
		return monDays;
	}
	
	//前一年
	function PrevYear(cBox) {
		xYear -=1;
		ShowCanlender(cBox);
	}
	
	//后一年
	function NextYear(cBox) {
		xYear +=1;
		ShowCanlender(cBox);
	}
	
	//前一月
	function PrevMonth(cBox) {
		xMonth -=1;
		if (xMonth <= 0) { 
			xMonth = 12;
			xYear -=1;
		}
		ShowCanlender(cBox);
	}
	
	//后一月
	function NextMonth(cBox) {
		xMonth +=1;
		if (xMonth >= 13) { 
			xMonth = 1;
			xYear +=1;
		}
		ShowCanlender(cBox);
	}
	
	//选择日期
	function ChooseDate(choYear,choMonth,choDate,xBox) {
		xYear = choYear;
		xMonth = choMonth;
		xDate = choDate;
		SetCanlenderValue(xBox);
	}
	
	//显示日历
	function ShowCanlender(cBox) {
		var strShow,y,x
		var monDays = GetMonthDays(xYear,xMonth);				//日份月天数
		var firDate = new Date(xYear,xMonth-1,1);				//月份第一天
		var firDay = firDate.getDay();									//月份第一天星期
		var endDate = new Date(xYear,xMonth-1,monDays);	//月份最后一天
		var endDay = endDate.getDay();									//月份最后一天星期
		var canDays = firDay + monDays + (6-endDay);		//日历显示天数
		var canRows = canDays/7;												//日历行数
		var starDay = 0-firDay+1;												//日历显示第一天日期
		var xDay;
		
		strShow = "<table border=0 bgcolor='#ffffff' cellpadding=2 cellspacing=0><tr><td>";
		strShow += "<table border=0 width='100%' cellpadding=0 cellspacing=0><tr align='center'>";
		strShow += "<td><a href=javascript:PrevYear('"+cBox+"')><img src='/UI/images/first.gif' border=0 align='absmiddle'></a></td>";
		strShow += "<td><a href=javascript:PrevMonth('"+cBox+"')><img src='/UI/images/previous.gif' border=0 align='absmiddle'></a></td>";
		strShow += "<td align='center' style='cursor:default'>"+xYear+"年"+xMonth+"月</td>";
		strShow += "<td><a href=javascript:NextMonth('"+cBox+"')><img src='/UI/images/next.gif' border=0 align='absmiddle'></a></td>";
		strShow += "<td><a href=javascript:NextYear('"+cBox+"')><img src='/UI/images/last.gif' border=0 align='absmiddle'></a></td>";
		strShow += "</tr></table>";
		strShow += "<table border=0 cellpadding=2 cellspacing=0><tr align='center' bgcolor='#fef0d8'>"
		strShow += "<td style='cursor:default;color:#ff0000'>日</td>"
		strShow += "<td style='cursor:default'>一</td>"
		strShow += "<td style='cursor:default'>二</td>"
		strShow += "<td style='cursor:default'>三</td>"
		strShow += "<td style='cursor:default'>四</td>"
		strShow += "<td style='cursor:default'>五</td>"
		strShow += "<td style='cursor:default;color:#00cc00'>六</td>"
		strShow += "</tr>"
		for (y=1;y<=canRows;y++) {
			strShow += "<tr align='center'>";
			for (x=0;x<7;x++) {
				if (starDay>=1 && starDay <= monDays) {
					xDay = new Date(xYear,xMonth-1,starDay);
					
					if (xDate == starDay)
						strShow += "<td bgcolor='#d2d2d2'";
					else
						strShow += "<td bgcolor='#ffffff'";
					
					strShow += " style='padding-top:0px;padding-bottom:0px'>";
					
					if (TheYear == xYear && TheMonth == xMonth && TheDate == starDay)
						strShow += "<a style='color:#ff0000'"
					//else if (xDay > Today)
					//	strShow += "<a style='color:#c0c0c0'"
					else
						strShow += "<a style='color:#313131'"
					
					strShow += " href=javascript:ChooseDate("+xYear+","+xMonth+","+starDay+",'"+cBox+"')>"+starDay+"</a></td>";
				}
				else strShow += "<td style='cursor:default' style='padding-top:1px;padding-bottom:1px'>&nbsp;</td>";
				starDay +=1
			}
			strShow += "</tr>";
		}
		strShow += "</table></td></tr></table>";
		
		document.getElementById(cBox).innerHTML = strShow;
	}
	
	function OpenCanlender(xBox) {
		var cBox = document.getElementById(xBox);
		var vText = document.getElementById(xBox + "Date");
		
		if (cBox.style.visibility == "visible") {
			cBox.style.visibility = "hidden";
		}
		else {
			if (checkDate(vText.value)) {
				xYear = Number(vText.value.substring(0,4));
				xMonth = Number(vText.value.substring(5,vText.value.lastIndexOf("-")));
				xDate = Number(vText.value.substring(vText.value.lastIndexOf("-")+1));
			}
			else
			{
				xYear = Today.getYear();
				xMonth = Today.getMonth()+1;
				xDate = Today.getDate();
			}
			cBox.style.visibility = "visible";
			ShowCanlender(xBox);
		}
	}
	
	function SetCanlenderValue(xBox){
		var cBox = document.getElementById(xBox);
		var vText = document.getElementById(xBox + "Date");
		if (cBox.style.visibility == "visible") {
			cBox.style.visibility = "hidden";
			vText.value = xYear + "-" + xMonth + "-" + xDate;
		}
	}
	
	function CloseCanlender(xBox) {
		var cBox = document.getElementById(xBox);
		//var vText = document.getElementById(xBox + "Date");
		if (cBox.style.visibility == "visible") {
			cBox.style.visibility = "hidden";
			//vText.value = xYear + "-" + xMonth + "-" + xDate;
		}
	}

    // 检查日期
    function checkDate(str)
    {
	    var arr=str.match(/^(19|20)\d{2}-([1-9]|0[1-9]|10|11|12)-([1-9]|0[1-9]|1[0-9]|2[0-9]|3[0-1])$/g);
	    if (arr!=null && arr[0]==str)
		    return true;
	    else 
		    return false;
    }