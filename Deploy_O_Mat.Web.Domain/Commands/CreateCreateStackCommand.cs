using System;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Commands
{
    public class CreateCreateStackCommand : CreateStackCommand
    {
        public CreateCreateStackCommand(
            string name,
            string file)
        {
            File = file;
            Name = name;
        }
    }
}
