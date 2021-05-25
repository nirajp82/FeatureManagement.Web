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
        Admin,
        AddNewCreditCard,
        Print,
        PrintPreview,
        [PreserveFeatureAcrossRequests]
        TimeElapsed,
        LogUrl,
        Setting,
    }
}
