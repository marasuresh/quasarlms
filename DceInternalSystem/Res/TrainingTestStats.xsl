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
   
   <xsl:template name="Test">
      <tr bgColor="#FFFFFF">
         <td><xsl:value-of select="TestName"/></td>
         <td>
            <xsl:call-template name="boolval">
            <xsl:with-param name="node" select="Complete"/>
            </xsl:call-template>
         </td>
         <td>
            <xsl:call-template name="boolval">
            <xsl:with-param name="node" select="Skipped"/>
            </xsl:call-template>
         </td>
         <td><xsl:value-of select="Tries"/></td>
         <td><xsl:value-of select="AllowTries"/></td>
         <td>
            <xsl:call-template name="formatDateTime"> 
               <xsl:with-param name="strDate" select="TryStart" />
            </xsl:call-template>
         </td>
         <td><xsl:value-of select="Points"/></td>
      </tr>
   </xsl:template>

   
   <xsl:template match="DataSet/TestDetails">
   
   <H3>
      <xsl:choose>
<!-- Тест -->      
         <xsl:when test="Type = '1'">
            <xsl:if test= "Parent = /xml/DataSet/Training/Course" >
                  Курсовой тест
            </xsl:if>
            <xsl:if test= "Parent != /xml/DataSet/Training/Course" >
               Тест на тему: <xsl:value-of select="ThemeName"/>
               <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
                  <tr bgColor="#BFBFBF">
                     <th>Длительность, мин</th>
                     <th>Проходной балл</th>
                     <th>Показывать результат студенту</th>
                     <th>Завершать при наборе проходного балла</th>
                  </tr>
                  <tr bgColor="#FFFFFF">
                     <td><xsl:value-of select="Duration"/></td>
                     <td><xsl:value-of select="Points"/></td>
                     <td>
                        <xsl:call-template name="boolval">
                           <xsl:with-param name="node" select="Show"/>
                        </xsl:call-template>
                     </td>
                     <td>
                        <xsl:call-template name="boolval">
                           <xsl:with-param name="node" select="AutoFinish"/>
                        </xsl:call-template>
                     </td>
                  </tr>
               </table>
            </xsl:if>
         
         </xsl:when>
<!-- Практическая работа -->      
         <xsl:when test="Type = '2'">
               Практическая работа по теме: <xsl:value-of select="ThemeName"/>
         </xsl:when>
      </xsl:choose>
      </H3>
   </xsl:template>

   <xsl:template match="Question">
      <tr bgColor="#FFFFFF">
         <td><xsl:value-of select="Content"/></td>
         <td><xsl:value-of select="Points"/></td>
         <td><xsl:value-of select="RightCount"/></td>
         <td><xsl:value-of select="WrongCount"/></td>
         <td><xsl:value-of select="Min"/></td>
         <td><xsl:value-of select="Max"/></td>
         <td><xsl:value-of select="Average"/></td>
      </tr>
   </xsl:template>   
   
   <xsl:template match="Questions">
      <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
      <tr><th colspan="7"><font color="#FFFFFF">Вопросы</font></th></tr>
      <tr bgColor="#BFBFBF">
         <th>Вопрос</th>
         <th>Баллы за прав. ответ</th>
         <th>Правильных ответов</th>
         <th>Неправильных ответов</th>
         <th>Мин. время ответа, сек</th>
         <th>Макс. время ответа, сек</th>
         <th>Среднее время ответа, сек</th>
      </tr>
      <xsl:apply-templates select="Question"/>
      </table>     
   </xsl:template>

   <xsl:template match="DataSet/TestResults">
      <xsl:param name="CourseTest">0</xsl:param>
      
      <tr bgColor="#FFFFFF">
         <td>
            <xsl:value-of select="LastName"/>&#160;
            <xsl:value-of select="FirstName"/>&#160;
            <xsl:value-of select="Patronymic"/>
         </td>
         <td>
            <xsl:call-template name="boolval">
               <xsl:with-param name="node" select="Complete"/>
            </xsl:call-template>
         </td>
         
         <xsl:if test="$CourseTest ='0'">
            <td>
               <xsl:call-template name="boolval">
                  <xsl:with-param name="node" select="Skipped"/>
               </xsl:call-template>
            </td>
         </xsl:if>
         
         <td>
            <xsl:value-of select="Tries"/>
         </td>

         <xsl:if test="$CourseTest ='0'">
         <td>
            <xsl:value-of select="AllowTries"/>
         </td>
         </xsl:if>
         
         <td>
            <xsl:call-template name="formatDateTime"> 
               <xsl:with-param name="strDate" select="TryStart" />
            </xsl:call-template>
         </td>
         <td>
            <xsl:value-of select="Points"/>
         </td>
      </tr>
   </xsl:template>
   
   <xsl:template match="DataSet/PracticeResults">
      <tr bgColor="#FFFFFF">
         <td>
            <xsl:value-of select="LastName"/>&#160;
            <xsl:value-of select="FirstName"/>&#160;
            <xsl:value-of select="Patronymic"/>
         </td>
         <td>
            <xsl:call-template name="boolval">
               <xsl:with-param name="node" select="Complete"/>
            </xsl:call-template>
         </td>
         <td>
            <xsl:call-template name="formatDateTime"> 
               <xsl:with-param name="strDate" select="CompletionDate" />
            </xsl:call-template>
         </td>
      </tr>
   </xsl:template>
   
   <xsl:template match="/">
      
         <H1>Статистика тренинга</H1>
         Дата генерации отчета: <xsl:value-of select="xml/StatDate"/>
         <br/>   <br/>
         <xsl:apply-templates select="/xml/DataSet/Training"/>

