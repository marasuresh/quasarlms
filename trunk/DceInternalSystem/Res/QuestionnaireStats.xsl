<?xml version="1.0" encoding="windows-1251"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="unicode" omit-xml-declaration="yes" indent="no"/>

   <xsl:template match="Answer[@selected='true']">
      <li><xsl:value-of select="."/></li>
   </xsl:template>
   
   <xsl:template match="/xml/DataSet/Answers" mode="GropByQuestions">
   <tr bgColor="#FFFFFF"><td>
      &#160;
      <xsl:choose>
      
         <xsl:when test="Answer/Question/@type='single'">
            <xsl:value-of select="./Answer/Question/Answer[@selected='true']"/>
         </xsl:when>
         
         <xsl:when test="Answer/Question/@type='multiple'">
            <xsl:apply-templates select="./Answer/Question/Answer[@selected='true']"/>
         </xsl:when>
         
         <xsl:when test="Answer/Question/@type='textbox'">
            <xsl:value-of select="./Answer/Question/Answer/result"/>
         </xsl:when>
         
      </xsl:choose>
   </td></tr>      
   </xsl:template>


   <xsl:template match="/xml/DataSet/Answers" mode="GropByRespondents">
   <tr bgColor="#FFFFFF">
      <td>
         <xsl:variable name="q" select="Question"/>
         <xsl:apply-templates select="/xml/DataSet/Questions[id=$q]" mode="GropByRespondents"/>
      </td>
      <td>
      &#160;
         <xsl:choose>
         
            <xsl:when test="Answer/Question/@type='single'">
               <xsl:value-of select="./Answer/Question/Answer[@selected='true']"/>
            </xsl:when>
            
            <xsl:when test="Answer/Question/@type='multiple'">
               <xsl:apply-templates select="./Answer/Question/Answer[@selected='true']"/>
            </xsl:when>
            
            <xsl:when test="Answer/Question/@type='textbox'">
               <xsl:value-of select="./Answer/Question/Answer/result"/>
            </xsl:when>
            
         </xsl:choose>
      </td>

   </tr>      
   
   
   <xsl:if test="string(following::TestResults) != string(TestResults)">
      <tr bgColor="#FFFFFF"><td colspan="2"><hr/></td></tr>
   </xsl:if>
   </xsl:template>

   
   <xsl:template match="DataSet/Questions"  mode="GropByQuestions">
      <tr bgColor="#FFFFFF"><td>
         <xsl:value-of select="QContent"/>
         </td>
         <td>
         <xsl:variable name="id" select="id"/>
         <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
            <xsl:apply-templates select="/xml/DataSet/Answers[Question=$id]" mode="GropByQuestions"/>
         </table>
         </td>
      </tr>      
   </xsl:template>
  
   <xsl:template match="DataSet/Questions"  mode="GropByRespondents">
         <xsl:value-of select="QContent"/>
   </xsl:template>
   
   <xsl:template match="/xml">

      <H1>Результаты анкетирования</H1>
         Дата генерации отчета: <xsl:value-of select="StatDate"/>
      <br/> 
      <H3>
         Название анкеты: <xsl:value-of select="DataSet/Questionnaire/InternalName"/>
      <br/>
         Количество респондентов: <xsl:value-of select="DataSet/Questionnaire/ResCount"/>
      </H3>
         <br/>
   <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
   <xsl:choose>
      <xsl:when test="GroupByQuestions='True'">
      <tr bgColor="#BFBFBF"><th>Вопросы</th><th>Ответы</th></tr>
            <xsl:apply-templates select="DataSet/Questions" mode="GropByQuestions" /> 
      </xsl:when>
      <xsl:otherwise>
         <tr bgColor="#BFBFBF"><th>Вопросы</th><th>Ответы</th></tr>
               <xsl:apply-templates select="DataSet/Answers" mode="GropByRespondents"/> 

      </xsl:otherwise>
   </xsl:choose>
   
   </table>
   </xsl:template>
</xsl:stylesheet>
 