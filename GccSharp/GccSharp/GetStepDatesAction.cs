using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Coypu;

namespace GccSharp
{
    public class GetStepDatesAction
    {
        public IEnumerable<DateTime> Go(BrowserSession session)
        {
            session.Visit("/event/steps");

            var scope = session.FindAllCss(".steps-to-enter .date");
            return scope.Select(s => ParseDate(s.Text));
        }

        private static DateTime ParseDate(string text)
        {
            var parts = text.Split(' ');
            return DateTime.ParseExact(
                string.Format("{0} {1} {2}", parts[2], parts[1], DateTime.Today.Year),
                "d MMM yyyy",
                CultureInfo.InvariantCulture
                );
        }
    }
}
