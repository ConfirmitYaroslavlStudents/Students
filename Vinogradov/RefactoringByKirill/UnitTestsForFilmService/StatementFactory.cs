using FilmService.KindsOfStatements;

namespace UnitTestsForFilmService
{
    class StatementFactory
    {
        public static Statement GetStatement()
        {
            var statements = new CompositeStatement();
            statements.AddStatement(new StatementInJSON());
            statements.AddStatement(new StatementInString());

            return statements;
        }
    }
}
