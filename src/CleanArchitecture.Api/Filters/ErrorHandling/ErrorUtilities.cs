using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Api.Filters.ErrorHandling
{
    public static class ErrorUtilities
    {
        public static string BuildErrorMessage(List<string> modelErrors)
        {
            StringBuilder msg = new StringBuilder();

            msg.Append("Invalid ModelState: ");

            foreach (var error in modelErrors)
            {
                msg.Append(error + " ");
            }

            return msg.ToString();
        }
    }
}
