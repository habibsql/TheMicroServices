namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISerializer
    {
        Task<string> Serialize<T>(T t);

        Task<T> Decerialize<T>(string data);


    }
}
