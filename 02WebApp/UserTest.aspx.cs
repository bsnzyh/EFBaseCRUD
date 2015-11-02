using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _02WebApp
{
    public partial class UserTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            P03BLL.Msg bll = new P03BLL.Msg();
           
            Response.Write(bll.TestForEf());
        }
    }
}