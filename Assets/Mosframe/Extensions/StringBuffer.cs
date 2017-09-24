/**
 * StringBuffer.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe
{
    using System.Text;

	/// <summary>
	/// String Buffer
	/// </summary>
	public static class StringBuffer
	{
        public static StringBuilder set( string s ) { _buffer.Length=0; return _buffer.Append(s); }

        private static StringBuilder _buffer = new StringBuilder(4096);
	}
}

