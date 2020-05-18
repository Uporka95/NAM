using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab2
{
	class Program
	{
		static void Main(string[] args)
		{
			MarkovMachine mm = new MarkovMachine("aababa");

			mm.addMatch(new Match("*b", "#b", false));
			mm.addMatch(new Match("*a", "@a", false));

			mm.addMatch(new Match("#a", "a#", false));
			mm.addMatch(new Match("@a", "a@", false));
			mm.addMatch(new Match("@b", "b@", false));
			mm.addMatch(new Match("#b", "b#", false));

			mm.addMatch(new Match("a@", "!", false));
			mm.addMatch(new Match("b#", "!", false));
			mm.addMatch(new Match("a#", "a=", false));
			mm.addMatch(new Match("b@", "b=", false));

			mm.addMatch(new Match("a=", "=a", false));
			mm.addMatch(new Match("b=", "=b", false));
			mm.addMatch(new Match("a!", "!a", false));
			mm.addMatch(new Match("b!", "!b", false));

			mm.addMatch(new Match("!a", "", true));
			mm.addMatch(new Match("!b", "", true));
			mm.addMatch(new Match("=a", "a", true));
			mm.addMatch(new Match("=b", "b", true));

			mm.addMatch(new Match(" ", "*", false));

			mm.addMatch(new Match("b", "#b", false));
			mm.addMatch(new Match("a", "@a", false));

			mm.Start();

		}
	}
}

