using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _01FirstEF
{
    public partial class User
    {
        public override string ToString()
        {
            return this.uId + "," + this.uName;
        }
    }
}
