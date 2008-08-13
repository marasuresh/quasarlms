using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using sec = System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class Tools_RegionEditControl : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        BuildRegionList();
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    public Guid? RegionGuid
    {
        get
        {
            if( string.IsNullOrEmpty(RegionID) )
                return null;
            else
                return GuidService.Parse(RegionID);
        }
        set
        {
            if( value == null )
                RegionID = null;
            else
                RegionID = value.Value.ToString();
        }
    }

    public string RegionID
    {
        get
        {
            return regionDropDownList.SelectedValue;
        }
        set
        {
            if( CurrentUser.Role.IsGlobal )
            {
                if( string.IsNullOrEmpty(value) )
                    value = "";
                regionDropDownList.SelectedValue = value;
            }
            else
            {
                regionDropDownList.Items.Clear();
                regionDropDownList.Items.Add(GetItemOfRegion(value));
            }
        }
    }

    ListItem GetItemOfRegion(string regionID)
    {
        if( string.IsNullOrEmpty(regionID) )
            return GetItemOfRegion((Guid?)null);
        else
            return GetItemOfRegion(GuidService.Parse(regionID));
    }

    ListItem GetItemOfRegion(Guid? regionID)
    {
        return GetItemOfRegion(Regions.FindByID(regionID));
    }

    ListItem GetItemOfRegion(Regions.RegionInfo region)
    {
        string id;
        if( region.ID == null )
            id = "";
        else
            id = region.ID.ToString();

        return new ListItem(region.Name, id);
    }

    Guid? GetBindingValue(object value)
    {
        if( value is Guid )
            return (Guid)value;
        else if( value is string && ((string)value).Length > 0 )
            return GuidService.Parse(value.ToString());
        else
            return null;
    }

    void BuildRegionList()
    {
        regionDropDownList.Items.Clear();
        if( CurrentUser.Role.IsGlobal )
        {
            List<Regions.RegionInfo> regionList = new List<Regions.RegionInfo>(Regions.GetAllRegionsWithGlobal());
            regionList.Sort(delegate(Regions.RegionInfo r1, Regions.RegionInfo r2)
            {
                if( r1.ID == null )
                    return (r1.ID != null).CompareTo(r2.ID != null);
                else
                    return string.Compare(r1.Name, r2.Name, StringComparison.OrdinalIgnoreCase);
            });

            foreach( Regions.RegionInfo region in regionList )
            {
                ListItem item = GetItemOfRegion(region);
                regionDropDownList.Items.Add(item);
            }
        }
        else
        {
            regionDropDownList.Items.Add(GetItemOfRegion(CurrentUser.Region.ID));
        }
    }
}