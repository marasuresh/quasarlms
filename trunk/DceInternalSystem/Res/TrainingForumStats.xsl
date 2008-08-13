<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="unicode" omit-xml-declaration="yes" indent="no"/>


   <xsl:template name="formatDate">
	   <xsl:param name="strDate"/>
	   <!--2003-06-11T17:41:37.0630000+03:00
		    123456789012345678901234567890123
		    0        1         2         3-->
      <nobr>
	   <xsl:value-of select="substring($strDate, 9, 2)"/>.<xsl:value-of select="substring($strDate, 6, 2)"/>.<xsl:value-of select="substring($strDate, 1, 4)"/></nobr>
   </xsl:template>
   
   <xsl:template name="formatDateTime">
	   <xsl:param name="strDate"/>
	   <!--2003-06-11T17:41:37.0630000+03:00
		   123456789012345678901234567890123
		   0        1         2         3-->
      <nobr>
	   <xsl:value-of select="substring($strDate, 9, 2)"/>.<xsl:value-of select="substring($strDate, 6, 2)"/>.<xsl:value-of select="substring($strDate, 1, 4)"/>
	   &#160;<xsl:value-of select="substring($strDate, 12, 8)"/>
	   </nobr>
   </xsl:template>


   <xsl:template name="S">
      Студент: 
      <xsl:value-of select="LastName"/>&#160;
      <xsl:value-of select="FirstName"/>&#160;
      <xsl:value-of select="Patronymic"/>&#160;
      <br/>Последний вход: 
      <xsl:call-template name="formatDateTime"> 
         <xsl:with-param name="strDate" select="LastLogin" />
      </xsl:call-template>
      
   </xsl:template>
   
   <xsl:template name="Training" match="/xml/DataSet/Training">
      <H3>Тренинг:
      <xsl:value-of select="TName"/>&#160;
      <br/> 
      Код тренинга:
      <xsl:value-of select="Code"/>
      </H3>
   </xsl:template>


   <xsl:template name="boolval">
      <xsl:param name="node" />
      <xsl:for-each select="$node">
         <xsl:choose>
         <xsl:when test="$node='true'">Да</xsl:when>
         <xsl:otherwise>Нет</xsl:otherwise>
         </xsl:choose>
      </xsl:for-each>
   </xsl:template>
   
   <xsl:template match="DataSet/StudentStats">
   <tr bgColor="#FFFFFF"> 
      <td><xsl:value-of select="StudentName"/></td>
      <td><xsl:value-of select="StartedTopics"/></td>
      <td><xsl:value-of select="ReplyCount"/></td>
   </tr>
   </xsl:template>

   
   <xsl:template match="/">
      
      <H1>Статистика форума тренинга</H1>
      Дата генерации отчета: <xsl:value-of select="xml/StatDate"/>
      <br/>   <br/>
      <xsl:apply-templates select="/xml/DataSet/Training"/>

      <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
         <tr bgColor="#BFBFBF">
            <th>Студент</th>
            <th>Кол-во начатых нитей</th>
            <th>Кол-во ответов в нитях</th>
         </tr>
         <xsl:apply-templates select="/xml/ForumReport"/>
      </table>         

   </xsl:template>
</xsl:stylesheet>
