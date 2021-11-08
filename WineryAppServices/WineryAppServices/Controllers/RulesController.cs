using CodeEffects.Rule.Core;
using CodeEffects.Rule.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using WineryAppServices.CodeEffectsRules;
using WineryAppServices.Models;

namespace WineryAppServices.Controllers
{
    public class RulesController : Controller
    {
        //Initialize an Database instance
        private WineryAppServicesContext db = new WineryAppServicesContext();

        // GET: Rules
        public ActionResult RuleEditor()
        {
            ViewBag.Rule = RuleModel.Create(typeof(UserData));
            return View();
        }

        [HttpPost]
        // Notice that the name of the only parameter
        // matches the server ID set in the view
        public ActionResult Save(RuleModel ruleEditor)
        {
            // We need to "bind" the source type to the rule model
            ruleEditor.BindSource(typeof(UserData));

            // Add the rule model to the ViewBag object
            ViewBag.Rule = ruleEditor;

            // Make sure that the Rule Area is not empty and the current rule is valid
            if (ruleEditor.IsEmpty() || !ruleEditor.IsValid())
            {
                return View("RuleEditor");
            }
            
            // Get Rule XML
            string xml = ruleEditor.GetRuleXml();


            //XDocument xmlDoc = XDocument.Parse(xml);
            //var val = xmlDoc.Descendants("rule")
            //                .Attributes("name")
            //                .FirstOrDefault();

            //var query = from x in xmlDoc.Descendants("rule")
            //           select new
            //           {                           
            //               Name = x.Descendants("name")
            //           };

            // Save the rule to your database
            SaveXMLFileToDb(xml,ruleEditor.Id);

            // Done. Load the view
            return View("RuleEditor");
        }


        /// <summary>
        /// Save xml rule into XmlRules table
        /// </summary>
        /// <param name="file"></param>
        /// <param name="ruleEditorId"></param>
        //Store the rule to db
        public void SaveXMLFileToDb (String file, string ruleEditorId)
        {            

            XMLRule rule = new XMLRule()
            {
                RuleEditorId = ruleEditorId,
                xmlRule = file

            };
            db.XMLRules.Add(rule);
            db.SaveChanges();
        }   
    }
}