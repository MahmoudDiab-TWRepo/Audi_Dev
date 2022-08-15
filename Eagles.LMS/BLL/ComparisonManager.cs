using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class ComparisonManager : Reposatory<Comparison>
    {
        public ComparisonManager(LMS.Data.EmcNewsContext ctx) : base(ctx) { }
    }
}