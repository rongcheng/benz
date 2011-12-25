function SetupJSCalendarLanguage(a_lang)
{
	if((a_lang==null) || (a_lang=='')) return;
	
	switch(a_lang)
	{
		case 'en-US':
			SetupJSCalendarUS();
			break;
		case 'zh-CN':
			SetupJSCalendarChinese();
			break;
	}
}

function SetupJSCalendarUS()
{
/*
* ** I18N
* Calendar EN language
* Author: John
* Encoding: any
* Distributed under the same terms as the calendar itself.
*
* For translators: please use UTF-8 if possible.  We strongly believe that
* Unicode is the answer to a real internationalized world.  Also please
* include your contact information in the header, as can be seen above.
*
* lang : en-US
*/
/*
* Please note that the following array of short day names (and the same goes
* for short month names, _SMN) isn't absolutely necessary.  We give it here
* for exemplification on how one can customize the short day names, but if
* they are simply the first N letters of the full name you can simply say:
*
*   Calendar._SDN_len = N; // short day name length
*   Calendar._SMN_len = N; // short month name length
*
* If N = 3 then this is not needed either since we assume a value of 3 if not
* present, to be compatible with translation files that were written before
* this feature.
*/

// short day names
Calendar._SDN = new Array
("SUN",		//SUNDAY
 "MON",		//MONDAY
 "TUS",		//TUESDAY
 "WEN",		//WENSEDAY
 "THUR",	//THURSDAY
 "FRI",		//FRIDAY
 "SAT",		//SATURDAY
 "SUN");	//SUNDAY

// full day names
Calendar._DN = new Array
("Sunday",		//Sunday
 "Monday",		//Monday
 "Tuesday",		//Tuesday
 "Wenseday",	//Wenseday
 "Thursday",	//Thursday
 "Friday",		//Friday
 "Saturday",	//Saturday
 "Sunday");		//Sunday

// short month names
Calendar._SMN = new Array
("JAN",		//JANUARY
 "FEB",		//FEBRUARY
 "MAR",		//MARCH
 "APR",		//APPIL
 "MAY",		//MAY
 "JUN",		//JUNE
 "JUL",		//JULY
 "AUG",		//AUGUST
 "SEP",		//SEPTEMBER
 "OCT",		//OCTOBER
 "NOV",		//NOVEMBER
 "DEC");	//DECEMBER

// full month names
Calendar._MN = new Array
("January",			//January
 "February",		//February
 "March",			//March
 "Appil",			//Appil
 "May",				//May
 "June",			//June
 "July",			//July
 "August",			//August
 "September",		//September
 "October",			//October
 "November",		//November
 "December");		//December

// tooltips
Calendar._TT = {};
Calendar._TT["INFO"] = "Help";

Calendar._TT["ABOUT"] =

"DHTML Date/Time Selector\n" +
"Author: John \n" + // don't translate this this ;-)
"2006-02-01 version 1.0\n\n" +
"Select Date:\n" +
"- Click \xab \xbb button to select Year. \n" +
"- Click " + String.fromCharCode(0x2039) + String.fromCharCode(0x203a) + " button to select Month. \n" +
"- Click button and Holding to select Year or Month from menu list. ";
Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Select Time:\n" +
"- 点击小时或分钟可使改数值加一\n" +
"- 按住Shift键点击小时或分钟可使改数值减一\n" +
"- 点击拖动鼠标可进行快速选择";

Calendar._TT["PREV_YEAR"] = "Previous Year (Holding to show menu)";
Calendar._TT["PREV_MONTH"] = "Previous Month (Holding to show menu)";
Calendar._TT["GO_TODAY"] = "Today";
Calendar._TT["NEXT_MONTH"] = "Next Month (Holding to show menu)";
Calendar._TT["NEXT_YEAR"] = "Next Year (Holding to show menu)";
Calendar._TT["SEL_DATE"] = "Select Date";
Calendar._TT["DRAG_TO_MOVE"] = "Drag to move";
Calendar._TT["PART_TODAY"] = "Today";
Calendar._TT["MON_FIRST"] = "The First Monday";
Calendar._TT["SUN_FIRST"] = "The First Sunday";

// the following is to inform that "%s" is to be the first day of week
// %s will be replaced with the day name.
Calendar._TT["DAY_FIRST"] = "'%s'  Show at left";

// This may be locale-dependent.  It specifies the week-end days, as an array
// of comma-separated numbers.  The numbers are from 0 to 6: 0 means Sunday, 1
// means Monday, etc.
Calendar._TT["WEEKEND"] = "0,6";

Calendar._TT["CLOSE"] = "Close";
Calendar._TT["TODAY"] = "Today";
//Calendar._TT["TIME_PART"] = "(Shift-)?? ?? ??? ???";

// date formats
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d";
Calendar._TT["TT_DATE_FORMAT"] = "%a, %b %e";

Calendar._TT["WK"] = "Week";
Calendar._TT["TIME"] = "Time:";

}

