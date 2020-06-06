//Program.cs
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

			NAM nam = new NAM("ababbb");

			nam.AddRule(new Rule("?a!", "#!a", false));
			nam.AddRule(new Rule("?b!", "#!b", false));
			nam.AddRule(new Rule("*a#", "a*?", false));
			nam.AddRule(new Rule("*b#", "b*?", false));
			nam.AddRule(new Rule("?aa", "a?a", false));
			nam.AddRule(new Rule("?ab", "b?a", false));
			nam.AddRule(new Rule("?ba", "a?b", false));
			nam.AddRule(new Rule("?bb", "b?b", false));
			nam.AddRule(new Rule("aa#", "a#a", false));
			nam.AddRule(new Rule("ab#", "b#a", false));
			nam.AddRule(new Rule("ba#", "a#b", false));
			nam.AddRule(new Rule("bb#", "b#b", false));

			nam.AddRule(new Rule("*#!", "", true));

			nam.AddRule(new Rule("!a", "a!", false));
			nam.AddRule(new Rule("!b", "b!", false));
			nam.AddRule(new Rule("a!", "a#!", false));
			nam.AddRule(new Rule("b!", "b#!", false));
			nam.AddRule(new Rule(" ", "*!", false));

			nam.Execute();

		}
	}
}