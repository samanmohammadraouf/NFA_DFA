using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Glee.Drawing;


namespace NFA_DFA2_project
{
    class Transition
    {
        public State Qi;
        public State Qf;
        public string Alphabet;
    }
    class State
    {
        public List<Transition> Tr = new List<Transition>();
        public bool Final;
        public bool Initial;
        public string name;
        public string groupName;
    }

    class Equations
    {
        public State leftSide = new State();
        public List<Transition> RightSide = new List<Transition>();
        public string StrEquation;
    }

    class StateGroup
    {
        public string name;
        public List<State> states = new List<State>();
    }

    class Program
    {
        public static List<State> StatesNodes = new List<State>();
        public static List<Transition> Transitions = new List<Transition>();
        public static string[] Alphabet;
        public static void Main(string[] args)
        {
            Console.WriteLine("Plese Enter the states in one line and space between each the characters.(example :q0 q1 q2 q3)");
            string[] States = Console.ReadLine().Trim().Split(' ');
            Console.WriteLine("Plese Enter the alphabet in one line and space between each the characters.(example :a b)");
            Alphabet = Console.ReadLine().Trim().Split(' ');
            Console.WriteLine("Plese Enter the final states in one line and space between each the characters.(example :q2 q3)");
            string[] FinalStates = Console.ReadLine().Trim().Split(' ');
            Console.WriteLine("Please enter the number of transitions:(example:6)");
            string x = Console.ReadLine();
            while (true)
            {
                bool isNumeric = int.TryParse(x, out int result);
                if (isNumeric)
                {
                    break;
                }
                else
                {
                    x = Console.ReadLine();
                }
            }


            for (int i = 0; i < States.Length; i++)
            {
                State NewState = new State();
                if (i == 0)
                {
                    NewState.Initial = true;
                }
                NewState.name = States[i];
                StatesNodes.Add(NewState);
            }

            for (int i = 0; i < FinalStates.Length; i++)
            {
                for (int j = 0; j < StatesNodes.Count; j++)
                {
                    if (FinalStates[i] == StatesNodes[j].name)
                    {
                        StatesNodes[j].Final = true;
                    }
                }
            }

            int numberOfTransitions = int.Parse(x);
            Console.WriteLine("please enter the transitions like q1 q2 a:");
            Console.WriteLine("every trasition in a new line.");
            for (int i = 0; i < numberOfTransitions; i++)
            {
                string[] Transition = Console.ReadLine().Split(' ');
                string qi = Transition[0];
                string qf = Transition[1];
                string Alpha;
                if (Transition.Length == 3)
                {
                    Alpha = Transition[2];
                }
                else
                {
                    Alpha = "_";
                }

                Transition newTransition = new Transition();
                newTransition.Alphabet = Alpha;
                for (int j = 0; j < StatesNodes.Count; j++)
                {
                    if (qi == StatesNodes[j].name)
                    {
                        newTransition.Qi = StatesNodes[j];
                    }
                    if (qf == StatesNodes[j].name)
                    {
                        newTransition.Qf = StatesNodes[j];
                    }
                }
                Transitions.Add(newTransition);
            }


            for (int i = 0; i < StatesNodes.Count; i++)
            {
                for (int j = 0; j < Transitions.Count; j++)
                {
                    if (StatesNodes[i] == Transitions[j].Qi)
                    {
                        StatesNodes[i].Tr.Add(Transitions[j]);
                    }
                }
            }


            choice();
            Console.ReadKey();
        }

        public static void choice()
        {
            Console.WriteLine("choose and enter a number and enter:(example 1)");
            Console.WriteLine("1.NFA");
            Console.WriteLine("2.DFA");

            string x = Console.ReadLine();
            while (x != "1" && x != "2")
            {
                Console.WriteLine("enter number 1 or 2:");
                x = Console.ReadLine();
            }

            if (x == "1")
            {
                NFA_choice();
            }
            else
            {
                DFA_choice();
            }
        }

        public static void NFA_choice()
        {
            Console.WriteLine("choose one of the items with entering the number(example: 1)");
            Console.WriteLine("1.IsAcceptedByNFA");
            Console.WriteLine("2.Create equivalent DFA");
            Console.WriteLine("3.Regular Expression");
            Console.WriteLine("4.Graph schematic");
            Console.WriteLine("5.Back");
            string x = Console.ReadLine();
            while (x != "1" && x != "2" && x != "3" && x != "4" && x != "5")
            {
                Console.WriteLine("enter number 1 or 2 or 3 or 4 or5:");
                x = Console.ReadLine();
            }


            if (x == "1")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                Console.WriteLine("enter the string :");
                string str = Console.ReadLine();
                Console.WriteLine(newNFA.isAcceptedByNFA(str));
                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if (x == "2")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                Console.WriteLine("Convert and Print the DFA:");
                newNFA.PrintDFA();
                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if (x == "3")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                Console.WriteLine("Regular Expression for this NFA:");
                string RegExp = newNFA.regularExp();
                string RegExp2 = "";
                for(int i=1;i<RegExp.Length-1;i++)
                {
                    RegExp2 = RegExp2 + RegExp[i];
                }
                Console.WriteLine(RegExp2);
                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if(x=="4")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                Console.WriteLine("schematic NFA:");
                newNFA.showSchematic();
                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if (x == "5")
            {
                // Starts a new instance of the program itself
                System.Diagnostics.Process.Start(Application.ExecutablePath);

                // Closes the current process
                Environment.Exit(0);
            }
        }

