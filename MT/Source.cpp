#include <vector>
#include <iostream>
#include <iterator>
#include <string>
#include <map>

using namespace std;

struct State;
struct Condition;

enum Movement {
	L, R, N
};

struct Condition {
	map<char, State> q;
};

struct State
{
	char wr_symbol = ' ';
	Movement next_move = Movement::N;
	Condition* next_q = nullptr;
};


class TuringMachine {
public:
	TuringMachine(string word);
	vector<char> tape;
	vector<char>::iterator head_position;
	Condition* current_cond;
	void print_tape();
	void tick();
	//void execute();

private:

	int strt_ind = 248;
};

TuringMachine::TuringMachine(string word)
{
	tape.resize(500);
	tape.assign(500, '_');
	head_position = tape.begin() + 250;

	auto it = head_position;
	for (auto it1 : word) {
		*it = it1;
		it++;
	}
}

void TuringMachine::print_tape()
{
	int head_ind = distance(tape.begin(), head_position) - strt_ind;
	for (int i = strt_ind; i < strt_ind + 40; i++) {
		cout << '|' << tape[i];
	}
	cout << '|' << endl << ' ';

	for (int i = 0; i < head_ind; i++)
	{
		cout << "  ";
	}
	cout << '^' << endl;

}

void TuringMachine::tick()
{
	State* cur_state = &current_cond->q[*head_position];
	if (cur_state->wr_symbol != ' ')
		*head_position = cur_state->wr_symbol;

	switch (cur_state->next_move)
	{
	case R: head_position++;
		break;
	case L: head_position--;
		break;
	default:
		break;
	}

	current_cond = cur_state->next_q;
	print_tape();

}

void main() {

	Condition q1, q2, q3, q4, q5, q6, q7, q8, q9, q10;


	string word = "11111111%111";
	TuringMachine mt(word);
	mt.current_cond = &q1;

	q1.q['1'] = { '1',R,&q1 };
	q1.q['%'] = { '%',R,&q2 };

	q2.q['1'] = { '_',R,&q3 };
	q2.q['_'] = { '_',R,&q2 };
	
	q3.q['1'] = { '1',L,&q4 };
	q3.q['_'] = { '_',L,&q8 };

	
	q4.q['%'] = { '%',L,&q5 };
	q4.q['_'] = { '_',L,&q4 };

	q5.q['1'] = { '1',L,&q5 };
	q5.q['_'] = { '_',R,&q6 };

	q6.q['1'] = { '_',R,&q1 };
	q6.q['%'] = { '_',R,&q7 };

	q7.q['1'] = { '1',N,nullptr };
	q7.q['_'] = { '_',R,&q3 };

	q8.q['%'] = { '%',L,&q5 };
	q8.q['_'] = { '1',L,&q8 };

	q9.q['_'] = { '1',L,&q5 };

	mt.print_tape();
	while (1)
	{
		mt.tick();
		getchar();
	}



}
