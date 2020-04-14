using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NoiThatAdmin.Utilities
{
    public static class WebConstants
    {
        public static string ImgProduct = ConfigurationManager.AppSettings["ImgProduct"];
        public static string ImgProductThumbs = ConfigurationManager.AppSettings["ImgProductThumbs"];

        public static string ImgProductShow = ConfigurationManager.AppSettings["ImgProductShow"];
        public static string ImgProductThumbsShow = ConfigurationManager.AppSettings["ImgProductThumbsShow"];

        public static string ImgCollections = ConfigurationManager.AppSettings["ImgCollections"];
        public static string ImgImgCollectionsThumbs = ConfigurationManager.AppSettings["ImgImgCollectionsThumbs"];
        public static string ImgCollectionsShow = ConfigurationManager.AppSettings["ImgCollectionsShow"];

        public static string ImgSlideshow = ConfigurationManager.AppSettings["ImgSlideshow"];

        //public static string ImgProductShow = ConfigurationManager.AppSettings["ImgProductShow"];
        //public static string ImgProductThumbsShow = ConfigurationManager.AppSettings["ImgProductThumbsShow"];

        public static string imgNewsURL = "~/Photos/News";
        public static string imgNewsURLShow = "/Photos/News";

        public static string imgLogoCust = "~/Photos/LogosCust";
        public static string imgLogoCustShow = "/Photos/LogosCust";

        public static string DateFormatVI = "dd/MM/yyyy";
        public static string DatetimeFormatVI = "dd/MM/yyyy HH:mm";

        public static int CategoryProduct = 1;
        public static int CategoryCollection = 2;
        public static int CategoryNews = 3;
        public static int CategoryAboutUs = 4;

        public static int BlogNews = 1;
        public static int BlogCollection = 2;
        public static int BlogAboutUs = 4;
        public static int BlogAboutUs_more = 5;

        public static int ProductMoi = 1;
        public static int ProductNoiBat = 2;
        public static int ProductBanChay = 3;
        public static int ProductDangKM = 4;
    }
}