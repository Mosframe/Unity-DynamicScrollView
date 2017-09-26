/**
 * xRichText.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe
{
    using System.Text;
	
	/// <summary>
	/// xRichText
	/// </summary>
	public class RichText
	{
        public static string bold       ( object text                   ) { return new StringBuilder("<b>"       ).Append( text  ).Append("</b>").ToString(); }
        public static string italic     ( object text                   ) { return new StringBuilder("<i>"       ).Append( text  ).Append("</i>").ToString(); }
        public static string size       ( object text, int      size    ) { return new StringBuilder("<size="    ).Append( size  ).Append(">").Append(text).Append("</size>" ).ToString(); }
        public static string color      ( object text, string   color   ) { return new StringBuilder("<color="   ).Append( color ).Append(">").Append(text).Append("</color>").ToString(); }

        // fixed colors

        public static string aqua       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.aqua       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string black      ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.black      ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string blue       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.blue       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string brown      ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.brown      ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string cyan       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.cyan       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string darkBlue   ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.darkBlue   ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string green      ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.green      ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string grey       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.grey       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string lightBlue  ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.lightBlue  ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string lime       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.lime       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string magenta    ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.magenta    ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string maroon     ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.maroon     ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string navy       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.navy       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string olive      ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.olive      ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string orange     ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.orange     ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string purple     ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.purple     ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string red        ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.red        ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string silver     ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.silver     ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string teal       ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.teal       ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string white      ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.white      ).Append(">").Append(text).Append("</color>").ToString(); }
        public static string yellow     ( object text ) { return new StringBuilder("<color="   ).Append( HtmlColor.yellow     ).Append(">").Append(text).Append("</color>").ToString(); }
    }
}

