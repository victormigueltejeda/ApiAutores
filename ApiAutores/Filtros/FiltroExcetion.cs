using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiAutores.Filtros
{
    public class FiltroExcetion : ExceptionFilterAttribute
    {
        private readonly ILogger logger;
        public FiltroExcetion(ILogger<FiltroExcetion> logger)
        {
            this.logger = logger;
        }


        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}
