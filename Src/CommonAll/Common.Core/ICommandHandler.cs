namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICommandHandler<ICommand, CommandResponse>
    {
        public Task<CommandResponse> Handle(ICommand command);
    }
}
