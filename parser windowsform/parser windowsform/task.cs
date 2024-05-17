
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF               =  0, // (EOF)
        SYMBOL_ERROR             =  1, // (Error)
        SYMBOL_WHITESPACE        =  2, // Whitespace
        SYMBOL_MINUS             =  3, // '-'
        SYMBOL_MINUSMINUS        =  4, // '--'
        SYMBOL_EXCLAMEQ          =  5, // '!='
        SYMBOL_PERCENT           =  6, // '%'
        SYMBOL_LPAREN            =  7, // '('
        SYMBOL_RPAREN            =  8, // ')'
        SYMBOL_TIMES             =  9, // '*'
        SYMBOL_COMMA             = 10, // ','
        SYMBOL_DIV               = 11, // '/'
        SYMBOL_QUESTION          = 12, // '?'
        SYMBOL_CARET             = 13, // '^'
        SYMBOL_LBRACE            = 14, // '{'
        SYMBOL_RBRACE            = 15, // '}'
        SYMBOL_PLUS              = 16, // '+'
        SYMBOL_PLUSPLUS          = 17, // '++'
        SYMBOL_LT                = 18, // '<'
        SYMBOL_LTEQ              = 19, // '<='
        SYMBOL_EQ                = 20, // '='
        SYMBOL_EQEQ              = 21, // '=='
        SYMBOL_GT                = 22, // '>'
        SYMBOL_GTEQ              = 23, // '>='
        SYMBOL_DIGIT             = 24, // Digit
        SYMBOL_ELSE              = 25, // else
        SYMBOL_END               = 26, // End
        SYMBOL_ID                = 27, // ID
        SYMBOL_IF                = 28, // if
        SYMBOL_LOOP              = 29, // loop
        SYMBOL_METHOD            = 30, // method
        SYMBOL_START             = 31, // Start
        SYMBOL_ASSIGN            = 32, // <assign>
        SYMBOL_CONCEPT           = 33, // <concept>
        SYMBOL_COND              = 34, // <cond>
        SYMBOL_DIGIT2            = 35, // <digit>
        SYMBOL_EXP               = 36, // <exp>
        SYMBOL_EXPR              = 37, // <expr>
        SYMBOL_FACTOR            = 38, // <factor>
        SYMBOL_FOR_STMT          = 39, // <for_stmt>
        SYMBOL_ID2               = 40, // <id>
        SYMBOL_IF_STMT           = 41, // <if_stmt>
        SYMBOL_METHODCALL        = 42, // <MethodCall>
        SYMBOL_METHODDECLARATION = 43, // <MethodDeclaration>
        SYMBOL_OP                = 44, // <op>
        SYMBOL_PARAMETERLIST     = 45, // <ParameterList>
        SYMBOL_STARTPROGRAM      = 46, // <Start program>
        SYMBOL_STEP              = 47, // <step>
        SYMBOL_STMT_LIST         = 48, // <stmt_list>
        SYMBOL_TERM              = 49  // <term>
    };

    enum RuleConstants : int
    {
        RULE_STARTPROGRAM_START_END                                        =  0, // <Start program> ::= Start <stmt_list> End
        RULE_STMT_LIST                                                     =  1, // <stmt_list> ::= <concept>
        RULE_STMT_LIST2                                                    =  2, // <stmt_list> ::= <concept> <stmt_list>
        RULE_CONCEPT                                                       =  3, // <concept> ::= <assign>
        RULE_CONCEPT2                                                      =  4, // <concept> ::= <if_stmt>
        RULE_CONCEPT3                                                      =  5, // <concept> ::= <for_stmt>
        RULE_ASSIGN_EQ                                                     =  6, // <assign> ::= <id> '=' <expr>
        RULE_ID_ID                                                         =  7, // <id> ::= ID
        RULE_EXPR_PLUS                                                     =  8, // <expr> ::= <expr> '+' <term>
        RULE_EXPR_MINUS                                                    =  9, // <expr> ::= <expr> '-' <term>
        RULE_EXPR                                                          = 10, // <expr> ::= <term>
        RULE_TERM_TIMES                                                    = 11, // <term> ::= <term> '*' <factor>
        RULE_TERM_DIV                                                      = 12, // <term> ::= <term> '/' <factor>
        RULE_TERM_PERCENT                                                  = 13, // <term> ::= <term> '%' <factor>
        RULE_TERM                                                          = 14, // <term> ::= <factor>
        RULE_FACTOR_CARET                                                  = 15, // <factor> ::= <factor> '^' <exp>
        RULE_FACTOR                                                        = 16, // <factor> ::= <exp>
        RULE_EXP_LPAREN_RPAREN                                             = 17, // <exp> ::= '(' <expr> ')'
        RULE_EXP                                                           = 18, // <exp> ::= <id>
        RULE_EXP2                                                          = 19, // <exp> ::= <digit>
        RULE_DIGIT_DIGIT                                                   = 20, // <digit> ::= Digit
        RULE_IF_STMT_IF_LPAREN_RPAREN_START_END                            = 21, // <if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End
        RULE_IF_STMT_IF_LPAREN_RPAREN_START_END_ELSE_START_END             = 22, // <if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End else Start <stmt_list> End
        RULE_COND                                                          = 23, // <cond> ::= <expr> <op> <expr>
        RULE_OP_LT                                                         = 24, // <op> ::= '<'
        RULE_OP_LTEQ                                                       = 25, // <op> ::= '<='
        RULE_OP_GTEQ                                                       = 26, // <op> ::= '>='
        RULE_OP_GT                                                         = 27, // <op> ::= '>'
        RULE_OP_EQEQ                                                       = 28, // <op> ::= '=='
        RULE_OP_EXCLAMEQ                                                   = 29, // <op> ::= '!='
        RULE_FOR_STMT_LOOP_LPAREN_COMMA_COMMA_RPAREN_LBRACE_RBRACE         = 30, // <for_stmt> ::= loop '(' <assign> ',' <cond> ',' <step> ')' '{' <stmt_list> '}'
        RULE_STEP_MINUSMINUS                                               = 31, // <step> ::= '--' <id>
        RULE_STEP_MINUSMINUS2                                              = 32, // <step> ::= <id> '--'
        RULE_STEP_PLUSPLUS                                                 = 33, // <step> ::= '++' <id>
        RULE_STEP_PLUSPLUS2                                                = 34, // <step> ::= <id> '++'
        RULE_STEP                                                          = 35, // <step> ::= <assign>
        RULE_METHODDECLARATION_METHOD_LPAREN_QUESTION_RPAREN_LBRACE_RBRACE = 36, // <MethodDeclaration> ::= method <id> '(' <ParameterList> '?' ')' '{' <stmt_list> '}'
        RULE_PARAMETERLIST_QUESTION                                        = 37, // <ParameterList> ::= <id> <id> '?'
        RULE_METHODCALL_LPAREN_QUESTION_RPAREN                             = 38  // <MethodCall> ::= <id> '(' <expr> '?' ')'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox ls;
        public MyParser(string filename,ListBox lst,ListBox ls)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst = lst;
            this.ls = ls;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_QUESTION :
                //'?'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CARET :
                //'^'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_END :
                //End
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //ID
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP :
                //loop
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD :
                //method
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONCEPT :
                //<concept>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COND :
                //<cond>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXP :
                //<exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPR :
                //<expr>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR_STMT :
                //<for_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF_STMT :
                //<if_stmt>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODCALL :
                //<MethodCall>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODDECLARATION :
                //<MethodDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMETERLIST :
                //<ParameterList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STARTPROGRAM :
                //<Start program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEP :
                //<step>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STMT_LIST :
                //<stmt_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<term>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_STARTPROGRAM_START_END :
                //<Start program> ::= Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST :
                //<stmt_list> ::= <concept>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STMT_LIST2 :
                //<stmt_list> ::= <concept> <stmt_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT :
                //<concept> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT2 :
                //<concept> ::= <if_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONCEPT3 :
                //<concept> ::= <for_stmt>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ :
                //<assign> ::= <id> '=' <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= ID
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_PLUS :
                //<expr> ::= <expr> '+' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR_MINUS :
                //<expr> ::= <expr> '-' <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPR :
                //<expr> ::= <term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<term> ::= <term> '*' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<term> ::= <term> '/' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_PERCENT :
                //<term> ::= <term> '%' <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<term> ::= <factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_CARET :
                //<factor> ::= <factor> '^' <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<factor> ::= <exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP_LPAREN_RPAREN :
                //<exp> ::= '(' <expr> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP :
                //<exp> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXP2 :
                //<exp> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_START_END :
                //<if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IF_STMT_IF_LPAREN_RPAREN_START_END_ELSE_START_END :
                //<if_stmt> ::= if '(' <cond> ')' Start <stmt_list> End else Start <stmt_list> End
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COND :
                //<cond> ::= <expr> <op> <expr>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EQEQ :
                //<op> ::= '=='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FOR_STMT_LOOP_LPAREN_COMMA_COMMA_RPAREN_LBRACE_RBRACE :
                //<for_stmt> ::= loop '(' <assign> ',' <cond> ',' <step> ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS :
                //<step> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_MINUSMINUS2 :
                //<step> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS :
                //<step> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP_PLUSPLUS2 :
                //<step> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEP :
                //<step> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATION_METHOD_LPAREN_QUESTION_RPAREN_LBRACE_RBRACE :
                //<MethodDeclaration> ::= method <id> '(' <ParameterList> '?' ')' '{' <stmt_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERLIST_QUESTION :
                //<ParameterList> ::= <id> <id> '?'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODCALL_LPAREN_QUESTION_RPAREN :
                //<MethodCall> ::= <id> '(' <expr> '?' ')'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"in line : "+args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = "Expected Token :"+args.ExpectedTokens.ToString();
            lst.Items.Add(m2);

            //todo: Report message to UI?
        }
        private void TokenReadEvent(LALRParser pr, TokenReadEventArgs args)
        {
            string info = args.Token.Text + " \t \t" + (SymbolConstants) args.Token.Symbol.Id;
            ls.Items.Add(info);
        }

    }
}
