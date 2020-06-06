//NAM.h
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
	public class Rule
	{
		public string From { get; set; } = " ";
		public string To { get; set; } = " ";
		public bool Final { get; set; } = false;

		public Rule(string from, string to, bool final)
		{
			From = from;
			To = to;
			Final = false;
		}
	}

	public class NAM
	{
		public ObservableCollection<Rule> rules = new ObservableCollection<Rule>();
		public Rule CurrentRule;
		private string inputWord = " ";
		private string outputWord = " ";


		public bool Finished { get; set; } = false;
		public string InputWord
		{
			get => inputWord;
			set
			{
				inputWord = value;
				outputWord = value;
			}
		}

		public string OutputWord { get => outputWord; set => outputWord = value; }
		public NAM(string word)
		{
			inputWord = word;
		}

		public void AddRule(Rule rule)
		{
			rules.Add(rule);
			if (CurrentRule == null) CurrentRule = rule;

		}

		public void Execute()
		{
			Console.WriteLine(inputWord);
			outputWord = inputWord.Insert(0, " ");
			while (!Finished)
			{
				foreach (var rule in rules)
				{
					CurrentRule = rule;
					if (!Step()) break;
				}

			}
		}

		private bool Step()
		{
			string pattern = Regex.Escape(CurrentRule.From);
			Regex regex = new Regex(pattern);

			if (regex.IsMatch(outputWord))
			{
				ReplaceSubstr(regex, CurrentRule.To);
				Console.WriteLine(outputWord);
				return false;
			}
			if (CurrentRule.Final)
			{
				Finished = true;
				return false;
			}
			return true;
		}

		private void ReplaceSubstr(Regex regex, string replacement)
		{

			OutputWord = regex.Replace(OutputWord, replacement, 1);
		}

	}
}


