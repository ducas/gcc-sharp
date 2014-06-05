# GCC Sharp

A C# API for interacting with the Global Corporate Challenge website.

## Usage

All operations are accessible from the Client class. You should always dispose (or wrap it in a using block).

### Authentication

All operations require authentication. To establish an authenticated session, call Client.Login -

```csharp
    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
	}
```	

You can also terminate the session by logging out -

```csharp
    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
		// Do stuff...
		client.Logout();
	}
```	

### Days to Submit

To get a list of days (DateTimes) that need to be submitted, call Client.GetStepDates -

```csharp
    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
		var dates = client.GetStepDates();
		// Do something useful with them...
	}
```	

### Submit Activities

To submit an activity (or many activities), call Client.Submit -

```csharp
    using (var client = new Client())
	{
	    client.Login("my@ema.il", "password");
		var dates = client.GetStepDates();
		var activities = dates.Select(d => new Activity { Date = d, Steps = new Random().Next() });
		client.Submit(activities.ToArray());
	}
```	

---

## Console Wrapper

Basic wrapper around the Client API to submit an entry for the previous day

Uses the [Command Line Parser Library](https://github.com/gsscoder/commandline)

### Credentials

Either:
- Save login & password in the console's App.Config 
- Wait for the command line prompt

### Usage

```shell
GCC.exe [verb] [options]
```

#### Activity Verb

**Options**

Steps Walked

 -w, --walk [integer]

Swimming Metres

 -s, --swim [integer]

Biking Kilometres

 -b, --bike [decimal]

**Examples**

```shell
GCC.exe activity --walk 10000 --bike 15 --swim 750
GCC.exe activity -w 10000
```

#### NoActivity Verb

**Options**

 -r, --reason (sick | travelling | unabletowear)

**Examples**

```shell
GCC.exe noactivity --reason sick
GCC.exe noactivity -r travelling
```
