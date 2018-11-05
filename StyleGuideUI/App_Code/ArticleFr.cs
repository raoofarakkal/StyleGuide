using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace StyleGuideUI.App_Code
{
    public class ArticleFr
    {
        private static StyleGuide.SgEntities.Entities entities = null;
        public bool InitEntities()
        {
            bool ret = false;
            StyleGuide.API api = new StyleGuide.API();
            try
            {
                ArticleFr.entities = api.getAllEntities(null, true);
            }
            finally
            {
                api.Dispose();
            }
            return ret;
        }

        public string getEntity(long id)
        {
            if (entities == null)
            {
                InitEntities();
            }
            string ret = "";
            var results = (from e in ArticleFr.entities where e.ID == id select new {e.Name,e.Notes,e.Suggestions,e.Suspects}).FirstOrDefault();
            if (results != null)
            {
                string comma = "";
                string[] suggestions = new string[0];
                if (!string.IsNullOrWhiteSpace(results.Suggestions))
                {
                    suggestions = results.Suggestions.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                string[] suspects = new string[0];
                if (!string.IsNullOrWhiteSpace(results.Suspects))
                {
                    suspects = results.Suspects.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                string mNotes = results.Notes;
                if (!string.IsNullOrWhiteSpace(mNotes))
                {
                    mNotes = mNotes.Replace('"', '\'');
                }
                else
                {
                    mNotes ="";
                }
                ret = string.Format("\"Name\":\"{0}\"" + ",\"Notes\":\"{1}\",\"TotalSuspects\":\"{2}\",\"TotalSuggestions\":\"{3}\", \"Suggestions\":[", results.Name, mNotes, suspects.Length, suggestions.Length);
                foreach (string suggestion in suggestions)
                {
                    ret += comma + "\"" + suggestion + "\"";
                    comma = ",";
                }
                ret += "],\"Suspects\":[";
                comma = "";
                foreach (string suspect in suspects)
                {
                    ret += comma + "\"" + suspect + "\"";
                    comma = ",";
                }
                ret += "]";

                ret = ret.Replace("\r", "");
                ret = ret.Replace("\n", "");
            }
            return "{" + ret + "}";

        }

        public string getSgEntities(string content)
        {
            if (entities == null)
            {
                InitEntities();
            }
            ignoredWords iw = new ignoredWords();
            string comma = "";
            int count = 0;
            List<EntityFr> Efrs = new List<EntityFr>();
            string JSON = "";

            IEnumerable<string> Contents = Regex.Matches(content, "[a-z]+", RegexOptions.IgnoreCase)
                            .Cast<Match>()
                            .Select(m => m.Value);
            //string tContent = content.ToLower();

            foreach(string word1 in Contents)
            {
                string w = "";
                if (!string.IsNullOrWhiteSpace(word1))
                {
                    w = word1.Replace("\r", ""); ;
                    w = w.Replace("\n", "");
                    if (!string.IsNullOrWhiteSpace(w))
                    {
                        //if (true)//!iw.Words.Contains(w.ToLower()))
                        //{
                        //    //EntityFr ent = getSgEntity(w);
                        //}
                        List<EntityFr> ents = _getSgEntities(w);
                        if (ents != null)
                        {
                            foreach (EntityFr ent in ents)
                            {
                                if (ent != null)
                                {
                                    if (!exist(ent, Efrs))
                                    {
                                        List<string> kw = new List<string>();
                                        //kw.Add(ent.Name.ToLower());
                                        if (ent.Suspects != null)
                                        {
                                            foreach (string s in ent.Suspects.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
                                            {
                                                kw.Add(s);
                                            }
                                            if (ContainsListOfWords(content, kw)) // (tContent.Contains(ent.Name.ToLower()))
                                            {
                                                Efrs.Add(ent);
                                                JSON += comma + ent.toJSON();
                                                comma = ",";
                                                count++;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    
                    }
                }
            }
            Efrs = null;
            return "{\"TotalEntities\":\"" + count + "\",\"Entities\":[" + JSON + "]}";
        }

        private bool ContainsListOfWords(string source, List<string> keywords)
        {
            bool ret = false;
            foreach (string kw in keywords)
            {
                if (source.ToLower().Contains(kw.ToLower()))
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        private bool exist(EntityFr ent,List<EntityFr> ents)
        {
            var result = (from e in ents where e.Name == ent.Name select new { }).Count();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private EntityFr _getSgEntity(string word)
        {
            EntityFr ret = null;
            var results = (
                from e in ArticleFr.entities 
                where 
                    //(                    
                    //    e.Name != null && e.Name.ToLower().Contains(word.ToLower())
                    //)
                    //|| 
                    (
                        e.Suspects != null && e.Suspects.ToLower().Contains(word.ToLower())
                    )
                select new { e.ID, e.Name, e.Suspects, e.Suggestions }).FirstOrDefault();
            if(results!=null){
                ret = new EntityFr(results.ID, results.Name, word, results.Suspects,results.Suggestions);
            }
            return ret;
        }

        private List<EntityFr> _getSgEntities(string word)
        {
            List<EntityFr> ret = null;
            var results = (
                from e in ArticleFr.entities
                where
                    //(                    
                    //    e.Name != null && e.Name.ToLower().Contains(word.ToLower())
                    //)
                    //|| 
                    (
                        e.Suspects != null && e.Suspects.ToLower().Contains(word.ToLower())
                    )
                select new { e.ID, e.Name, e.Suspects, e.Suggestions }
                );
            foreach (var result in results)
            {
                if (result != null)
                {
                    if (ret == null)
                    {
                        ret = new List<EntityFr>();
                    }
                    ret.Add(new EntityFr(result.ID, result.Name, word, result.Suspects, result.Suggestions));
                }

            }
            return ret;
        }


        private class ignoredWords{

            private static IEnumerable<string> words;

            public ignoredWords(){
                if (words == null)
                {
                    string IgnoredWords = HttpContext.Current.Server.MapPath("~/IgnoredWords/") + "IgnoredWords.txt";
                    if (!File.Exists(IgnoredWords))
                    {
                        throw new Exception("Cannot find IgnoredWords File. " + IgnoredWords);
                    }
                    words = Regex.Matches(File.ReadAllText(IgnoredWords).ToLower(), "[a-z]+", RegexOptions.IgnoreCase)
                            .Cast<Match>()
                            .Select(m => m.Value);
                }

            }
            public IEnumerable<string> Words
            {
                get { return words; }
            }


        }

        private class EntityFr
        {
            public EntityFr(long id, string name, string searchWord, string suspects,string suggestions)
            {
                ID = id;
                Name = name;
                SearchWord = searchWord;
                Suspects = suspects;
                Suggestions = suggestions;
            }
            public long ID { get; set; }
            public string Name { get; set; }
            public string SearchWord { get; set; }
            public string Suspects { get; set; }
            public string Suggestions { get; set; }

            public string toJSON()
            {
                string ret = "";
                string comma = "";
                string[] suspects = new string[0];
                string[] suggestions = new string[0];
                if (!string.IsNullOrWhiteSpace(Suspects))
                {
                    suspects = Suspects.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
                if (!string.IsNullOrWhiteSpace(Suggestions))
                {
                    suggestions = Suggestions.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                }


                ret = "{" + string.Format("\"ID\":\"{0}\",\"Entity\":\"{1}\",\"searchword\":\"{2}\",\"TotalSuspects\":\"{3}\",\"TotalSuggestions\":\"{4}\", \"Suspects\":[", ID, Name, SearchWord, suspects.Length, suggestions.Length);
                foreach (string suspect in suspects)
                {
                    ret += comma + "\"" + suspect  + "\"";
                    comma = ",";
                }
                comma = "";
                ret += "], \"Suggestions\":[";
                foreach (string suggestion in suggestions)
                {
                    ret += comma + "\"" + suggestion + "\"";
                    comma = ",";
                }
                ret += "]}";

                ret = ret.Replace("\r","");
                ret = ret.Replace("\n", "");
                return ret; 
                //return "{" + string.Format("'ID':'{0}','Entity':'{1}'", ID, Name) + "}";
            }
        }

    }


}