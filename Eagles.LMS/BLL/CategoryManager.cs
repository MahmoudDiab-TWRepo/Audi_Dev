using Eagles.LMS.Data;
using Eagles.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.BLL
{
    public class CategoryManager : Reposatory<Category>
    {
        public CategoryManager(EmcNewsContext ctx) : base(ctx)
        {

        }
        public int GetCount(int id)
        {
            return ctx.cars.Where(s => s.CategoryId == id).Count();
        }

        public List<Category> GetCategorieswithCount()
        {
            List<Category> categories = ctx.Categories.ToList();
            foreach (var category in categories)
            {
                category.Count = GetCount(category.ID);
            }
            return categories;
        }
    }
}