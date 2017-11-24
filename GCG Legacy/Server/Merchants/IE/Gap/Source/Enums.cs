using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace GCGCommon
{
    public enum HTMLEnumTagNames { a, input, button, img, hr, div, select, formNOTDONE, area, };
    public enum HTMLEnumAttributes { id, name, src, value, InnerText, innerHTML, OuterHtml, classs, OuterText, href, alt };
    public enum GCTypes { GCBALANCE, GCBALANCEERR, GCNEEDSMOREINFO, GCTIMEOUT, GCCAPTCHA, GCERR, GCCUSTOM };
    public enum JanitorTypes { JEXEMISSING, JNOTRUNNING };
    public enum WebserviceTypes { WSERR, WSTIMEOUT, WSINVALIDSESSION, WSBLOCKEDIP };
    public enum Delimiters { [Description("^)(")] LINEDEL, [Description("~_~")] POSDEL };
    public static class EnumExtensions
    {
        public enum Delimiters { [Description("^)(")] LINEDEL, [Description("~_~")] POSDEL };
        public enum GCTypes { GCBALANCE, GCBALANCEERR, GCNEEDSMOREINFO, GCTIMEOUT, GCCAPTCHA, GCERR, GCCUSTOM };
        public enum JanitorTypes { JEXEMISSING, JNOTRUNNING };
        public enum WebserviceTypes { WSERR, WSTIMEOUT, WSINVALIDSESSION, WSBLOCKEDIP };
        public static string Description(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute),
                                                       false);
            return attributes.Length == 0
                ? value.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }
    }
}