# GCC Sharp

A C# API for interacting with the Global Corporate Challenge website.

## Usage

All operations are accessible from the Client class. You should always dispose (or wrap it in a using block).

### Authentication

All operations require authentication. To establish an authenticated session, call Client.Login -

    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
	}

You can also terminate the session by logging out -

    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
		// Do stuff...
		client.Logout();
	}

### Days to Submit

To get a list of days (DateTimes) that need to be submitted, call Client.GetStepDates -

    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
		var dates = client.GetStepDates();
		// Do something useful with them...
	}

### Submit Activities

To submit an activity (or many activities), call Client.Submit -

    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
		var dates = client.GetStepDates();
		var activities = dates.Select(d => new Activity { Date = d, Steps = new Random().Next() });
		client.Submit(activities.ToArray());
	}