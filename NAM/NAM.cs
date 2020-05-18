using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NAM
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

	public class NAM : INotifyPropertyChanged
	{
		public ObservableCollection<Rule> rules;
		public Rule CurrentRule;
		private string inputWord = " ";
		private string outputWord = " ";

		public event PropertyChangedEventHandler PropertyChanged;

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
		public NAM()
		{
		}

		public void AddRule(Rule rule)
		{
			rules.Add(rule);
			if (CurrentRule == null) CurrentRule = rule;

		}

		public void Execute()
		{
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
			if (Regex.IsMatch(outputWord, pattern))
			{
				ReplaceSubstr(pattern, CurrentRule.To);
				return false;
			}
			if (CurrentRule.Final)
			{
				Finished = true;
				return false;
			}
			return true;
		}

		private void ReplaceSubstr(string pattern, string replacement)
		{

			OutputWord = Regex.Replace(OutputWord, pattern, replacement);
		}

	}
}
