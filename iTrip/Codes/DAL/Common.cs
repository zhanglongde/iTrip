using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public class Common
{
    public Common()
    {
    }

    /// <summary>
    /// 数据库连接串
    /// </summary>
    public static readonly string ConnString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
}
