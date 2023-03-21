using Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Logging
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase userCase, IApplicationActor actor, object useCaseData)
        {
            Console.WriteLine($"{DateTime.Now}: {actor.Identity} is trying to execute {userCase.Name} using data: " +
                $"{JsonConvert.SerializeObject(useCaseData)}");
        }

        public void Log(IUseCase userCase, IApplicationActor actor, object firstUseCaseData, object secondUseCaseData)
        {
            Console.WriteLine($"{DateTime.Now}: {actor.Identity} is trying to execute {userCase.Name} using data: " +
                $"{JsonConvert.SerializeObject(firstUseCaseData)} id and {JsonConvert.SerializeObject(secondUseCaseData)} id");
        }
    }
}
