using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeatureManagement.Web.Models
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class PreserveFeatureAcrossRequestsAttribute : Attribute
    {
    }

    public enum FeatureFlag
    {
        [PreserveFeatureAcrossRequests]
        Print,
        PrintPreview,
        TimeElapsed,
        LogUrl
    }

    public class A : ISessionManager
    {
        public Task<bool?> GetAsync(string featureName)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string featureName, bool enabled)
        {
            throw new NotImplementedException();
        }
    }
}
