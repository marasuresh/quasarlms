using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public partial class Tools_Administration_Irbis_SyncUserBase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if( CurrentUser.Role != Dce.Roles.Administrator )
            Response.Redirect("../Default.aspx");
    }

    bool AllowSync = false;

    public void PerformSync()
    {
        Response.BufferOutput = false;
        if( !AllowSync )
            return;

        SetStartButtonEnabledState(false);
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("uk-ua");

            WriteLine("Синхронізація почалась "+DateTime.Now.ToLongDateString()+" "+DateTime.Now.ToLongTimeString());
            WriteLine("");

            Write("Завантаження БД ИРБИС...");
            IrbisData irbisData = LoadAllIrbisData();
            WriteLine(" O.K.");
            WriteLine(" LDAP[" + irbisData.LDAP.Count + "]");
            WriteLine(" PRS4DOC[" + irbisData.PRS4DOC.Count + "]");
            WriteLine(" STR4DOC[" + irbisData.STR4DOC.Count + "]");
            WriteLine("");

            Write("Завантаження існуючих користувачів...");
            DceUser[] dceUserList = DceUserService.GetAllUsers();
            WriteLine(" O.K. "+dceUserList.Length);

            WriteLine("Підготовчі дії з побудування індексів...");

            SetStatus("Індекс dceUserByLogin...");
            Dictionary<string, DceUser> dceUserByLogin = new Dictionary<string, DceUser>(dceUserList.Length, StringComparer.OrdinalIgnoreCase);
            foreach( DceUser u in dceUserList )
            {
                dceUserByLogin.Add(u.Login, u);
            }

            SetStatus("Індекс departmentByKey...");
            Dictionary<decimal, IrbisData.STR4DOCRow> departmentByKey = new Dictionary<decimal, IrbisData.STR4DOCRow>(irbisData.STR4DOC.Count);
            List<decimal> doubleDepartmentKeys = new List<decimal>();
            foreach( IrbisData.STR4DOCRow departmentRow in irbisData.STR4DOC )
            {
                decimal key = departmentRow.OU_SYSNO;
                if( doubleDepartmentKeys.Contains(key) )
                    continue;
                
                if( departmentByKey.ContainsKey(key) )
                {
                    departmentByKey.Remove(key);
                    doubleDepartmentKeys.Add(key);
                    continue;
                }

                departmentByKey.Add(key, departmentRow);
            }

            SetStatus("Індекс userKeyByLogin...");
            Dictionary<string, decimal> userKeyByLogin = new Dictionary<string, decimal>(irbisData.LDAP.Count, StringComparer.OrdinalIgnoreCase);
            Dictionary<string, int> doubleLogins = new Dictionary<string, int>();
            int emptyLdapNameCount = 0;
            foreach( IrbisData.LDAPRow ldapRow in irbisData.LDAP )
            {
                if( string.IsNullOrEmpty(ldapRow.LDAP_NAME) )
                {
                    emptyLdapNameCount++;
                    continue;
                }

                if( doubleLogins.ContainsKey(ldapRow.LDAP_NAME) )
                {
                    doubleLogins[ldapRow.LDAP_NAME] += 1;
                    continue;
                }

                if( userKeyByLogin.ContainsKey(ldapRow.LDAP_NAME) )
                {
                    userKeyByLogin.Remove(ldapRow.LDAP_NAME);
                    doubleLogins.Add(ldapRow.LDAP_NAME, 1);
                    continue;
                }

                userKeyByLogin.Add(ldapRow.LDAP_NAME, ldapRow.PRS_SYSNO);
            }

            SetStatus("Індекс personRowByKey...");
            Dictionary<decimal, IrbisData.PRS4DOCRow> personRowByKey = new Dictionary<decimal, IrbisData.PRS4DOCRow>();
            List<decimal> doublePersonKeys = new List<decimal>();
            foreach( IrbisData.PRS4DOCRow personRow in irbisData.PRS4DOC )
            {
                if( doublePersonKeys.Contains(personRow.PRS_SYSNO) )
                    continue;

                if( personRowByKey.ContainsKey(personRow.PRS_SYSNO) )
                {
                    personRowByKey.Remove(personRow.PRS_SYSNO);
                    doublePersonKeys.Add(personRow.PRS_SYSNO);
                    continue;
                }

                personRowByKey.Add(personRow.PRS_SYSNO, personRow);
            }

            WriteLine(" O.K.");
            WriteLine("  dceUserByLogin[" + dceUserByLogin.Count + "]");
            WriteLine("  userKeyByLogin[" + userKeyByLogin.Count + "]");
            WriteLine("  doubleLogins[" + doubleLogins.Count + "]");

            WriteLine("  personRowByKey[" + personRowByKey.Count + "]");
            WriteLine("  doublePersonKeys[" + doublePersonKeys.Count + "]");

            WriteLine("  departmentByKey[" + departmentByKey.Count + "]");
            WriteLine("  doubleDepartmentKeys[" + doubleDepartmentKeys.Count + "]");

            
            WriteLine("  emptyLdapNameCount: " + emptyLdapNameCount);


            WriteLine("");

            DateTime nextDump = DateTime.Now;

            WriteLine("Обробка та синхронізація. Це може бути довготривалий процесс, заждіть хвилинку.");
            List<string> unknownLogins = new List<string>();
            int updateCount = 0;
            int createCount = 0;
            int nonModifiedCount = 0;

            int notFoundInIndex = 0;
            int notFoundInPrs = 0;
            
            List<string> skipLdapNoPrsList = new List<string>();
         
            List<string> ldapNameListSorted = new List<string>(userKeyByLogin.Keys);
            ldapNameListSorted.Sort();

            int runIndex = 0;
            DateTime startTime = DateTime.Now;
            foreach( string ldapName in ldapNameListSorted )
            {
                runIndex++;

                if( !userKeyByLogin.ContainsKey(ldapName) )
                {
                    WriteLine("Знайдено ім'я у таблиці LDAP не має відповідного запису у індексі userKeyByLogin: "+ldapName+" (пропуск синхронізації).");
                    unknownLogins.Add(ldapName);
                    notFoundInIndex++;
                    continue;
                }

                IrbisData.PRS4DOCRow personRow;
                if( !personRowByKey.TryGetValue(userKeyByLogin[ldapName], out personRow) )
                {
                    //WriteLine("Знайдений ключ не має відповідного запису у таблиці PRS4DOC: " + ldapName + ", ключ " + userKeyByLogin[ldapName] + " (пропуск синхронізації).");
                    skipLdapNoPrsList.Add(ldapName);
                    continue;
                }

                bool dump = (DateTime.Now > nextDump);
                if( dump )
                {
                    nextDump = DateTime.Now.AddSeconds(new Random().NextDouble()*3 + 0.3);
                }

                DceUser existingUser;
                if( dceUserByLogin.TryGetValue(ldapName, out existingUser) )
                {
                    bool updatedActually;

                    if( dump )
                    {
                        double secForAccount = (DateTime.Now-startTime).TotalSeconds/runIndex;
                        double restSeconds = secForAccount * (ldapNameListSorted.Count-runIndex);
                        TimeSpan restTime = TimeSpan.FromSeconds(restSeconds);
                        string restTimeString;
                        if( restTime.TotalDays>5 )
                             restTimeString = restTime.Days+" днiв";
                        else if( restTime.TotalDays>1 )
                             restTimeString = restTime.Days+" днiв "+restTime.Hours+" годин";
                        else if( restTime.TotalHours>5 )
                             restTimeString = restTime.Hours+" годин";
                        else if( restTime.TotalHours>1 )
                             restTimeString = restTime.Hours+" годин "+restTime.Minutes+" мiнут";
                        else if( restTime.Minutes>5 )
                             restTimeString = restTime.Minutes+" мiнут";
                        else if( restTime.Minutes>1 )
                             restTimeString = restTime.Minutes+" мiнут "+restTime.Seconds+" секунд";
                        else
                             restTimeString = restTime.TotalSeconds.ToString("0.0")+" секунд";

                        string progress = (runIndex*100.0/ldapNameListSorted.Count).ToString("0.0")+"%"+
                          " ще близько "+restTimeString;

                        SetStatus("Оновлення " + ldapName + "...   "+progress);
                        UpdateDceUser(existingUser, personRow, departmentByKey, out updatedActually);

                        SetStatus(
                          "Оновлення " + ldapName +
                          " O.K. "+progress);
                    }
                    else
                    {
                        UpdateDceUser(existingUser, personRow, departmentByKey, out updatedActually);
                    }

                    if( updatedActually )
                        updateCount++;
                    else
                        nonModifiedCount++;
                }
                else
                {
                    if( dump )
                    {
                        double secForAccount = (DateTime.Now-startTime).TotalSeconds/runIndex;
                        double restSeconds = secForAccount * (ldapNameListSorted.Count-runIndex);
                        TimeSpan restTime = TimeSpan.FromSeconds(restSeconds);
                        string restTimeString;
                        if( restTime.TotalDays>5 )
                             restTimeString = restTime.Days+" днiв";
                        else if( restTime.TotalDays>1 )
                             restTimeString = restTime.Days+" днiв "+restTime.Hours+" годин";
                        else if( restTime.TotalHours>5 )
                             restTimeString = restTime.Hours+" годин";
                        else if( restTime.TotalHours>1 )
                             restTimeString = restTime.Hours+" годин "+restTime.Minutes+" мiнут";
                        else if( restTime.Minutes>5 )
                             restTimeString = restTime.Minutes+" мiнут";
                        else if( restTime.Minutes>1 )
                             restTimeString = restTime.Minutes+" мiнут "+restTime.Seconds+" секунд";
                        else
                             restTimeString = restTime.TotalSeconds.ToString("0.0")+" секунд";

                        string progress = (runIndex*100.0/ldapNameListSorted.Count).ToString("0.0")+"%"+
                          " ще близько "+restTimeString;

                        SetStatus("Створення користувача " + ldapName + "...   "+progress);
                        CreateDceUser(personRow, ldapName, departmentByKey);

                        SetStatus("Створення користувача " + ldapName+
                          " O.K. "+progress);
                    }
                    else
                    {
                        CreateDceUser(personRow, ldapName, departmentByKey);
                    }

                    createCount++;
                }
            }

            WriteLine("");
            WriteLine("Completed " + DateTime.Now.ToLongTimeString() + ".");
            WriteLine("Updated: " + updateCount + ".");
            WriteLine("Create: " + createCount + ".");
            WriteLine("Already current: " + nonModifiedCount + ".");
            WriteLine("-----------------------");
            WriteLine("Total processed: " + (updateCount+createCount+nonModifiedCount )+ ".");

            WriteLine("");
            WriteLine("Not found in index and skipped: " + notFoundInIndex+ ".");
            WriteLine("Not found in PRS4DOC table and skipped: " + skipLdapNoPrsList.Count+".");
            WriteLine("-----------------------");
            WriteLine("Total skipped: " + (notFoundInIndex+notFoundInPrs) + ".");

            WriteLine("");
            WriteLine("=========================");
            WriteLine("Total iterations: " + (updateCount+createCount+nonModifiedCount + notFoundInIndex + notFoundInPrs) + ".");

            WriteLine("");
            WriteLine("");

            StringBuilder b = new StringBuilder(skipLdapNoPrsList.Count * 16);
            foreach( string ldap in skipLdapNoPrsList )
            {
                if( b.Length>0 )
                    b.Append(',');
                b.Append(ldap);
            }

            WriteLine("LDAP names not found in PRS4DOC table:\r\n");
            WriteLine(b.ToString());
        }
        catch( Exception error )
        {
            WriteLine("\r\n\r\n" + error);
        }

        SetStartButtonEnabledState(true);
    }

    private void CreateDceUser(IrbisData.PRS4DOCRow irbisRow, string login, Dictionary<decimal, IrbisData.STR4DOCRow> departmentByKey)
    {
        DceUserService.CreateUser(login);
        DceUser newDceUser = DceUserService.GetUserByLogin(login);
        newDceUser.FirstName = irbisRow.PRS_FIRSTNAME;
        newDceUser.LastName = irbisRow.PRS_LASTNAME;
        newDceUser.Patronymic = irbisRow.PRS_PATRYC;

        bool modified_FlagNotUsed = true;
        UpdateLdap(newDceUser, ref modified_FlagNotUsed);

        DceUserService.UpdateUser(newDceUser);
    }

    private void UpdateDceUser(DceUser dceUser, IrbisData.PRS4DOCRow irbisRow, Dictionary<decimal,IrbisData.STR4DOCRow> departmentByKey, out bool modified)
    {
        modified = false;

        if( dceUser.LastName != irbisRow.PRS_LASTNAME )
        {
            modified = true;
            dceUser.LastName = irbisRow.PRS_LASTNAME;
        }
        if( dceUser.FirstName != irbisRow.PRS_FIRSTNAME )
        {
            modified = true;
            dceUser.FirstName = irbisRow.PRS_FIRSTNAME;
        }
        if( dceUser.FirstName != irbisRow.PRS_PATRYC )
        {
            modified = true;
            dceUser.Patronymic = irbisRow.PRS_PATRYC;
        }


        string departmentName;
        if( departmentByKey.ContainsKey(irbisRow.OU_SYSNO) )
            departmentName = departmentByKey[irbisRow.OU_SYSNO].OU_FNAME;
        else
            departmentName = null;

        if( dceUser.Comments != departmentName )
        {
            modified = true;
            dceUser.Comments = departmentName;
        }

        UpdateLdap(dceUser, ref modified);

        if( modified )
        {
            DceUserService.UpdateUser(dceUser);
        }
    }

    private void UpdateLdap(DceUser dceUser, ref bool modified)
    {
        Account ldapAccount;
        try
        {  ldapAccount = LdapService.GetAccount(dceUser.Login); }
        catch
        { return; }

        if( ldapAccount == null )
            return;

        Regions.RegionInfo ldapRegion = Regions.FindByCode(ldapAccount.RegionCode);
        if( ldapRegion == null )
        {
            ConfigData.RegionsDataTable regionAddTable = new ConfigData.RegionsDataTable();
            Guid regionID = Guid.NewGuid();
            string regionName = "??" + ldapAccount.RegionCode + " " + dceUser.Login + " at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();

            MailErrorLog.SendMessage(
                "New region found [" + ldapAccount.RegionCode + "]",
                "New region found [" + ldapAccount.RegionCode + "], ID=" + regionID + "\r\n\r\n" + regionName);
        }
        else
        {
            if( dceUser.RegionID != ldapRegion.ID )
            {
                modified = true;
                dceUser.RegionID = ldapRegion.ID;
            }
        }

        if( dceUser.EMail != ldapAccount.EMail )
        {
            modified = true;
            dceUser.EMail = ldapAccount.EMail;
        }
    }

    void SetStartButtonEnabledState(bool isEnabled)
    {
        bool isDisabled = !isEnabled;

        Response.Write(
            "<script language=JavaScript> \r\n" +
            "document.all[\"" + startButton.ClientID + "\"].disabled = " + isDisabled.ToString().ToLowerInvariant() + ";\r\n" +
            "</script>\r\n");
        Response.Flush();
        Response.Write(" ");
        Response.Flush();
    }

    void SetStatus(string statusText)
    {
        Response.Write(
            "<script language=JavaScript> \r\n" +
            "document.all[\"" + statusTextBox.ClientID + "\"].value = \"" + FormatDumpInnerText(statusText) + "\";\r\n" +
            "</script>\r\n");
        Response.Flush();
        Response.Write(" ");
        Response.Flush();
    }

    void WriteLine(string text)
    {
        Write(text + "\r\n");
    }

    void Write(string text)
    {
        Response.Write(
            "<script language=JavaScript> \r\n"+
            logLabel.ClientID + ".innerHTML += \"" + FormatDumpInnerHTML(text) + "\";\r\n" +
            "document.all[\""+statusTextBox.ClientID+"\"].value = \"\";\r\n"+
            "</script>\r\n");
        Response.Flush();
        Response.Write(" ");
        Response.Flush();
    }

    string FormatDumpInnerHTML(string text)
    {
        return text
            .Replace("\\","\\\\")
            .Replace("\"","\\\"")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("&", "&amp;")
            .Replace("\r\n", "<br>")
            .Replace("\r", "<br>")
            .Replace("\n", "<br>")
            .Replace("  "," &nbsp");
    }

    string FormatDumpInnerText(string text)
    {
        return text
            .Replace("\\", "\\\\")
            .Replace("\"", "\\\"")
            .Replace("\r", "\\r")
            .Replace("\n", "\\n");
    }

    protected void startButton_Click(object sender, EventArgs e)
    {
        AllowSync = true;
        statusTextBox.Focus();
    }

    IrbisData LoadAllIrbisData()
    {
        IrbisData result = new IrbisData();
        SetStatus("Таблиця LDAP...");
        new IrbisDataTableAdapters.LDAP().Fill(result.LDAP);
        SetStatus("Таблиця PRS4DOC...");
        new IrbisDataTableAdapters.PRS4DOC().Fill(result.PRS4DOC);
        SetStatus("Таблиця STR4DOC...");
        new IrbisDataTableAdapters.STR4DOC().Fill(result.STR4DOC);

        return result;
    }

}