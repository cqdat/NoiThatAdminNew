using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NoiThatAdmin.Utilities
{
    public class WebConstants
    {
        public static string ImgProduct = ConfigurationManager.AppSettings["ImagesBasePath"];
        public static string ImgProductThumbs = "~/Photos/Products/_thumbs";

        public static string ImgProductShow = "~/Photos/Products";
        public static string ImgProductThumbsShow = "Photos/Products/_thumbs";

        public static string imgNewsURL = "~/Photos/News";
        public static string imgNewsURLShow = "/Photos/News";

        public static string DateFormatVI = "dd/MM/yyyy";
        public static string DatetimeFormatVI = "dd/MM/yyyy HH:mm";

        public static int CategoryProduct = 1;
        public static int CategoryCollection = 2;
        public static int CategoryNews = 3;

        public static int BlogNews = 1;
        public static int BlogCollection = 2;
    }
}