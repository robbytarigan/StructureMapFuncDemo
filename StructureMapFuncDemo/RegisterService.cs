using System;
using System.Runtime.InteropServices.ComTypes;

namespace StructureMapFuncDemo
{
    public interface IRegisterProfiler
    {
    }   

    public sealed class RegisterProfiler : IRegisterProfiler
    {        
    }

    public sealed class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public interface ISimpleRegisterService
    {
        
    }

    public sealed class SimpleRegisterService : ISimpleRegisterService
    {
        private readonly Func<Job, IRegisterProfiler> _registerFactory;

        public SimpleRegisterService(Func<Job, IRegisterProfiler> registerFactory)
        {
            _registerFactory = registerFactory;
        }
    }    

    public interface IComplexRegisterService
    {

    }

    public sealed class ComplexRegisterService : IComplexRegisterService
    {
        private readonly Func<Job, Candidate, IRegisterProfiler> _registerFactory;

        public ComplexRegisterService(Func<Job, Candidate, IRegisterProfiler> registerFactory)
        {
            _registerFactory = registerFactory;
        }
    }

    public interface IComplexRegisterProfiler
    {
        Job Job { get; }

        Candidate Candidate { get; }
    }

    public sealed class ComplexRegisterProfiler : IComplexRegisterProfiler
    {
        public ComplexRegisterProfiler((Job, Candidate) parameters)
        {
            (Job, Candidate) = parameters;
        }

        public Job Job { get; }
        public Candidate Candidate { get; }
    }

    public interface IModifiedComplexRegisterService
    {
        IComplexRegisterProfiler CreateProfiler(Job job, Candidate candidate);
    }

    public sealed class ModifiedComplexRegisterService : IModifiedComplexRegisterService
    {
        private readonly Func<(Job, Candidate), IComplexRegisterProfiler> _registerFactory;

        public ModifiedComplexRegisterService(Func<(Job, Candidate), IComplexRegisterProfiler> registerFactory)
        {
            _registerFactory = registerFactory;
        }       

        public IComplexRegisterProfiler CreateProfiler(Job job, Candidate candidate)
        {
            return _registerFactory((job, candidate));
        }
    }
}
