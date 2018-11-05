using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace StyleGuideUI
{
    /// <summary>
    /// Summary description for ArticleFr1
    /// </summary>
    public class ArticleFrHandler : IHttpHandler, IReadOnlySessionState
    {
        protected HttpContext thisContext;

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Write(string pStr)
        {
            //System.Threading.Thread.Sleep(1000);
            thisContext.Response.Write(pStr);
        }

        public void ProcessRequest(HttpContext context)
        {
            thisContext = context;
            thisContext.Response.ContentType = "text/plain";
            NameValueCollection parameters;
            if (thisContext.Request.Form.HasKeys())
            {
                parameters = thisContext.Request.Form;
            }
            else if (thisContext.Request.QueryString.HasKeys())
            {
                parameters = thisContext.Request.QueryString;
            }
            else
            {
                parameters = null;
            }

            if (parameters != null)
            {
                string command = parameters["cmd"];
                switch (command)
                {
                    case "getStyleGuideEntities":
                        {
                            if (string.IsNullOrWhiteSpace(parameters["content"]))
                            {
                                Write("parameter content required ");
                                thisContext.Response.End();
                            }
                            string content = parameters["content"]; //if conetent contains '&' will break the content, below case will be fine, but it will have + sign between words
                            //string content = parameters.ToString();
                            //content = content.Replace("cmd=getStyleGuideEntities&content=", "");

                            if(!string.IsNullOrWhiteSpace(content))
                            {
                                StyleGuideUI.App_Code.ArticleFr afr = new StyleGuideUI.App_Code.ArticleFr();
                                Write(afr.getSgEntities(content));
                                afr = null;
                            }
                            break;
                        }
                    case "getEntity":
                        {
                            if (string.IsNullOrWhiteSpace(parameters["id"]))
                            {
                                Write("parameter id required ");
                                thisContext.Response.End();
                            }
                            string id = parameters["id"];
                            if (!string.IsNullOrWhiteSpace(id))
                            {
                                StyleGuideUI.App_Code.ArticleFr afr = new StyleGuideUI.App_Code.ArticleFr();
                                Write(afr.getEntity(Convert.ToInt64(id)));
                                afr = null;
                            }
                            break;
                        }
                    case "reinitialize":
                        {
                            StyleGuideUI.App_Code.ArticleFr afr = new StyleGuideUI.App_Code.ArticleFr();
                            afr.InitEntities();
                            afr = null;
                            break;
                        }
                    default:
                        {
                            Write("Invalid cmd");
                            break;
                        }
                }
            }
            else
            {
                Write("Parameters required");
            }

        }
    
    }
}