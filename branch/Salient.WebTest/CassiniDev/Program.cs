// /* **********************************************************************************
//  *
//  * Copyright (c) Sky Sanders. All rights reserved.
//  * 
//  * This source code is subject to terms and conditions of the Microsoft Public
//  * License (Ms-PL). A copy of the license can be found in the license.htm file
//  * included in this distribution.
//  *
//  * You must not remove this notice, or any other, from this software.
//  *
//  * **********************************************************************************/
using System;
using System.Threading;
using System.Windows.Forms;
using Cassini.CommandLine;

namespace CassiniDev
{
    /// <summary>
    /// 12/29/09 sky: Implemented more robust command line argument parser (CommandLineParser.cs)
    /// 12/29/09 sky: Implemented hosts file modification mode allowing this executable to be run in an elevated process
    ///               to add/remove hosts file entry corresponding to specified hostname, if desired.
    /// 12/29/09 sky: Implemented a MVP pattern with service locator and abstract factory for testing and to simplify 
    ///               gui and console with same codebase
    /// 01/07/10 sky: removed console view, it was only there to provide a start and stop mechanism. very stupid design flaw
    /// 
    /// Issues:
    /// FormView.Stop() doesn't seem to kill the server host? works in console.
    /// Is not a critical issue yet - starting another server on same port works just fine.
    /// I think it may have to do with vshost.exe persistance as killing vs stops it.
    /// 
    /// 
    /// /a:"E:\Projects\cassinidev\trunk\TestWebApp" /v:"/" /h:mycomputer /ah+ /im:Specific /i:192.168.1.102 /v6- /pm:Specific /p:8082 /prs:0 /pre:0
    /// /a:"E:\Projects\cassinidev\trunk\TestWebApp"
    /// </summary>
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            CommandLineArguments sargs = new CommandLineArguments();
#if GUI
            if (!Parser.ParseArguments(args, sargs))
            {
                string usage = Parser.ArgumentsUsage(typeof (CommandLineArguments), 120);
                MessageBox.Show(usage);
                Environment.Exit(-1);
                return;
            }
            switch (sargs.RunMode)
            {
                case RunMode.Server:
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    using (IPresenter presenter = ServiceFactory.CreatePresenter())
                    {
                        IView view = ServiceFactory.CreateFormsView();
                        presenter.InitializeView(view, sargs);
                        Application.Run((Form) view);
                    }
                    break;
                case RunMode.Hostsfile:
                    SetHostsFile(sargs);
                    break;
            }
#endif
#if CONSOLE
            if (!Parser.ParseArgumentsWithUsage(args, sargs))
            {
                Environment.Exit(-1);
            }
            else
            {
                switch (sargs.RunMode)
                {
                    case RunMode.Server:
                        using (IPresenter presenter = ServiceFactory.CreatePresenter())
                        {
                            try
                            {
                                if (string.IsNullOrEmpty(sargs.ApplicationPath))
                                {
                                    throw new CassiniException(
                                        "ApplicationPath is null.\r\n" +
                                        Parser.ArgumentsUsage(typeof (CommandLineArguments)), ErrorField.ApplicationPath);
                                }



                                #region Listen to presenter events
                                if (!sargs.Quiet)
                                {
                                    // since we don't need to unhook it just use anonymous delegate
                                    presenter.RequestComplete +=
                                        (s, e) => Console.WriteLine("RequestComplete:" + e.Request.ToString(true));
                                    presenter.RequestBegin +=
                                        (s, e) => Console.WriteLine("RequestBegin:" + e.Request.ToString(true));
                                }

                                presenter.ServerStarted += ((s, e) =>
                                                                {
                                                                    Console.WriteLine("started: {0}", e.RootUrl);
                                                                    Console.WriteLine("Press Enter key to exit....");
                                                                });

                                bool stopped = false;
                                presenter.ServerStopped += ((s, e) =>
                                                                {
                                                                    stopped = true;
                                  
                                                                });

                                #endregion

                                
                                presenter.Start(sargs);


                                //TODO: refine this - sleep is not optimal
                                if (sargs.Headless)
                                {
                                    while (!stopped)
                                    {
                                        
                                        Thread.Sleep(10);
                                    }
                                }
                                else
                                {
                                    while (!stopped && !Console.KeyAvailable)
                                    {
                                        Thread.Sleep(10);
                                    }

                                    if (!stopped)
                                    {
                                        Console.ReadKey();
                                        presenter.Stop();
                                    }

                                }
                                
                                Console.WriteLine("stopped:");


                                
                                
                            }
                            catch (CassiniException ex)
                            {
                                Console.WriteLine("error:{0} {1}",
                                                  ex.Field == ErrorField.None
                                                      ? ex.GetType().Name
                                                      : ex.Field.ToString(), ex.Message);
                            }
                            catch (Exception ex2)
                            {
                                Console.WriteLine("error:{0}", ex2.Message);
                                Console.WriteLine(Parser.ArgumentsUsage(typeof (CommandLineArguments)));
                            }
                        }
                        break;
                    case RunMode.Hostsfile:
                        SetHostsFile(sargs);
                        break;
                }
            }
#endif
        }


        private static void SetHostsFile(CommandLineArguments sargs)
        {
            try
            {
                if (sargs.AddHost)
                {
                    ServiceFactory.Rules.AddHostEntry(sargs.IPAddress, sargs.HostName);
                }
                else
                {
                    ServiceFactory.Rules.RemoveHostEntry(sargs.IPAddress, sargs.HostName);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Environment.Exit(-1);
            }
            catch
            {
                Environment.Exit(-2);
            }
        }
    }
}