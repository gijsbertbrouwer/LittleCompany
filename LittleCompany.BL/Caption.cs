using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCompany.BL
{

    public class Caption
    {
        // Languagecode, dictionary of captions
        private Dictionary<string, BO.Language> languages;

        public void Load()
        {
            languages = new DAL.Captions().Get_CaptionsAll();
        }




        public List<BO.Caption> GetCaptionListFromLanguage(string code, string languagecode)
        {


            // first time? load
            if (languages == null || languages.Count == 0) { Load(); }


            // no language? log and return
            if (!languagecode.Contains(languagecode))
            {

                new DAL.Logger().Log("BL.Caption", string.Format("(GetCaption) - The language {1} was not found", languagecode));
                return null;
            }


            var lang = languages[languagecode];


            var r = new List<BO.Caption>();

            foreach (var item in lang.captions)
            {
                r.Add(item.Value);
            }

            return r;



        }
        public BO.Caption GetCaption(string code, string languagecode, BO.CaptionType type)
        {


            // first time? load
            if (languages == null || languages.Count == 0) { Load(); }


            // no language? log and return
            if (!languagecode.Contains(languagecode))
            {

                new DAL.Logger().Log("BL.Caption", string.Format("(GetCaption) - The language {1} was not found", languagecode));
                return null;
            }


            var lang = languages[languagecode];

            // Add Singular || Plural
            if (code.EndsWith("_") && type != BO.CaptionType.None) { code += type.ToString(); }


            if (lang.captions.ContainsKey(code))
            {
                // return correct caption
                return lang.captions[code];
            }
            else
            {
                // caption could not be found
                new DAL.Logger().Log("BL.Captions", string.Format("(GetCaption) - The caption: {0} was not found in language {1}", code, languagecode));
                return null;
            }




        }

    }
}