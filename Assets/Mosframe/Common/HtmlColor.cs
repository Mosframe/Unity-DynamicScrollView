/**
 * HtmlColor.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe
{
    using UnityEngine;

	/// <summary>
	/// HTML Color Tags
    /// <para>Reference : https://docs.unity3d.com/Manual/StyledText.html </para>
	/// </summary>
	public class HtmlColor
	{
		public const string aqua		= "#00ffffff";
		public const string black		= "#000000ff";
		public const string blue		= "#0000ffff";
		public const string brown		= "#a52a2aff";
		public const string cyan		= "#00ffffff";
		public const string darkBlue	= "#0000a0ff";
		public const string green		= "#008000ff";
		public const string grey		= "#808080ff";
		public const string lightBlue	= "#add8e6ff";
		public const string lime		= "#00ff00ff";
		public const string magenta		= "#ff00ffff";
		public const string maroon		= "#800000ff";
		public const string navy		= "#000080ff";
		public const string olive		= "#808000ff";
		public const string orange		= "#ffa500ff";
		public const string purple		= "#800080ff";
		public const string red			= "#ff0000ff";
		public const string silver		= "#c0c0c0ff";
		public const string teal		= "#008080ff";
		public const string white		= "#ffffffff";
		public const string yellow		= "#ffff00ff";

        /// <summary> Color32 to HtmlColor </summary>
        public static string convert( Color32 value ) {
            return string.Format("#{0:x02}{1:x02}{2:x02}{3:x02}", value.r, value.g, value.b, value.a );
        }
        /// <summary> Color to HtmlColor </summary>
        public static string convert( Color value ) {
            var color32 = (Color32)value;
            return string.Format("#{0:x02}{1:x02}{2:x02}{3:x02}", color32.r, color32.g, color32.b, color32.a );
        }

        /// <summary> int(RGB) to HtmlColor </summary>
        public static string convert( int value ) {
            return string.Format("#{0:x02}{1:x02}{2:x02}ff", (value & 0x00FF0000) >> 16, (value & 0x0000FF00) >> 8, (value & 0x000000FF) >> 0);
        }

        /// <summary> uint(ARGB) to HtmlColor </summary>
        public static string convert( uint value ) {
            return string.Format("#{0:x02}{1:x02}{2:x02}{3:x02}", (value & 0x00FF0000) >> 16, (value & 0x0000FF00) >> 8, (value & 0x000000FF) >> 0, (value & 0xFF000000) >> 24);
        }
	}
}

