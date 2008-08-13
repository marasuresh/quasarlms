using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;

public static class Regions
{
    static readonly Dictionary<Guid, RegionInfo> regionsByID;
    static readonly Dictionary<string, RegionInfo> regionsByCode;
    static readonly RegionInfo globalRegion;

    static readonly IList<RegionInfo> m_AllRegionsNoGlobal;
    static readonly IList<RegionInfo> m_AllRegionsWithGlobal;

    public sealed class RegionInfo
    {
        readonly Guid? m_ID;
        readonly string m_Name;

        public Guid? ID
        {
            get { return m_ID; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public RegionInfo(
            Guid? id,
            string name)
        {
            this.m_ID = id;
            this.m_Name = name;
        }
    }

    private static int CompareByName(RegionInfo x, RegionInfo y)
    {
        if( x==y )
            return 0;

        if( x == null )
            return -1;
        if( y == null )
            return 1;

        string xName = x.Name;
        string yName = y.Name;

        if( x==globalRegion )
            xName = "";
        if( y==globalRegion )
            yName = "";

        int result = string.Compare(
            xName,
            yName,
            StringComparison.OrdinalIgnoreCase);

        if( result==0 )
            return string.Compare(
                x.ID.ToString(),
                y.ID.ToString(),
                StringComparison.OrdinalIgnoreCase);
        else
            return result;
    }

    public static RegionInfo Global
    {
        get { return globalRegion; }
    }

    public static IList<RegionInfo> GetAllRegionsWithGlobal()
    {
        return m_AllRegionsWithGlobal;
    }

    public static IList<RegionInfo> GetAllRegionsNoGlobal()
    {
        return m_AllRegionsNoGlobal;
    }

    public static IList<RegionInfo> GetRegionsForRead()
    {
        if( CurrentUser.Role.IsGlobal )
        {
            return GetAllRegionsWithGlobal();
        }
        else
        {
            if( CurrentUser.Region == Regions.Global )
            {
                return new RegionInfo[] { globalRegion };
            }
            else
            {
                return new RegionInfo[] { globalRegion, CurrentUser.Region };
            }
        }
    }

    public static IList<RegionInfo> GetRegionsForWrite()
    {
        if( CurrentUser.Role.IsGlobal )
        {
            return GetAllRegionsWithGlobal();
        }
        else
        {
            if( CurrentUser.Region == Regions.Global )
            {
                return new RegionInfo[] {};
            }
            else
            {
                return new RegionInfo[] { CurrentUser.Region };
            }
        }
    }

    public static RegionInfo FindByCode(string regionCode)
    {
        if( regionCode == null )
            return globalRegion;

        RegionInfo result;
        if( regionsByCode.TryGetValue(regionCode, out result) )
            return result;
        else
            return null;
    }

    public static RegionInfo FindByID(Guid? regionID)
    {
        if( regionID == null )
            return globalRegion;
        else
            return regionsByID[regionID.Value];
    }

    static Regions()
    {
        ConfigData.RegionsDataTable regionsTable = new ConfigDataTableAdapters.RegionsTableAdapter().GetData();

        regionsByID = new Dictionary<Guid,RegionInfo>(regionsTable.Count);
        regionsByCode = new Dictionary<string,RegionInfo>(regionsTable.Count+5,StringComparer.OrdinalIgnoreCase);
        
        foreach( ConfigData.RegionsRow regionRow in regionsTable )
        {
            if( regionRow.IsIDNull() )
            {
                RegionInfo region = new RegionInfo(null,regionRow.Name);
                globalRegion = region;
            }
            else
            {
                RegionInfo region = new RegionInfo(regionRow.ID,regionRow.Name);
                regionsByID.Add(regionRow.ID, region);

                foreach( string regionCode in regionRow.CodesCommaSeparated.Split(',') )
                {
                    regionsByCode.Add(regionCode, region);
                }
            }
        }

        if( globalRegion == null )
        {
            StringBuilder regionTableDump;
            if( regionsTable.Count == 0 )
            {
                regionTableDump = new StringBuilder("No rows in Regions table.");
            }
            else
            {
                regionTableDump = new StringBuilder("Rows in Regions table (" + regionsTable.Count + "):");
                regionTableDump.AppendLine();
                foreach( ConfigData.RegionsRow regionRow in regionsTable )
                {
                    regionTableDump.AppendLine(
                        "  "+(regionRow.IsIDNull() ? "(null)" : regionRow.ID.ToString())+": "+
                        regionRow.CodesCommaSeparated+" \""+regionRow.Name+"\"");
                }
            }

            throw new ConfigurationErrorsException(
                "There is no global region in Regions table (ID if global region have to be NULL).\r\n\r\n"+regionTableDump);
        }

        List<RegionInfo> allRegionsNoGlobal = new List<RegionInfo>(regionsByID.Values);
        allRegionsNoGlobal.Sort(CompareByName);

        m_AllRegionsNoGlobal = allRegionsNoGlobal.AsReadOnly();

        List<RegionInfo> allRegionsWithGlobal = new List<RegionInfo>(allRegionsNoGlobal.Count);
        allRegionsWithGlobal.Add(globalRegion);
        allRegionsWithGlobal.AddRange(allRegionsNoGlobal);

        m_AllRegionsWithGlobal = allRegionsWithGlobal.AsReadOnly();
    }
}