function SetupJSCalendarChinese()
{
// ** I18N

// Calendar ZH language
// Author: John
// Encoding: GB2312 or GBK
// Distributed under the same terms as the calendar itself.

// full day names
Calendar._DN = new Array
("星期日",
 "星期一",
 "星期二",
 "星期三",
 "星期四",
 "星期五",
 "星期六",
 "星期日");

// Please note that the following array of short day names (and the same goes
// for short month names, _SMN) isn't absolutely necessary.  We give it here
// for exemplification on how one can customize the short day names, but if
// they are simply the first N letters of the full name you can simply say:
//
//   Calendar._SDN_len = N; // short day name length
//   Calendar._SMN_len = N; // short month name length
//
// If N = 3 then this is not needed either since we assume a value of 3 if not
// present, to be compatible with translation files that were written before
// this feature.

// short day names
Calendar._SDN = new Array
("日",
 "一",
 "二",
 "三",
 "四",
 "五",
 "六",
 "日");

// full month names
Calendar._MN = new Array
("一月",
 "二月",
 "三月",
 "四月",
 "五月",
 "六月",
 "七月",
 "八月",
 "九月",
 "十月",
 "十一月",
 "十二月");

// short month names
Calendar._SMN = new Array
("一月",
 "二月",
 "三月",
 "四月",
 "五月",
 "六月",
 "七月",
 "八月",
 "九月",
 "十月",
 "十一月",
 "十二月");

// tooltips
Calendar._TT = {};
Calendar._TT["INFO"] = "帮助";

Calendar._TT["ABOUT"] =
"DHTML Date/Time Selector\n" +
"Author: John \n" + // don't translate this this ;-)
"2006-02-01 version 1.0\n\n" +
"选择日期:\n" +
"- 点击 \xab \xbb 按钮选择年份\n" +
"- 点击 " + String.fromCharCode(0x2039) + String.fromCharCode(0x203a) + " 按钮选择月份\n" +
"- 长按以上按钮可从菜单中快速选择年份或月份";
Calendar._TT["ABOUT_TIME"] = "\n\n" +
"选择时间:\n" +
"- 点击小时或分钟可使改数值加一\n" +
"- 按住Shift键点击小时或分钟可使改数值减一\n" +
"- 点击拖动鼠标可进行快速选择";

Calendar._TT["PREV_YEAR"] = "上一年 (按住出菜单)";
Calendar._TT["PREV_MONTH"] = "上一月 (按住出菜单)";
Calendar._TT["GO_TODAY"] = "转到今日";
Calendar._TT["NEXT_MONTH"] = "下一月 (按住出菜单)";
Calendar._TT["NEXT_YEAR"] = "下一年 (按住出菜单)";
Calendar._TT["SEL_DATE"] = "选择日期";
Calendar._TT["DRAG_TO_MOVE"] = "拖动";
Calendar._TT["PART_TODAY"] = " (今日)";

// the following is to inform that "%s" is to be the first day of week
// %s will be replaced with the day name.
Calendar._TT["DAY_FIRST"] = "最左边显示  '%s'";

// This may be locale-dependent.  It specifies the week-end days, as an array
// of comma-separated numbers.  The numbers are from 0 to 6: 0 means Sunday, 1
// means Monday, etc.
Calendar._TT["WEEKEND"] = "0,6";

Calendar._TT["CLOSE"] = "关闭";
Calendar._TT["TODAY"] = "今日";
Calendar._TT["TIME_PART"] = "(Shift-)点击鼠标或拖动改变值";

// date formats
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d";
Calendar._TT["TT_DATE_FORMAT"] = "%A, %b %e日";

Calendar._TT["WK"] = "周";
Calendar._TT["TIME"] = "时间:";
}