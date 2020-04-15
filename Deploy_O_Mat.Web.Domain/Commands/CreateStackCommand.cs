using System;
using MicroRabbit.Domain.Core.Commands;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateStackCommand : Command
    {
        public string File { get; set; }
        public string Name { get; set; }
    }
}
