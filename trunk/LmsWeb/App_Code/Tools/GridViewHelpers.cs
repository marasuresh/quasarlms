using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public static class GridViewHelpers
{
    public static Guid GetKeyByCommandArgument(
        object commandArgument,
        GridView gridViewControl)
    {
        int rowIndex;

        if( commandArgument is string )
            rowIndex = int.Parse(commandArgument.ToString(), System.Globalization.CultureInfo.InvariantCulture);
        else
            rowIndex = (int)(commandArgument);

        try
        {
            Guid key = (Guid)(
                gridViewControl.DataKeys[
                    gridViewControl.Rows[rowIndex].DataItemIndex
                        % gridViewControl.PageSize].Value);

            return key;
        }
        catch( Exception error )
        {
            throw;
        }
    }
}
