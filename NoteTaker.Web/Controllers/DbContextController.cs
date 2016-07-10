using System.Web.Mvc;
using NoteTaker.Core.Command;
using NoteTaker.Core.Queries;

namespace NoteTaker.Web.Controllers
{
    public class DbContextController : Controller
    {
        public static TResult Query<TResult>(Query<TResult> query)
        {
            return query.PerformQuery();
        }

        public static TResult Execute<TResult>(Command<TResult> command)
        {
            return command.Execute();
        }
    }
}