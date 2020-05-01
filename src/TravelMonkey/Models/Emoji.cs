using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMonkey.Models
{
    /// <summary>
    /// Emoji class.
    /// Source: https://github.com/egbakou/EmojisInXamarinForms
    /// </summary>
    public class Emoji
    {
        readonly int[] codes;

        public Emoji(int[] codes)
        {
            this.codes = codes;
        }

        public Emoji(int code)
        {
            codes = new int[] { code };
        }

        public override string ToString()
        {
            if (codes == null)
                return string.Empty;

            var sb = new StringBuilder(codes.Length);

            foreach (var code in codes)
                sb.Append(Char.ConvertFromUtf32(code));

            return sb.ToString();
        }
    }
}