        public static void DFA_choice()
        {
            Console.WriteLine("choose one of the items with entering the number(example: 1)");
            Console.WriteLine("1.IsAcceptedByDFA");
            Console.WriteLine("2.make Simple DFA");
            Console.WriteLine("3.show Schematic DFA");
            Console.WriteLine("4.Back");
            string x = Console.ReadLine();
            while (x != "1" && x != "2" && x != "3" && x != "4")
            {
                Console.WriteLine("Please enter number 1 or 2 or 3 or 4:");
                x = Console.ReadLine();
            }


            if (x == "1")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                List<State> inputDFA = newNFA.NFAtoDFA();
                DFA dfa = new DFA(inputDFA, Alphabet);
                Console.WriteLine("enter the string :");
                string str = Console.ReadLine();
                Console.WriteLine(dfa.isAcceptedByDFA(str));

                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if (x == "2")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                List<State> inputDFA = newNFA.NFAtoDFA();
                DFA dfa = new DFA(inputDFA, Alphabet);
                dfa.simpleDFA();
                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if (x == "3")
            {
                NFA newNFA = new NFA(StatesNodes, Transitions, Alphabet);
                List<State> inputDFA = newNFA.NFAtoDFA();
                DFA dfa = new DFA(inputDFA, Alphabet);
                Console.WriteLine("schematic DFA:");
                dfa.showSchematic();
                Console.WriteLine("press -1 to exit.");
                Console.WriteLine("press any key... to choose again.");
                string ans = Console.ReadLine();
                if (ans == "-1")
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    // Starts a new instance of the program itself
                    System.Diagnostics.Process.Start(Application.ExecutablePath);

                    // Closes the current process
                    Environment.Exit(0);
                }
            }
            else if (x == "4")
            {
                // Starts a new instance of the program itself
                System.Diagnostics.Process.Start(Application.ExecutablePath);

                // Closes the current process
                Environment.Exit(0);
            }
        }

        
    }

    class NFA
    {
        public List<State> StatesNodes;
        public List<Transition> Transitions;
        public String[] Alphabet;
        public NFA(List<State> States, List<Transition> transitions, string[] Alphabet)
        {
            this.StatesNodes = States;
            this.Transitions = transitions;
            this.Alphabet = Alphabet;
        }
        public bool isAcceptedByNFA(string str)
        {
            char[] StrArr = str.ToCharArray();
            return (IsNFA(StrArr, 0, StatesNodes[0]));
        }

        public bool IsNFA(char[] StrArr, int i, State CurrentState)
        {
            if (i == StrArr.Length && CurrentState.Final == true)
            {
                return true;
            }
            else if (i > StrArr.Length)
            {
                return false;
            }
            else
            {
                bool accepted = false;
                bool temp = false;
                for (int j = 0; j < CurrentState.Tr.Count; j++)
                {
                    if (i < StrArr.Length)
                    {
                        if (CurrentState.Tr[j].Alphabet == StrArr[i].ToString())
                        {
                            temp = IsNFA(StrArr, i + 1, CurrentState.Tr[j].Qf);
                            if (temp == true)
                            {
                                accepted = true;
                                break;
                            }
                        }
                    }
                }

                for (int j = 0; j < CurrentState.Tr.Count; j++)
                {
                    if (CurrentState.Tr[j].Alphabet == "_")
                    {
                        temp = IsNFA(StrArr, i, CurrentState.Tr[j].Qf);
                        if (temp == true)
                        {
                            accepted = true;
                            break;
                        }
                    }

                }

                return accepted;
            }
        }

        public void PrintDFA()
        {
            List<State> DFA = NFAtoDFA();

            //printing 
            List<string> PrintDFA = new List<string>();

            Console.WriteLine();
            for(int i = 0; i < DFA.Count; i++)
            {
                if(DFA[i].Initial == true)
                {
                    Console.WriteLine("initial state: " + DFA[i].name);
                }
            }
            Console.WriteLine();
            for (int i = 0; i < DFA.Count; i++)
            {
                if (DFA[i].Final == true)
                {
                    Console.WriteLine("final states: " + DFA[i].name);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Simple DFA Printing:");
            for (int i = 0; i < DFA.Count; i++)
            {
                for (int j = 0; j < DFA[i].Tr.Count; j++)
                {
                    string initial = DFA[i].Tr[j].Qi.name;
                    string Final = DFA[i].Tr[j].Qf.name;
                    if (initial == "" || initial == null)
                    {
                        initial = "tohi";
                    }
                    if (Final == "" || Final == null)
                    {
                        Final = "tohi";
                    }
                    //Console.WriteLine(initial.Trim() + "--->" + Final.Trim() + "      " + DFA[i].Tr[j].Alphabet.Trim());
                    PrintDFA.Add(initial.Trim() + "--->" + Final.Trim() + "      " + DFA[i].Tr[j].Alphabet.Trim());
                }
            }

            for (int i = 0; i < PrintDFA.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (PrintDFA[i] == PrintDFA[j])
                    {
                        PrintDFA[i] = "";
                    }
                }
            }

            for (int i = 0; i < PrintDFA.Count; i++)
            {
                if (PrintDFA[i] != "")
                {
                    Console.WriteLine(PrintDFA[i]);
                }
            }
        }

        public List<State> NFAtoDFA()
        {


            List<State> StateNodesBackUp = new List<State>();
            for (int i = 0; i < StatesNodes.Count; i++)
            {
                State newState = new State();
                newState.Final = StatesNodes[i].Final;
                newState.Initial = StatesNodes[i].Initial;

                for (int j = 0; j < StatesNodes[i].Tr.Count; j++)
                {
                    Transition newTrans = new Transition();
                    newTrans.Qi = StatesNodes[i].Tr[j].Qi;
                    newTrans.Qf = StatesNodes[i].Tr[j].Qf;
                    newTrans.Alphabet = StatesNodes[i].Tr[j].Alphabet;
                    newState.Tr.Add(newTrans);
                }
                newState.name = StatesNodes[i].name;
                StateNodesBackUp.Add(newState);
            }

            for (int i = 0; i < StatesNodes.Count; i++)
            {
                for (int j = 0; j < StatesNodes[i].Tr.Count; j++)
                {
                    if (StatesNodes[i].Tr[j].Alphabet == "_")
                    {
                        StatesNodes[i].name = StatesNodes[i].name + " " + StatesNodes[i].Tr[j].Qf.name;
                    }
                }
                StatesNodes[i].name = StatesNodes[i].name.Trim();
            }

            for (int i = StatesNodes.Count - 1; i >= 0; i--)
            {
                for (int j = StatesNodes[i].Tr.Count - 1; j >= 0; j--)
                {
                    if (StatesNodes[i].Tr[j].Alphabet == "_")
                    {
                        StatesNodes[i].Tr.Remove(StatesNodes[i].Tr[j]);
                    }
                }
            }

            List<State> DFA = new List<State>();
            State InitialState = new State();
            InitialState.name = StatesNodes[0].name;
            InitialState.Initial = true;
            InitialState.Final = StatesNodes[0].Final;

            DFA.Add(InitialState);

            for (int i = 0; i < DFA.Count; i++)
            {
                for (int j = 0; j < Alphabet.Length; j++)
                {
                    State NewState = new State();
                    NewState.Final = false;
                    List<State> childStates = new List<State>();

                    string[] States = DFA[i].name.Trim().Split(' ');


                    for (int k = 0; k < States.Length; k++)
                    {
                        for (int m = 0; m < StateNodesBackUp.Count; m++)
                        {
                            for (int n = 0; n < StateNodesBackUp[m].Tr.Count; n++)
                            {
                                if (StateNodesBackUp[m].name == States[k] && StateNodesBackUp[m].Tr[n].Alphabet == Alphabet[j])
                                {
                                    childStates.Add(StateNodesBackUp[m].Tr[n].Qf);
                                }
                                //else if (StatesNodes[m].name == States[k] && StatesNodes[m].Tr[n].Alphabet=="_")
                                //{
                                //    childStates.Add(StatesNodes[m].Tr[n].Qf);
                                //}
                            }
                        }
                    }

                    string newStateName = "";
                    for (int k = 0; k < childStates.Count; k++)
                    {
                        newStateName = newStateName + " " + childStates[k].name;
                    }
                    for (int k = 0; k < childStates.Count; k++)
                    {
                        if (childStates[k].Final == true)
                        {
                            NewState.Final = true;
                        }
                    }

                    NewState.name = newStateName;


                    bool IsAlreadyExists = false;
                    int index = -1;
                    for (int k = 0; k <= i; k++)
                    {
                        if (DFA[k].name.Trim() == newStateName.Trim())
                        {
                            IsAlreadyExists = true;
                            index = k;
                        }
                    }


                    if (IsAlreadyExists)
                    {
                        Transition newTransitionDFA = new Transition();
                        newTransitionDFA.Qi = DFA[i];
                        newTransitionDFA.Alphabet = Alphabet[j];
                        newTransitionDFA.Qf = DFA[index];
                        DFA[i].Tr.Add(newTransitionDFA);
                    }
                    else
                    {
                        Transition newTransitionDFA = new Transition();
                        newTransitionDFA.Qi = DFA[i];
                        newTransitionDFA.Alphabet = Alphabet[j];
                        newTransitionDFA.Qf = NewState;
                        DFA.Add(NewState);
                        DFA[i].Tr.Add(newTransitionDFA);
                    }
                }
            }

            return DFA;
        }

        List<State> RegExpStates = new List<State>();

        public string regularExp()
        {
            FixNFA();
            RemoveStates();
            State initial = new State();
            for (int i = 0; i < RegExpStates.Count; i++)
            {
                if (RegExpStates[i].name == "S" && RegExpStates[i].Initial == true)
                {
                    initial = RegExpStates[i];
                }
            }
            string RegExp = initial.Tr[0].Alphabet;
            string RegularExpression = "";
            for (int i = 0; i < RegExp.Length; i++)
            {
                if (RegExp[i] != '_')
                {
                    RegularExpression = RegularExpression + RegExp[i];
                }
            }

            return RegularExpression;
        }
        public void FixNFA()
        {
            for (int i = 0; i < StatesNodes.Count; i++)
            {
                RegExpStates.Add(StatesNodes[i]);
            }
            State newInitialstate = new State();
            newInitialstate.Initial = true;
            newInitialstate.name = "S";
            newInitialstate.Final = false;
            for (int i = 0; i < RegExpStates.Count; i++)
            {
                if (RegExpStates[i].Initial == true)
                {
                    Transition newTransition = new Transition();
                    newTransition.Qi = newInitialstate;
                    newTransition.Qf = RegExpStates[i];
                    newTransition.Alphabet = "_";
                    newInitialstate.Tr.Add(newTransition);
                }
            }
            RegExpStates.Add(newInitialstate);

            State newFinalState = new State();
            newFinalState.name = "F";
            newFinalState.Final = true;
            newFinalState.Initial = false;
            for (int i = 0; i < RegExpStates.Count; i++)
            {
                if (RegExpStates[i].Final == true)
                {
                    RegExpStates[i].Final = false;
                    Transition newTrans = new Transition();
                    newTrans.Alphabet = "_";
                    newTrans.Qi = RegExpStates[i];
                    newTrans.Qf = newFinalState;

                    RegExpStates[i].Tr.Add(newTrans);
                }
            }
            RegExpStates.Add(newFinalState);
            for (int i = RegExpStates.Count - 1; i >= 0; i--)
            {
                for (int j = RegExpStates[i].Tr.Count - 1; j >= 0; j--)
                {
                    if (RegExpStates[i].Tr[j].Alphabet == "_")
                    {
                        RegExpStates[i].Tr[j].Alphabet = "";
                    }
                }
            }
        }

        public void RemoveStates()
        {
            while (RegExpStates.Count > 2)
            {
                List<State> childs = new List<State>();
                List<State> Parents = new List<State>();

                //i always consider the last element of this RegExpStates should be remove
                State RemoveState = new State();
                Random rand = new Random();
                int x = rand.Next(0, RegExpStates.Count);
                RemoveState = RegExpStates[x];
                int i = x;
                //we don't want initial or final state to be deleted.
                if (RemoveState.name == "S" && RemoveState.Initial == true)
                {
                    continue;
                }
                if (RemoveState.name == "F" && RemoveState.Final == true)
                {
                    continue;
                }

                //other states...

                //handle loops -> removing the loops
                //for example q0 ---> q0 a 
                //and q0 ---> q1 b
                //i make q0 ---> q1  (a)*b
                //then delete the loop transition
                string SelfLoop = "";
                bool Loop = false;
                for (int m = 0; m < RemoveState.Tr.Count; m++)
                {
                    if (RemoveState.Tr[m].Qf == RemoveState)
                    {
                        Loop = true;
                        SelfLoop = SelfLoop + "+" + RemoveState.Tr[m].Alphabet;
                    }
                }

                for (int m = RemoveState.Tr.Count - 1; m >= 0; m--)
                {
                    if (RemoveState.Tr[m].Qf == RemoveState)
                    {
                        RemoveState.Tr.Remove(RemoveState.Tr[m]);
                    }
                }

                string SelfLoop2 = "";
                for (int m = 0; m < SelfLoop.Length; m++)
                {
                    if (m != 0)
                    {
                        SelfLoop2 = SelfLoop2 + SelfLoop[m];
                    }
                }

                if(SelfLoop2.Length==1)
                {
                    SelfLoop2 = SelfLoop2 + "*";
                }
                else if(SelfLoop2.Length>=1)
                {
                    SelfLoop2 = "(" + SelfLoop2 + ")*";
                }

                if(Loop)
                {
                    for (int m = 0; m < RemoveState.Tr.Count; m++)
                    {
                        RemoveState.Tr[m].Alphabet = SelfLoop2 + RemoveState.Tr[m].Alphabet;
                    }
                }
                


                RegExpStates[i] = RemoveState;
                //making childs
                for (int j = 0; j < RegExpStates[i].Tr.Count; j++)
                {
                    childs.Add(RegExpStates[i].Tr[j].Qf);
                }
                //making parents
                for (int j = 0; j < RegExpStates.Count; j++)
                {
                    for (int k = 0; k < RegExpStates[j].Tr.Count; k++)
                    {
                        if (RegExpStates[j].Tr[k].Qf == RemoveState)
                        {
                            Parents.Add(RegExpStates[j]);
                        }
                    }
                }



                for (int j = 0; j < Parents.Count; j++)
                {
                    for (int k = 0; k < childs.Count; k++)
                    {

                        Transition newTrans = new Transition();
                        newTrans.Qi = Parents[j];
                        newTrans.Qf = childs[k];

                        for (int m = 0; m < Parents[j].Tr.Count; m++)
                        {
                            if (Parents[j].Tr[m].Qf == childs[k])
                            {
                                if (Parents[j] != childs[k])
                                {
                                    if (Parents[j].Tr[m].Alphabet != "")
                                    {
                                        newTrans.Alphabet = newTrans.Alphabet + "+" + Parents[j].Tr[m].Alphabet;

                                        //if (Parents[j].Tr[m].Alphabet.Length==1)
                                        //{
                                        //    newTrans.Alphabet = newTrans.Alphabet + "+" + Parents[j].Tr[m].Alphabet;
                                        //}
                                        //else
                                        //{
                                        //    newTrans.Alphabet = newTrans.Alphabet + "+(" + Parents[j].Tr[m].Alphabet + ")";
                                        //}
                                    }
                                    else
                                    {
                                        newTrans.Alphabet = newTrans.Alphabet + "+" + "lambda";

                                    }
                                }
                                //is this parantize in the next line important??
                                //
                                //
                                //
                                //
                                //
                                //
                            }
                        }
                        //




                        for (int m = 0; m < Parents[j].Tr.Count; m++)
                        {
                            if (Parents[j].Tr[m].Qf == RemoveState)
                            {
                                //bool loop = false;
                                //int LoopIndex = -1;
                                //for (int n = 0; n < RemoveState.Tr.Count; n++)
                                //{
                                //    if(RemoveState.Tr[n].Qf == RemoveState)
                                //    {
                                //        loop = true;
                                //        LoopIndex = n;
                                //    }
                                //}

                                //for(int n=0;n<RemoveState.Tr.Count;n++)
                                //{
                                //    if(RemoveState.Tr[n].Qf == childs[k] && !loop)
                                //    {
                                //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + Parents[j].Tr[m].Alphabet + ")" + "(" + RemoveState.Tr[n].Alphabet + ")";
                                //    }
                                //    else if(RemoveState.Tr[n].Qf == childs[k] && loop)
                                //    {
                                //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + Parents[j].Tr[m].Alphabet + ")" +"("+RemoveState.Tr[LoopIndex].Alphabet+")"+ "(" + RemoveState.Tr[n].Alphabet + ")";
                                //    }
                                //}

                                for (int n = 0; n < RemoveState.Tr.Count; n++)
                                {
                                    if (RemoveState.Tr[n].Qf == childs[k])
                                    {
                                        if (Parents[j].Tr[m].Alphabet == "" && RemoveState.Tr[n].Alphabet == "")
                                        {
                                            newTrans.Alphabet = newTrans.Alphabet + "+" + "lambda";
                                        }
                                        else if (Parents[j].Tr[m].Alphabet != "" || RemoveState.Tr[n].Alphabet != "")
                                        {
                                            newTrans.Alphabet = newTrans.Alphabet + "+" + Parents[j].Tr[m].Alphabet + RemoveState.Tr[n].Alphabet;
                                        }
                                        //if(Parents[j].Tr[m].Alphabet!="" && RemoveState.Tr[n].Alphabet!="")
                                        //{
                                        //    if(Parents[j].Tr[m].Alphabet.Length==1 && RemoveState.Tr[n].Alphabet.Length==1)
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + Parents[j].Tr[m].Alphabet + RemoveState.Tr[n].Alphabet + ")";
                                        //    }
                                        //    else if(Parents[j].Tr[m].Alphabet.Length == 1 && RemoveState.Tr[n].Alphabet.Length != 1)
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+" + Parents[j].Tr[m].Alphabet + "(" + RemoveState.Tr[n].Alphabet + ")";
                                        //    }
                                        //    else if(Parents[j].Tr[m].Alphabet.Length != 1 && RemoveState.Tr[n].Alphabet.Length == 1)
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + Parents[j].Tr[m].Alphabet + ")" + RemoveState.Tr[n].Alphabet;
                                        //    }
                                        //    else if(Parents[j].Tr[m].Alphabet.Length != 1 && RemoveState.Tr[n].Alphabet.Length != 1)
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + Parents[j].Tr[m].Alphabet + ")" +"("+ RemoveState.Tr[n].Alphabet+")";
                                        //    }
                                        //}
                                        //else if(Parents[j].Tr[m].Alphabet != "" && RemoveState.Tr[n].Alphabet == "")
                                        //{
                                        //    if(Parents[j].Tr[m].Alphabet.Length==1)
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+"  + Parents[j].Tr[m].Alphabet ;
                                        //    }
                                        //    else
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + Parents[j].Tr[m].Alphabet + ")";
                                        //    }
                                        //}
                                        //else if(Parents[j].Tr[m].Alphabet == "" && RemoveState.Tr[n].Alphabet != "")
                                        //{
                                        //    if(RemoveState.Tr[n].Alphabet.Length==1)
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+"  + RemoveState.Tr[n].Alphabet ;
                                        //    }
                                        //    else
                                        //    {
                                        //        newTrans.Alphabet = newTrans.Alphabet + "+" + "(" + RemoveState.Tr[n].Alphabet + ")";
                                        //    }
                                        //}
                                        //else if(Parents[j].Tr[m].Alphabet == "" && RemoveState.Tr[n].Alphabet == "")
                                        //{
                                        //    newTrans.Alphabet = newTrans.Alphabet + "+" +"lambda" ;
                                        //}
                                    }
                                }
                            }
                        }

                        //remove previous transitions between the currentinitial and currentfinal states
                        for (int m = Parents[j].Tr.Count - 1; m > 0; m--)
                        {
                            if (Parents[j].Tr[m].Qf == childs[k])
                            {
                                Parents[j].Tr.Remove(Parents[j].Tr[m]);
                            }
                        }

                        Transition NewTrans = new Transition();
                        bool nonEssentialPlus = false;
                        if (newTrans.Alphabet != null)
                        {
                            if (newTrans.Alphabet.Length > 0)
                            {
                                if (newTrans.Alphabet[0] == '+')
                                {
                                    nonEssentialPlus = true;

                                    NewTrans.Qi = newTrans.Qi;
                                    NewTrans.Qf = newTrans.Qf;

                                    for (int m = 1; m < newTrans.Alphabet.Length; m++)
                                    {
                                        NewTrans.Alphabet = NewTrans.Alphabet + newTrans.Alphabet[m];
                                    }
                                }
                            }
                        }



                        if (!nonEssentialPlus)
                        {
                            if (newTrans.Alphabet.Length != 1)
                            {
                                newTrans.Alphabet = "(" + newTrans.Alphabet + ")";
                            }
                            Parents[j].Tr.Add(newTrans);
                        }
                        else if (nonEssentialPlus)
                        {
                            if (NewTrans.Alphabet.Length != 1)
                            {
                                NewTrans.Alphabet = "(" + NewTrans.Alphabet + ")";
                            }
                            Parents[j].Tr.Add(NewTrans);
                        }
                    }
                }

                for (int j = 0; j < Parents.Count; j++)
                {
                    for (int k = Parents[j].Tr.Count - 1; k >= 0; k--)
                    {
                        if (Parents[j].Tr[k].Qf == RemoveState)
                        {
                            Parents[j].Tr.Remove(Parents[j].Tr[k]);
                        }
                    }
                }

                RegExpStates.Remove(RemoveState);

                for (int j = 0; j < Parents.Count; j++)
                {
                    for (int k = 0; k < RegExpStates.Count; k++)
                    {
                        if (Parents[j].name == RegExpStates[k].name)
                        {
                            RegExpStates[k] = Parents[j];
                        }
                    }
                }

                for (int j = 0; j < childs.Count; j++)
                {
                    for (int k = 0; k < RegExpStates.Count; k++)
                    {
                        if (childs[j].name == RegExpStates[k].name)
                        {
                            RegExpStates[k] = childs[j];
                        }
                    }
                }
            }
        }



        public void showSchematic()
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Glee.GraphViewerGdi.GViewer viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
            Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("NFA");

            for(int i=0;i<StatesNodes.Count;i++)
            {
                for(int j=0;j<StatesNodes[i].Tr.Count;j++)
                {
                    graph.AddEdge(StatesNodes[i].name, StatesNodes[i].Tr[j].Alphabet, StatesNodes[i].Tr[j].Qf.name);
                }
            }

            for(int i=0;i<StatesNodes.Count;i++)
            {
                if(StatesNodes[i].Final==true)
                {
                    Microsoft.Glee.Drawing.Node final = graph.FindNode(StatesNodes[i].name);
                    final.Attr.Fillcolor= Microsoft.Glee.Drawing.Color.Blue;
                    final.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;

                }
            }
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();

        }
        

    }

    class DFA
    {
        private List<State> inputDFA;
        private string[] alphabet;

        public DFA(List<State> inputDFA, string[] alphabet)
        {
            this.inputDFA = inputDFA;
            this.alphabet = alphabet;
        }

        public bool isAcceptedByDFA(string str)
        {
            int i, j;
            bool accepted = false;
            //shows where I am.
            State currentState = new State();
            //We start with initial state
            currentState = inputDFA[0];

            //for each step we check what is our transition. continue to the end
            for(i = 0; i < str.Length; i++)
            {
                for(j = 0; j < alphabet.Length; j++)
                {
                    if (str[i].ToString() == currentState.Tr[j].Alphabet)
                    {
                        currentState = currentState.Tr[j].Qf;
                    }
                }
            }

            //check if current state is final state or not
            if(currentState.Final == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void simpleDFA()
        {
            StateGroup finalStates = new StateGroup();
            StateGroup notFinal = new StateGroup();
            List<StateGroup> groupList = new List<StateGroup>();
            string finalNumber1, finalNumber2;
            List<string> finalNumbers = new List<string>();
            int i, j, z;
            bool finish = false;
            int countSaver;
            string holdName;
            bool sameName = false;
            List<State> visited = new List<State>();

            /*for (i = 0; i < inputDFA.Count; i++)
            {
                Console.Write(inputDFA[i].name + " , ");
            }
            Console.WriteLine();*/

            //detect and delete unreachable states.
            visited = unReachable(inputDFA, visited, inputDFA[0]);

            for(i = 0; i < inputDFA.Count; i++)
            {
                if(visited.Contains(inputDFA[i]) == false)
                {
                    inputDFA.RemoveAt(i);
                }
            }

            /*for (i = 0; i < inputDFA.Count; i++)
            {
                Console.Write(inputDFA[i].name + " , ");
            }*/


            //first step. group final states and not final states
            for (i = 0; i < inputDFA.Count; i++)
            {
                if(inputDFA[i].Final == true)
                {
                    finalStates.states.Add(inputDFA[i]);
                    inputDFA[i].groupName = "0";
                }
                else
                {
                    notFinal.states.Add(inputDFA[i]);
                    inputDFA[i].groupName = "1";
                }
            }

            finalStates.name = "0";
            notFinal.name = "1";
            groupList.Add(finalStates);
            groupList.Add(notFinal);

            /*for(i = 0; i < groupList.Count; i++)
            {
                Console.WriteLine("group name: " + groupList[i].name);
                for(j = 0; j < groupList[i].states.Count; j++)
                {
                    Console.Write(groupList[i].states[j].name + " , ");
                }
                Console.WriteLine();
            }*/


            while (finish == false)
            {
                countSaver = groupList.Count;
                //making new lists for this step
                //for(i = 0; i < ((groupList.Count) * (groupList.Count)); i++)
                //{
                //    List<State> tempList = new List<State>();
                //    groupList.Add(tempList);
                //}

                for (i = 0; i < countSaver; i++)
                {
                    for (j = 0; j < groupList[i].states.Count; j++)
                    {
                        
                        sameName = false;
                        //in this part we save index the group number of the final state of each state transition
                        for(z = 0; z < alphabet.Length; z++)
                        {
                            finalNumber1 = groupList[i].states[j].Tr[z].Qf.groupName;
                            finalNumbers.Add(finalNumber1);
                        }
                        //finalNumber1 = groupList[i].states[j].Tr[0].Qf.groupName;
                        //finalNumber2 = groupList[i].states[j].Tr[1].Qf.groupName;
                        holdName = "";
                        //each stateGroup has name and states. here we make its name. Then if we had this name before we add new state
                        //and if we don't have, we make new one.
                        holdName = groupList[i].states[j].groupName;
                        for (z = 0; z < alphabet.Length; z++)
                        {
                            holdName = holdName + finalNumbers[z];
                        }

                        finalNumbers.Clear();
                        //holdName = groupList[i].states[j].groupName + finalNumber1 + finalNumber2;
                        for (z = 0; z < groupList.Count; z++)
                        {
                            if (holdName == groupList[z].name)
                            {
                                groupList[z].states.Add(groupList[i].states[j]);
                                sameName = true;
                                //groupList[i].states[j].groupName = holdName;
                            }
                        }

                        if (sameName == false)
                        {
                            StateGroup temp = new StateGroup();
                            temp.name = holdName;
                            temp.states.Add(groupList[i].states[j]);
                            groupList.Add(temp);
                            //groupList[i].states[j].groupName = holdName;
                        }

                    }
                }


                if (groupList.Count == (countSaver * 2))
                {
                    finish = true;
                    for (i = 0; i < countSaver; i++)
                    {
                        groupList.RemoveAt(groupList.Count - 1);
                    }
                }
                else
                {
                    for (i = 0; i < countSaver; i++)
                    {
                        groupList.RemoveAt(0);
                    }
                }

                for (i = 0; i < groupList.Count; i++)
                {
                    for (j = 0; j < groupList[i].states.Count; j++)
                    {
                        groupList[i].states[j].groupName = groupList[i].name;
                    }
                }
            }

            /*for (i = 0; i < groupList.Count; i++)
            {
                Console.WriteLine("group name: " + groupList[i].name);
                for (j = 0; j < groupList[i].states.Count; j++)
                {
                    Console.Write(groupList[i].states[j].name + " , ");
                }
                Console.WriteLine();
            }*/

            groupList = naming(groupList, inputDFA);

            for (i = 0; i < groupList.Count; i++)
            {
                for (j = 0; j < groupList[i].states.Count; j++)
                {
                    groupList[i].states[j].groupName = groupList[i].name;
                }
            }

            printDFA(groupList, inputDFA);

        }

        private List<StateGroup> naming(List<StateGroup> groupList, List<State> inputDFA)
        {
            string newName = "";
            List<string> stateNames = new List<string>();
            int i, j;
            for (i = 0; i < groupList.Count; i++)
            {
                newName = "";
                stateNames.Clear();
                for (j = 0; j < groupList[i].states.Count; j++)
                {
                    if (stateNames.Contains(groupList[i].states[j].name) == false)
                    {
                        if (groupList[i].states[j].name == null || groupList[i].states[j].name == "")
                        {
                            if(stateNames.Contains("") == false)
                            {
                                newName = newName + "tohi" + " ";
                                stateNames.Add("");
                            }
                            
                        }
                        else
                        {
                            newName = newName + groupList[i].states[j].name + " ";
                            stateNames.Add(groupList[i].states[j].name);
                        }

                    }
                }

                groupList[i].name = newName;

            }

            return groupList;
        }

        private void printDFA(List<StateGroup> groupList, List<State> inputDFA)
        {
            int i, j;
            Console.WriteLine();
            Console.WriteLine("simple DFA: ");
            Console.WriteLine();
            for (i = 0; i < groupList.Count; i++)
            {
                for(j = 0; j < groupList[i].states.Count; j++)
                {
                    if(groupList[i].states[j].Initial == true)
                    {
                        Console.WriteLine("Initial state: " + groupList[i].name.Trim());
                    }
                }
            }

            for (i = 0; i < groupList.Count; i++)
            {
                for (j = 0; j < groupList[i].states.Count; j++)
                {
                    if (groupList[i].states[j].Final == true)
                    {
                        Console.WriteLine("final states: " + groupList[i].name.Trim());
                    }
                }
            }

            Console.WriteLine();

            for (i = 0; i < groupList.Count; i++)
            {
                for(j = 0; j < alphabet.Length; j++)
                {
                    Console.WriteLine(groupList[i].name.Trim() + "  --->  " + groupList[i].states[0].Tr[j].Qf.groupName.Trim() + "      " + groupList[i].states[0].Tr[j].Alphabet.Trim());
                }
                //Console.WriteLine(groupList[i].name.Trim() + "  --->  " + groupList[i].states[0].Tr[0].Qf.groupName.Trim() + "      " + groupList[i].states[0].Tr[0].Alphabet.Trim());
                //Console.WriteLine(groupList[i].name.Trim() + "  --->  " + groupList[i].states[0].Tr[1].Qf.groupName.Trim() + "      " + groupList[i].states[0].Tr[1].Alphabet.Trim());
            }
            //initial.Trim() + "--->" + Final.Trim() + "      " + DFA[i].Tr[j].Alphabet.Trim()
            Console.WriteLine();
        }

        private List<State> unReachable(List<State> inputDFA, List<State> visited, State currentState) 
        {
            int i;
            visited.Add(currentState);
            State nextState;
            for(i = 0; i < alphabet.Length; i++)
            {
                nextState = currentState.Tr[i].Qf;
                if (visited.Contains(nextState) == false)
                {
                    visited.Add(nextState);
                    visited = unReachable(inputDFA, visited, nextState);
                }
            }
            /*nextState = currentState.Tr[0].Qf;
            if(visited.Contains(nextState) == false) 
            {
                visited.Add(nextState);
                visited = unReachable(inputDFA, visited, nextState);
            }

            nextState = currentState.Tr[1].Qf;
            if (visited.Contains(nextState) == false)
            {
                visited.Add(nextState);
                visited = unReachable(inputDFA, visited, nextState);
            }*/

            return visited;
        }

        public void showSchematic()
        {
            int i, j;
            List<string> names = new List<string>();
            for(i = 0; i <inputDFA.Count; i++)
            {
                if(inputDFA[i].name == null || inputDFA[i].name == "")
                {
                    inputDFA[i].name = "tohi";
                }
            }

            for (i = 0; i < inputDFA.Count; i++)
            {
                if (names.Contains(inputDFA[i].name) == false)
                {
                    names.Add(inputDFA[i].name);
                }
                else
                {
                    inputDFA.RemoveAt(i);
                }
            }
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            Microsoft.Glee.GraphViewerGdi.GViewer viewer = new Microsoft.Glee.GraphViewerGdi.GViewer();
            Microsoft.Glee.Drawing.Graph graph = new Microsoft.Glee.Drawing.Graph("DFA");

            for (i = 0; i < inputDFA.Count; i++)
            {
                for (j = 0; j < inputDFA[i].Tr.Count; j++)
                {
                    graph.AddEdge(inputDFA[i].name, inputDFA[i].Tr[j].Alphabet, inputDFA[i].Tr[j].Qf.name);
                }
            }

            for (i = 0; i < inputDFA.Count; i++)
            {
                if (inputDFA[i].Final == true)
                {
                    Microsoft.Glee.Drawing.Node final = graph.FindNode(inputDFA[i].name);
                    final.Attr.Fillcolor = Microsoft.Glee.Drawing.Color.Blue;
                    final.Attr.Shape = Microsoft.Glee.Drawing.Shape.DoubleCircle;

                }
            }
            viewer.Graph = graph;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();

        }
    }
}
