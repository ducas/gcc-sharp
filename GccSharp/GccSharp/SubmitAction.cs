using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Coypu;

namespace GccSharp
{
    internal class SubmitAction
    {
        public IEnumerable<Activity> Activities { get; set; }

        public void Go(BrowserSession session)
        {
            var dates = new GetStepDatesAction()
                .Go(session)
                .ToArray();
            var rows = session.FindAllCss(".steps-to-enter").ToArray();

            foreach (var activity in Activities)
            {
                var index = Array.IndexOf(dates, activity.Date);

                if (index == -1)
                    throw new Exception("Unable to submit activity on " + activity.Date.ToShortDateString());

                var row = rows[index];

                FillActivity(activity, row);
            }

            session.ClickButton("Submit");
            session.ClickButton("Confirm");

        }

        private static void FillActivity(Activity activity, Scope row)
        {
            if (activity.NoActivityReason != null)
            {
                row.FindCss(".stepNoactivity")
                    .SelectOption(activity.NoActivityReason.ToString());
                return;
            }

            if (activity.Steps != 0)
                row.FindCss(".walk_entry")
                    .FillInWith(activity.Steps.ToString(CultureInfo.InvariantCulture));
            
            if (activity.Bike != 0)
                row.FindCss(".bike_entry")
                    .FillInWith(activity.Bike.ToString(CultureInfo.InvariantCulture));

            if (activity.Swim != 0)
                row.FindCss(".swim_entry")
                    .FillInWith(activity.Swim.ToString(CultureInfo.InvariantCulture));
        }
    }
}