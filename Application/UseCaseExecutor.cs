using Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class UseCaseExecutor
    {
        private readonly IUseCaseLogger _logger;
        private readonly IApplicationActor _actor;

        public UseCaseExecutor(IApplicationActor actor, IUseCaseLogger logger)
        {
            _actor = actor;
            _logger = logger;
        }
        
        public TResult ExecuteQuery<TSearch,TResult>(IQuery<TSearch,TResult> query, TSearch search)
        {
            _logger.Log(query, _actor, search);
            if (!query.Roles.Contains(_actor.Role))
            {
                throw new UnauthorizedUseCaseException(query, _actor);
            }
            return query.Execute(search);
        }

        public void ExecuteCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            _logger.Log(command, _actor, request);
            if (!command.Roles.Contains(_actor.Role))
            {
                throw new UnauthorizedUseCaseException(command, _actor);
            }
            command.Execute(request);
        }
    }
}
