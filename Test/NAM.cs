using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2
{
	public class Match
	{
		public string from = " ";
		public string to  = " ";
		public bool final = false;

		public Match(string _from, string _to, bool _final)
		{
			from = _from;
			to = _to;
			final = _final;
		}
	}

	public class MarkovMachine
	{
		public List<Match> rules = new List<Match>();
		public Match curMatch;
		private string inWord = " ";
		private string outWord = " ";

		public bool finished = false;
		public MarkovMachine(string word)
		{
			inWord = word;
		}

		public void addMatch(Match rule)
		{
			rules.Add(rule);
			if (curMatch == null) curMatch = rule;

		}

		public void Start()
		{
			Console.WriteLine(inWord);
			outWord = inWord.Insert(0, " ");
			while (finished != true)
			{
				foreach (var rule in rules)
				{
					curMatch = rule;
					if (!Step()) break;	
				}
				
			}
		}

		private bool Step()
		{
			string pattern = Regex.Escape(curMatch.from);
			Regex regex = new Regex(pattern);

			if (regex.IsMatch(outWord))
			{
				Replace(regex, curMatch.to);
				Console.WriteLine(outWord);

				if (curMatch.final)
				{
					finished = true;
					return false;
				}
				return false;
			}
			return true;
		}

		private void Replace(Regex regex, string replacement)
		{

			outWord = regex.Replace(outWord, replacement, 1);
		}

	}
}
