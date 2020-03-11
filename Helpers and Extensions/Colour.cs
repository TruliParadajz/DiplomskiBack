using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Helpers_and_Extensions
{
    public class Colour
    {
        public string Value { get; set; }
        public Colour(string value)
        {
            Value = value;
        }

        public static Colour Red { get { return new Colour("#ad2121"); } }
        public static Colour Blue { get { return new Colour("#1e90ff"); } }
        public static Colour Yellow { get { return new Colour("#e3bc08"); } }
    }
}
