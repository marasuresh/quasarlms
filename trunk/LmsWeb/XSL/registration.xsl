<?xml version="1.0" encoding="windows-1251" ?>
<xsl:stylesheet version="1.0" xmlns:xsl='http://www.w3.org/1999/XSL/Transform'
xmlns:msxml="urn:schemas-microsoft-com:xslt"
xmlns:script="myspace">
<xsl:output method="html" indent="no"/>
<msxml:script implements-prefix="script" language="jscript">
<![CDATA[		
	function LeftPadStyle(valid)
	{	
		if(	valid == "false" )	return "LeftPadError";
		return "LeftPad";
	}
]]>
</msxml:script>

<xsl:param name="ValidForm" select="True"/>

<xsl:template match="/">
 <script language="javascript">
 function ViewPic()
 {
 currPic = document.getElementById("usrPhotoId");
 document.getElementById("picId").src = currPic.value;
 }
 </script>
 
	<table align="left" cellspacing="0" cellpadding="0" border="0" width="100%" class="RegForm">
		<colgroup>
			<col/>
			<col valign="top" style="padding-left: 0;" width="250px%" align="right" nowrap="true"/>
			<col width="50%"/>
		</colgroup>
		<tr>
		<td></td><td align="left" colspan="2">
		 <xsl:choose>
		 <xsl:when test="$ValidForm='False'">
 <a align="center" style="color:#ff3300; font-size:12pt;"><xsl:value-of select="//PageError"/></a>
		 </xsl:when>
		 <xsl:otherwise>
		 
		 <xsl:choose>
		 <xsl:when test="//userProperty">
		 </xsl:when>
		 <xsl:otherwise>
		 <xsl:choose>
		 <xsl:when test="//notreg">
 		 <h3 class="failure"><xsl:value-of select="//PageNote1"/></h3>
		 </xsl:when>
		 <xsl:otherwise>
 		 <h3 class="cap3"><xsl:value-of select="//PageNote1"/></h3>
		 </xsl:otherwise>
		 </xsl:choose>
 		 
 <a align="center" style="font-size:11pt;">
 <br/><xsl:value-of select="//PageNote2"/><br/></a>
		 </xsl:otherwise>
		 </xsl:choose>


		 <a align="center" style="font-size:11pt;">
		 <xsl:value-of select="//PageNote"/>&#160;*</a>
		 </xsl:otherwise>
		 </xsl:choose>
		</td>
		<td></td>
		</tr>
