using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.DirectoryServices.Protocols;
using System.Net;

public static class LdapService
{
    public static Account GetAccount(string login)
    {
        CheckLoginFormat(login);

        using( LdapConnection ldapConnection = BindConnectionSearchAccount() )
        {
            SearchResponse resp = SearchGetResponse("uid="+login, ldapConnection);

            if( resp.Entries.Count < 1 )
                return null;
            else if( resp.Entries.Count > 1 )
                throw new InvalidOperationException("There are multiple accounts with such login.");

            return BuildAccountFromLdapEntry(resp.Entries[0]);
        }
    }

    public static Account[] GetAccountList()
    {
        using( LdapConnection ldapConnection = BindConnectionSearchAccount() )
        {
            ldapConnection.Timeout = LdapSettings.ListTimeout;

            SearchResponse resp = SearchGetResponse(null, ldapConnection);
            
            List<Account> resultList = new List<Account>(resp.Entries.Count);
            foreach( SearchResultEntry foundEntry in resp.Entries )
            {
                resultList.Add(
                    BuildAccountFromLdapEntry(foundEntry));
            }

            return resultList.ToArray();
        }
    }

    public static Account CheckLogin(string login, string password)
    {
        CheckLoginFormat(login);

        SearchResultEntry userEntry;

        using( LdapConnection ldapConnection = BindConnectionSearchAccount() )
        {
            SearchResponse resp = SearchGetResponse("uid=" + login, ldapConnection);

            if( resp.Entries.Count < 1 ) //throw new Exception("DBEUG: no user found");
                return null;
            else if( resp.Entries.Count > 1 )
                throw new InvalidOperationException("There are multiple accounts with such login.");

            userEntry = resp.Entries[0];
        }

        try
        {
            using( LdapConnection userLdapConnection = BindConnectionUserAccount(userEntry.DistinguishedName, password) )
            {
            }
        }
        catch( LdapException )
        {
            if( HttpContext.Current != null &&
                HttpContext.Current.IsDebuggingEnabled )
                throw;
            else
                return null;
        }

        return BuildAccountFromLdapEntry(userEntry);
    }





    static void CheckLoginFormat(string login)
    {
        if( login.Contains(",") )
            throw new ArgumentException("LOCALIZE! Login may not contain special characters.","login");
    }

    static Account BuildAccountFromLdapEntry(SearchResultEntry ldapEntry)
    {
        Account resultAccount = new Account();
        resultAccount.DistinguishedName = ldapEntry.DistinguishedName;
        resultAccount.Login = ExtractDnLogin(ldapEntry.DistinguishedName);
        resultAccount.RegionCode = ExtractRegionCode(ldapEntry.DistinguishedName);

        DirectoryAttribute attrMail = ldapEntry.Attributes["mail"];
        if( attrMail != null && attrMail.Count>0 )
            resultAccount.EMail = (string)attrMail[0];

        return resultAccount;
    }

    static string ExtractDnLogin(string distinguishedName)
    {
        string[] dnEqPairs = distinguishedName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        string login = null;
        foreach( string eqPair in dnEqPairs )
        {
            string[] nameValuePair = eqPair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if( nameValuePair.Length != 2 )
                continue;

            string name = nameValuePair[0].Trim();
            string value = nameValuePair[1].Trim();

            if( string.Equals(name, "uid", StringComparison.OrdinalIgnoreCase) )
            {
                login = value;
                break;
            }
        }

        if( login == null )
            throw new FormatException("LOCALIZE! Wrong LDAP DN format (no uid part found).");

        return login;
    }

    static string ExtractRegionCode(string distinguishedName)
    {
        string[] dnEqPairs = distinguishedName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        List<string> ouValues = new List<string>(GetOuParts(distinguishedName));

        if( ouValues.Count == 0 )
            return null;

        if( ouValues.Count < 2 )
            return null;
        else
            return ouValues[1];
    }

    static IEnumerable<string> GetOuParts(string distinguishedName)
    {
        foreach( KeyValuePair<string, string> dnPart in SplitDistinguishedNameParts(distinguishedName) )
        {
            if( string.Equals(dnPart.Key, "ou", StringComparison.OrdinalIgnoreCase) )
                yield return dnPart.Value;
        }
    }

    static IEnumerable<KeyValuePair<string, string>> SplitDistinguishedNameParts(string distinguishedName)
    {
        foreach( string eqPair in distinguishedName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) )
        {
            string[] nameValuePair = eqPair.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            if( nameValuePair.Length != 2 )
                continue;

            string name = nameValuePair[0].Trim();
            string value = nameValuePair[1].Trim();

            yield return new KeyValuePair<string, string>(name, value);
        }
    }

    static LdapConnection CreateConnection()
    {
        LdapConnection connection = new LdapConnection(LdapSettings.LdapServer);
        connection.AuthType = AuthType.Basic;

        return connection;
    }

    static LdapConnection BindConnectionSearchAccount()
    {
        LdapConnection connection = CreateConnection();
        connection.Bind(new NetworkCredential(
           LdapSettings.SearchAccountDistinguishedName,
           LdapSettings.SearchAccountPassword));

        return connection;
    }

    static LdapConnection BindConnectionUserAccount(string distinguishedName, string password)
    {
        LdapConnection connection = CreateConnection();
        connection.Bind(new NetworkCredential(
           distinguishedName,
           password));

        return connection;
    }

    static SearchRequest CreateSearchRequest(string filter)
    {
        if( !string.IsNullOrEmpty(LdapSettings.Filter) )
        {
            if( filter == null )
                filter = LdapSettings.Filter;
            else
                filter = filter + "," + LdapSettings.Filter;
        }

        SearchRequest req = new SearchRequest(
                LdapSettings.SearchBase,
                filter,
                SearchScope.Subtree,
                "dn","mail");

        return req;
    }

    static SearchResponse SearchGetResponse(string filter, LdapConnection connection)
    {
        SearchRequest request = CreateSearchRequest(filter);
        return (SearchResponse)connection.SendRequest(request);
    }
}