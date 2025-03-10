﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify_Tutorial_Series.Applicaiton.Abstractions.Services;

namespace Eventify_Tutorial_Series.Infrastructure.Services
{
	public class FileService : IFileService
	{
	
		public async Task SaveTextAsync(string text, string path)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(path))
		         	throw new ArgumentNullException(nameof(path));
				await File.WriteAllTextAsync(path, text);
			}
			catch {
				Console.WriteLine("hata");
			}

		}
	}
}