<!--	<xsl:if test="//userProperty">
	<form method="post" action="" name="passwordForm" runat="server">
 <input type="hidden" value="Password" name="formAction"/>
		<tr>
		<td></td><td colspan="2">
		
	 <table align="left" cellspacing="0" cellpadding="0" border="1" width="75%">
	 <tr><td style="PADDING-LEFT: 25px;PADDING-top: 10px;PADDING-right: 25px;">
	 <table align="left" cellspacing="0" cellpadding="0" border="0" width="100%" class="RegForm">
			<col valign="top" style="padding-left: 0;" width="250px%" align="right" nowrap="true"/>
			<col width="*" valign="top"/>
	 <tr><td colspan="2" valign="center" align="left">
	 <a align="center" style="font-size:10pt; font-weight:bold;"><xsl:value-of select="//ChangePass"/></a>
	 </td></tr>
	 <tr>
		 <td><input type="password" name="usrPasswordOld" value="{//existData/PasswordOld}" maxlength="50"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/PasswordOld/@valid))}"><xsl:value-of select="//PasswordOld"/></td>
		</tr>
	 <tr>
		 <td><input type="password" name="usrPassword" value="{//existData/Password}" maxlength="50"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Password/@valid))}"><xsl:value-of select="//Password"/></td>
		</tr>
		<tr>
		 <td height="35px"><input type="password" name="usrPasswordConfirm" value="{//existData/PasswordConfirm}" maxlength="50"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/PasswordConfirm/@valid))}"><xsl:value-of select="//PasswordConfirm"/></td>
		</tr>
	 <tr><td></td><td class="btn" valign="center" align="left" height="45px">
	 <input runat="server" type="submit" value="{//ChangePassword}" name="PasswordBtn"/>
	 </td></tr>
		</table>
		</td></tr>
		</table>
		
		</td>
		</tr>
 </form>
 <tr><td colspan="3" height="15"></td></tr>
 </xsl:if>-->
	<form method="post" action="" name="registrationForm" enctype="multipart/form-data" runat="server">
 <input type="hidden" value="Registration" name="formAction"/>
		<xsl:if test="//LastName != 0">
		<tr>
		<td></td><td><input type="text" name="usrLastName" value="{//existData/LastName}" maxlength="50"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/LastName/@valid))}">*&#160;<xsl:value-of select="//LastName"/></td>
		</tr>
		</xsl:if>
		<xsl:if test="//FirstName != 0">
		<tr>
		<td></td><td><input type="text" name="usrFirstName" value="{//existData/FirstName}" maxlength="30"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/FirstName/@valid))}">*&#160;<xsl:value-of select="//FirstName"/></td>
		</tr>
		</xsl:if>
		<xsl:if test="//MidName != 0">
		<tr>
		<td></td><td><input type="text" name="usrMidName" value="{//existData/MidName}" maxlength="50"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/MidName/@valid))}">*&#160;<xsl:value-of select="//MidName"/></td>
		</tr>
		</xsl:if>
		<tr>
		<td></td><td><input type="text" name="usrFrsName" value="{//existData/FrsName}" maxlength="30"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/FrsName/@valid))}">*&#160;First name:</td>
		</tr>
		<tr>
		<td></td><td><input type="text" name="usrLstName" value="{//existData/LstName}" maxlength="50"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/LstName/@valid))}">*&#160;Last name:</td>
		</tr>
		<tr>
		<td></td><td><input type="text" name="usrEmail" value="{//existData/Email}" maxlength="80"/></td>
		<td class="{script:LeftPadStyle(string(//existData/Email/@valid))}">*&#160;<xsl:value-of select="//Email"/></td>
		</tr>
		<xsl:if test="not(//userProperty)">
		 <tr>
		 <td></td><td><input type="password" name="usrPassword" value="{//existData/Password}" maxlength="50"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Password/@valid))}">*&#160;<xsl:value-of select="//Password"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="password" name="usrPasswordConfirm" value="{//existData/PasswordConfirm}" maxlength="50"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/PasswordConfirm/@valid))}">*&#160;<xsl:value-of select="//PasswordConfirm"/></td>
		 </tr>
		</xsl:if>
		<tr>
		<td></td><td align="center">
		 <xsl:choose>
		 <xsl:when test="//existData/Sex = 'Male'">
		 <input type="radio" name="usrSex" checked="1" value="Male" class="clear"/>
		 </xsl:when>
		 <xsl:otherwise>
		 <input type="radio" name="usrSex" value="Male" class="clear"/>
		 </xsl:otherwise>
		 </xsl:choose>
		 <xsl:value-of select="//Male"/>&#160;&#160;&#160;&#160;&#160;&#160;
		 <xsl:choose>
		 <xsl:when test="//existData/Sex = 'Female'">
 		 <input type="radio" name="usrSex" checked="1" value="Female" class="clear"/>
		 </xsl:when>
		 <xsl:otherwise>
 		 <input type="radio" name="usrSex" value="Female" class="clear"/>
		 </xsl:otherwise>
		 </xsl:choose>
		 <xsl:value-of select="//Female"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/Sex/@valid))}">*&#160;<xsl:value-of select="//Sex"/></td>
		</tr>
		<tr>
		<td></td><td align="left" class="BirthDay" nowrap="true">
		 <input type="text" name="usrBirthDay" value="{//existData/BirthDay}" maxlength="2" class="day"/>&#160;&#160;<xsl:value-of select="//BirthDay"/>&#160;
		 <input type="text" name="usrBirthMonth" value="{//existData/BirthMonth}" maxlength="2" class="day"/>&#160;&#160;<xsl:value-of select="//BirthMonth"/>&#160;
		 <input type="text" name="usrBirthYear" value="{//existData/BirthYear}" maxlength="4" class="year"/>&#160;&#160;<xsl:value-of select="//BirthYear"/>
		</td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/Birth/@valid))}">*&#160;<xsl:value-of select="//Birth"/></td>
		</tr>
		<!--tr>
		<td></td><td><input type="text" name="usrWorkPlace" value="{//existData/WorkPlace}" maxlength="100"/></td>
		<td nowrap="true" class="{script:LeftPadStyle(string(//existData/WorkPlace/@valid))}"><xsl:value-of select="//WorkPlace"/></td>
		</tr-->
		<xsl:if test="//userProperty">
		 <tr>
		 <td></td><td><input type="text" name="usrCompanyName" value="{//existData/CompanyName}" maxlength="120"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/CompanyName/@valid))}"><xsl:value-of select="//CompanyName"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrCompanyType" value="{//existData/CompanyType}" maxlength="120"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/CompanyType/@valid))}"><xsl:value-of select="//CompanyType"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrJobOccup" value="{//existData/JobOccup}" maxlength="120"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/JobOccup/@valid))}"><xsl:value-of select="//JobOccup"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrManager" value="{//existData/Manager}" maxlength="200"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Manager/@valid))}"><xsl:value-of select="//Manager"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrManagerOccup" value="{//existData/ManagerOccup}" maxlength="120"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/ManagerOccup/@valid))}"><xsl:value-of select="//ManagerOccup"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrManagerPhone" value="{//existData/ManagerPhone}" maxlength="20"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/ManagerPhone/@valid))}"><xsl:value-of select="//ManagerPhone"/></td>
		 </tr>
		 <tr>
		 <td></td><td>
			 <select name="usrCountry" style="WIDTH: 100%">
			 <option value="" disabled="true">...select the country...</option>
			 <xsl:copy-of select="//Countries"/>
			 </select>
		 </td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Country/@valid))}"><xsl:value-of select="//Country"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrCity" value="{//existData/City}" maxlength="40"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/City/@valid))}"><xsl:value-of select="//City"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrPostalCode" value="{//existData/PostalCode}" maxlength="20"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/PostalCode/@valid))}"><xsl:value-of select="//PostalCode"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrAdress" value="{//existData/Adress}" maxlength="80"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Adress/@valid))}"><xsl:value-of select="//Adress"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrPhoneCityCode" value="{//existData/PhoneCityCode}" maxlength="30"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/PhoneCityCode/@valid))}"><xsl:value-of select="//PhoneCityCode"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrFaxCityCode" value="{//existData/FaxCityCode}" maxlength="30"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/FaxCityCode/@valid))}"><xsl:value-of select="//FaxCityCode"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrCollegeProf" value="{//existData/CollegeProf}" maxlength="200"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/CollegeProf/@valid))}"><xsl:value-of select="//CollegeProf"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrCourses" value="{//existData/Courses}" maxlength="255"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Courses/@valid))}"><xsl:value-of select="//Courses"/></td>
		 </tr>
		 <tr>
		 <td></td><td><input type="text" name="usrCert" value="{//existData/Cert}" maxlength="255"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Cert/@valid))}"><xsl:value-of select="//Cert"/></td>
		 </tr>
		 <tr>
		 <td></td><td><textarea name="usrAddInfo" rows="5" cols="40" value="{//existData/AddInfo}" maxlength="512" style="overflow:hidden"><xsl:value-of select="//existData/AddInfo"/></textarea></td>
		 <td valign="top" nowrap="true" class="{script:LeftPadStyle(string(//existData/AddInfo/@valid))}">
		 <xsl:value-of select="//AddInfo"/>
		 <br/><br/><a name="SetPhoto">
		 <IMG id="picId" height="120" alt="" src="UserPhoto.aspx?id={//existData/PhotoId}" border="0"/></a>
		 </td>
		 </tr>
		 <tr>
		 <td></td><td><input runat="server" id="usrPhotoId" type="file" name="usrPhoto" onload="javascript:ViewPic()" onpropertychange="javascript:ViewPic()"/></td>
		 <td nowrap="true" class="{script:LeftPadStyle(string(//existData/Photo/@valid))}"><xsl:value-of select="//Photo"/>
		 </td></tr>
		</xsl:if>
		<tr>
		<td class="btn" colspan="3" align="center"><br/><br/><input runat="server" type="submit" value="{//Submit}" name="SubmitBtn"/>&#160;&#160;&#160;<input type="reset" value="{//Reset}" name="ResetBtn"/>
		</td>
		</tr>
	</form>
	</table>
</xsl:template>
</xsl:stylesheet>