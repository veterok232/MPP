using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using FakerLib.Generators.Interfaces;

namespace FakerLib.Interfaces
{
    /// <summary>
    /// Interface for FakerConfig
    /// </summary>
    public interface IFakerConfig
    {
        void Add<DTObjectType, MemberType, GeneratorType>(Expression<Func<DTObjectType, MemberType>> expression)
            where DTObjectType : class
            where GeneratorType : IGenerator;
    }
}
