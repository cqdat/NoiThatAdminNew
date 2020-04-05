using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoiThatAdmin.Models.DataModels;

namespace NoiThatAdmin.Models
{
    public class MenuViewModel
    {
        public List<Category> lstMenuCha { get; set; }
        //public List<Category> lstMenuCon { get; set; }
        public List<Category> lstCollectionParent { get; set; }
        public List<Category> lstNews { get; set; }
    }
}