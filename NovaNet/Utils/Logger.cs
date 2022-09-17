/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 23/06/2009
 * Time: 5:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace NovaNet
{
	namespace Utils
	{
		namespace exLog
		{
			/// <summary>
			/// Identify the level of loggin to be done with these values
			/// </summary>
			public enum LogLevel {Dev=1, Test, Beta, Release};
			/// <summary>
			/// Description of Logger. Patterned with chain of responsibility
			/// </summary>
			public abstract class Logger
			{
				public const int _SUCCESS = 1;
				public const int _ERROR = -1;
				protected Logger nextLogger = null;
				
				/// <summary>
				/// Set next logger
				/// </summary>
				/// <param name="prmLogger"></param>
				public void SetNextLogger(Logger prmLogger)
				{
					nextLogger = prmLogger;
				}
				/// <summary>
				/// Name of the output device, can be obtained as a get property
				/// To be initialized at constructor
				/// </summary>
				private string outputName;
				/// <summary>
				/// Identify which type of logger implementation is used
				/// Logger implementation should fill this value with
				/// their corresponding identification
				/// </summary>
				protected string outputType;
				/// <summary>
				/// Stores the level of logging to be done
				/// To be set during construction
				/// </summary>
				private LogLevel logLevel;
				/// <summary>
				/// Holds the version of the implementation;
				/// </summary>
				protected string Ver;
				/// <summary>
				/// Constructor. To be initialized with name of the device
				/// and level of logging
				/// </summary>
				protected Logger(string prmOutputName, LogLevel prmLgLevel)
				{
					outputName = prmOutputName;
					logLevel = prmLgLevel;
				}
				/// <summary>
				/// Logs the exception into output device
				/// </summary>
				/// <param name="ex">The Exception to be logged</param>
				/// <returns>No. of characters written</returns>
				public abstract int Log(Exception ex);
                /// <summary>
                /// Logs the exception and state of the variables of a method into output device
                /// </summary>
                /// <param name="ex">The Exception to be logged</param>
                /// <returns>No. of characters written</returns>
                public abstract int Log(Exception ex,StateData er);

                public abstract int Log(string pLogMsg);
			}
		}
	}
}