using System;
using System.CodeDom;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HomeVital.API.Extensions
{
    public static class ModelStateExtensions
    {
        public static string RetrieveErrorString(this ModelStateDictionary modelState)
        {
            var errorString = "Model was not properly formatted \n";
            foreach (var value in modelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errorString += $"Attempted value: {value.AttemptedValue}, Error: {error.ErrorMessage}\n";
                }
            }

            return errorString;
        }
    }
}