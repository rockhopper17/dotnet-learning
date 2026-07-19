// using System;
// using System.Threading;
// using System.Threading.Tasks;

namespace TeleprompterConsole;

internal class Program
{
	//static void Main(string[] args)
	static async Task Main(string[] args)
	{
		//Console.WriteLine("Hello, World!");
		
          //var lines = ReadFrom("sampleQuotes.txt");
		//foreach (var line in lines)
		//{
			////Console.WriteLine(line);

               //Console.Write(line);
			//if (!string.IsNullOrWhiteSpace(line))
			//{
				//var pause = Task.Delay(200);
				//pause.Wait();
			//}
		//}
	
		// await ShowTeleprompter();

		await RunTeleprompter();
	}

	static IEnumerable<string> ReadFrom(string file)
	{
		string? line;
		using (var reader = File.OpenText(file))
		{
			while ((line = reader.ReadLine()) != null)
			{
				//yield return line;
				var words = line.Split(' ');
				var lineLength = 0;
				
				foreach (var word in words)
				{
					yield return word + " ";
					lineLength += word.Length + 1;
					if (lineLength > 70)
					{
						yield return Environment.NewLine;
						lineLength = 0;
					}
				}
				yield return Environment.NewLine;
			}
		}
	}

	private static async Task RunTeleprompter()
	{
		var config = new TeleprompterConfig();

		var displayTask = ShowTeleprompter(config);
		var speedTask = GetInput(config);

		await Task.WhenAny(displayTask, speedTask);
	}

	private static async Task ShowTeleprompter(TeleprompterConfig config)
	{
  		var words = ReadFrom("sampleQuotes.txt");
		foreach (var word in words)
		{
   			Console.Write(word);
			if (!string.IsNullOrWhiteSpace(word))
			{
				// await Task.Delay(200);
				await Task.Delay(config.DelayInMilliseconds);
			}
		}

		config.SetDone();
	}

	private static async Task GetInput(TeleprompterConfig config)
	{
		// var delay = 200;
		Action work = () =>
		{
			do {
				var key = Console.ReadKey(true);
				if (key.KeyChar == '>')
				{
					// delay -= 10;
					config.UpdateDelay(-10);
				}
				else if (key.KeyChar == '<')
				{
					// delay += 10;
					config.UpdateDelay(10);
				}
				else if (key.KeyChar == 'X' || key.KeyChar == 'x')
				{
					// break;
					config.SetDone();
				}
			// } while (true);
			} while (!config.Done);
		};
		await Task.Run(work);
	}
}

