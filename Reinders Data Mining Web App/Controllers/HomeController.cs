﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reinders_Data_Mining_Web_App.Models;
using Reinders_Data_Mining_Web_App.Library;
using System.IO;
using HtmlAgilityPack;

namespace Reinders_Data_Mining_Web_App.Controllers
{
    public class HomeController : Controller
    {
        Models.Filter filter = new Models.Filter();
        // GET: Home
        public ActionResult Index()
        {
            /*string test = "Hello";
            ViewBag.Test = test;*/
            return View();
        }

        public JsonResult Test()
        {
            string blah = "This is test reply";
            return Json(blah, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTargetSource(string url)
        {
            HTTPSocket socket = new HTTPSocket(url);
            string source = socket.GetSource();

            if (source == "invalid")
                return Json(source, JsonRequestBehavior.AllowGet);


            HtmlDocument htmlDoc = new HtmlDocument(); //urgh sooo ugly
            htmlDoc.LoadHtml(source);
            HtmlNode head = htmlDoc.DocumentNode.SelectSingleNode("//head");
            string newBaseContent = string.Format("<base href='http://{0}'/>", socket.Host);
            string newCssLinkContent = "<link href=\"/Content/remote.css\" rel=\"stylesheet\" type=\"text/css\">";
            HtmlNode newBase = HtmlNode.CreateNode(newBaseContent);
            HtmlNode newCssLink = HtmlNode.CreateNode(newCssLinkContent);
            head.PrependChild(newBase);
            head.PrependChild(newCssLink);
            source = htmlDoc.DocumentNode.InnerHtml;
            return Json(source, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FileUpload(HttpPostedFileBase uploadFile)
        {
            if (uploadFile.ContentLength > 0)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("../Uploads"),
                                               Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
            }
            return RedirectToAction("Index");
        }
    }
}