<!-- обычные тесты (по темам) -->
         <xsl:for-each select="xml/TestReport[DataSet/TestDetails/Parent != /xml/DataSet/Training/Course][DataSet/TestDetails/Type = '1']">
            <br/>
            <br/>
            <hr noshade="true"/>
            <xsl:apply-templates select="DataSet/TestDetails"/>
            <br/>
            <br/>
            <xsl:apply-templates select="Questions"/>

            <xsl:if test="count(DataSet/TestResults) >0">
            
            <br/>
            <br/>
               <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
                  <tr><th colspan="7"><font color="#FFFFFF">Результаты тестирования</font></th></tr>
                  <tr bgColor="#BFBFBF"><th rowspan="2">Студент</th><th rowspan="2">Сдан</th><th rowspan="2">Пропущен</th><th rowspan="2">Попыток</th>
                  <th rowspan="2">Осталось попыток</th><th colspan="2">Последняя попытка</th></tr>
                  <tr bgColor="#BFBFBF"><th>Дата</th><th>Баллы</th></tr>

                  <xsl:apply-templates select="DataSet/TestResults"/>

               </table>         
            </xsl:if>
            
         </xsl:for-each>

<!-- практические работы (по темам) -->
         <xsl:for-each select="xml/TestReport[DataSet/TestDetails/Type = '2']">
            <br/>
            <br/>
            <hr noshade="true"/>
            <xsl:apply-templates select="DataSet/TestDetails"/>

            <xsl:if test="count(DataSet/PracticeResults) >0">
            <br/>
               <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
                  <tr bgColor="#BFBFBF">
                     <th>Студент</th>
                     <th>Выполнена</th>
                     <th>Дата выполнения</th>
                  </tr>
                  <xsl:apply-templates select="DataSet/PracticeResults"/>
               </table>         
            </xsl:if>
            
         </xsl:for-each>

<!-- Курсовой тест -->
         <xsl:for-each select="xml/TestReport[DataSet/TestDetails/Parent = /xml/DataSet/Training/Course]">
            <br/>
            <br/>
            <hr noshade="true"/>
            <xsl:apply-templates select="DataSet/TestDetails"/>
            <xsl:if test="count(DataSet/TestResults) >0">
            
            <br/>
            <br/>
               <table cellspacing="1" cellpadding="2" border="0" width="100%" bgColor="#000000">
                  <tr><th colspan="7"><font color="#FFFFFF">Результаты тестирования</font></th></tr>
                  <tr bgColor="#BFBFBF">
                     <th>Студент</th>
                     <th>Сдан</th>
                     <th>Попыток</th>
                     <th>Дата</th>
                     <th>Баллы</th>
                  </tr>

                  <xsl:apply-templates select="DataSet/TestResults">
                     <xsl:with-param name="CourseTest">1</xsl:with-param>
                  </xsl:apply-templates>

               </table>         
            </xsl:if>
            
         </xsl:for-each>
         
   </xsl:template>
</xsl:stylesheet>
